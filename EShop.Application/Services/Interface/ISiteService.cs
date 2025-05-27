using EShop.Domain.DTOs.Site;
using EShop.Domain.Entities.Site;

namespace EShop.Application.Services.Interface;

public interface ISiteService : IAsyncDisposable
{
    Task<SiteSettingDto> GetDefaultSiteSetting();
}