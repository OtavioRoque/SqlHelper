using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SqlHelper.UserControls
{
    public partial class CopyableCodeTextBox : UserControl
    {
        public CopyableCodeTextBox()
        {
            InitializeComponent();
            DataContext = this;
        }

        private string conteudoTextbox;

        public string ConteudoTexbox
        {
            get { return conteudoTextbox; }
            set { conteudoTextbox = value; }
        }

        #region Eventos

        private void btnCopiar_Click(object sender, RoutedEventArgs e)
        {
            txtCodigo.SelectAll();
            txtCodigo.Copy();

            btnCopiar.Content = "✅Copiado!";
            btnCopiar.IsHitTestVisible = false;
        }

        private void btnCopiar_MouseEnter(object sender, MouseEventArgs e)
        {
            if (btnCopiar.IsHitTestVisible)
                this.Cursor = Cursors.Hand;
        }

        private void txtCodigo_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnCopiar.Content = "📋Copiar";
            btnCopiar.IsHitTestVisible = true;
        }

        #endregion
    }
}
