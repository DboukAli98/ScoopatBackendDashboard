using System.ComponentModel.DataAnnotations;

namespace ScoopatBackend.Models.Farmers;

public class Question
{
    [Key]
    public int QuestionId { get; set; }
    public string QuestionContent { get; set; }
    
    public Category Category { get; set; }

}