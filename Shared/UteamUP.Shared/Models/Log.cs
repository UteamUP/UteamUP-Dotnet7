namespace UteamUP.Shared.Models;

public class Log : Base
{
    [Key] public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public MUser? Creator { get; set; }
}