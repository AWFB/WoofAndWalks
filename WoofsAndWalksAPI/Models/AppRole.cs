using Microsoft.AspNetCore.Identity;

namespace WoofsAndWalksAPI.Models;

public class AppRole : IdentityRole<int>
{
    public ICollection<AppUserRole> UserRoles { get; set; }
}