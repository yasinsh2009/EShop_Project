namespace ECommerceApp.Application.Services.Interface;

public interface IRoleService : IAsyncDisposable
{
    Task<string> GetRoleNameByRoleId(long roleId);
}