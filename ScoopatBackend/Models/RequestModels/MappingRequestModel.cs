using System.ComponentModel.DataAnnotations;

namespace ScoopatBackend.Models.RequestModels;

public class MappingRequestModel
{
    [Required]
    public string Area { get; set; }
}