namespace UteamUP.Shared.Models;

public class GPS : Base
{
    [Key] public int Id { get; set; }
    // GPS Coordinates
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? Altitude { get; set; }
    public double? Accuracy { get; set; }
    public double? AltitudeAccuracy { get; set; }
    public double? Heading { get; set; }
    public double? Speed { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
}