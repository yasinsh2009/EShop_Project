using ECommerceApp.Application.Utilities;
using ECommerceApp.Domain.Entities.Site;

namespace ECommerceApp.Application.EntitiesExtentions
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
