using EcommerApp.Application.Utilities;
using EcommerApp.Domain.Entities.Site;

namespace EcommerApp.Application.EntitiesExtentions
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
