using System.ComponentModel.DataAnnotations;

namespace ScoopatBackend.Models.Farmers;

public class Farmer
{
    public int FarmerId { get; set; }
    [Required(ErrorMessage = "Farmer Code Is Required")]
    public string? FarmerCode { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? IdType { get; set; }
    [Required]
    public string? IdNumber { get; set; }
    [Required]
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
    public bool? isFarmOwner { get; set; }



}