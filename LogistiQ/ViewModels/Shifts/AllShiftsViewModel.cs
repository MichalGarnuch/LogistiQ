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
            throw new System.NotImplementedException();
        }
        //tu decydujemy jak sortować
        public override void Sort()
        {
            throw new System.NotImplementedException();
        }
        //tu decydujemy po czym wyszukiwać
        public override List<string> GetComboboxFindList()
        {
            throw new System.NotImplementedException();
        }
        //tu decydujemy jak wyszukiwać
        public override void Find()
        {
            throw new System.NotImplementedException();
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