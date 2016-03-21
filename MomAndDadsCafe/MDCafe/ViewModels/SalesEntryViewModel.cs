﻿using System;
using System.Linq;

namespace MDCafe.ViewModels
{
    public class SalesEntryViewModel
    {
        public Model1Container _modelContext;

        public Model1Container ModelContext { get { return _modelContext; } }


        public SalesEntryViewModel()
        {
            _modelContext = new Model1Container();
        }

        public void BalanceCustomerAccount(int customerId, decimal amount)
        {
            var customer = GetCustomer(customerId);

            if (customerId == -1 || !customer.IsExistingCustomer.Value) return;
            
            if (customer.BalanceAmount ==null || customer.BalanceAmount < 0) throw new Exception("Balance is low :" + customer.BalanceAmount);

            customer.BalanceAmount -= amount;            
        }

        public customer GetCustomer(int customerId)
        {
            return _modelContext.customers.FirstOrDefault(c => c.id == customerId);
        }
    }
}