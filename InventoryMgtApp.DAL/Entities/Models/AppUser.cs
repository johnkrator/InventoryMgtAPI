using Microsoft.AspNetCore.Identity;

namespace InventoryMgtApp.DAL.Entities.Models;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
    public DateTime DOB { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string PhoneNumber { get; set; }

    public ICollection<Product> Products { get; set; }
    public ICollection<Sale> Sales { get; set; }
}
