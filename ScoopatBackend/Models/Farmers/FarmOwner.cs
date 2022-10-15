using System.ComponentModel.DataAnnotations;

namespace ScoopatBackend.Models.Farmers;

public class FarmOwner
{
    [Key]
    public int OwnerId { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string IdType { get; set; }
    [Required]
    public string IdNumber { get; set; }
    [Required]
    public string Sex { get; set; }
    [Required]
    public string Contact { get; set; }
    
}