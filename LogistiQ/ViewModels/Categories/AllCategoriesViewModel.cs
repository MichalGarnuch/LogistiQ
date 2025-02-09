using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.ViewModels.Categories;
using LogistiQ.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.ViewModels.Categories
{
    /// <summary>
    /// AllCategoriesViewModel odpowiada za prezentację listy kategorii.
    /// Dziedziczy po <see cref="AllViewModel{T}"/> z typem <see cref="CategoryForAllView"/>,
    /// co umożliwia korzystanie z mechanizmów ładowania, sortowania i wyszukiwania danych.
    /// </summary>
    public class AllCategoriesViewModel : AllViewModel<CategoryForAllView>
    {
        #region Constructor
        /// <summary>
        /// Konstruktor inicjalizuje widok listy kategorii.
        /// Ustawia nazwę wyświetlaną na "Categories".
        /// </summary>
        public AllCategoriesViewModel()
            : base()
        {
            base.DisplayName = "Categories";
        }

        /// <summary>
        /// Tworzy nowy widok do dodawania kategorii.
        /// Ta metoda jest wywoływana przy próbie dodania nowej kategorii.
        /// </summary>
        /// <returns>Nowa instancja <see cref="NewCategoryViewModel"/>.</returns>
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewCategoryViewModel();
        }
        #endregion

        #region Sort And Find

        /// <summary>
        /// Zwraca listę kryteriów sortowania, które będą dostępne w ComboBox.
        /// W tym przypadku kryteriami są "name" oraz "description".
        /// </summary>
        /// <returns>Lista kryteriów sortowania.</returns>
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "name", "description" };
        }

        /// <summary>
        /// Sortuje listę kategorii według wybranego kryterium określonego w właściwości <c>SortField</c>.
        /// Jeśli <c>SortField</c> ma wartość "name", sortowanie odbywa się według nazwy kategorii.
        /// Jeśli <c>SortField</c> ma wartość "description", sortowanie odbywa się według opisu.
        /// </summary>
        public override void Sort()
        {
            if (SortField == "name")
                List = new ObservableCollection<CategoryForAllView>(List.OrderBy(item => item.Name));
            if (SortField == "description")
                List = new ObservableCollection<CategoryForAllView>(List.OrderBy(item => item.Description));
        }

        /// <summary>
        /// Zwraca listę kryteriów wyszukiwania, które będą dostępne w ComboBox.
        /// W tym przypadku kryteriami są "name" oraz "description".
        /// </summary>
        /// <returns>Lista kryteriów wyszukiwania.</returns>
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "name", "description" };
        }

        /// <summary>
        /// Filtrowanie danych na podstawie wybranego kryterium wyszukiwania (FindField) i tekstu wpisanego w polu wyszukiwania (FindTextBox).
        /// Przed filtrowaniem lista jest odświeżana, aby operować na pełnym zbiorze danych.
        /// </summary>
        public override void Find()
        {
            // Odśwież dane przed przystąpieniem do filtrowania
            Load();

            if (FindField == "name")
                List = new ObservableCollection<CategoryForAllView>(
                    List.Where(item => item.Name != null && item.Name.StartsWith(FindTextBox)));
            if (FindField == "description")
                List = new ObservableCollection<CategoryForAllView>(
                    List.Where(item => item.Description != null && item.Description.StartsWith(FindTextBox)));
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Metoda Load pobiera dane kategorii z bazy i aktualizuje właściwość <c>List</c>.
        /// W tym przypadku dane są pobierane z kontekstu bazy danych (logistiq_Entities) i mapowane do obiektów <see cref="CategoryForAllView"/>.
        /// </summary>
        public override void Load()
        {
            List = new ObservableCollection<CategoryForAllView>(
                from categories in logistiq_Entities.Categories
                select new CategoryForAllView
                {
                    CategoryID = categories.CategoryID,
                    Name = categories.Name,
                    Description = categories.Description
                });
        }

        #endregion
    }
}
