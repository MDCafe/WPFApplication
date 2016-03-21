using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System;
using MDCafe.Models;
using System.Windows.Input;
using MDCafe.Utility;
using System.Windows.Data;
using MDCafe.ViewModels;

namespace MDCafe.Sales
{
    public partial class SalesEntry : Window
    {
        SalesEntryViewModel _viewModel = new SalesEntryViewModel();
        Model1Container _modelContext;

        public SalesEntry()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {

            _modelContext = _viewModel.ModelContext;
            Panel1cboCustomer1.DataContext = _modelContext.customers.OrderBy(s => s.Name).ToList();
            //btnDone.CommandBindings.Add(new CommandBinding(ApplicationCommands.Print, BtnDoneExecuted, BtnDoneCanExecute));

            var saleItems = new SaleItems();
            
            saleItems.ItemsCollection = _modelContext.items.OrderBy(s=>s.Name).ToList();
            Panel1.DataContext = saleItems;

            //List<Items> lst = new List<Items>();
            //var connString = "server=127.0.0.1;user id=root;database=MDCafe";
            //using (var conn = new MySqlConnection(connString))
            //{
            //    var cmdText = "select * from items";
            //    using (var command = new MySqlCommand(cmdText,conn))
            //    {
            //        conn.Open();

            //        using (var reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                lst.Add(new Items()
            //                {
            //                    Code = reader.GetString("code"),
            //                    Name = reader.GetString("Name"),
            //                    Description = reader.GetString("Description"),
            //                    currentPrice = reader.GetDecimal("currentPrice"),
            //                    Category = reader.GetString("Category")
            //                }
            //                );
            //            }
            //        }
            //    }
            //}
            //this.DataContext = lst;
        }
            
        private void btAddRow_Click(object sender, RoutedEventArgs e)
        {
            var scrollViewer = new ScrollViewer() { HorizontalScrollBarVisibility = ScrollBarVisibility.Auto, VerticalScrollBarVisibility = ScrollBarVisibility.Hidden };
            var mainGridChildCount = GridMain.Children.Count + 1;
            var mainGridChildCountString = mainGridChildCount.ToString();

            var panelName = "Panel" + mainGridChildCountString;
            var stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Name= panelName };


            var saleItems = new SaleItems();
            saleItems.ItemsCollection = _modelContext.items.ToList();
            stackPanel.DataContext = saleItems;

            var customerCombo = new ComboBox() { Name = panelName + "cboCustomer" + mainGridChildCount };
            customerCombo.ItemsSource = _modelContext.customers.ToList();
            customerCombo.IsEditable = true;
            customerCombo.SelectedValuePath = "id";
            customerCombo.DisplayMemberPath = "Name";
            customerCombo.Width = 75;
            customerCombo.Margin = new Thickness(5f);
            //customerCombo.SelectedValue = saleItems.CustomerId;
            Binding selectedValueBinding = new Binding() { Source = saleItems, Path = new PropertyPath("CustomerId"), Mode = BindingMode.TwoWay };
            //cbPriceSource.SetBinding(ComboBox.SelectedValueProperty, selectedItemBinding);
            customerCombo.SetBinding(ComboBox.SelectedValueProperty, selectedValueBinding);
            stackPanel.Children.Add(customerCombo);

            var totalTxt = new TextBox();
            totalTxt.Name = "txtTotal" + mainGridChildCountString;
            totalTxt.IsEnabled = false;
            totalTxt.Width = 50;
            totalTxt.Margin = new Thickness(5f);
            totalTxt.SetBinding(TextBox.TextProperty, "TotalAmount");
            stackPanel.Children.Add(totalTxt);

            for (int i = 0; i < 7; i++)
            {
                ComboBox cbo = CreateItemComboBox(i);
                stackPanel.Children.Add(cbo);
                TextBox txt = CreateTextBox(i);
                stackPanel.Children.Add(txt);
            }

            var buttonAdd = new Button()
            {
                Content = "Add",
                Width = 50,
                Margin = new Thickness(5),
                Tag = mainGridChildCountString + "btnAdd"
            };

            buttonAdd.Click += btnAddItem_Click;

            stackPanel.Children.Add(buttonAdd);

            var buttonDone = new Button()
            {
                Content = "Done",
                Width = 75,
                Margin = new Thickness(5),
                Command = ApplicationCommands.Print,
                Tag = mainGridChildCountString
            };

            buttonDone.CommandBindings.Add(new CommandBinding(ApplicationCommands.Print, BtnDoneExecuted, BtnDoneCanExecute));
            stackPanel.Children.Add(buttonDone);

            scrollViewer.Content = stackPanel;
            GridMain.Children.Add(scrollViewer);
            Grid.SetRow(scrollViewer, mainGridChildCount -1);
            Grid.SetColumn(scrollViewer, 0);

        }

        private TextBox CreateTextBox(int i)
        {
            var txt = new TextBox() { Width = 50, Margin = new Thickness(5f) };
            txt.Style = Resources["txtStyle"] as Style;
            txt.Width = 30;
            txt.SetBinding(TextBox.TextProperty, "SaleItemsDetailsCollection[" + i + "].ItemQty");
            return txt;
        }

        private ComboBox CreateItemComboBox(int i)
        {
            var cbo = new ComboBox()
            {
                Width = 75,
                Margin = new Thickness(5f),
                ItemsSource = _modelContext.items.ToList(),
                DisplayMemberPath = "Description",
                SelectedValuePath = "Code",
                IsEditable = true
            };

            cbo.SetBinding(ComboBox.SelectedItemProperty, "SaleItemsDetailsCollection[" + i + "].SelectedItemItem");
            return cbo;
        }

        //private void txtCbo1_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    //var txt = sender as TextBox;
        //    //if (string.IsNullOrWhiteSpace(txt.Text)) return; 

        //    //var index  = firstPanel.Children.IndexOf(txt);
        //    //var cbo = firstPanel.Children[index - 1] as ComboBox;
        //    //var selectedItem = cbo.SelectedItem as MDCafe.item;
        //    //var totalAmount = string.IsNullOrWhiteSpace(txtTotalAmt1.Text) ? 0 : decimal.Parse(txtTotalAmt1.Text);
        //    //txtTotalAmt1.Text = (totalAmount + selectedItem.CurrentPrice * int.Parse(txt.Text)).ToString();
        //}       

        private void ClearValues(SaleItems saleItems)
        {
            saleItems.CustomerId = -1;
            foreach (var item in saleItems.SaleItemsDetailsCollection)
            {
                item.ItemQty = 0;
                item.SelectedItemItem = null;
                item.PreviousSelectedItem = null;
                item.PrevItemQty = 0;
            }           
        }

        private void BtnDoneCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var button = sender as Button;            
            var stackpanel = button.Parent as StackPanel;
            if (stackpanel == null) return;

            var cboCustomer  = Utils.FindChild<ComboBox>(GridMain, stackpanel.Name + "cboCustomer" + button.Tag);

            //var cboCustomer = GridMain.FindName() as ComboBox;
            e.CanExecute = cboCustomer != null && cboCustomer.SelectedItem !=null;
            //var cboCuetomer = stackpanel.Children.IndexOf( );

        }

        private void BtnDoneExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                var button = sender as Button;
                var stackpanel = button.Parent as StackPanel;
                var saleItems = stackpanel.DataContext as SaleItems;
                var customer  = _viewModel.GetCustomer(saleItems.CustomerId);

                var saleConfWindow = new SaleConfirmation(saleItems.TotalAmount, customer.IsExistingCustomer);
                var dialogResult = saleConfWindow.ShowDialog();
                if (!dialogResult.Value) return;

                foreach (var item in saleItems.SaleItemsDetailsCollection)
                {
                    if (item.SelectedItemItem == null) continue;
                    _modelContext.sales.Add(new sale()
                    {
                        CustomerId = saleItems.CustomerId,
                        ItemCode = item.SelectedItemItem.code,
                        Quantity = item.ItemQty,
                        SaleDate = DateTime.Now,
                        CurrentPrice = item.SelectedItemItem.CurrentPrice
                    });
                }

                _viewModel.BalanceCustomerAccount(saleItems.CustomerId, saleItems.TotalAmount);
                _modelContext.SaveChanges();
                ClearValues(saleItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save. Error :" + ex.Message);
            }
        }

        private void btnShowCustomers_Click(object sender, RoutedEventArgs e)
        {
            Customers cust = new Customers();
            cust.ShowDialog();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            var parentPanel = (sender as Button).Parent as StackPanel;
            var dataContextSaleItem = parentPanel.DataContext as SaleItems;
            var count = dataContextSaleItem.SaleItemsDetailsCollection.Count - 1;
            var txtItem = CreateTextBox(count);            
            
            //txtItem.SetBinding(TextBox.TextProperty, "SaleItemsDetailsCollection[" + count + "].ItemQty}");
            var cbo = CreateItemComboBox(count);            
            parentPanel.Children.Insert(parentPanel.Children.Count - 2,cbo);
            parentPanel.Children.Insert(parentPanel.Children.Count - 2,txtItem);
        }

        private void Show_Items_Click(object sender, RoutedEventArgs e)
        {
            Customers cust = new Customers();
            cust.ShowDialog();
        }
    }
}
