using System.Data;
using System.Windows;
using System.Windows.Controls;

#pragma warning disable CS8600

namespace SqlHelper.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCbDatabases();
        }

        private void LoadCbDatabases()
        {
            string sql = "SELECT name FROM sys.databases ORDER BY name";
            DataTable dt = DB.FillDataTable(sql);

            cbDatabases.ItemsSource = dt.DefaultView;
            cbDatabases.DisplayMemberPath = "name";
            cbDatabases.SelectedValuePath = "name";
        }

        private string GetSelectedDatabase()
        {
            return cbDatabases.SelectedValue.ToString() ?? string.Empty;
        }

        private bool TryGetSelectedDatabase(out string databaseName)
        {
            databaseName = GetSelectedDatabase();
            return !string.IsNullOrEmpty(databaseName);
        }

        private void cbDatabases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!TryGetSelectedDatabase(out string databaseName))
                return;

            string sql = $@"
                SELECT t.name AS Tabela, SUM(p.rows) AS Rows
                FROM {databaseName}.sys.tables t
                JOIN {databaseName}.sys.partitions p ON t.object_id = p.object_id
                WHERE p.index_id IN (0,1) AND Rows > 0
                GROUP BY t.name";

            dgTables.ItemsSource = DB.FillDataTable(sql).DefaultView;
        }
    }
}