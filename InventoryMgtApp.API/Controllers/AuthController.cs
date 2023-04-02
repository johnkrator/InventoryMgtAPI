using InventoryMgtApp.BLL.Services.Contracts;
using InventoryMgtApp.DAL.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMgtApp.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("UserRegistration")]
    public async Task<ActionResult> UserRegistration(RegistrationDto model)
    {
        var newUser = await _authService.UserRegisteration(model);

        if (newUser is null)
            return StatusCode(StatusCodes.Status400BadRequest);

        return Ok(newUser);
    }

    [HttpPost("AdminRegistration")]
    public async Task<ActionResult> AdminRegistration(RegistrationDto model)
    {
        var newAdmin = await _authService.AdminRegistration(model);

        if (newAdmin is null)
            return StatusCode(StatusCodes.Status400BadRequest);

        return Ok(newAdmin);
    }

    [HttpPost("SuperAdminRegistration")]
    public async Task<ActionResult> SuperAdminRegistration(RegistrationDto model)
    {
        var newSuperAdmin = await _authService.SuperAdminRegistration(model);

        if (newSuperAdmin is null)
            return StatusCode(StatusCodes.Status400BadRequest);

        return Ok(newSuperAdmin);
    }

    [HttpPost("GeneralLogin")]
    public async Task<ActionResult> Login(LoginDto model)
    {
        var login = await _authService.Login(model);

        if (login is null)
            return StatusCode(StatusCodes.Status400BadRequest);

        return Ok(login);
    }

    [HttpGet("GetUsers")]
    public async Task<ActionResult> GetUsers()
    {
        var users = await _authService.GetUsers();

        if (users is null)
            return StatusCode(StatusCodes.Status404NotFound);

        return Ok(users);
    }

    [HttpGet("GetUser")]
    public async Task<ActionResult> GetUser(string id)
    {
        var user = await _authService.GetUser(id);

        if (user is null)
            return StatusCode(StatusCodes.Status404NotFound);

        return Ok(user);
    }

    [HttpPost("UpdateUser")]
    public async Task<ActionResult> UpdateUser(string id, [FromBody] UpdateDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authService.UpdateUser(id, model);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("DeleteUser")]
    public async Task<ActionResult> DeleteUser(string id)
    {
        var deleteUser = await _authService.DeleteUser(id);

        if (deleteUser is null)
            return StatusCode(StatusCodes.Status404NotFound);

        return Ok(deleteUser);
    }
}
