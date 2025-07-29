﻿using EShop.Application.Services.Interface;
using EShop.Domain.Entities.Account.Role;
using EShop.Domain.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Services.Implementation;

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
        var role = await _roleRepository
            .GetQuery()
            .SingleOrDefaultAsync(x => x.Id == roleId && !x.IsDelete);

        if (role != null)
        {
            return role.RoleName;
        }

        return null;
    }

    #region Dispose

    public async ValueTask DisposeAsync() { }

    #endregion
}