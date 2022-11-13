using System.ComponentModel.DataAnnotations;

namespace ScoopatBackend.Models.Farmers;

public class Farm
{
    [Key]
    public int FarmId { get; set; }
    [Required]
    public string FarmCode { get; set; }
    [Required]
    public double TotalArea { get; set; }
    [Required]
    public double Lattitude { get; set; }
    [Required]
    public double Longtitude { get; set; }
    public ICollection<CultivationInformation> CultivationInformations { get; set; }
    public ICollection<Workers> Workers { get; set; }
    public ICollection<Inspection> Inspections { get; set; }
    public ICollection<Mapping> Mappings { get; set; }





}