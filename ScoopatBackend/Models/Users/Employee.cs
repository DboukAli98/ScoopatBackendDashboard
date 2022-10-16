using System.ComponentModel.DataAnnotations;

namespace ScoopatBackend.Models.Users;

public class Employee
{
    public int Id { get; set; }
    [Required]
    public ApplicationUser User { get; set; }
    public DateTime CreatedAt { get; set; }
    [Required]
    public string IdType { get; set; }
    [Required]
    public string IdNumber { get; set; }
    [Required]
    public string Contact { get; set; }
    public string role { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
}