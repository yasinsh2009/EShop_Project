namespace EShop.Domain.DTOs.Site.Slider
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
