namespace EcommerApp.Domain.DTOs.Product.ProductColor
{
    public class EditProductColorDto : CreateProductColorDto
    {
        public long Id { get; set; }
    }

    public enum EditProductColorResult
    {
        Error,
        ColorNotFound,
        DuplicateColor,
        Success,
    }
}
