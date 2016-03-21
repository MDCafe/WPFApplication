using MDCafe.Models;
using System.Windows;

namespace MDCafe.Sales
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SaleConfirmation : Window
    {
        SaleConfirmationModel _saleConfirmationModel;

        public SaleConfirmation(decimal totalAmount)
        {
            InitializeComponent();
            _saleConfirmationModel = new SaleConfirmationModel(totalAmount);
            this.DataContext = _saleConfirmationModel;
        }

        private void PrintCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _saleConfirmationModel !=null && _saleConfirmationModel.AmountPaid != 0;
        }

        private void PrintExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
