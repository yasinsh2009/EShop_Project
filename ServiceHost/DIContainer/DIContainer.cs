using EShop.Application.Services.Implementation;
using EShop.Application.Services.Interface;
using EShop.Domain.Repository.Implementation;
using EShop.Domain.Repository.Interface;

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

        #endregion

        #region Common Services

        services.AddHttpContextAccessor();

        #endregion
    }
}