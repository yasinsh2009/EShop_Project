using EShop.Domain.DTOs.Account.User;

namespace EShop.Application.Services.Interface;

public interface IUserService : IAsyncDisposable
{
    Task<RegisterUserResult> RegisterUser(RegisterUserDTO register);
    Task<bool> IsUserExistByMobile(string mobile);
}