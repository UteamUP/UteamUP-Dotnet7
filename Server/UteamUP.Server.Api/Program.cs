using System.Text.Json.Serialization;
using UteamUP.Client.Web.Repository.Implementations;
using UteamUP.Client.Web.Repository.Interfaces;
using UteamUP.Server.Repository.GenericRepository.Implementations;
using UteamUP.Server.Repository.GenericRepository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

// Adding Global Services.
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();

// Add Database Service
builder.Services.AddDbContext<pgContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("UteamupDB"),
    b => b.MigrationsAssembly("UteamUP.Server.Api")));

// Add AutoMapper Service
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Add MediatR Service

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
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

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "UteamUP Server API",
        Version = "v1",
        Description = "API documentation",
        TermsOfService = new Uri("http://www.uteamup.com"),
        Contact = new OpenApiContact
        {
            Name = "Gisli Gudmundsson",
            Email = "gisli@uteamup.com"
        },
        License = new OpenApiLicense
        {
            Name = "In progress",
            Url = new Uri("http://www.uteamup.com")
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


builder.Services.AddCors();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
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