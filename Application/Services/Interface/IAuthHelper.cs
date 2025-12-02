using EcommerApp.Domain.DTOs.Account.Auth;

namespace EcommerApp.Application.Services.Interface
{
    public interface IAuthHelper
    {
        string CurrentAccountRole();
        AuthViewModel CurrentAccountInfo();
        long CurrentAccountId();
        //Task<EditUserDTO> GetUserInfo(long id);
    }
}
