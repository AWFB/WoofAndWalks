using System.ComponentModel.DataAnnotations;

namespace WoofsAndWalksAPI.DTOs;

public class RegisterDto
{
    [Required]
    public string UserName { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 5)]
    public string Password { get; set; }
}
