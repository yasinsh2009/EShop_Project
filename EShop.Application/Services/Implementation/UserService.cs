using EShop.Application.Extensions;
using EShop.Application.Services.Interface;
using EShop.Application.Utilities;
using EShop.Domain.DTOs.Account.User;
using EShop.Domain.Entities.Account.Role;
using EShop.Domain.Entities.Account.User;
using EShop.Domain.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Services.Implementation;

public class UserService : IUserService
{
    #region Constructor

    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly ISmsService _smsService;
    private readonly IRoleService _roleService;

    public UserService(IGenericRepository<User> userRepository, IGenericRepository<Role> roleRepository,
        ISmsService smsService, IRoleService roleService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _smsService = smsService;
        _roleService = roleService;
    }

    #endregion

    #region Services

    #region Account

    #region User Validation
    public async Task<UserValidationResult> IsUserValidate(UserValidationDto validate)
    {
        var user = await _userRepository
            .GetQuery()
            .SingleOrDefaultAsync(x => x.Mobile == validate.Mobile);

        if (user != null)
        {
            if (user.IsMobileActive)
            {
                return UserValidationResult.ExistAndActive;
            }

            await _smsService.SendVerificationSms(validate.Mobile, user.MobileActiveCode);


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
                    RoleId = 2
                };

                //await _smsService.SendVerificationSms(register.Mobile, newUser.MobileActiveCode);

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

            if (await IsUserExistByMobile(login.Mobile))
            {
                if (!user.IsMobileActive)
                {
                    await _smsService.SendVerificationSms(login.Mobile, user.MobileActiveCode);

                    return UserLoginResult.MobileNotActivated;
                }

                if (user.Password == PasswordManager.HashPassword(login.Password, user.Salt))
                {
                    return UserLoginResult.Success;
                }

                return UserLoginResult.WrongPassword;
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
            if (activateMobile.MobileActiveCode == user.MobileActiveCode)
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

    #region User Avatar

    public async Task<string?> GetUserAvatar(long userId)
    {
        var user = await _userRepository
            .GetQuery()
            .SingleOrDefaultAsync(x => x.Id == userId);

        if (user != null)
        {
            return user.AvatarPath;
        }

        return null;
    }

    #endregion

    #region User Role

    public async Task<string> GetUserRole(long userId)
    {
        var user = await _userRepository
            .GetQuery()
            .SingleOrDefaultAsync(x => x.Id == userId);

        if (user != null)
        {
            return await _roleService.GetRoleNameByRoleId(user.RoleId);
        }

        return null;
    }

    #endregion

    #region Get User Profile

    public async Task<ReadUserProfileDto> GetUserProfile(long userId)
    {
        var user = await _userRepository
            .GetQuery()
            .SingleOrDefaultAsync(x => x.Id == userId && !x.IsDelete);

        if (user != null)
        {
            return new ReadUserProfileDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Mobile = user.Mobile,
                Email = user.Email,
                AvatarPath = user.AvatarPath,
                RegisterDate = user.CreateDate.ToStringShamsiDate()
            };
        }

        return null;
    }

    #endregion

    #region Update User Profile

    public async Task<UpdateUserProfileDto> GetUserProfileForEdit(long userId)
    {
        var user = await _userRepository.GetEntityById(userId);

        if (user != null)
        {
            return new UpdateUserProfileDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Mobile = user.Mobile,
                Email = user.Email,
            };
        }

        return null;
    }
    public async Task<UpdateUserProfileResult> EditUserProfile(UpdateUserProfileDto profile, long userId, IFormFile? avatar)
    {
        try
        {
            var user = await _userRepository.GetEntityById(userId);

            if (user == null)
            {
                return UpdateUserProfileResult.NotFound;
            }

            user.Edit(profile.FirstName, profile.LastName, profile.Email, profile.Mobile);

            if (avatar != null && avatar.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(avatar.FileName);

                avatar.AddImageToServer(imageName, PathExtension.UserAvatarOriginServer,
                    100, 100, PathExtension.UserAvatarThumbServer, user.AvatarPath);

                user.AvatarPath = imageName;
            }

            _userRepository.EditEntity(user);
            await _userRepository.SaveChanges();

            return UpdateUserProfileResult.Success;
        }
        catch (Exception)
        {
            return UpdateUserProfileResult.Error;
        }
    }

    #endregion

    #region Change User Password

    public async Task<ChangeUserPasswordResult> ChangeUserPassword(ChangeUserPasswordDto changePassword, long userId)
    {
        var user = await _userRepository
            .GetQuery()
            .SingleOrDefaultAsync(x => x.Id == userId && !x.IsBlocked && !x.IsDelete);

        if (user == null)
        {
            return ChangeUserPasswordResult.NotFound;
        }

        if (user.Password != PasswordManager.HashPassword(changePassword.CurrentPassword, user.Salt))
        {
            return ChangeUserPasswordResult.WrongCurrentPassword;
        }

        if (changePassword.NewPassword == user.Password)
        {
            return ChangeUserPasswordResult.CurrentPasswordSameAsNew;
        }

        user.Password = PasswordManager.HashPassword(changePassword.NewPassword, user.Salt);

        _userRepository.EditEntity(user);
        await _userRepository.SaveChanges();

        return ChangeUserPasswordResult.Success;
    }
    public async Task<string[]> GetUserFullNameById(long userId)
    {
        var user = await _userRepository
            .GetQuery()
            .SingleOrDefaultAsync(x => x.Id == userId && !x.IsBlocked && !x.IsDelete);

        if (user != null)
        {
            var userFullName = new string[]
            {
                user.FirstName,
                user.LastName
            };
            return userFullName;
        }

        return null;
    }
    public async Task<string> GetUserMobileById(long userId)
    {
        var user = await _userRepository
            .GetQuery()
            .AsQueryable()
            .SingleOrDefaultAsync(x => x.Id == userId && !x.IsBlocked && !x.IsDelete);

        if (user != null)
        {
            return user.Mobile;
        }

        return null;
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

