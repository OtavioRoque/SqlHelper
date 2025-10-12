using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SqlHelper.Models;
using SqlHelper.Utils;
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
        private DataTable _tableData;

        [ObservableProperty]
        private int _selectedTabIndex;

        [ObservableProperty]
        private string _copyableTextBoxContent;

        public ObservableCollection<DatabaseModel> Databases { get; } = new();
        public ObservableCollection<TableModel> Tables { get; } = new();

        public MainWindowViewModel()
        {
            MetadataLoader.LoadDatabases(Databases);
        }

        partial void OnSelectedDatabaseChanged(DatabaseModel value)
        {
            MetadataLoader.LoadTables(Tables, value);
        }

        #region Commands

        [RelayCommand]
        private void ShowData(TableModel table)
        {
            string sql = $"SELECT TOP 300 * FROM [{SelectedDatabase.Name}].[{table.Schema}].[{table.Name}];";
            TableData = SQL.FillDataTable(sql);
        }

        [RelayCommand]
        private void GenerateSelect(TableModel table)
        {
            SelectedTabIndex = 1;
            CopyableTextBoxContent = "teste";
        }

        #endregion
    }
}
