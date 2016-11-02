using MDCafe.Models;
using System.Windows;
using System.Windows.Input;

namespace MDCafe.Sales
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MiscItemsView : Window
    {
        SaleItems _saleItems;

        public MiscItemsView(SaleItems saleItems)
        {
            InitializeComponent();
            _saleItems = saleItems;            
        }        

        private void SaveCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(txtTotalAmout.Text);
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
