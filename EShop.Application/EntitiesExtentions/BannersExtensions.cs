using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Application.Utilities;
using EShop.Domain.Entities.Site;

namespace EShop.Application.EntitiesExtensions
{
    public static class BannersExtensions
    {
        public static string GetSiteMainImageAddress(this SiteBanner banner)
        {
            return PathExtension.SiteBannerOrigin + banner.ImageName;
        }
    }
}
