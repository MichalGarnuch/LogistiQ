using System;
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
using LogistiQ.ViewModels.BusinessLogicViewModel;

namespace LogistiQ.ViewModels.BaseWorkspace
{
    /// <summary>
    /// MainWindowViewModel odpowiada za zarządzanie głównym widokiem aplikacji.
    /// Odpowiada za inicjalizację dostępnych komend (CommandViewModel) oraz otwartych obszarów roboczych (WorkspaceViewModel).
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields

        // Kolekcja tylko do odczytu przechowująca dostępne komendy.
        private ReadOnlyCollection<CommandViewModel> _Commands;

        // Kolekcja obiektów WorkspaceViewModel, reprezentująca otwarte widoki (obszary robocze).
        private ObservableCollection<WorkspaceViewModel> _Workspaces;

        #endregion

        #region Commands

        /// <summary>
        /// Właściwość Commands zwraca listę komend (CommandViewModel) dostępnych w aplikacji.
        /// Jeśli kolekcja nie została jeszcze utworzona, następuje jej inicjalizacja poprzez wywołanie metody CreateCommands().
        /// </summary>
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

        /// <summary>
        /// Metoda CreateCommands tworzy listę komend, które umożliwiają otwieranie poszczególnych widoków.
        /// Każda komenda jest reprezentowana przez CommandViewModel z nazwą oraz powiązaną komendą (BaseCommand).
        /// </summary>
        /// <returns>Lista obiektów CommandViewModel.</returns>
        private List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    "Overview",
                    new BaseCommand(() => this.CreateView(new WarehouseOverviewViewModel()))),
                new CommandViewModel(
                    "DeliverRecord",
                    new BaseCommand(() => this.CreateView(new DeliveryRecordViewModel()))),
                new CommandViewModel(
                    "OrderRecord",
                    new BaseCommand(() => this.CreateView(new OrderRecordViewModel()))),
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

        /// <summary>
        /// Kolekcja Workspaces zawiera otwarte obszary robocze (widoki) i informuje o zmianach, dzięki czemu widok może zostać zaktualizowany.
        /// </summary>
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _Workspaces;
            }
        }

        /// <summary>
        /// Metoda OnWorkspacesChanged obsługuje zdarzenie zmiany kolekcji Workspaces.
        /// Subskrybuje zdarzenie RequestClose dla nowych obiektów oraz usuwa subskrypcję dla usuniętych.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose; // Dodanie obsługi zdarzenia zamykania dla nowych widoków

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose; // Usunięcie obsługi zdarzenia zamykania dla usuniętych widoków
        }

        /// <summary>
        /// Metoda OnWorkspaceRequestClose obsługuje żądanie zamknięcia konkretnego obszaru roboczego.
        /// Po otrzymaniu zdarzenia usuwa dany widok z kolekcji Workspaces.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający zdarzenie, powinien być typu WorkspaceViewModel.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            // Opcjonalnie można dodać logikę zwalniania zasobów, np. workspace.Dispose();
            this.Workspaces.Remove(workspace);
        }

        #endregion // Workspaces

        #region Private Helpers

        /// <summary>
        /// Metoda CreateView dodaje nowy widok do kolekcji Workspaces i ustawia go jako aktywny.
        /// </summary>
        /// <param name="n">Nowa instancja WorkspaceViewModel reprezentująca widok do dodania.</param>
        public void CreateView(WorkspaceViewModel n)
        {
            this.Workspaces.Add(n);
            this.SetActiveWorkspace(n);
        }

        /// <summary>
        /// Metoda ShowAllView sprawdza, czy widok danego typu już istnieje w kolekcji Workspaces.
        /// Jeśli tak, ustawia istniejący widok jako aktywny, w przeciwnym razie dodaje nowy widok do kolekcji.
        /// </summary>
        /// <param name="n">Instancja WorkspaceViewModel reprezentująca widok do wyświetlenia.</param>
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

        /// <summary>
        /// Metoda SetActiveWorkspace ustawia dany widok jako aktywny, wykorzystując domyślny widok kolekcji (ICollectionView).
        /// </summary>
        /// <param name="workspace">Obiekt WorkspaceViewModel, który ma zostać ustawiony jako aktywny.</param>
        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace)); // Upewnienie się, że kolekcja zawiera wskazany widok.

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        #endregion
    }
}
