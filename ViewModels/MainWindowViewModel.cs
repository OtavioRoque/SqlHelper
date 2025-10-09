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
        private int _currentTabIndex;

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

        #region Commands

        [RelayCommand]
        private void ShowData(TableModel table)
        {
            string sql = $"SELECT TOP 300 * FROM [{table.Database.Name}].[{table.Schema}].[{table.Name}];";
            TableData = SQL.FillDataTable(sql);
        }

        [RelayCommand]
        private void GenerateSelect(TableModel table)
        {
            CurrentTabIndex = 1;
        }

        #endregion
    }
}
