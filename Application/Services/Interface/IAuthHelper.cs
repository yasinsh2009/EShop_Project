using ECommerceApp.Domain.DTOs.Account.Auth;

namespace ECommerceApp.Application.Services.Interface
{
    public interface IAuthHelper
    {
        string CurrentAccountRole();
        AuthViewModel CurrentAccountInfo();
        long CurrentAccountId();
        //Task<EditUserDTO> GetUserInfo(long id);
    }
}
