using EShop.Application.Services.Interface;
using EShop.Application.Utilities;
using EShop.Domain.DTOs.Account.User;
using EShop.Domain.Entities.Account.Role;
using EShop.Domain.Entities.Account.User;
using EShop.Domain.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EShop.Application.Services.Implementation;

public class UserService : IUserService
{
    #region Constructor

    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly ISmsService _smsService;

    public UserService(IGenericRepository<User> userRepository, IGenericRepository<Role> roleRepository,
        ISmsService smsService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _smsService = smsService;
    }

    #endregion

    #region Services

    #region Account

    #region User Validation

    public async Task<UserValidationResult> IsUserValidate(UserValidationDto validate)
    {
        var user = await _userRepository
            .GetQuery()
            .AsQueryable()
            .SingleOrDefaultAsync(x => x.Mobile == validate.Mobile);

        if (user != null)
        {
            if (user.IsMobileActive)
            {
                return UserValidationResult.ExistAndActive;
            }

            return UserValidationResult.ExistAndNotActive;
        }

        return UserValidationResult.NotExists;
    }

    #endregion

    #region User Register

    public async Task<UserRegisterResult> UserRegister(UserRegisterDto register)
    {
        try
        {
            if (!await IsUserExistByMobile(register.Mobile))
            {
                var salt = PasswordManager.GenerateSalt(16);

                var newUser = new User
                {
                    Mobile = register.Mobile,
                    MobileActiveCode = new Random().Next(100000, 999999).ToString(),
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Salt = salt,
                    Password = PasswordManager.HashPassword(register.Password, salt),
                    RoleId = 2,
                };

                await _smsService.SendVerificationSms(register.Mobile, newUser.MobileActiveCode);

                await _userRepository.AddEntity(newUser);
                await _userRepository.SaveChanges();

                return UserRegisterResult.Success;
            }

            return UserRegisterResult.MobileExists;
        }
        catch (Exception)
        {
            return UserRegisterResult.Error;
        }
    }

    public async Task<bool> IsUserExistByMobile(string mobile)
    {
        var user = await _userRepository
            .GetQuery()
            .AsQueryable()
            .SingleOrDefaultAsync(x => x.Mobile == mobile);

        if (user != null)
        {
            return true;
        }

        return false;
    }

    #endregion

    #region User Login

    public async Task<UserLoginResult> UserLogin(UserLoginDto login)
    {
        try
        {
            var user = await _userRepository
                .GetQuery()
                .SingleOrDefaultAsync(x => x.Mobile == login.Mobile);

            if (!await IsUserExistByMobile(login.Mobile))
            {
                if (!user.IsMobileActive)
                {
                    return UserLoginResult.MobileNotActivated;
                }

                if (user.Password != PasswordManager.HashPassword(login.Password, login.Salt))
                {
                    return UserLoginResult.WrongPassword;
                }

                await _smsService.SendVerificationSms(login.Mobile, user.MobileActiveCode);

                return UserLoginResult.Success;
            }

            return UserLoginResult.UserNotFound;
        }
        catch (Exception e)
        {
            return UserLoginResult.Error;
        }
    }

    public async Task<User> GetUserByMobile(string mobile)
    {
        var user = await _userRepository
            .GetQuery()
            .SingleOrDefaultAsync(x => x.Mobile == mobile);

        return user;
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
            var otpCode =
                $"{activateMobile.digit1}{activateMobile.digit2}{activateMobile.digit3}{activateMobile.digit4}{activateMobile.digit5}{activateMobile.digit6}";

            if (user.MobileActiveCode == otpCode)
            {
                user.IsMobileActive = true;
                user.MobileActiveCode = new Random().Next(100000, 9999999).ToString();
                await _userRepository.SaveChanges();

                return true;
            }
        }

        return false;
    }

    #endregion

    #region Restore User Password

    public async Task<ForgotPasswordResult> RestoreUserPassword(ForgotPasswordDto forgot)
    {
        try
        {
            if (await IsUserExistByMobile(forgot.Mobile))
            {
                var user = await _userRepository
                    .GetQuery()
                    .AsQueryable()
                    .SingleOrDefaultAsync(x => x.Mobile == forgot.Mobile);

                if (user == null)
                {
                    return ForgotPasswordResult.UserNotFound;
                }

                var newPassword = PasswordManager.CreateRandomPassword();
                user.Password = PasswordManager.HashPassword(newPassword, user.Salt);

                _userRepository.EditEntity(user);

                await _smsService.SendRestorePasswordSms(forgot.Mobile, newPassword);

                await _userRepository.SaveChanges();

                return ForgotPasswordResult.Success;
            }

            return ForgotPasswordResult.UserNotFound;
        }
        catch (Exception e)
        {
            return ForgotPasswordResult.Error;
        }
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