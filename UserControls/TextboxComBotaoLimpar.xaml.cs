using System.Runtime.ConstrainedExecution;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#pragma warning disable CS8618

namespace SqlHelper.UserControls
{
    public partial class TextboxComBotaoLimpar : UserControl
    {
        public TextboxComBotaoLimpar()
        {
            InitializeComponent();
            DataContext = this;
        }

        private string _placeHolder;

        public string Placeholder
        {
            get { return _placeHolder; }
            set { _placeHolder = value; }
        }

        private void btnLimpar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            txtInput.Clear();
            txtInput.Focus();
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtInput.Text))
                btnLimpar.Visibility = Visibility.Hidden;
            else
                btnLimpar.Visibility = Visibility.Visible;
        }
    }
}
