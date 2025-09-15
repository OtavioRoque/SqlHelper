using CommunityToolkit.Mvvm.ComponentModel;
using SqlHelper.Models;
using SqlHelper.Utils;
using System.Collections.ObjectModel;

#pragma warning disable CS8618

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
                    MetadataLoader.LoadTables(Tables, SelectedDatabase);
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
                    MetadataLoader.LoadColumns(Columns, SelectedDatabase, SelectedTable);
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
    }
}
