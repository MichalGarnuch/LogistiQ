﻿using LogistiQ.Models.BusinessLogic;
using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using LogistiQ.Validators;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Views.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.ViewModels.Deliveries
{
    public class NewDeliveryViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Deliveries>, IDataErrorInfo
    {

        #region Konstruktor
        public NewDeliveryViewModel()
            : base("Deliveries")
        {
            item = new Models.Entities.Deliveries();
        }

        #endregion

        #region Properties

       
        public int DeliveryID
        {
            get
            {
                return item.DeliveryID;
            }
            set
            {
                item.DeliveryID = value;
                OnPropertyChanged(() => DeliveryID);
            }
        }
        public int? SupplierID
        {
            get
            {
                return item.SupplierID;
            }
            set
            {
                item.SupplierID = value;
                OnPropertyChanged(() => SupplierID);
            }
        }
        public DateTime DeliveryDate
        {
            get
            {
                return item.DeliveryDate;
            }
            set
            {
                item.DeliveryDate = value;
                OnPropertyChanged(() => DeliveryDate);
            }
        }
        public decimal Cost
        {
            get
            {
                return item.Cost;
            }
            set
            {
                item.Cost = value;
                OnPropertyChanged(() => Cost);
            }
        }
        public string Status
        {
            get
            {
                return item.Status;
            }
            set
            {
                item.Status = value;
                OnPropertyChanged(() => Status);
            }
        }
        public int? WarehouseID
        {
            get
            {
                return item.WarehouseID;
            }
            set
            {
                item.WarehouseID = value;
                OnPropertyChanged(() => WarehouseID);
            }
        }
        #endregion

        #region PropertiesForCombobox

        public IQueryable<KeyAndValue> SupplierKeyAndValueItems
        {
            get
            {
                return new LogistiQ.Models.BusinessLogic.
                    SupplierB(logistiQ_Entities).GetSupplierKeyAndValueItems();
            }
        }
        public IQueryable<KeyAndValue> WarehouseKeyAndValueItems
        {
            get
            {
                return new LogistiQ.Models.BusinessLogic.
                    WarehouseB(logistiQ_Entities).GetWarehouseKeyAndValueItems();
            }
        }
        #endregion
       
        #region Helpers
        public override void Save()
        {
            logistiQ_Entities.Deliveries.Add(item);
            logistiQ_Entities.SaveChanges();
        }

        #endregion

        #region Validation

        public string Error => string.Empty;
        private readonly Dictionary<string, string> _validationMessages = new Dictionary<string, string>();

        public string this[string properties]
        {
            get
            {
                string validateMessage = string.Empty;

                if (properties == nameof(SupplierID))
                {
                    validateMessage = StringValidator.ValidateIsNotEmpty(SupplierID?.ToString());
                }
                else if (properties == nameof(Cost))
                {
                    validateMessage = BusinessValidator.ValidateIsPricePositive(Cost);
                }
                else if (properties == nameof(WarehouseID))
                {
                    validateMessage = StringValidator.ValidateIsNotEmpty(WarehouseID?.ToString());
                }

                if (!string.IsNullOrEmpty(validateMessage))
                {
                    _validationMessages[properties] = validateMessage;
                }
                else
                {
                    _validationMessages.Remove(properties);
                }

                return validateMessage;
            }
        }

        public override bool IsValid()
        {
            return !_validationMessages.Any();
        }

        #endregion
    }
}
