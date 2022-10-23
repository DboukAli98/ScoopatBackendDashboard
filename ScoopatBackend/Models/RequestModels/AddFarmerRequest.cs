using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScoopatBackend.Models.RequestModels;

public class AddFarmerRequest
{
  
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? IdType { get; set; }
    [Required]
    public string? IdNumber { get; set; }
    [Required]
    [Column(TypeName="date")]
    public string? BirthPlace { get; set; }
    [Required]
    public DateTime? BirthDate { get; set; }
    [Required]
    public string? Sex { get; set; }
    [Required]
    public string? Contact { get; set; }
    [Required]
    public string? Location { get; set; }
    [Required]
    public string? Section { get; set; }
    [Required]
    public string? Region { get; set; }
    [Required]
    public string? Department { get; set; }
    [Required]
    public bool? isFarmOwner { get; set; }
}