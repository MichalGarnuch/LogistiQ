using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogistiQ.ViewModels.Discounts;
using System.ComponentModel.DataAnnotations;

namespace LogistiQ.ViewModels.Customers
{
    public class AllCustomersViewModel : AllViewModel<LogistiQ.Models.Entities.Customers>
    {
        #region Constructor

        public AllCustomersViewModel()
            : base()
        {
            base.DisplayName = "Customers";

        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewCustomersViewModel(); // Zwracamy odpowiedni ViewModel
        }
        #endregion

        #region Sort And Find
        //tu decydujemy po czym sortować do comboboxa
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "id", "first name", "last name", "NIP", "address", " phone", "email", "remarks"};
        }
        //tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "id")
                List = new ObservableCollection<LogistiQ.Models.Entities.Customers>(List.OrderBy(item => item.CustomerID));
            if (SortField == "first name")
                List = new ObservableCollection<LogistiQ.Models.Entities.Customers>(List.OrderBy(item => item.FirstName));
            if (SortField == "last name")
                List = new ObservableCollection<LogistiQ.Models.Entities.Customers>(List.OrderBy(item => item.LastName));
            if (SortField == "NIP")
                List = new ObservableCollection<LogistiQ.Models.Entities.Customers>(List.OrderBy(item => item.NIP));
            if (SortField == "address")
                List = new ObservableCollection<LogistiQ.Models.Entities.Customers>(List.OrderBy(item => item.Address));
            if (SortField == "phone")
                List = new ObservableCollection<LogistiQ.Models.Entities.Customers>(List.OrderBy(item => item.Phone));
            if (SortField == "email")
                List = new ObservableCollection<LogistiQ.Models.Entities.Customers>(List.OrderBy(item => item.Email));
            if (SortField == "remarks")
                List = new ObservableCollection<LogistiQ.Models.Entities.Customers>(List.OrderBy(item => item.Remarks));
        }
        //tu decydujemy po czym wyszukiwać
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "id", "first name", "last name", "NIP", "address", " phone", "email", "remarks" };
        }
        //tu decydujemy jak wyszukiwać
        public override void Find()
        {
            Load();
            if (FindField == "first name")
                List = new ObservableCollection<LogistiQ.Models.Entities.Customers>(List.Where(item => item.FirstName != null && item.FirstName.StartsWith(FindTextBox)));
            if (FindField == "last name")
                List = new ObservableCollection<LogistiQ.Models.Entities.Customers>(List.Where(item => item.LastName != null && item.LastName.StartsWith(FindTextBox)));
            if (FindField == "NIP")
                List = new ObservableCollection<LogistiQ.Models.Entities.Customers>(List.Where(item => item.NIP != null && item.NIP.StartsWith(FindTextBox)));
            if (FindField == "address")
                List = new ObservableCollection<LogistiQ.Models.Entities.Customers>(List.Where(item => item.Address != null && item.Address.StartsWith(FindTextBox)));
            if (FindField == "phone")
                List = new ObservableCollection<LogistiQ.Models.Entities.Customers>(List.Where(item => item.Phone != null && item.Phone.StartsWith(FindTextBox)));
            if (FindField == "email")
                List = new ObservableCollection<LogistiQ.Models.Entities.Customers>(List.Where(item => item.Email != null && item.Email.StartsWith(FindTextBox)));
            if (FindField == "remarks")
                List = new ObservableCollection<LogistiQ.Models.Entities.Customers>(List.Where(item => item.Remarks != null && item.Remarks.StartsWith(FindTextBox)));
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<LogistiQ.Models.Entities.Customers>
                (
                    logistiq_Entities.Customers.ToList()
                );
        }

        #endregion
    }

}
