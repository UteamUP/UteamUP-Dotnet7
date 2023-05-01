namespace UteamUP.Client.Web.Stores.Global;

[FeatureState]
public record GlobalState
{
    public Tenant ActiveTenant { get; init; }
    public List<TenantDto> Tenants { get; init; }
    public List<InvitedUserDto> TenantsInvited { get; init; }
    public bool HasTenantInvites { get; set; }
    public string Oid { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public bool IsActivated { get; set; }
    public bool HasDatabaseUser { get; set; }
    public bool FirstLogin { get; set; }

    // Workorders
    // public List<Workorder> CriticalWorkorders => Workorders.Where(s => s.Priority == 4).OrderBy(s => s.DueDate).Where(s => s.Status != "Complete").ToList();
    // public List<Workorder> RecentWorkorders => Workorders.OrderByDescending(s => s.CreatedAt).Where(s => s.Status != "Complete").ToList();
    // public List<Workorder> PendingWorkorders => Workorders.Where(s => s.Status.Contains("Pending") || s.Status == "On hold").ToList();
    // public List<Workorder> DueWorkorders => Workorders.OrderByDescending(s => s.DueDate).Where(s => s.Status != "Complete").ToList();
    // public List<Workorder> AssignedWorkorders => Workorders.Where(s => s.AssignedUserName == "34").Where(s => s.Status != "Complete").ToList();
    // public List<Workorder> Workorders { get; init; } = new();
}