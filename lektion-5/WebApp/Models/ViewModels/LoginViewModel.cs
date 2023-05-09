using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels;

public class LoginViewModel
{
    [Required]
    [Display(Name = "E-mail address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;


    [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;


    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
}
