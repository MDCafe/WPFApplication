using System;
using System.ComponentModel;

namespace MDCafe.Models
{
    class SaleConfirmationModel : BaseModel
    {
        private float _totalAmount;
        private float _amountPaid;
        private float _balance;
        private bool? _isExistingCustomer;

        public SaleConfirmationModel(float totalAmount)
        {
            _totalAmount = totalAmount;
        }

        public float TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }

        public float AmountPaid
        {
            get { return _amountPaid; }
            set
            {
                _amountPaid = value;
                Balance = _amountPaid - _totalAmount;
                OnPropertyChanged(new PropertyChangedEventArgs("AmountPaid"));
            }
        }

        public float Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;               
                OnPropertyChanged(new PropertyChangedEventArgs("Balance"));
            }
        }

        public bool? IsExistingCustomer
        {
            get { return _isExistingCustomer; }
            set { _isExistingCustomer = value; }
        }
    }
}
