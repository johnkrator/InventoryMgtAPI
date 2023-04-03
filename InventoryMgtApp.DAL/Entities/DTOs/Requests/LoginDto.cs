using System.ComponentModel.DataAnnotations;

namespace InventoryMgtApp.DAL.Entities.DTOs.Requests;

public class LoginDto
{
    [Required] public string? Username { get; set; }
    [Required] public string? Password { get; set; }
}