namespace ScoopatBackend.Models.RequestModels;

public class AddFarmOwnerRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IdType { get; set; }
    public string IdNumber { get; set; }
    public string Sex { get; set; }
    public string Contact { get; set; }
    public string Location { get; set; }
    public int? FarmerId { get; set; }
}