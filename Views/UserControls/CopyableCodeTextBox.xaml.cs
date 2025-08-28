using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

#pragma warning disable CS8618

namespace SqlHelper.Views.UserControls
{
    public partial class CopyableCodeTextBox : UserControl
    {
        public CopyableCodeTextBox()
        {
            InitializeComponent();
            DataContext = this;
        }

        private string textBoxContent;

        public string TextBoxContent
        {
            get { return textBoxContent; }
            set { textBoxContent = value; }
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            txtCode.SelectAll();
            txtCode.Copy();

            btnCopy.Content = "✅Copiado!";
            btnCopy.IsHitTestVisible = false;
        }

        private void btnCopy_MouseEnter(object sender, MouseEventArgs e)
        {
            if (btnCopy.IsHitTestVisible)
                this.Cursor = Cursors.Hand;
        }

        private void txtCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnCopy.Content = "📋Copiar";
            btnCopy.IsHitTestVisible = true;
        }
    }
}
