﻿using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogistiQ.ViewModels.Discounts;

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