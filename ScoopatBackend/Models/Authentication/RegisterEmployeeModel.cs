using System.ComponentModel.DataAnnotations;

namespace ScoopatBackend.Models.Authentication;

public class RegisterEmployeeModel
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? Firstname { get; set; }
    [Required]
    public string? Lastname { get; set; }
    [Required]
    public string IdType { get; set; }
    [Required]
    public string IdNumber { get; set; }
    [Required]
    public string Contact { get; set; }
}