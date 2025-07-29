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
    private readonly IGenericRepository<AboutUs> _aboutUsRepository;
    private readonly IGenericRepository<Feature> _featureRepository;
    private readonly IGenericRepository<Question> _questionRepository;

    public SiteService(IGenericRepository<SiteSetting> siteSettingRepository, IGenericRepository<AboutUs> aboutUsRepository, IGenericRepository<Feature> featureRepository, IGenericRepository<Question> questionRepository)
    {
        _siteSettingRepository = siteSettingRepository;
        _aboutUsRepository = aboutUsRepository;
        _featureRepository = featureRepository;
        _questionRepository = questionRepository;
    }

    #endregion

    #region Services

    #region SiteSetting

    public async Task<SiteSettingDto> GetDefaultSiteSetting()
    {
        var siteSetting = await _siteSettingRepository
            .GetQuery()
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

    #region AboutUs

    public async Task<List<AboutUsDto>> GetAboutUs()
    {
        return await _aboutUsRepository
            .GetQuery()
            .Where(x => !x.IsDelete)
            .Select(x => new AboutUsDto
            {
                Id = x.Id,
                HeaderTitle = x.HeaderTitle,
                Description = x.Description
            }).OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    #region Features

    public async Task<List<FeatureDto>> GetAllFeatures()
    {
        return await _featureRepository
            .GetQuery()
            .Where(x => !x.IsDelete)
            .Select(x => new FeatureDto
            {
                Id = x.Id,
                FeatureTitle = x.FeatureTitle,
                Image = x.Image,
            }).OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    #endregion

    #region Questions

    public async Task<List<QuestionDto>> GetAllQuestions()
    {
        return await _questionRepository
            .GetQuery()
            .Where(x => !x.IsDelete)
            .Select(x => new QuestionDto
            {
                Id = x.Id,
                QuestionTitle = x.QuestionTitle,
                Answer = x.Answer
            }).OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    #endregion

    #endregion

    #endregion

    #region Dispose

    public async ValueTask DisposeAsync() { }

    #endregion
}