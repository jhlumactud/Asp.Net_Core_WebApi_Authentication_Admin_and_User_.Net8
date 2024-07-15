using System.ComponentModel.DataAnnotations;

namespace AdminAndUserRoleAuthentication.WebApi.DTOs;

public class UserDTO
{
    public string? Id { get; set; } = string.Empty;

    [Required(ErrorMessage ="Full Name is required.")]
    public string? FullName { get; set; } = string.Empty;

    public string? Address { get; set; } = string.Empty;

    [Required(ErrorMessage ="Email Address is required.")]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [MaxLength(20, ErrorMessage = "Password cannot exceed 20 characters.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$", ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
    public string? Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string? ConfirmPassword { get; set; } = string.Empty;
}
