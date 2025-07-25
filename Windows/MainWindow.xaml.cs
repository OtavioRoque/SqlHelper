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
            var bancosIgnorar = new string[]
            {
                "BDCreceber", "BDHistorico", "BDImagens", "BDOfic", "BDPecas", "BDRelatorio",
                "BDRepr", "BDTemp", "master", "model", "msdb", "tempdb"
            };

            string sql = $@"select * from tabela";

            using DataTable dt = DB.GetDataTable(sql);
            {
                foreach (DataRow row in dt.Rows)
                {
                    string databaseName = row["name"].ToString();
                }
            }
        }
    }
}