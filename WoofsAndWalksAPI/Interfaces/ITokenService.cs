using WoofsAndWalksAPI.Models;

namespace WoofsAndWalksAPI.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
