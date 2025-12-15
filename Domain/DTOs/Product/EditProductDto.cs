namespace ECommerceApp.Domain.DTOs.Product
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
