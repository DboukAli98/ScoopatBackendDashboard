using System.ComponentModel.DataAnnotations;

namespace ScoopatBackend.Models.Farmers;

public class Workers
{
    [Key]
    public int? WorkerId { get; set; }
    [Required]
    public string EmploymentType { get; set; }
    [Required]
    public string Firstname { get; set; }
    [Required]
    public string Lastname { get; set; }
    [Required]
    public int Age { get; set; }
    [Required]
    public string Sex { get; set; }
    [Required]
    public DateTime EmploymentDate { get; set; }
    [Required]
    public double Salary { get; set; }
    [Required]
    public Farm Farm { get; set; }
    

}