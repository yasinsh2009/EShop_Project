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

    public async Task<RegisterUserResult> RegisterUser(RegisterUserDTO register)
    {
        try
        {
            if (!await IsUserExistByMobile(register.Mobile))
            {
                var newUser = new User
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Mobile = register.Mobile,
                    MobileActiveCode = new Random().Next(100000, 999999).ToString(),
                    Email = register.Email != null ? register.Email : "info@gmail.com",
                    EmailActiveCode = Guid.NewGuid().ToString("N"),
                    Password = Sha256Example.ComputeSHA256Hash(register.Password),
                    AvatarPath = register.AvatarPath != null
                        ? await ImageCreator.CreateImage(register.AvatarPath, "user")
                        : null,
                    RoleId = 2
                };

                await _userRepository.AddEntity(newUser);
                await _userRepository.SaveChanges();

                //todo : Send Message to mobile

                return RegisterUserResult.Success;
            }
            else
            {
                return RegisterUserResult.MobileExists;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("خطای ثبت نام کاربر :", e);
            return RegisterUserResult.Error;
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

    #region Dispose

    public async ValueTask DisposeAsync()
    {
        if (_userRepository != null)
        {
            _userRepository.DisposeAsync();
        }

        if (_roleRepository != null)
        {
            _roleRepository.DisposeAsync();
        }
    }

    #endregion
}