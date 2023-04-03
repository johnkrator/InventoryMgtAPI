using InventoryMgtApp.DAL.Entities.DTOs;
using InventoryMgtApp.DAL.Entities.DTOs.Requests;
using InventoryMgtApp.DAL.Entities.DTOs.Responses;
using InventoryMgtApp.DAL.Entities.Models;

namespace InventoryMgtApp.BLL.Services.Contracts;

public interface IAuthService
{
    Task<Status> UserRegisteration(RegistrationDto model);
    Task<Status> AdminRegistration(RegistrationDto model);
    Task<LoginResponse> Login(LoginDto model);
    Task<Status> ChangePassword(ChangePasswordDto model);
    Task<List<AppUser>> GetUsers();
    Task<Status> GetUser(string id);
    Task<bool> UpdateUser(string id, UpdateDto registrationDto);
    Task<Status> DeleteUser(string id);
}