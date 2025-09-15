using CommunityToolkit.Mvvm.ComponentModel;
using SqlHelper.Models;
using SqlHelper.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

#pragma warning disable CS8618
#pragma warning disable CS8600
#pragma warning disable CS8604

namespace SqlHelper.ViewModels
{
    public class MainWindowViewModel : ObservableObject
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

        private TableModel _selectedTable;

        public TableModel SelectedTable
        {
            get => _selectedTable;
            set
            {
                if (_selectedTable != value)
                {
                    _selectedTable = value;
                    SelectedTable.IsChecked = !SelectedTable.IsChecked;
                    OnPropertyChanged();
                    LoadTableColumns();
                }
            }
        }


        public ObservableCollection<DatabaseModel> Databases { get; } = new ObservableCollection<DatabaseModel>();
        public ObservableCollection<TableModel> Tables { get; } = new ObservableCollection<TableModel>();
        public ObservableCollection<ColumnModel> Columns { get; } = new ObservableCollection<ColumnModel>();

        public MainWindowViewModel()
        {
            MetadataLoader.LoadDatabases(Databases);
        }

        private void LoadDatabaseTables()
        {
            if (SelectedDatabase is null)
                return;
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

            Tables.Clear();

            foreach (DataRow dr in dtTables.Rows)
            {
                string schema = dr["SchemaName"].ToString();
                string name = dr["TableName"].ToString();
                long rowCount = PH.ToLong(dr["RowCount"].ToString());

                Tables.Add(new TableModel(schema, name, rowCount));
            }
        }

        private void LoadTableColumns()
        {
            if (SelectedTable is null)
                return;
            if (string.IsNullOrWhiteSpace(SelectedTable.Schema) || string.IsNullOrWhiteSpace(SelectedTable.Name))
                return;

            string sql = @$"
                SELECT 
	                COLUMN_NAME,
	                DATA_TYPE
                FROM
	                {SelectedDatabase.Name}.INFORMATION_SCHEMA.COLUMNS
                WHERE
	                TABLE_SCHEMA = '{SelectedTable.Schema}' AND
                    TABLE_NAME = '{SelectedTable.Name}'";

            var dtColumns = DB.FillDataTable(sql);

            Columns.Clear();

            foreach (DataRow dr in dtColumns.Rows)
            {
                string name = dr["COLUMN_NAME"].ToString();
                SqlDbType dataType = PH.ToSqlDbType(dr["DATA_TYPE"].ToString());

                Columns.Add(new ColumnModel(name, dataType));
            }
        }
    }
}
