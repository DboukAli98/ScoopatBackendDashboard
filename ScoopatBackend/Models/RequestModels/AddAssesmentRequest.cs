using Microsoft.Build.Framework;

namespace ScoopatBackend.Models.RequestModels;

public class AddAssesmentRequest
{
    
    [Required]
    public string Status { get; set; }
    public string Notes { get; set; }
}