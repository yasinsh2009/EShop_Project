using EShop.Domain.DTOs.Account.User;
using EShop.Domain.Entities.Account.User;

namespace EShop.Application.Services.Interface;

public interface IUserService : IAsyncDisposable
{
    #region Account

    #region User register

    Task<UserRegisterResult> UserRegister(UserRegisterDto register);
    Task<bool> IsUserExistByMobile(string mobile);

    #endregion

    #region User Login

    Task<UserLoginResult> UserLogin(UserLoginDto login);
    Task<User> GetUserByMobile(string mobile);

    #endregion

    #endregion
}