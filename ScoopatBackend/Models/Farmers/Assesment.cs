namespace ScoopatBackend.Models.Farmers;

public class Assesment
{
    public int AssesmentId { get; set; }
    public int Conformity { get; set; }
    public int NonConformity { get; set; }
    public int TotalApplicable { get; set; }
    public int NonApplicable { get; set; }
    public string Status { get; set; }
    public string Notes{ get; set; }
    public Inspection Inspection { get; set; }
    public int InspectionId { get; set; }

}