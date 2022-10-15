namespace ScoopatBackend.Models.Authentication;

public class RegisterResponseModel
{
    public string Token { get; set; }
    public string id { get; set; }
    public string username { get; set; }
    public string role { get; set; }
}