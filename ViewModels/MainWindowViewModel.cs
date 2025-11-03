using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SqlHelper.Models;
using SqlHelper.Services;
using System.Collections.ObjectModel;
using System.Data;

#pragma warning disable CS8618

namespace SqlHelper.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private DatabaseModel _selectedDatabase;

        [ObservableProperty]
        private TableModel _selectedTable;

        [ObservableProperty]
        private DataView _tableView;

        [ObservableProperty]
        private int _selectedTabIndex;

        [ObservableProperty]
        private string _copyableTextBoxContent;

        public ObservableCollection<DatabaseModel> Databases { get; set; } = new();
        public ObservableCollection<TableModel> Tables { get; set; } = new();

        public MainWindowViewModel()
        {
            var databases = MetadataLoader.LoadDatabases();
            Databases = new ObservableCollection<DatabaseModel>(databases);
        }

        partial void OnSelectedDatabaseChanged(DatabaseModel value)
        {
            var tables = MetadataLoader.LoadTables(value);
            Tables.Clear();
            foreach (var table in tables)
                Tables.Add(table);
        }

        #region Commands

        [RelayCommand]
        private void ShowData()
        {
            TableView = DataLoader.LoadTableData(SelectedDatabase, SelectedTable).DefaultView;
        }

        [RelayCommand]
        private void GenerateSelect()
        {
            SelectedTabIndex = 1;
            CopyableTextBoxContent = "teste";
        }

        #endregion
    }
}
