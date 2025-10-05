using EShop.Application.Services.Interface;
using EShop.Domain.DTOs.Product;
using EShop.Domain.DTOs.Product.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.PresentationExtensions;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class ProductController : AdminBaseController
    {
        #region Ctor

        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public ProductController(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
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
            var productCategories = await _productService.GetAllActiveProductCategories();
            return View(productCategories);
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
            var productCategories = await _productService.GetAllActiveProductCategories();
            return View(newProduct);
        }

        #endregion

        #region Activate / DeActivate Product

        [HttpGet("ActivateProduct/{id}")]
        public async Task<IActionResult> ActivateProduct(long id)
        {
            var result = await _productService.ActivateProduct(id);
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

        [HttpGet("DeActivateProduct/{id}")]
        public async Task<IActionResult> DeActivateProduct(long id)
        {
            var result = await _productService.DeActivateProduct(id);
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

        #region Product Category

        #region Filter Product Categories

        [HttpGet("FilterProductCategories")]
        public async Task<IActionResult> FilterProductCategories(FilterProductCategoriesDto productCategory)
        {
            var productCategories = await _productService.FilterProductCategories(productCategory);
            return View(productCategories);
        }

        #endregion

        #region Create Product Category

        [HttpGet("CreateProductCategory")]
        public async Task<IActionResult> CreateProductCategory()
        {
            ViewBag.Categories = await _productService.GetAllActiveProductCategories();
            return View();
        }


        [HttpPost("CreateProductCategory"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProductCategory(CreateProductCategoryDto newProductCategory)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProductCategory(newProductCategory);

                switch (result)
                {
                    case CreateProductCategoryResult.Success:
                        TempData[SuccessMessage] = "دسته بندی جدید محصول با موفقیت ایجاد شد.";
                        RedirectToAction("FilterProductCategories", "Product", new { area = "Administration" });
                        break;
                    case CreateProductCategoryResult.Error:
                        TempData[ErrorMessage] = "فرایند ایجاد دسته بندی جدید محصول با خطا مواجه شد، لطفا بعدا امتحان کنید.";
                        break;
                    case CreateProductCategoryResult.ImageErrorType:
                        TempData[ErrorMessage] = "لطفا تصویری با فرمت مناسب بارگداری کنید.";
                        break;
                }
            }

            ViewBag.Categories = await _productService.GetAllActiveProductCategories();
            return View(newProductCategory);
        }

        #endregion

        #region Edit Product Category

        [HttpGet("EditProductCategory/{id}")]
        public async Task<IActionResult> EditProductCategory(long id)
        {
            var productCategory = await _productService.GetProductCategoryForEdit(id);
            return View(productCategory);
        }

        [HttpPost("EditProductCategory/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProductCategory(EditProductCategoryDto productCategory)
        {
            if (ModelState.IsValid)
            {
                var editorName = await _userService.GetUserFullNameById(User.GetUserId());
                var result = await _productService.EditProductCategory(productCategory, editorName);

                switch (result)
                {
                    case EditProductCategoryResult.Success:
                        TempData[SuccessMessage] = "دسته بندی محصول موردنظر با ویرایش ایجاد شد.";
                        return RedirectToAction("FilterProductCategories", "Product", new { area = "Administration" });
                    case EditProductCategoryResult.NotFound:
                        TempData[WarningMessage] = "دسته بندی محصول موردنظر یافت نشد";
                        break;
                    case EditProductCategoryResult.Error:
                        TempData[ErrorMessage] = "در فرایند ویرایش دسته بندی محصول موردنظر خطایی رخ داد، لطفا بعدا تلاش کنید.";
                        break;
                    case EditProductCategoryResult.ImageErrorType:
                        TempData[ErrorMessage] = "لطفا تصویری با فرمت مناسب بارگداری کنید.";
                        break;
                }
            }
            return View(productCategory);
        }

        #endregion

        #region Activate / DeActivate Product Category / SubCategory

        [HttpGet("ActivateProductCategory/{id}")]
        public async Task<IActionResult> ActivateProductCategory(long id)
        {
            var result = await _productService.ActivateProductCategory(id);
            if (result)
            {
                TempData[SuccessMessage] = "دسته بندی محصول موردنظر با موفقیت فعال شد.";
            }
            else
            {
                TempData[ErrorMessage] = "عملیات با خطا مواجه شد، لطفا مجددا تلاش کنید";
            }
            return RedirectToAction("FilterProductCategories", "Product", new { area = "Administration" });
        }

        [HttpGet("DeActivateProductCategory/{id}")]
        public async Task<IActionResult> DeActivateProductCategory(long id)
        {
            var result = await _productService.DeActivateProductCategory(id);
            if (result)
            {
                TempData[SuccessMessage] = "دسته بندی محصول موردنظر با موفقیت غیرفعال شد.";
            }
            else
            {
                TempData[ErrorMessage] = "عملیات با خطا مواجه شد، لطفا مجددا تلاش کنید";
            }
            return RedirectToAction("FilterProductCategories", "Product", new { area = "Administration" });
        }

        #endregion

        #endregion

        #region Product SubCategory

        #region Filter Product SubCategory

        [HttpGet("FilterProductSubCategories/{parentId}/{categoryName}")]
        public async Task<IActionResult> FilterProductSubCategories(string parentId, string categoryName, FilterProductCategoriesDto productSubCategory)
        {
            var productSubCategories = await _productService.FilterProductCategories(productSubCategory);
            return View(productSubCategories);
        }

        #endregion

        #region Create Product SubCategory

        [HttpGet("CreateProductSubCategory/{parentId}/{categoryName}")]
        public IActionResult CreateProductSubCategory(long parentId, string categoryName)
        {
            return View();
        }


        [HttpPost("CreateProductSubCategory/{parentId}/{categoryName}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProductSubCategory(CreateProductCategoryDto newProductSubCategory)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProductCategory(newProductSubCategory);

                switch (result)
                {
                    case CreateProductCategoryResult.Success:
                        TempData[SuccessMessage] = "دسته بندی جدید محصول با موفقیت ایجاد شد.";
                        return RedirectToAction("FilterProductSubCategories", "Product", new { area = "Administration", parentId = newProductSubCategory.ParentId, categoryName = newProductSubCategory.ParentName });
                    case CreateProductCategoryResult.Error:
                        TempData[ErrorMessage] = "فرایند ایجاد دسته بندی جدید محصول با خطا مواجه شد، لطفا بعدا امتحان کنید.";
                        break;
                    case CreateProductCategoryResult.ImageErrorType:
                        TempData[ErrorMessage] = "لطفا تصویری با فرمت مناسب بارگداری کنید.";
                        break;
                }
            }
            return View(newProductSubCategory);
        }

        #endregion

        #region Edit Product SubCategory

        [HttpGet("EditProductSubCategory/{id}")]
        public async Task<IActionResult> EditProductSubCategory(long id)
        {
            var productSubCategory = await _productService.GetProductCategoryForEdit(id);
            return View(productSubCategory);
        }

        [HttpPost("EditProductSubCategory/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProductSubCategory(EditProductCategoryDto productSubCategory)
        {
            if (ModelState.IsValid)
            {
                var editorName = await _userService.GetUserFullNameById(User.GetUserId());
                var result = await _productService.EditProductCategory(productSubCategory, editorName);

                switch (result)
                {
                    case EditProductCategoryResult.Success:
                        TempData[SuccessMessage] = "دسته بندی محصول موردنظر با ویرایش ایجاد شد.";
                        return RedirectToAction("FilterProductSubCategories", "Product", new { area = "Administration", parentId = productSubCategory.ParentId, categoryName = productSubCategory.Title });
                    case EditProductCategoryResult.NotFound:
                        TempData[WarningMessage] = "دسته بندی محصول موردنظر یافت نشد";
                        break;
                    case EditProductCategoryResult.Error:
                        TempData[ErrorMessage] = "در فرایند ویرایش دسته بندی محصول موردنظر خطایی رخ داد، لطفا بعدا تلاش کنید.";
                        break;
                    case EditProductCategoryResult.ImageErrorType:
                        TempData[ErrorMessage] = "لطفا تصویری با فرمت مناسب بارگداری کنید.";
                        break;
                }
            }
            return View(productSubCategory);
        }

        #endregion

        #endregion
    }
}
