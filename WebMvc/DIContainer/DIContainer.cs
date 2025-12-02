using EcommerApp.Application.Services.Implementation;
using EcommerApp.Application.Services.Interface;
using EcommerApp.Domain.Repository.Implementation;
using EcommerApp.Domain.Repository.Interface;
using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace EcommerApp.WebMvc.DIContainer;

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
        services.AddTransient<IAuthHelper, AuthHelper>();
        services.AddTransient<ISiteImagesService, SiteImagesService>();
        services.AddTransient<IProductService, ProductService>();

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