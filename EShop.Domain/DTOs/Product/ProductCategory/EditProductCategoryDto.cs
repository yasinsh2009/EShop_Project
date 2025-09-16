using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.DTOs.Product.ProductCategory
{
    public class EditProductCategoryDto : CreateProductCategoryDto
    {
        public long Id { get; set; }
    }

    public enum EditProductCategoryResult
    {
        Success,
        NotFound,
        Error,
        ImageErrorType,
    }
}
