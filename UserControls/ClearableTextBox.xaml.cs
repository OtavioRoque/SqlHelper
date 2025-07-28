using System.Windows;
using System.Windows.Controls;

namespace SqlHelper.UserControls
{
    public partial class ClearableTextBox : UserControl
    {
        public ClearableTextBox()
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
                tbPlaceholder.Visibility = Visibility.Visible;
            else
                tbPlaceholder.Visibility = Visibility.Hidden;
        }
    }
}
