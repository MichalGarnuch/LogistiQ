using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogistiQ.ViewModels.Shifts
{
    public class AllShiftsViewModel : AllViewModel<ShiftForAllView>
    {
        #region Constructor

        public AllShiftsViewModel()
            : base()
        {
            base.DisplayName = "Shifts";
            IsAddVisible = false;
        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return null;
        }
        public new void AddNew() { }
        #endregion

        #region Sort And Find
        //tu decydujemy po czym sortować do comboboxa
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "employee first name", "employee last name" };
        }
        //tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "employee first name")
                List = new ObservableCollection<ShiftForAllView>(List.OrderBy(item => item.EmployeeFirstName));
            if (SortField == "employee last name")
                List = new ObservableCollection<ShiftForAllView>(List.OrderBy(item => item.EmployeeLastName));
        }
        //tu decydujemy po czym wyszukiwać
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "employee first name", "employee last name" };
        }
        //tu decydujemy jak wyszukiwać
        public override void Find()
        {
            Load();
            if (FindField == "employee first name")
                List = new ObservableCollection<ShiftForAllView>(List.Where(item => item.EmployeeFirstName != null && item.EmployeeFirstName.StartsWith(FindTextBox)));
            if (FindField == "employee last name")
                List = new ObservableCollection<ShiftForAllView>(List.Where(item => item.EmployeeLastName != null && item.EmployeeLastName.StartsWith(FindTextBox)));
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<ShiftForAllView>
                (
                    from shifts in logistiq_Entities.Shifts
                    select new ShiftForAllView
                    {
                        ShiftID = shifts.ShiftID,
                        EmployeeFirstName = shifts.Employees.FirstName,
                        EmployeeLastName = shifts.Employees.LastName,
                        EmployeePosition = shifts.Employees.Position,
                        Date = shifts.Date,
                        StartTime = shifts.StartTime,
                        EndTime = shifts.EndTime,
                        WarehouseName = shifts.Warehouses.Name,
                        WarehouseLocation = shifts.Warehouses.Location,
                    }
                );
        }
        #endregion
    }
}