namespace InventoryMgtApp.DAL.Entities.DTOs.Responses;

public class TokenResponse
{
    public string? TokenString { get; set; }
    public DateTime ValidTo { get; set; }
}
