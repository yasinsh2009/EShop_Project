﻿using System.ComponentModel.DataAnnotations;
using EShop.Domain.DTOs.Paging;

namespace EShop.Domain.DTOs.Product
{
    public class FilterProductDto : BasePaging
    {
        #region Constructor

        public FilterProductDto()
        {
            OrderBy = FilterProductOrderBy.CreateDateDescending;
        }

        #endregion

        #region Properties

        public long ProductId { get; set; }
        public string Search { get; set; }
        public string ProductTitle { get; set; }
        public string ProductCode { get; set; }
        public string ProductBrand { get; set; }
        public string Category { get; set; }
        public int FilterMinPrice { get; set; }
        public int FilterMaxPrice { get; set; }
        public int SelectedMinPrice { get; set; }
        public int SelectedMaxPrice { get; set; }
        public int MobileSelectedMinPrice { get; set; }
        public int MobileSelectedMaxPrice { get; set; }
        public List<Entities.Product.Product> Products { get; set; }
        public FilterProductState ProductState { get; set; }
        public FilterProductOrderBy OrderBy { get; set; }
        public FilterProductOrder ProductOrder { get; set; }
        public List<long> SelectedProductCategories { get; set; }
        //public List<Entities.Products.ProductCategory> ProductCategories { get; set; }

        #endregion

        #region Methods

        public FilterProductDto SetProduct(List<Entities.Product.Product> products)
        {
            this.Products = products;
            return this;
        }

        public FilterProductDto SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.AllEntitiesCount = paging.AllEntitiesCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.PageCount = paging.PageCount;

            return this;
        }

        #endregion
    }


    public enum FilterProductState
    {
        [Display(Name = "همه")]
        All,

        [Display(Name = "فعال")]
        Active,

        [Display(Name = "غیرفعال")]
        NotActive
    }

    public enum FilterProductOrder
    {
        CreateDateDescending,
        CreateDateAscending,
    }

    public enum FilterProductOrderBy 
    {
        [Display(Name = "جدیدترین")]
        CreateDateDescending,

        [Display(Name = "ارزانترین")]
        PriceAscending,

        [Display(Name = "گرانترین")]
        PriceDescending,

        [Display(Name = "پر بازدیدترین")]
        ViewDescending,

        [Display(Name = "پر فروشترین")]
        SellDescending,

        [Display(Name = "کم فروشترین")]
        SellAscending,

        [Display(Name = "منتخب")]
        CreateDateAscending,

    }
}
