namespace InventoryMgtApp.DAL.Entities.DTOs.Requests;

public class RefreshTokenRequestDto
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}