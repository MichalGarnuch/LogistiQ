using LogistiQ.Helper;
using LogistiQ.Models.BusinessLogic.SharedLogic;
using LogistiQ.Models.BusinessLogic;
using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;

namespace LogistiQ.ViewModels.BusinessLogicViewModel
{
    public class OrderRecordViewModel : AllViewModel<OrderRecordForAllView>
    {
        private readonly OrderRecordB orderRecordB;
        private readonly OrderB orderB;

        #region Konstruktor
        public OrderRecordViewModel()
            : base()
        {
            base.DisplayName = "Order Records";

            orderRecordB = new OrderRecordB(logistiq_Entities);
            orderB = new OrderB(logistiq_Entities);

            CustomersList = new ObservableCollection<KeyAndValue>(orderRecordB.GetCustomerKeyAndValueItems());
            SelectedCustomerId = CustomersList.Any() ? CustomersList.First().Key : 0;

            Load();

            RefreshCommand = new BaseCommand(Load);
        }
        #endregion

        #region Właściwości

        private ObservableCollection<KeyAndValue> _customersList;
        public ObservableCollection<KeyAndValue> CustomersList
        {
            get { return _customersList; }
            set
            {
                _customersList = value;
                OnPropertyChanged(() => CustomersList);
            }
        }

        private int _selectedCustomerId;
        public int SelectedCustomerId
        {
            get { return _selectedCustomerId; }
            set
            {
                if (_selectedCustomerId != value)
                {
                    _selectedCustomerId = value;
                    OnPropertyChanged(() => SelectedCustomerId);
                    Load();
                }
            }
        }
        #endregion

        #region Sortowanie i wyszukiwanie

        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "customer", "order date", "status", "product" };
        }

        public override void Sort()
        {
            if (SortField == "customer")
                List = new ObservableCollection<OrderRecordForAllView>(List.OrderBy(item => item.CustomerName));
            if (SortField == "order date")
                List = new ObservableCollection<OrderRecordForAllView>(List.OrderBy(item => item.OrderDate));
            if (SortField == "status")
                List = new ObservableCollection<OrderRecordForAllView>(List.OrderBy(item => item.Status));
            if (SortField == "product")
                List = new ObservableCollection<OrderRecordForAllView>(List.OrderBy(item => item.ProductName));
        }

        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "customer", "product" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "customer")
                List = new ObservableCollection<OrderRecordForAllView>(List.Where(item => item.CustomerName != null && item.CustomerName.StartsWith(FindTextBox)));
            if (FindField == "product")
                List = new ObservableCollection<OrderRecordForAllView>(List.Where(item => item.ProductName != null && item.ProductName.StartsWith(FindTextBox)));
        }
        #endregion

        #region Komendy
        public ICommand RefreshCommand { get; set; }
        #endregion

        #region Metody pomocnicze
        public override void Load()
        {
            if (SelectedCustomerId != 0)
            {
                List = new ObservableCollection<OrderRecordForAllView>(
                    orderRecordB.GetOrdersByCustomer(SelectedCustomerId));
            }
        }

        public override WorkspaceViewModel CreateNewViewModel()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
