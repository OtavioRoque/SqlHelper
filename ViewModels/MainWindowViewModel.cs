using SqlHelper.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

#pragma warning disable CS8618

namespace SqlHelper.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private DatabaseModel _selectedDatabase;

        public DatabaseModel SelectedDatabase
        {
            get => _selectedDatabase;
            set
            {
                if (_selectedDatabase != value)
                {
                    _selectedDatabase = value;
                    OnPropertyChanged();
                    LoadDatabaseTables();
                }
            }
        }

        public ObservableCollection<DatabaseModel> Databases { get; } = new ObservableCollection<DatabaseModel>();

        public MainWindowViewModel()
        {
            LoadDatabases();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadDatabases()
        {
            string sql = "SELECT name FROM sys.databases";
            var dtDatabases = DB.FillDataTable(sql);

            Databases.Clear();
            Databases.Add(new DatabaseModel(string.Empty));

            foreach (DataRow dr in dtDatabases.Rows)
            {
                string databaseName = dr["name"].ToString() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(databaseName))
                    continue;

                Databases.Add(new DatabaseModel(databaseName));
            }

            SelectedDatabase = Databases.First();
        }

        private void LoadDatabaseTables()
        {
            if (string.IsNullOrWhiteSpace(SelectedDatabase.Name))
                return;

            string sql = $@"
                SELECT
                    s.name AS SchemaName,
                    t.name AS TableName,
                    SUM(p.rows) AS [RowCount]
                FROM
                    {SelectedDatabase.Name}.sys.tables t
                    JOIN {SelectedDatabase.Name}.sys.schemas s ON t.schema_id = s.schema_id
                    JOIN {SelectedDatabase.Name}.sys.partitions p ON t.object_id = p.object_id
                WHERE
                    p.index_id IN (0,1)
                GROUP BY
                    s.name, t.name
                HAVING
                    SUM(p.rows) > 0
                ORDER BY
                    s.name, t.name";

            var dtTables = DB.FillDataTable(sql);

            foreach (DataRow dr in dtTables.Rows)
            {
                string schema = dr["SchemaName"].ToString() ?? string.Empty;
                string name = dr["TableName"].ToString() ?? string.Empty;
                long rowCount = PH.ToLong(dr["RowCount"].ToString() ?? string.Empty);

                SelectedDatabase.AddTable(new TableModel(schema, name, rowCount));
            }
        }
    }
}
