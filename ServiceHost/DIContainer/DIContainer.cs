using System.Text.Encodings.Web;
using System.Text.Unicode;
using EShop.Application.Services.Implementation;
using EShop.Application.Services.Interface;
using EShop.Domain.Repository.Implementation;
using EShop.Domain.Repository.Interface;
using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;

namespace ServiceHost.DIContainer;

public static class DIContainer
{
    public static void RegisterService(this IServiceCollection services)
    {
        #region Repositories

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        #endregion

        #region General Services

        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ISiteService, SiteService>();
        services.AddTransient<ISmsService, SmsService>();
        services.AddTransient<IContactService, ContactService>();
        services.AddTransient<IRoleService, RoleService>();

        #endregion

        #region Common Services

        services.AddHttpContextAccessor();
        services.AddSingleton<HtmlEncoder>(
            HtmlEncoder.Create(allowedRanges: [UnicodeRanges.BasicLatin, UnicodeRanges.Arabic])
        );
        services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();

        #endregion
    }
}