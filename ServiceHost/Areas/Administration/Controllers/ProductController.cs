using EShop.Application.Services.Implementation;
using EShop.Application.Services.Interface;
using EShop.Domain.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class ProductController : AdminBaseController
    {
        #region Ctor

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        #region Product

        #region Filter Products

        [HttpGet("FilterProducts")]
        public async Task<IActionResult> FilterProducts(FilterProductDto product)
        {
            var products = await _productService.FilterProducts(product);
            return View(products);
        }

        #endregion

        #region Create Product

        [HttpGet("CreateProduct")]
        public async Task<IActionResult> CreateProduct()
        {
            return View();
        }


        [HttpPost("CreateProduct"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProductDto newProduct)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProduct(newProduct);

                switch (result)
                {
                    case CreateProductResult.Success:
                        TempData[SuccessMessage] = "محصول جدید با موفقیت ایجاد شد.";
                        RedirectToAction("FilterProducts", "Product", new { area = "Administration" });
                        break;
                    case CreateProductResult.Error:
                        TempData[ErrorMessage] = "فرایند ایجاد محصول با خطا مواجه شد، لطفا بعدا امتحان کنید.";
                        break;
                    case CreateProductResult.ImageErrorType:
                        TempData[ErrorMessage] = "لطفا تصویری با فرمت مناسب بارگداری کنید.";
                        break;
                }
            }
            return View(newProduct);
        }

        #endregion

        #region Activate / DeActivate Product

        [HttpGet("ActivateProduct/{productId}")]
        public async Task<IActionResult> ActivateProduct(long productId)
        {
            var result = await _productService.ActivateProduct(productId);
            if (result)
            {
                TempData[SuccessMessage] = "محصول موردنظر با موفقیت فعال شد.";
            }
            else
            {
                TempData[ErrorMessage] = "عملیات با خطا مواجه شد، لطفا مجددا تلاش کنید";
            }
            return RedirectToAction("FilterProducts", "Product", new { area = "Administration" });
        }

        [HttpGet("DeActivateProduct/{productId}")]
        public async Task<IActionResult> DeActivateProduct(long productId)
        {
            var result = await _productService.DeActivateProduct(productId);
            if (result)
            {
                TempData[SuccessMessage] = "محصول موردنظر با موفقیت غیرفعال شد.";
            }
            else
            {
                TempData[ErrorMessage] = "عملیات با خطا مواجه شد، لطفا مجددا تلاش کنید";
            }
            return RedirectToAction("FilterProducts", "Product", new { area = "Administration" });
        }

        #endregion

        #endregion


    }
}
