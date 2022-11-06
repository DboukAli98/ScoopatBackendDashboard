using System.ComponentModel.DataAnnotations;

namespace ScoopatBackend.Models.RequestModels;

public class QuestionRequestModel
{
    
    [Required]
    public string QuestionContent { get; set; }
    
}