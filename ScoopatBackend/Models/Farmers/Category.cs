using System.ComponentModel.DataAnnotations;

namespace ScoopatBackend.Models.Farmers;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    public string CategoryTitle { get; set; }
    public ICollection<Question> Questions { get; set; }
}