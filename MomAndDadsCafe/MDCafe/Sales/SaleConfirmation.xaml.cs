using MDCafe.Models;

using System.Windows;
using System.Windows.Input;

namespace MDCafe.Sales
{
    /// <summary>
    /// Sale Confirmation dialog window
    /// </summary>
    public partial class SaleConfirmation : Window
    {
        SaleConfirmationModel _saleConfirmationModel;

        public SaleConfirmation(float totalAmount, bool? isExistingCustomer, float? accountBalance)
        {
            InitializeComponent();
            _saleConfirmationModel = new SaleConfirmationModel(totalAmount, accountBalance);
            _saleConfirmationModel.IsExistingCustomer = isExistingCustomer;
            _saleConfirmationModel.CustomerAccountBal = accountBalance;

            this.DataContext = _saleConfirmationModel;            
        }

        //private void SaleConfirmation_Loaded1(object sender, RoutedEventArgs e)
        //{
        //    var visibilityValue = _saleConfirmationModel.IsExistingCustomer == true ? 0 : 1;
        //    this.txtCustomerBalance.Visibility = (Visibility)System.Enum.Parse(typeof(Visibility), visibilityValue.ToString());
            
        //}

        private void SaleConfirmation_Loaded(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void PrintCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _saleConfirmationModel !=null && _saleConfirmationModel.AmountPaid > 0
                            && !string.IsNullOrWhiteSpace(txtAmountPaid.Text);            
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
                            && _saleConfirmationModel.AmountPaid > 0
                            && !string.IsNullOrWhiteSpace(txtAmountPaid.Text);
                                                  
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Utility.Utils.IsTextAllowed(e.Text) || string.IsNullOrWhiteSpace(e.Text)) e.Handled = true;
        }
    }
}
