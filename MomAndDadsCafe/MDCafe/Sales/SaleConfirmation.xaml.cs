using MDCafe.Models;
using System.Windows;
using System.Windows.Input;

namespace MDCafe.Sales
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SaleConfirmation : Window
    {
        SaleConfirmationModel _saleConfirmationModel;

        public SaleConfirmation(float totalAmount, bool? isExistingCustomer)
        {
            InitializeComponent();
            _saleConfirmationModel = new SaleConfirmationModel(totalAmount);
            _saleConfirmationModel.IsExistingCustomer = isExistingCustomer;

            this.DataContext = _saleConfirmationModel;
        }

        private void PrintCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _saleConfirmationModel !=null && _saleConfirmationModel.AmountPaid != 0;
        }

        private void PrintExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();            
        }

        private void SaveCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _saleConfirmationModel != null && _saleConfirmationModel.IsExistingCustomer.HasValue
                            && _saleConfirmationModel.IsExistingCustomer.Value
                            && _saleConfirmationModel.AmountPaid > 0;                            
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
