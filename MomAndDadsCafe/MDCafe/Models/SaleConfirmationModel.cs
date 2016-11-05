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
        private float? _customerAccountBal;
        private float? _initalCustomerBalance;

        public SaleConfirmationModel(float totalAmount,float? initalBalance)
        {
            _totalAmount = totalAmount;
            _initalCustomerBalance = initalBalance;
        }

        public float? CustomerAccountBal
        {
            get { return _customerAccountBal; }
            set { _customerAccountBal = value; }
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
                Balance = _totalAmount - _amountPaid;                
                OnPropertyChanged(new PropertyChangedEventArgs("AmountPaid"));
                if (!_isExistingCustomer.HasValue) return;
                _customerAccountBal = _initalCustomerBalance - _amountPaid;
                OnPropertyChanged(new PropertyChangedEventArgs("CustomerAccountBal"));
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
