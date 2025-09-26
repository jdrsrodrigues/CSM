using System.ComponentModel.DataAnnotations;

namespace CSMDashboard.Shared.Models;
public class LoginModel
{
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória")]
    public string Password { get; set; }
}
