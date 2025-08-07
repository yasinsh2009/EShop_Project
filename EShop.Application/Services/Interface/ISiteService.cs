using EShop.Domain.DTOs.Site;
using EShop.Domain.Entities.Site;

namespace EShop.Application.Services.Interface;

public interface ISiteService : IAsyncDisposable
{
    #region SiteSetting

    Task<SiteSettingDto> GetDefaultSiteSetting();
    Task<EditSiteSettingDto> GetSiteSettingForEdit(long settingId);
    Task<EditSiteSettingResult> EditSiteSetting(EditSiteSettingDto newSetting, string userName);

    #endregion

    #region AboutUs

    Task<List<AboutUsDto>> GetAboutUs();

    #region Features

    Task<List<FeatureDto>> GetAllFeatures();

    #endregion

    #region Questions

    Task<List<QuestionDto>> GetAllQuestions();

    #endregion

    #endregion
}