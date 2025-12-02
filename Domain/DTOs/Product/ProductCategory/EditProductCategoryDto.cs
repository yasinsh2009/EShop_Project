namespace EcommerApp.Domain.DTOs.Product.ProductCategory
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
