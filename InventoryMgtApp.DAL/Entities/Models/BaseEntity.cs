namespace InventoryMgtApp.DAL.Entities.Models;

public class BaseEntity
{
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime? DateUpdated { get; set; }
}
