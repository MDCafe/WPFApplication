using System;
using System.ComponentModel;

namespace MDCafe.Models
{
    class SaleConfirmationModel : BaseModel
    {
        private decimal _totalAmount;
        private Decimal _amountPaid;
        private decimal _balance;

        public SaleConfirmationModel(decimal totalAmount)
        {
            _totalAmount = totalAmount;
        }

        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }

        public Decimal AmountPaid
        {
            get { return _amountPaid; }
            set
            {
                _amountPaid = value;
                Balance = _amountPaid - _totalAmount;
                OnPropertyChanged(new PropertyChangedEventArgs("AmountPaid"));
            }
        }

        public decimal Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;               
                OnPropertyChanged(new PropertyChangedEventArgs("Balance"));
            }
        }
    }
}
