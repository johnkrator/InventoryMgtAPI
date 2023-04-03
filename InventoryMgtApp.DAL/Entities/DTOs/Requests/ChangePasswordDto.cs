using System.ComponentModel.DataAnnotations;

namespace InventoryMgtApp.DAL.Entities.DTOs.Requests;

public class ChangePasswordDto
{
    [Required] public string Username { get; set; }
    [Required] public string CurrentPassword { get; set; }
    [Required] public string NewPassword { get; set; }
    [Required] [Compare("NewPassword")] public string ConfirmPassword { get; set; }
}