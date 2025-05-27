using EShop.Application.Services.Interface;
using EShop.Domain.DTOs.Site;
using EShop.Domain.Entities.Site;
using EShop.Domain.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Services.Implementation;

public class SiteService : ISiteService
{
    #region Constructor

    private readonly IGenericRepository<SiteSetting> _siteSettingRepository;

    public SiteService(IGenericRepository<SiteSetting> siteSettingRepository)
    {
        _siteSettingRepository = siteSettingRepository;
    }

    #endregion

    #region Services

    #region Get Default Site Setting

    public async Task<SiteSettingDto> GetDefaultSiteSetting()
    {
        var siteSetting = await _siteSettingRepository
            .GetQuery()
            .AsQueryable()
            .Select(x => new SiteSettingDto
            {
                SiteName = x.SiteName,
                FooterText = x.FooterText,
                Email = x.Email,
                Phone = x.Phone,
                Mobile = x.Mobile,
                CopyRight = x.CopyRight,
                Address = x.Address,
                MapScript = x.MapScript,
                IsDefault = x.IsDefault
            }).FirstOrDefaultAsync(x => x.IsDefault);

        return siteSetting ?? new SiteSettingDto();
    }

    #endregion

    #endregion

    #region Dispose

    public async ValueTask DisposeAsync()
    {
        // TODO release managed resources here
    }

    #endregion
}