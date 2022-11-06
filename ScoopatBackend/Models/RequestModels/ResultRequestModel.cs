using System.ComponentModel.DataAnnotations;

namespace ScoopatBackend.Models.RequestModels;

public class ResultRequestModel
{
    [Required]
    public string ResultType { get; set; }
    public string Observations { get; set; }
}