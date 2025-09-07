using EShop.Domain.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Interface
{
    public interface IProductService : IAsyncDisposable
    {
        Task<FilterProductDto> FilterProducts(FilterProductDto product);
        Task<CreateProductResult> CreateProduct(CreateProductDto product);
    }
}
