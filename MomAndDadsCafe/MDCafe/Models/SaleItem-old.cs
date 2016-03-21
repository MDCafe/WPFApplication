using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDCafe.Models
{
    class SaleItems : BaseModel
    {
        private Decimal _totalAmount;        
        private List<item> _itemsCollection;
        private List<SaleItemsDetails> _saleItemsDetailsCollection;   
        private int _customerId;

        public SaleItems()
        {
            _saleItemsDetailsCollection = new List<SaleItemsDetails>();
            for (int i = 0; i < 7; i++)
            {
                var item = new SaleItemsDetails();
                item.PropertyChanged += SaleItems_PropertyChanged;
                _saleItemsDetailsCollection.Add(item);                
            }            
        }

        private void SaleItems_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var saleItesmDetails = sender as SaleItemsDetails;
            if (saleItesmDetails.PreviousSelectedItem != null)//&& saleItesmDetails.PrevItemQty != 0
            {
                if (e.PropertyName == "SelectedItemItem")
                {
                    TotalAmount -= (decimal)(saleItesmDetails.PreviousSelectedItem.CurrentPrice * saleItesmDetails.ItemQty);
                }
                else
                {
                    if(saleItesmDetails.SelectedItemItem !=null)
                        TotalAmount -= (decimal)(saleItesmDetails.SelectedItemItem.CurrentPrice * saleItesmDetails.PrevItemQty);
                }
            }
            //calculate only if selected item is not null
            if (saleItesmDetails.SelectedItemItem !=null)
                TotalAmount += (decimal)(saleItesmDetails.SelectedItemItem.CurrentPrice * saleItesmDetails.ItemQty);

        }

        public Decimal TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                _totalAmount = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TotalAmount"));
            }
        }

        //private void CalculateTotalAmount(string value)
        //{
        //    if (!string.IsNullOrWhiteSpace(itemQty))
        //        totalAmount -= (decimal)(selectedItemItem.CurrentPrice) * decimal.Parse(itemQty);
        //    itemQty = value;
        //    TotalAmount += (decimal)(selectedItemItem.CurrentPrice) * decimal.Parse(itemQty);
        //}

        public List<item> ItemsCollection
        {
            get { return _itemsCollection; }
            set { _itemsCollection = value; }
        }

        public List<SaleItemsDetails> SaleItemsDetailsCollection
        {
            get { return _saleItemsDetailsCollection; }
            set { _saleItemsDetailsCollection = value; }
        }

        public int CustomerId
        {

            get { return _customerId; }
            set
            {
                _customerId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CustomerId"));
            }
        }
    }


    public class SaleItemsDetails : BaseModel
    {
        private item _selectedItemItem;
        private int _itemQty;        

        public item PreviousSelectedItem { get; set; }
        public int PrevItemQty { get; set; }
      
        public item SelectedItemItem
        {
            get { return _selectedItemItem; }
            set
            {
                if (_selectedItemItem == value) return;
                if(_selectedItemItem == null)
                    PreviousSelectedItem = value;
                else
                    PreviousSelectedItem = _selectedItemItem;
                _selectedItemItem = value;
                //if (!string.IsNullOrWhiteSpace(_itemQty) && _selectedItemItem != null)
                //    _totalAmount -= (decimal)(_selectedItemItem.CurrentPrice) * decimal.Parse(_itemQty);
                //_selectedItemItem = value;
                //if (string.IsNullOrWhiteSpace(_itemQty)) return;

                //_totalAmount += (decimal)(_selectedItemItem.CurrentPrice) * decimal.Parse(_itemQty);
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedItemItem"));
            }
        }

        public int ItemQty
        {
            get { return _itemQty; }
            set
            {
                if (_itemQty == value) return;
                PrevItemQty = _itemQty;
                _itemQty = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ItemQty"));
            }
        }       
    }
}
