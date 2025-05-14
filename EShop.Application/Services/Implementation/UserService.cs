using EShop.Application.Services.Interface;
using EShop.Application.Utilities;
using EShop.Domain.DTOs.Account.User;
using EShop.Domain.Entities.Account.Role;
using EShop.Domain.Entities.Account.User;
using EShop.Domain.Repository.Interface;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Utilities;

namespace EShop.Application.Services.Implementation;

public class UserService : IUserService
{
    #region Constructor

    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<Role> _roleRepository;

    public UserService(IGenericRepository<User> userRepository, IGenericRepository<Role> roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    #endregion

    #region Services

    #region Account

    #region User Register

    public async Task<UserRegisterResult> UserRegister(UserRegisterDTO register)
    {
        try
        {
            if (!await IsUserExistByMobile(register.Mobile))
            {
                var salt = PasswordManager.GenerateSalt(16);

                var newUser = new User
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Mobile = register.Mobile,
                    MobileActiveCode = new Random().Next(100000, 999999).ToString(),
                    Email = register.Email ?? "info@gmail.com",
                    EmailActiveCode = Guid.NewGuid().ToString("N"),
                    Password = PasswordManager.HashPassword(register.Password, salt),
                    Salt = salt,
                    AvatarPath = register.AvatarPath != null
                        ? await ImageCreator.CreateImage(register.AvatarPath, "user") : null,
                    RoleId = 2
                };

                if (newUser.IsMobileActive)
                {
                    //todo : Send Message to mobile

                    await _userRepository.AddEntity(newUser);
                    await _userRepository.SaveChanges();

                    return UserRegisterResult.Success;
                }

                return UserRegisterResult.Error;
            }
            else
            {
                return UserRegisterResult.MobileExists;
            }
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

    public async Task<UserLoginResult> UserLogin(UserLoginDTO login)
    {
        var user = await _userRepository
            .GetQuery()
            .SingleOrDefaultAsync
            (x => x.Mobile == login.Mobile &&
                  x.Password == PasswordManager.HashPassword(login.Password, login.Salt));

        if (user == null)
        {
            return UserLoginResult.NotFound;
        }

        if (user.Mobile != login.Mobile)
        {
            return UserLoginResult.WrongInformation;
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