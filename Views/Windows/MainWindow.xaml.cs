using System.Windows;
using SqlHelper.ViewModels;

namespace SqlHelper.Views.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}