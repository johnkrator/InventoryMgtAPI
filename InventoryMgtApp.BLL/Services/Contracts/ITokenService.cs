using System.Security.Claims;
using InventoryMgtApp.DAL.Entities.DTOs.Responses;
using InventoryMgtApp.DAL.Entities.Token;

namespace InventoryMgtApp.BLL.Services.Contracts;

public interface ITokenService
{
    TokenResponse GetToken(IEnumerable<Claim> claims);
    string GetRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}