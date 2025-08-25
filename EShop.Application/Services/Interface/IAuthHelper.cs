using EShop.Domain.DTOs.Account.Auth;

namespace EShop.Application.Services.Interface
{
    public interface IAuthHelper
    {
        string CurrentAccountRole();
        AuthViewModel CurrentAccountInfo();
        long CurrentAccountId();
        //Task<EditUserDTO> GetUserInfo(long id);
    }
}
