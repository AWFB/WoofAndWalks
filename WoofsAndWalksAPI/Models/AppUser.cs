using WoofsAndWalksAPI.Extensions;

namespace WoofsAndWalksAPI.Models;

public class AppUser
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? KnownAs { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastActive { get; set; } = DateTime.Now;
    public string? Gender { get; set; }
    public string? Introduction { get; set; }
    public string? NameOfDog { get; set; }
    public string? BreedOfDog { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }

    // ef - one to many
    public ICollection<Photo> Photos { get; set; }

    // Extension method for DOB
    // Needs to be named like this for automapper to work
    //public int GetAge()
    //{
    //    return DateOfBirth.CalculateAge();
    //}
}
