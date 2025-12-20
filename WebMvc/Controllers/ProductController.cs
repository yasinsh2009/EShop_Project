using ECommerceApp.Application.Services.Interface;
using ECommerceApp.Domain.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.WebMvc.Controllers
{
    public class ProductController : SiteBaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("products")]
        [HttpGet("products/{category}")]
        public async Task<IActionResult> FilterProducts(FilterProductDto filterProduct)
        {
            var products = await _productService.FilterProducts(filterProduct);

            ViewBag.ProductCategories = await _productService.GetAllActiveProductCategories();
            return View(products);
        }

        [HttpGet("product/{id}")]
        public async Task<IActionResult> ProductDetails(long id)
        {
            var product = await _productService.GetProductDetails(id);

            return View(product);
        }
    }
}
