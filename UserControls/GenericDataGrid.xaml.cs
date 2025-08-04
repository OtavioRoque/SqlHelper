using System.Windows.Controls;
using System.Collections;

namespace SqlHelper.UserControls
{
    public partial class GenericDataGrid : UserControl
    {
        public GenericDataGrid()
        {
            InitializeComponent();
        }

        public IEnumerable ItemsSource
        {
            get => dgGeneric.ItemsSource;
            set => dgGeneric.ItemsSource = value;
        }
    }
}
