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

        public ObservableCollection<DatabaseModel> Databases { get; } = new();
        public ObservableCollection<TableModel> Tables { get; } = new();

        public MainWindowViewModel()
        {
            var databases = MetadataLoader.LoadDatabases();
            Databases = new ObservableCollection<DatabaseModel>(databases);
        }

        partial void OnSelectedDatabaseChanged(DatabaseModel value)
        {
            MetadataLoader.LoadTables(Tables, value);
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
