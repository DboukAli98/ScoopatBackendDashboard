namespace ScoopatBackend.Models.RequestModels;

public class AddFarmRequest
{
    public string FarmCode { get; set; }
    public double TotalArea { get; set; }
    public double Lattitude { get; set; }
    public double Longtitude { get; set; }
}