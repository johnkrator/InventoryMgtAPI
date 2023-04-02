using System.ComponentModel.DataAnnotations;

namespace InventoryMgtApp.DAL.Entities.DTOs;

public class UpdateDto
{
    [Required] public string? Fullname { get; set; }
    [Required] public string? Username { get; set; }
    [Required] public string? PhoneNumber { get; set; }
    [Required] public DateTime DOB { get; set; }
    [Required] public string? Address { get; set; }
    [Required] public string? PostalCode { get; set; }
}
