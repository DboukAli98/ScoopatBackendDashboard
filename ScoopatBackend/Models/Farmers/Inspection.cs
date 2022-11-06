using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ScoopatBackend.Models.Users;

namespace ScoopatBackend.Models.Farmers;

public class Inspection
{
    [Key]
    public int? InspectionId { get; set; }
    public Employee Employee { get; set; }
    public Farm Farm { get; set; }
    public ICollection<InspectionQuestionResult> InspectionQuestionResult { get; set; }

    public Farmer Farmer { get; set; }
    public Assesment Assesment { get; set; }
    [Required]
    public DateTime DateOfInspection { get; set; }
    
    

}