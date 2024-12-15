﻿using System; 
using System.Collections.Generic; 
using System.Linq;
using System.Text; 
using System.Collections.ObjectModel; 
using LogistiQ.Helper; 
using System.Diagnostics; 
using System.Collections.Specialized; 
using System.ComponentModel; 
using System.Windows.Data; 
using LogistiQ.ViewModels.Products;
using LogistiQ.ViewModels.Invoices;
using LogistiQ.ViewModels.Inventory;
using LogistiQ.ViewModels.Categories;
using LogistiQ.ViewModels.Customers;
using LogistiQ.ViewModels.Deliveries;
using LogistiQ.ViewModels.Employees;
using LogistiQ.ViewModels.Orders;
using LogistiQ.ViewModels.Payments;
using LogistiQ.ViewModels.Returns;
using LogistiQ.ViewModels.Shifts;
using LogistiQ.ViewModels.StockLevels;
using LogistiQ.ViewModels.Suppliers;
using LogistiQ.ViewModels.Warehouses;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.ViewModels.Discounts;
using LogistiQ.ViewModels.Documents;

namespace LogistiQ.ViewModels.BaseWorkspace 
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private ReadOnlyCollection<CommandViewModel> _Commands; // Kolekcja tylko do odczytu przechowująca
                                                                // dostępne komendy
        private ObservableCollection<WorkspaceViewModel> _Workspaces; // Kolekcja obiektów
                                                                      // WorkspaceViewModel, która informuje
                                                                      // o zmianach
        #endregion

        #region Commands
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_Commands == null)
                {
                    List<CommandViewModel> cmds = this.CreateCommands(); 
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _Commands;
            }
        }

        private List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    "Products",
                    new BaseCommand(() => this.ShowAllView(new AllProductsViewModel()))), 
                new CommandViewModel(
                    "Product",
                    new BaseCommand(() => this.CreateView(new NewProductViewModel()))), 
            
                new CommandViewModel(
                    "Invoices",
                    new BaseCommand(() => this.ShowAllView(new AllInvoicesViewModel()))), 
                new CommandViewModel(
                    "Invoice",
                    new BaseCommand(() => this.CreateView(new NewInvoiceViewModel()))),

                new CommandViewModel(
                    "Inventory",
                    new BaseCommand(() => this.ShowAllView(new InventoryViewModel()))),

                new CommandViewModel(
                    "Categories",
                    new BaseCommand(() => this.ShowAllView(new AllCategoriesViewModel()))), 
                new CommandViewModel(
                    "Category",
                    new BaseCommand(() => this.CreateView(new NewCategoryViewModel()))),
                
                new CommandViewModel(
                    "Customers",
                    new BaseCommand(() => this.ShowAllView(new AllCustomersViewModel()))), 
                new CommandViewModel(
                    "Customer",
                    new BaseCommand(() => this.CreateView(new NewCustomersViewModel()))),
                
                new CommandViewModel(
                    "Deliveries",
                    new BaseCommand(() => this.ShowAllView(new AllDeliveriesViewModel()))), 
                new CommandViewModel(
                    "DeliveryDetails",
                    new BaseCommand(() => this.ShowAllView(new DeliveryDetailsViewModel()))), 
                new CommandViewModel(
                    "Delivery",
                    new BaseCommand(() => this.CreateView(new NewDeliveryViewModel()))),
               
                new CommandViewModel(
                    "Discounts",
                    new BaseCommand(() => this.ShowAllView(new AllDiscountsViewModel()))), 
                new CommandViewModel(
                    "Discount",
                    new BaseCommand(() => this.CreateView(new NewDiscountViewModel()))), 

                new CommandViewModel(
                    "Documents",
                    new BaseCommand(() => this.ShowAllView(new AllDocumentsViewModel()))), 
                new CommandViewModel(
                    "Document",
                    new BaseCommand(() => this.CreateView(new NewDocumentViewModel()))), 

                new CommandViewModel(
                    "Employees",
                    new BaseCommand(() => this.ShowAllView(new AllEmployeesViewModel()))), 
                new CommandViewModel(
                    "Employee",
                    new BaseCommand(() => this.CreateView(new NewEmployeeViewModel()))), 

                new CommandViewModel(
                    "Orders",
                    new BaseCommand(() => this.ShowAllView(new AllOrdersViewModel()))),
                new CommandViewModel(
                    "OrderDetail",
                    new BaseCommand(() => this.ShowAllView(new OrderDetailsViewModel()))), 
                new CommandViewModel(
                    "Order",
                    new BaseCommand(() => this.CreateView(new NewOrderViewModel()))), 

                new CommandViewModel(
                    "Payments",
                    new BaseCommand(() => this.ShowAllView(new AllPaymentsViewModel()))), 
               
                new CommandViewModel(
                    "Returns",
                    new BaseCommand(() => this.ShowAllView(new AllReturnsViewModel()))), 
                new CommandViewModel(
                    "Return",
                    new BaseCommand(() => this.CreateView(new NewReturnViewModel()))), 

                new CommandViewModel(
                    "Shifts",
                    new BaseCommand(() => this.ShowAllView(new AllShiftsViewModel()))), 
               
                new CommandViewModel(
                    "StockLevels",
                    new BaseCommand(() => this.ShowAllView(new StockLevelsViewModel()))), 

                new CommandViewModel(
                    "Suppliers",
                    new BaseCommand(() => this.ShowAllView(new AllSuppliersViewModel()))), 
                new CommandViewModel(
                    "Supplier",
                    new BaseCommand(() => this.CreateView(new NewSupplierViewModel()))), 

                new CommandViewModel(
                    "Warehouses",
                    new BaseCommand(() => this.ShowAllView(new AllWarehousesViewModel()))), 
                new CommandViewModel(
                    "Warehouse",
                    new BaseCommand(() => this.CreateView(new NewWarehouseViewModel())))
            };
        }
        #endregion

        #region Workspaces
        // Właściwość Workspaces zwracająca kolekcję otwartych obszarów roboczych
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.OnWorkspacesChanged; // Subskrybuje zdarzenie
                                                                               // zmiany w kolekcji
                }
                return _Workspaces;
            }
        }

        // Obsługa zdarzenia zmiany w kolekcji Workspaces
        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose; // Dodanie obsługi zamykania
                                                                            // dla nowych obiektów

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose; // Usunięcie obsługi zamykania
                                                                            // dla usuniętych obiektów
        }

        // Obsługa żądania zamknięcia obszaru roboczego
        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispos(); // Opcjonalnie można dodać logikę do zwalniania zasobów
            this.Workspaces.Remove(workspace); // Usunięcie obszaru roboczego z kolekcji
        }
        #endregion // Workspaces

        #region Private Helpers
        public void CreateView(WorkspaceViewModel n)
        {
            this.Workspaces.Add(n);
            this.SetActiveWorkspace(n); 
        }

        private void ShowAllView(WorkspaceViewModel n)
        {
            var existingWorkspace = this.Workspaces.FirstOrDefault(
                vm => vm.GetType() == n.GetType());

            if (existingWorkspace == null)
            {
                this.Workspaces.Add(n);
            }
            else
            {
                n = existingWorkspace;
            }

            this.SetActiveWorkspace(n);
        }

        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace)); 
                                                             

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }
        #endregion
    }
}