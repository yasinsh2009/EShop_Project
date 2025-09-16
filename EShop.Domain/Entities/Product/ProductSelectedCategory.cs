using EShop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Entities.Product
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
