using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Views.BaseWorkspace;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using System.ComponentModel;
using System.Windows.Forms;
using LogistiQ.Validators;

namespace LogistiQ.ViewModels.Products
{
    public class NewProductViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Products>, IDataErrorInfo
    {
        
    #region Konstruktor
        public NewProductViewModel()
            : base("Products")
    {
            item = new Models.Entities.Products();
    }

    #endregion

    #region Properties

    public int ProductID
    {
        get
        {
            return item.ProductID;
        }
        set
        {
            item.ProductID = value;
            OnPropertyChanged(() => ProductID);
        }
    }
    public string Name
    {
        get
        {
            return item.Name;
        }
        set
        {
            item.Name = value;
            OnPropertyChanged(() => Name);
        }
    }
    public string Type
    {
        get
        {
            return item.Type;
        }
        set
        {
            item.Type = value;
            OnPropertyChanged(() => Type);
        }
    }
    public string Brand
    {
        get
        {
            return item.Brand;
        }
        set
        {
            item.Brand = value;
            OnPropertyChanged(() => Brand);
        }
    }
    public decimal UnitPrice
    {
        get
        {
            return item.UnitPrice;
        }
        set
        {
            item.UnitPrice = value;
            OnPropertyChanged(() => UnitPrice);
        }
        }
        public string Description
        {
            get
            {
                return item.Description;
            }
            set
            {
                item.Description = value;
                OnPropertyChanged(() => Description);
            }
        }
        public decimal VAT
    {
        get
        {
            return item.VAT;
        }
        set
        {
            item.VAT = value;
            OnPropertyChanged(() => VAT);
        }
    }
    public int? CategoryID
    {
        get
        {
            return item.CategoryID;
        }
        set
        {
            item.CategoryID = value;
            OnPropertyChanged(() => CategoryID);
        }
    }

        #endregion
        
    #region PropertiesForCombobox

        public IQueryable<KeyAndValue> CategoryKeyAndValueItems
        {
            get
            {
                return new LogistiQ.Models.BusinessLogic.
                    CategoryB(logistiQ_Entities).GetCategoryKeyAndValueItems();
            }
        }
        #endregion
        
    #region Helpers

        public override void Save()
    {
        logistiQ_Entities.Products.Add(item);
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

                if (properties == nameof(Name))
                {
                    validateMessage = StringValidator.ValidateIsFirstLetterUpper(Name);
                }
                else if (properties == nameof(UnitPrice))
                {
                    validateMessage = BusinessValidator.ValidateIsPricePositive(UnitPrice);
                }
                else if (properties == nameof(Type))
                {
                    validateMessage = StringValidator.ValidateIsNotEmpty(Type);
                }
                else if (properties == nameof(Brand))
                {
                    validateMessage = StringValidator.ValidateIsNotEmpty(Brand);
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

