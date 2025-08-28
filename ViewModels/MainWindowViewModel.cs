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

            foreach (DataRow row in dtDatabases.Rows)
            {
                string databaseName = row["name"].ToString() ?? string.Empty;

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

            // falta dar seguimento aqui
        }
    }
}
