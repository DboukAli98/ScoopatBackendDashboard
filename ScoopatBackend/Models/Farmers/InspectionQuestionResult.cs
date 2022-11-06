using System.ComponentModel.DataAnnotations;

namespace ScoopatBackend.Models.Farmers;

public class InspectionQuestionResult
{
    [Key]
    public int InsQuestId { get; set; }
    public Inspection Inspection { get; set; }
    public Result Result { get; set; }
}