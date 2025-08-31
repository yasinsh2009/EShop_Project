namespace EShop.Domain.DTOs.Site.Silder
{
    public class EditSliderDto : CreateSliderDto
    {
        public long Id { get; set; }
    }

    public enum EditSliderResult
    {
        Success, 
        Error,
        NotFound,
    }
}
