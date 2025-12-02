using EcommerApp.Application.Utilities;
using EcommerApp.Domain.Entities.Site;

namespace EcommerApp.Application.EntitiesExtentions
{
    public static class BannersExtensions
    {
        public static string GetSiteMainImageAddress(this SiteBanner banner)
        {
            return PathExtension.SiteBannerOrigin + banner.ImageName;
        }
    }
}
