using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Windows.Controls;
using System.Linq;
using System;
using MDCafe.Models;
using System.Windows.Input;
using MDCafe.Utility;
using System.Windows.Data;

namespace MDCafe.Sales
{    
    public partial class SalesEntry : Window
    {
        Model1Container modelContext = new Model1Container();        
       
        public SalesEntry()
        {
            InitializeComponent();
            Initialize();
            

        }

        public void Initialize()
        {            
                        
            Panel1cboCustomer1.DataContext = modelContext.customers.ToList();
            //btnDone.CommandBindings.Add(new CommandBinding(ApplicationCommands.Print, BtnDoneExecuted, BtnDoneCanExecute));

            var saleItems = new SaleItems();
            saleItems.ItemsCollection = modelContext.items.ToList();
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
            var mainGridChildCount = GridMain.Children.Count + 1;
            var mainGridChildCountString = mainGridChildCount.ToString();

            var panelName = "Panel" + mainGridChildCountString;
            var stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Name= panelName };


            var saleItems = new SaleItems();
            saleItems.ItemsCollection = modelContext.items.ToList();
            stackPanel.DataContext = saleItems;

            var customerCombo = new ComboBox() { Name = panelName + "cboCustomer" + mainGridChildCount };
            customerCombo.ItemsSource = modelContext.customers.ToList();
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
                var cbo = new ComboBox()
                {
                    Width = 75,
                    Margin = new Thickness(5f),
                    ItemsSource = modelContext.items.ToList(),
                    DisplayMemberPath = "Description",
                    SelectedValuePath = "Code",
                    IsEditable = true
                };

                cbo.SetBinding(ComboBox.SelectedItemProperty, "SaleItemsDetailsCollection[" + i + "].SelectedItemItem");
                stackPanel.Children.Add(cbo);
                var txt = new TextBox() { Width = 50, Margin = new Thickness(5f) };
                txt.SetBinding(TextBox.TextProperty, "SaleItemsDetailsCollection[" + i + "].ItemQty");
                stackPanel.Children.Add(txt);
            }
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

            GridMain.Children.Add(stackPanel);
            Grid.SetRow(stackPanel, mainGridChildCount -1);
            Grid.SetColumn(stackPanel, 0);

           


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
                SaleConfirmation saleConfWindow = new SaleConfirmation(saleItems.TotalAmount);
                Nullable<bool> dialogResult = saleConfWindow.ShowDialog();
                if (!dialogResult.Value) return;

                foreach (var item in saleItems.SaleItemsDetailsCollection)
                {
                    if (item.SelectedItemItem == null) continue;
                    modelContext.sales.Add(new sale()
                    {
                        CustomerId = saleItems.CustomerId,
                        ItemCode = item.SelectedItemItem.code,
                        Quantity = item.ItemQty,
                        SaleDate = DateTime.Now,
                        CurrentPrice = item.SelectedItemItem.CurrentPrice
                    });
                }
                modelContext.SaveChanges();
                ClearValues(saleItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save. Error :" + ex.Message);
            }
        }
    }
}
