using System.ComponentModel.DataAnnotations;

namespace ScoopatBackend.Models.Farmers;

public class Result
{
    [Key]
    public int ResultId { get; set; }
    public string ResultType { get; set; }
    public string Observations { get; set; }
    public Question Question { get; set; }
    public int QuestionId { get; set; }
    
}