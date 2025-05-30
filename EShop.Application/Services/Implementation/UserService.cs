﻿using EShop.Application.Services.Interface;
using EShop.Application.Utilities;
using EShop.Domain.DTOs.Account.User;
using EShop.Domain.Entities.Account.Role;
using EShop.Domain.Entities.Account.User;
using EShop.Domain.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Services.Implementation;

public class UserService : IUserService
{
    #region Constructor

    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly ISmsService _smsService;

    public UserService(IGenericRepository<User> userRepository, IGenericRepository<Role> roleRepository, ISmsService smsService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _smsService = smsService;
    }

    #endregion

    #region Services

    #region Account

    #region User Register

    public async Task<UserRegisterResult> UserRegister(UserRegisterDto register)
    {
        try
        {
            var newUser = new User
            {
                Mobile = register.Mobile,
                MobileActiveCode = new Random().Next(1000000, 9999999).ToString(),
                RoleId = 2
            };

            await _smsService.SendVerificationSms(register.Mobile, newUser.MobileActiveCode);

            if (newUser.IsMobileActive)
            {
                await _userRepository.AddEntity(newUser);
                await _userRepository.SaveChanges();

                return UserRegisterResult.Success;
            }

            return UserRegisterResult.Error;
        }
        catch (Exception e)
        {
            return UserRegisterResult.Error;
        }

    }

    public async Task<bool> IsUserExistByMobile(string mobile)
    {
        return await _userRepository
            .GetQuery()
            .AsQueryable()
            .AnyAsync(x => x.Mobile == mobile);
    }

    #endregion

    #region User Login

    public async Task<UserLoginResult> UserLogin(UserLoginDto login)
    {
        var user = await _userRepository
            .GetQuery()
            .SingleOrDefaultAsync
            (x => x.Password == PasswordManager.HashPassword(login.Password, login.Salt));

        if (user == null)
        {
            return UserLoginResult.NotFound;
        }

        if (user.Password != PasswordManager.HashPassword(login.Password, login.Salt))
        {
            return UserLoginResult.WrongInformation;
        }

        if (!user.IsMobileActive)
        {
            return UserLoginResult.MobileNotActivated;
        }

        return UserLoginResult.Success;
    }

    public async Task<User> GetUserByMobile(string mobile)
    {
        return await _userRepository
            .GetQuery()
            .SingleOrDefaultAsync(x => x.Mobile == mobile);
    }

    #endregion

    #region Activation Mobile

    public async Task<bool> ActivateMobile(ActivateMobileDto activateMobile)
    {
        var user = await _userRepository
            .GetQuery()
            .SingleOrDefaultAsync(x => x.Mobile == activateMobile.Mobile);

        if (user != null)
        {
            if (user.MobileActiveCode == activateMobile.MobileActivationCode)
            {
                user.IsMobileActive = true;
                user.MobileActiveCode = new Random().Next(10000, 999999).ToString();
                await _userRepository.SaveChanges();

                return true;
            }
        }

        return false;
    }

    #endregion

    #endregion

    #endregion

    #region Dispose

    public async ValueTask DisposeAsync()
    {
        if (_userRepository != null)
        {
            await _userRepository.DisposeAsync();
        }

        if (_roleRepository != null)
        {
            await _roleRepository.DisposeAsync();
        }
    }

    #endregion
}