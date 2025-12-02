using EcommerApp.Application.Services.Interface;
using EcommerApp.Application.Utilities;
using EcommerApp.Domain.Entities.Account.Role;
using EcommerApp.Domain.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EcommerApp.Application.Services.Implementation;

public class RoleService : IRoleService
{
    #region Constructor

    private readonly IGenericRepository<Role> _roleRepository;

    public RoleService(IGenericRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    #endregion

    public async Task<string> GetRoleNameByRoleId(long roleId)
    {
        try
        {
            var role = await _roleRepository
           .GetQuery()
           .SingleOrDefaultAsync(x => x.Id == roleId && !x.IsPublished);

            if (role != null)
            {
                return role.RoleName;
            }

            return null;
        }
        catch (Exception ex)
        {
            Logger.ShowError(ex);

            return null;
        }
    }

    #region Dispose

    public async ValueTask DisposeAsync() { }

    #endregion
}