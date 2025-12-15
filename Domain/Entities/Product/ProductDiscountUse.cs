using ECommerceApp.Domain.Entities.Common;

namespace ECommerceApp.Domain.Entities.Product
{
    public class ProductDiscountUse : BaseEntity
    {
        #region Properties

        public long DiscountId { get; set; }

        #endregion

        #region Relations

        public ProductDiscount ProductDiscount { get; set; }

        #endregion
    }
}
