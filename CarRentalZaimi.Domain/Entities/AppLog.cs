using System.ComponentModel.DataAnnotations;

namespace CarRentalZaimi.Domain.Entities;

public class AppLog
{
    [Key]
    public int Id { get; set; }
    public string? Message { get; set; }
    public string? Template { get; set; }
    public string? Level { get; set; }
    public string? Timestamp { get; set; }
    public string? Exception { get; set; }
    public string? Properties { get; set; }
}
