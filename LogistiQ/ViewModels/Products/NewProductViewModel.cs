﻿using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Views.BaseWorkspace;

namespace LogistiQ.ViewModels.Products
{
    public class NewProductViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Products>
    {
        
        #region Konstruktor
        public NewProductViewModel()
            : base("Products")
    {
            item = new Models.Entities.Products();
    }

    #endregion

    #region Properties

    //dla każdego pola na interfejsie tworzymy properties
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

    #region Helpers
    public override void Save()
    {
        logistiQ_Entities.Products.Add(item);//dodaje towar do lokalnej kolekcji
        logistiQ_Entities.SaveChanges();//zapisuje zmiany dokonane w bazie danych
    }

    #endregion
    }
}

