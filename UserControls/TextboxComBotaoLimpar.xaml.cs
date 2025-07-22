using System.Windows.Controls;

namespace SqlHelper.UserControls
{
    public partial class TextboxComBotaoLimpar : UserControl
    {
        public TextboxComBotaoLimpar()
        {
            DataContext = this;
            InitializeComponent();
        }

        private string _placeHolder;

        public string PlaceHolder
        {
            get { return _placeHolder; }
            set { _placeHolder = value; }
        }

        private void btnLimpar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            txtInput.Clear();
            txtInput.Focus();
        }
    }
}
