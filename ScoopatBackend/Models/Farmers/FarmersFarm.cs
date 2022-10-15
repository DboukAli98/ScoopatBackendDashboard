namespace ScoopatBackend.Models.Farmers;

public class FarmersFarm
{
    public int Id { get; set; }
    public Farm Farm { get; set; }
    public Farmer Farmer { get; set; }
}