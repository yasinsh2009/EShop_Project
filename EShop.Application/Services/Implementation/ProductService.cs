using EShop.Application.Extensions;
using EShop.Application.Services.Interface;
using EShop.Application.Utilities;
using EShop.Domain.DTOs.Paging;
using EShop.Domain.DTOs.Product;
using EShop.Domain.Entities.Product;
using EShop.Domain.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Services.Implementation
{
    public class ProductService : IProductService
    {
        #region Ctor

        private readonly IGenericRepository<Product> _productRepository;

        public ProductService(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        #endregion

        #region Product

        #region Filter Products

        public async Task<FilterProductDto> FilterProducts(FilterProductDto product)
        {
            var query = _productRepository
                .GetQuery()
                .AsQueryable();

            #region Product State

            switch (product.ProductState)
            {
                case FilterProductState.All:
                    break;
                case FilterProductState.Active:
                    query = query.Where(x => x.IsActive);
                    break;
                case FilterProductState.NotActive:
                    query = query.Where(x => !x.IsActive);
                    break;
            }

            #endregion

            #region Product Order

            switch (product.OrderBy)
            {
                case FilterProductOrderBy.CreateDateDescending:
                    query = query.OrderByDescending(x => x.CreateDate);
                    break;
                case FilterProductOrderBy.PriceAscending:
                    query = query.OrderBy(x => x.Price);
                    break;
                case FilterProductOrderBy.PriceDescending:
                    query = query.OrderByDescending(x => x.Price);
                    break;
                case FilterProductOrderBy.ViewDescending:
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case FilterProductOrderBy.SellDescending:
                    query = query.OrderByDescending(x => x.SellCount);
                    break;
                case FilterProductOrderBy.SellAscending:
                    query = query.OrderBy(x => x.SellCount);
                    break;
                case FilterProductOrderBy.CreateDateAscending:
                    query = query.OrderBy(x => x.CreateDate);
                    break;
            }

            #endregion

            #region Filter Products

            if (!string.IsNullOrWhiteSpace(product.ProductTitle))
            {
                query = query.Where(x => EF.Functions.Like(x.Title, $"%{product.ProductTitle}%"));
            }

            #endregion

            #region Product Paging

            var productCount = await query.CountAsync();

            var pager = Pager.Build(product.PageId, productCount, product.TakeEntity,
                product.HowManyShowPageAfterAndBefore);

            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion

            return product.SetPaging(pager).SetProduct(allEntities);
        }

        #endregion

        #region Create Product

        public async Task<CreateProductResult> CreateProduct(CreateProductDto product)
        {
            try
            {
                string productImageName = null;
                if (product.Image != null)
                {
                    if (product.Image.IsImage())
                    {
                        productImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(product.Image.FileName);
                        product.Image.AddImageToServer(productImageName, PathExtension.ProductOriginServer,
                            100, 100, PathExtension.ProductThumbServer);
                    }
                    else
                    {
                        return CreateProductResult.ImageErrorType;
                    }
                }

                var newProduct = new Product
                {
                    Title = product.Title,
                    Code = new Random().Next(100000, 999999).ToString(),
                    ShortDescription = product.ShortDescription,
                    Description = product.Description,
                    IsActive = product.IsActive,
                    Image = productImageName,
                    Price = product.Price,
                    SellCount = 0,
                    ViewCount = 0
                };

                await _productRepository.AddEntity(newProduct);
                await _productRepository.SaveChanges();

                return CreateProductResult.Success;
            }
            catch (Exception)
            {
                return CreateProductResult.Error;
            }
        }

        #endregion

        #region Activate / DeActivate Product

        public async Task<bool> ActivateProduct(long id)
        {
            var product= await _productRepository
                .GetQuery()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (product != null)
            {
                product.IsActive = true;
                product.IsDelete = false;

                _productRepository.EditEntity(product);
                await _productRepository.SaveChanges();

                return true;
            }

            return false;
        }

        public async Task<bool> DeActivateProduct(long id)
        {
            var product = await _productRepository
                .GetQuery()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (product != null)
            {
                product.IsActive = false;
                product.IsDelete = true;

                _productRepository.EditEntity(product);
                await _productRepository.SaveChanges();

                return true;
            }

            return false;
        }

        #endregion

        #endregion

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            if (_productRepository != null)
            {
                await _productRepository.DisposeAsync();
            }
        }

        #endregion
    }
}
