using Microsoft.Build.Framework;

namespace ScoopatBackend.Models.RequestModels;

public class AddAssesmentRequest
{
    [Required]
    public int Conformity { get; set; }
    [Required]
    public int NonConformity { get; set; }

    [Required]
    public int NonApplicable { get; set; }
}