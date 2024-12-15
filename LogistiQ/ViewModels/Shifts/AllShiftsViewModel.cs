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