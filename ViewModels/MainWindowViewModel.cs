using CommunityToolkit.Mvvm.ComponentModel;
using SqlHelper.Models;
using SqlHelper.Utils;
using System.Collections.ObjectModel;

#pragma warning disable CS8618

namespace SqlHelper.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private DatabaseModel _selectedDatabase;

        [ObservableProperty]
        private TableModel _selectedTable;

        public ObservableCollection<DatabaseModel> Databases { get; } = new();
        public ObservableCollection<TableModel> Tables { get; } = new();
        public ObservableCollection<ColumnModel> Columns { get; } = new();

        public MainWindowViewModel()
        {
            MetadataLoader.LoadDatabases(Databases);
        }

        partial void OnSelectedDatabaseChanged(DatabaseModel value)
        {
            if (value != null)
                MetadataLoader.LoadTables(Tables, value);
        }

        partial void OnSelectedTableChanged(TableModel value)
        {
            if (value != null)
                MetadataLoader.LoadColumns(Columns, SelectedDatabase, value);
        }
    }
}
