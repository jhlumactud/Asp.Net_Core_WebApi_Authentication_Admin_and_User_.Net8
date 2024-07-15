using System.ComponentModel.DataAnnotations;

namespace AdminAndUserRoleAuthentication.WebApi.DTOs;

public class LoginDTO
{
    [Required(ErrorMessage ="Username/Email is required.")]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; } = string.Empty;
}
