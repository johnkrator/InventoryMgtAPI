using InventoryMgtApp.DAL.Entities.DTOs;
using InventoryMgtApp.DAL.Entities.Models;
using InventoryMgtApp.DAL.Entities.Responses;

namespace InventoryMgtApp.BLL.Services.Contracts;

public interface IAuthService
{
    Task<Status> UserRegisteration(RegistrationDto model);
    Task<Status> AdminRegistration(RegistrationDto model);
    Task<Status> SuperAdminRegistration(RegistrationDto model);
    Task<LoginResponse> Login(LoginDto model);
    Task<Status> ChangePassword(ChangePasswordDto model);
    Task<List<AppUser>> GetUsers();
    Task<AppUser> GetUser(string id);
    Task<Status> UpdateUser(string id, RegistrationDto registrationDto);
    Task<Status> DeleteUser(string id);
}