using ECommerceApp.Domain.Entities.Common;

namespace ECommerceApp.Domain.Entities.Product
{
    public class ProductSelectedCategory : BaseEntity
    {
        #region Relation
        public long ProductId { get; set; }
        public long ProductCategoryId { get; set; }

        #endregion

        #region Relations

        public Product Product { get; set; }
        public ProductCategory ProductCategory { get; set; }

        #endregion
    }
}
