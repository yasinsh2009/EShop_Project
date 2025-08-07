namespace EShop.Domain.DTOs.Site
{
    public class EditAboutUsDto : CreateAboutUsDto
    {
        public long Id { get; set; }
    }

    public enum EditAboutUsResult
    {
        Success, 
        NotFound,
        Error

    }
}
