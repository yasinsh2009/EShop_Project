namespace EcommerApp.Domain.DTOs.Product.ProductDiscount
{
    public class EditProductDiscountDto : CreateProductDiscountDto
    {
        public long Id { get; set; }
        public string ProductTitle { get; set; }
    }

    public enum EditProductDiscountResult
    {
        Success,
        ProductNotFound,
        Error
    }
}
