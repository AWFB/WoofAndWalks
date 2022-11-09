using System.ComponentModel.DataAnnotations;

namespace WoofsAndWalksAPI.DTOs;

public class RegisterDto
{
    [Required] public string? Username { get; set; }
    [Required] public string? KnownAs { get; set; }
    [Required] public string? Gender { get; set; }
    [Required] public DateTime DateOfBirth { get; set; }
    [Required] public string? City { get; set; }
    [Required] public string? Country { get; set; }
    [Required] public string? NameOfDog { get; set; }
    [Required] public string? BreedOfDog { get; set; }
    public string? Introduction { get; set; } = "";

    [Required]
    [StringLength(20, MinimumLength = 5)]
    public string Password { get; set; }
}
