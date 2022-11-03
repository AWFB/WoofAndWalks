using Microsoft.AspNetCore.Identity;

namespace WoofsAndWalksAPI.Models;

public class AppUserRole : IdentityUserRole<int>
{
    // join table
    public AppUser User { get; set; }
    public AppRole Role { get; set; }
}