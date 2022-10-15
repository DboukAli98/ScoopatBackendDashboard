namespace ScoopatBackend.Models.Farmers;

public class OwnersFarms
{
    public int Id { get; set; }
    public Farm Farm { get; set; }
    public FarmOwner FarmOwner { get; set; }
}