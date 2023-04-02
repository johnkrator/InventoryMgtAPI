using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using InventoryMgtApp.BLL.Services.Contracts;
using InventoryMgtApp.DAL.DbConfig;
using InventoryMgtApp.DAL.Entities.DTOs;
using InventoryMgtApp.DAL.Entities.Models;
using InventoryMgtApp.DAL.Entities.Responses;
using InventoryMgtApp.DAL.Entities.Token;
using InventoryMgtApp.DAL.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InventoryMgtApp.BLL.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly DataContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;

    public AuthService(
        DataContext context,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ITokenService tokenService
    )
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
    }

    public async Task<Status> UserRegisteration(RegistrationDto model)
    {
        var status = new Status();
        if (string.IsNullOrEmpty(model.Fullname) || string.IsNullOrEmpty(model.Username) ||
            string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.PhoneNumber) ||
            string.IsNullOrEmpty(model.DOB.ToString()) || string.IsNullOrEmpty(model.Address) ||
            string.IsNullOrEmpty(model.PostalCode) || string.IsNullOrEmpty(model.Password))
        {
            status.StatusCode = 0;
            status.Message = "Please enter all required fields.";

            return status;
        }

        // check if user exists
        var userExist = await _userManager.FindByNameAsync(model.Username);
        if (userExist != null)
        {
            status.StatusCode = 0;
            status.Message = "Username already exists";

            return status;
        }

        var user = new AppUser()
        {
            SecurityStamp = Guid.NewGuid().ToString(),
            FullName = model.Fullname,
            UserName = model.Username,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            DOB = model.DOB,
            Address = model.Address,
            PostalCode = model.PostalCode
        };

        // create new user
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            status.StatusCode = 0;
            status.Message = "New user creation failed";

            return status;
        }

        // add roles
        // for admin registration, make use of UserRole.Admin instead of UserRole.User
        if (!await _roleManager.RoleExistsAsync(UserRole.User))
            await _roleManager.CreateAsync(new IdentityRole(UserRole.User));

        if (await _roleManager.RoleExistsAsync(UserRole.User))
            await _userManager.AddToRoleAsync(user, UserRole.User);

        status.StatusCode = 1;
        status.Message = "User was registered successfully";

        return status;
    }

    public async Task<Status> AdminRegistration(RegistrationDto model)
    {
        var status = new Status();
        if (string.IsNullOrEmpty(model.Fullname) || string.IsNullOrEmpty(model.Username) ||
            string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.PhoneNumber) ||
            string.IsNullOrEmpty(model.DOB.ToString()) || string.IsNullOrEmpty(model.Address) ||
            string.IsNullOrEmpty(model.PostalCode) || string.IsNullOrEmpty(model.Password))
        {
            status.StatusCode = 0;
            status.Message = "Please enter all required fields.";

            return status;
        }

        // check if user exists
        var userExist = await _userManager.FindByNameAsync(model.Username);
        if (userExist != null)
        {
            status.StatusCode = 0;
            status.Message = "Username already exists";

            return status;
        }

        var user = new AppUser()
        {
            SecurityStamp = Guid.NewGuid().ToString(),
            FullName = model.Fullname,
            UserName = model.Username,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            DOB = model.DOB,
            Address = model.Address,
            PostalCode = model.PostalCode
        };

        // create new user
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            status.StatusCode = 0;
            status.Message = "New user creation failed";

            return status;
        }

        // add roles
        // for admin registration, make use of UserRole.Admin instead of UserRole.User
        if (!await _roleManager.RoleExistsAsync(UserRole.Admin))
            await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin));

        if (await _roleManager.RoleExistsAsync(UserRole.Admin))
            await _userManager.AddToRoleAsync(user, UserRole.Admin);

        status.StatusCode = 1;
        status.Message = "Admin-user was registered successfully";

        return status;
    }

    public async Task<Status> SuperAdminRegistration(RegistrationDto model)
    {
        var status = new Status();
        if (string.IsNullOrEmpty(model.Fullname) || string.IsNullOrEmpty(model.Username) ||
            string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.PhoneNumber) ||
            string.IsNullOrEmpty(model.DOB.ToString()) || string.IsNullOrEmpty(model.Address) ||
            string.IsNullOrEmpty(model.PostalCode) || string.IsNullOrEmpty(model.Password))
        {
            status.StatusCode = 0;
            status.Message = "Please enter all required fields.";

            return status;
        }

        // check if user exists
        var userExist = await _userManager.FindByNameAsync(model.Username);
        if (userExist != null)
        {
            status.StatusCode = 0;
            status.Message = "Username already exists";

            return status;
        }

        var user = new AppUser()
        {
            SecurityStamp = Guid.NewGuid().ToString(),
            FullName = model.Fullname,
            UserName = model.Username,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            DOB = model.DOB,
            Address = model.Address,
            PostalCode = model.PostalCode
        };

        // create new user
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            status.StatusCode = 0;
            status.Message = "New user creation failed";

            return status;
        }

        // add roles
        // for admin registration, make use of UserRole.Admin instead of UserRole.User
        if (!await _roleManager.RoleExistsAsync(UserRole.SuperAdmin))
            await _roleManager.CreateAsync(new IdentityRole(UserRole.SuperAdmin));

        if (await _roleManager.RoleExistsAsync(UserRole.SuperAdmin))
            await _userManager.AddToRoleAsync(user, UserRole.SuperAdmin);

        status.StatusCode = 1;
        status.Message = "Super-admin was registered successfully";

        return status;
    }

    public async Task<LoginResponse> Login(LoginDto model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GetToken(authClaims);
            var refreshToken = _tokenService.GetRefreshToken();
            var tokenInfo = await _context.TokenInfos.FirstOrDefaultAsync(x => x.Username == user.UserName);

            if (tokenInfo == null)
            {
                var info = new TokenInfo()
                {
                    Username = user.UserName,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryDate = DateTime.Now.AddMinutes(5)
                };
                _context.TokenInfos.Add(info);
            }
            else
            {
                tokenInfo.RefreshToken = refreshToken;
                tokenInfo.RefreshTokenExpiryDate = DateTime.Now.AddMinutes(5);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return new LoginResponse()
            {
                Name = user.FullName,
                Username = user.UserName,
                Token = token.TokenString,
                RefreshToken = refreshToken,
                Expiration = token.ValidTo,
                StatusCode = 1,
                Message = "Logged In"
            };
        }

        return new LoginResponse()
        {
            StatusCode = 0,
            Message = "Invalid username or password",
            Token = "",
            Expiration = null
        };
    }

    public async Task<Status> ChangePassword(ChangePasswordDto model)
    {
        var status = new Status();

        if (string.IsNullOrEmpty(model.Username))
        {
            status.StatusCode = 0;
            status.Message = "Please enter all fields";

            return status;
        }

        var user = await _userManager.FindByNameAsync(model.Username);
        if (user is null)
        {
            status.StatusCode = 0;
            status.Message = "Invalid username";

            return status;
        }

        // check current password
        if (!await _userManager.CheckPasswordAsync(user, model.CurrentPassword))
        {
            status.StatusCode = 0;
            status.Message = "The current password you entered is incorrect";

            return status;
        }

        var changePassword = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        if (!changePassword.Succeeded)
        {
            status.StatusCode = 0;
            status.Message = "Password change failed. Check and try again";

            return status;
        }

        status.StatusCode = 1;
        status.Message = "Password update was successful";

        return status;
    }

    public async Task<List<AppUser>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();

        if (users is null)
            throw new NotFoundException(message: "Users not found");

        return users;
    }

    public async Task<AppUser> GetUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user is null)
            throw new NotFoundException(message: "User not found");

        return user;
    }

    public async Task<bool> UpdateUser(string id, UpdateDto model)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
            return false;

        user.FullName = model.Fullname;
        user.UserName = model.Username;
        user.PhoneNumber = model.PhoneNumber;
        user.DOB = model.DOB;
        user.Address = model.Address;
        user.PostalCode = model.PostalCode;

        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded;
    }

    public async Task<Status> DeleteUser(string id)
    {
        var status = new Status();
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            status.StatusCode = 0;
            status.Message = "User not found";
            return status;
        }

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            status.StatusCode = 0;
            status.Message = "User delete failed";
            return status;
        }

        status.StatusCode = 1;
        status.Message = "User deleted successfully";
        return status;
    }
}
