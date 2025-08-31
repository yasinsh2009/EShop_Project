using EShop.Domain.DTOs.Site.Slider;
using EShop.Domain.Entities.Site;
using Microsoft.AspNetCore.Http;

namespace EShop.Application.Services.Interface
{
    public interface ISiteImagesService : IAsyncDisposable
    {
        #region Slider

        Task<List<Slider>> GetAllSlides();
        Task<List<Slider>> GetAllActiveSlides();
        Task<CreateSliderResult> CreateSlide(CreateSliderDto slide, IFormFile slideImage, IFormFile slideMobileImage);
        Task<EditSliderDto> GetSlideForEdit(long id);
        Task<EditSliderResult> EditSlide(EditSliderDto silde, IFormFile slideImage, IFormFile slideMobileImage, string editorName);
        Task<bool> ActivateSlide(long id);
        Task<bool> DeActivateSlide(long id);

        #endregion
    }
}
