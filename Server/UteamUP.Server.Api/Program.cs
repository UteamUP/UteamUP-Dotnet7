using System.Text.Json.Serialization;
using UteamUP.Client.Web.WizardComponents.AddEditTenant.AddEditTenant.Profiles;
using UteamUP.Server.Api.Profiles;
using UteamUP.Server.Repository.GenericRepository.Implementations;
using UteamUP.Server.Repository.GenericRepository.Interfaces;
//using EFCoreSecondLevelCacheInterceptor;
using UteamUP.Client.Web.WizardComponents.AddEditVendor.Profiles;
using UteamUP.Server.Api.Helpers;
using Newtonsoft.Json;
var builder = WebApplication.CreateBuilder(args);

// Add localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Adding generic repository services.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Adding Global repository services.
builder.Services.AddScoped<IMUserRepository, MUserRepository>();
builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<IPartRepository, PartRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();

// Adding Global Services.
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();

// Adding Helpers
builder.Services.AddScoped<IProfileBuilder, ProfileBuilder>();

// Add Database Cache Service
builder.Services.AddMemoryCache();
/*
builder.Services.AddEFSecondLevelCache(
    options => options.UseMemoryCacheProvider()
        .DisableLogging(false)
        .UseCacheKeyPrefix("EF_"));
*/

// Add Database Service
builder.Services.AddDbContext<pgContext>(
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("UteamupDB"),
    b => b.MigrationsAssembly("UteamUP.Server.Api"))
        //.AddInterceptors(builder.Services.BuildServiceProvider().GetRequiredService<SecondLevelCacheInterceptor>())
    );

// Add AutoMapper Service
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(
    typeof(TenantProfile).Assembly, 
    typeof(TenantFormProfile).Assembly,
    typeof(VendorFormProfile).Assembly
    );

// Configure the Controllers
builder.Services.AddControllers();
/*
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve
);*/

/*
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
*/
builder.Services.AddControllersWithViews();

// Add the Razor Pages
builder.Services.AddRazorPages();

// Add Logging Service
builder.Services.AddLogging();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

// Add Authentication Service
builder.Services.AddAuthenticationCore();

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

// Add Authorization Service
JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

// Configure Swagger for API documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "UteamUP Server API",
        Version = "v1",
        Description = "API documentation",
        TermsOfService = new Uri("https://www.uteamup.com"),
        Contact = new OpenApiContact
        {
            Name = "Gisli Gudmundsson",
            Email = "gisli@uteamup.com"
        },
        License = new OpenApiLicense
        {
            Name = "In progress",
            Url = new Uri("https://www.uteamup.com")
        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Add CORS Service
builder.Services.AddCors();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", corsPolicyBuilder =>
    {
        corsPolicyBuilder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .SetIsOriginAllowed(_ => true); // Allow any origin
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();