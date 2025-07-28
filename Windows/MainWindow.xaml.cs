using System.Data;
using System.Windows;

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
            CarregarComboboxDatabases();
        }

        private void CarregarComboboxDatabases()
        {
            string sql = "SELECT name FROM sys.databases ORDER BY name";
            DataTable dt = DB.FillDataTable(sql);

            cbDatabases.ItemsSource = dt.DefaultView;
            cbDatabases.DisplayMemberPath = "name";
        }
    }
}