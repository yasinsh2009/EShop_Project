using EShop.Application.Utilities;
using EShop.Domain.Entities.Site;

namespace EShop.Application.EntitiesExtentions
{
    public static class SliderExtensions
    {
        public static string GetSliderImageAddress(this Slider slider)
        {
            return PathExtension.SliderOrigin + slider.ImageName;
        }

        public static string GetMobileSliderImageAddress(this Slider slider)
        {
            return PathExtension.MobileSliderOrigin + slider.MobileImageName;
        }

    }
}
