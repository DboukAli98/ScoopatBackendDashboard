namespace ScoopatBackend.Models.RequestModels;

public class AddCultivationRequest
{
    public string ProductType { get; set; }
    public double ProductArea { get; set; }
    public string Season { get; set; }
    public double TotalHarvestEstimate { get; set; }
    public double TotalHarvest { get; set; }
    public double DeliveredVolume { get; set; }
}