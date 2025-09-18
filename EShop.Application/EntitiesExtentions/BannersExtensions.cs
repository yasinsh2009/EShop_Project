using EShop.Application.Utilities;
using EShop.Domain.Entities.Site;

namespace EShop.Application.EntitiesExtentions
{
    public static class BannersExtensions
    {
        public static string GetSiteMainImageAddress(this SiteBanner banner)
        {
            return PathExtension.SiteBannerOrigin + banner.ImageName;
        }
    }
}
