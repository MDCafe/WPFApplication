﻿using MDCafe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MDCafe.Sales
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : Window
    {
        CustomerViewModel _viewModel = new CustomerViewModel();
        //Model1Container _modelContext;

        public Customers()
        {
            InitializeComponent();
            CustomerListView.ItemsSource = _viewModel.ModelContext.customers.Where(c => c.IsExistingCustomer.Value == true).ToList();
        }
    }
}
