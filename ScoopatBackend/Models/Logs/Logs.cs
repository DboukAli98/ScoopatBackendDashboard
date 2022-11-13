using System.ComponentModel.DataAnnotations;

namespace ScoopatBackend.Models.Logs;

public class Logs
{
    [Key]
    public int LogId { get; set; }
    public string DateTime { get; set; }
    public string UserName { get; set; }
    public string ActionType { get; set; }
    
}