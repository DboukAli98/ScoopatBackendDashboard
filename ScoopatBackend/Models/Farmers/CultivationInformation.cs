using System.ComponentModel.DataAnnotations;

namespace ScoopatBackend.Models.Farmers;

public class CultivationInformation
{
    [Key]
    public int CultivationId { get; set; }
    public string ProductType { get; set; }
    public double ProductArea { get; set; }
    public string Season { get; set; }
    public double TotalHarvestEstimate { get; set; }
    public double TotalHarvest { get; set; }
    public double DeliveredVolume { get; set; }
    public Farm Farm { get; set; }
}