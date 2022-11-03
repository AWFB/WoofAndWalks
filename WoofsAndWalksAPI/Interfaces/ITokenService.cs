using WoofsAndWalksAPI.Models;

namespace WoofsAndWalksAPI.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}
