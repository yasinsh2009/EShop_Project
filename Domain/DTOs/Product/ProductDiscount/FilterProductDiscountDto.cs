using ECommerceApp.Domain.DTOs.Paging;

namespace ECommerceApp.Domain.DTOs.Product.ProductDiscount
{
    public class FilterProductDiscountDto : BasePaging
    {
        #region Properties

        public long? ProductId { get; set; }
        public int Percentage { get; set; }
        public DateTime ExpireDate { get; set; }
        public int? DiscountNumber { get; set; }
        public string CreateDate { get; set; }
        public List<Entities.Product.ProductDiscount> ProductDiscounts { get; set; }

        #endregion

        #region Methods

        public FilterProductDiscountDto SetProductDiscount(List<Entities.Product.ProductDiscount> productDiscounts)
        {
            ProductDiscounts = productDiscounts;
            return this;
        }

        public FilterProductDiscountDto SetPaging(BasePaging paging)
        {
            PageId = paging.PageId;
            AllEntitiesCount = paging.AllEntitiesCount;
            StartPage = paging.StartPage;
            EndPage = paging.EndPage;
            HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            TakeEntity = paging.TakeEntity;
            SkipEntity = paging.SkipEntity;
            PageCount = paging.PageCount;

            return this;
        }

        #endregion
    }
}
