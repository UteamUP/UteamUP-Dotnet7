namespace UteamUP.Server.Database.Contexts;

public class pgContext : DbContext
{
    public pgContext(DbContextOptions<pgContext> options) : base(options)
    {
    }

    public DbSet<Asset> Assets { get; set; }
    public DbSet<AssetPart> AssetParts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<InvitedUser> InvitedUsers { get; set; }
    public DbSet<License> Licenses { get; set; }
    public DbSet<LicenseUsers> LicenseUsers { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<LocationTag> LocationTags { get; set; }
    //public DbSet<LocationStock> LocationStocks { get; set; }
    public DbSet<Part?> Parts { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    //public DbSet<StockItemPart> StockItemParts { get; set; }
    //public DbSet<StockItemLog> StockItemLogs { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    //public DbSet<TenantUser> TenantUsers { get; set; }
    public DbSet<Tool> Tools { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<MUser> Users { get; set; }
    public DbSet<Vendor> Vendor { get; set; }
    public DbSet<Workorder> Workorders { get; set; }
    public DbSet<WorkorderSchedule> WorkorderSchedules { get; set; }
    public DbSet<WorkorderStatusCategory> WorkorderStatusCategories { get; set; }
    public DbSet<WorkorderTemplate> WorkorderTemplates { get; set; }
    public DbSet<Log> Logs { get; set; }
    //public DbSet<GPS> Gpses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MUser>()
            .HasIndex(mUser => new { mUser.Oid })
            .IsUnique();
        
        modelBuilder.Entity<MUser>()
            .HasIndex(mUser => new { mUser.Email })
            .IsUnique();
        
        modelBuilder.Entity<Tenant>().HasIndex(b => b.Name).IsUnique();
        modelBuilder.Entity<Vendor>().HasIndex(b => b.Name).IsUnique();

        /*
        modelBuilder.Entity<Location>()
            .HasMany(e => e.Tags)
            .WithMany(e => e.Locations);
        
        modelBuilder.Entity<Tag>()
            .HasMany(e => e.Locations)
            .WithMany(e => e.Tags);
        */
        
        /*
        modelBuilder.Entity<TagLocation>()
            .HasKey(tl => new { tl.TagId, tl.LocationId }); // composite key

        modelBuilder.Entity<TagLocation>()
            .HasOne(tl => tl.Location)
            .WithMany(l => l.TagLocations)
            .HasForeignKey(tl => tl.LocationId);

        modelBuilder.Entity<TagLocation>()
            .HasOne(tl => tl.Tag)
            .WithMany(t => t.TagLocations)
            .HasForeignKey(tl => tl.TagId);
            */
    }
}