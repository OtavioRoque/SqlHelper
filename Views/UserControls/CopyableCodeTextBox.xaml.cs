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
        }

        public static readonly DependencyProperty TextBoxContentProperty =
            DependencyProperty.Register(
                nameof(TextBoxContent),
                typeof(string),
                typeof(CopyableCodeTextBox),
                new PropertyMetadata(string.Empty));

        public string TextBoxContent
        {
            get => (string)GetValue(TextBoxContentProperty);
            set => SetValue(TextBoxContentProperty, value);
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
