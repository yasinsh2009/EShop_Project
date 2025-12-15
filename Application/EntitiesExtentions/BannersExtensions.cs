using ECommerceApp.Application.Utilities;
using ECommerceApp.Domain.Entities.Site;

namespace ECommerceApp.Application.EntitiesExtentions
{
    public static class BannersExtensions
    {
        public static string GetSiteMainImageAddress(this SiteBanner banner)
        {
            return PathExtension.SiteBannerOrigin + banner.ImageName;
        }
    }
}
