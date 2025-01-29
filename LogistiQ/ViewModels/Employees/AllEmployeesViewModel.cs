using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogistiQ.ViewModels.Employees
{
    public class AllEmployeesViewModel : AllViewModel<EmployeeForAllView>
    {
        #region Constructor

        public AllEmployeesViewModel()
            : base()
        {
            base.DisplayName = "Employees";

        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewEmployeeViewModel(); // Zwracamy odpowiedni ViewModel
        }
        #endregion

        #region Sort And Find
        //tu decydujemy po czym sortować do comboboxa
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "first name", "last name" };
        }
        //tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "first name")
                List = new ObservableCollection<EmployeeForAllView>(List.OrderBy(item => item.FirstName));
            if (SortField == "last name")
                List = new ObservableCollection<EmployeeForAllView>(List.OrderBy(item => item.LastName));
        }
        //tu decydujemy po czym wyszukiwać
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "first name", "last name" };
        }
        //tu decydujemy jak wyszukiwać
        public override void Find()
        {
            Load();
            if (FindField == "first name")
                List = new ObservableCollection<EmployeeForAllView>(List.Where(item => item.FirstName != null && item.FirstName.StartsWith(FindTextBox)));
            if (FindField == "last name")
                List = new ObservableCollection<EmployeeForAllView>(List.Where(item => item.LastName != null && item.LastName.StartsWith(FindTextBox)));
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<EmployeeForAllView>
                (
                    from employees in logistiq_Entities.Employees
                    select new EmployeeForAllView
                    {
                        EmployeeID = employees.EmployeeID,
                        FirstName = employees.FirstName,
                        LastName = employees.LastName,
                        Position = employees.Position,
                        WarehouseName = employees.Warehouses.Name,
                        WarehouseLocation = employees.Warehouses.Location,
                        Remarks = employees.Remarks
                    }
                );
        }
        #endregion
    }
}