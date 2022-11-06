using System.ComponentModel.DataAnnotations;

namespace ScoopatBackend.Models.RequestModels;

public class CategoryRequestModel
{
    [Required]
    public string CategoryTitle { get; set; }
}