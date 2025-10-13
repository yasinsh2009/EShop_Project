using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.DTOs.Product
{
    public class EditProductDto : CreateProductDto
    {
        public long Id { get; set; }
    }

    public enum EditProductResult
    {
        NotFound,
        NotForUser,
        Success,
        Error,
        ImageErrorType
    }
}
