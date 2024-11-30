using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;
using VideoStreamingPlatform.Service;
using Duende.IdentityServer;
using VideoStreamingPlatform; // Add this for Duende IdentityServer functionality

var builder = WebApplication.CreateBuilder(args);

// Configure services for dependency injection
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Entity Framework and Identity
builder.Services.AddDbContext<VideoStreamingPlatformContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity with Entity Framework
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<VideoStreamingPlatformContext>()
    .AddDefaultTokenProviders();

// Configure IdentityServer
builder.Services.AddIdentityServer(options =>
{
    options.EmitStaticAudienceClaim = true; // Compatibility with JWT clients
})
.AddInMemoryApiScopes(Config.ApiScopes)    // In-memory API Scopes
.AddInMemoryClients(Config.Clients)        // In-memory Client Configs
.AddDeveloperSigningCredential();          // Development-only signing credential

// Configure JWT Authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001"; // IdentityServer URL
        options.Audience = "api1";                    // API resource audience
        options.RequireHttpsMetadata = false;         // Disable HTTPS only for development
    });

// Register services for Dependency Injection
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IBlogService, BlogService>();
builder.Services.AddSingleton<IWalletService, WalletService>();
builder.Services.AddSingleton<IUserTypeService, UserTypeService>();
builder.Services.AddSingleton<IMessageBodyService, MessageBodyService>();
builder.Services.AddSingleton<IAdvertisementService, AdvertisementService>();
builder.Services.AddSingleton<ISynchronizationService, SynchronizationService>();
builder.Services.AddSingleton<IPlaylistService, PlaylistService>();
builder.Services.AddSingleton<IActivePromoCodesService, ActivePromoCodesService>();
builder.Services.AddSingleton<ISupportService, SupportService>();
builder.Services.AddSingleton<IUserValuesService, UserValuesService>();
builder.Services.AddSingleton<ICardPaymentService, CardPaymentService>();
builder.Services.AddSingleton<IMembershipService, MembershipService>();
builder.Services.AddSingleton<INotificationService, NotificationsService>();
builder.Services.AddSingleton<INotificationTypeService, NotificationTypeService>();
builder.Services.AddSingleton<IReportService, ReportService>();
builder.Services.AddSingleton<IReportTypeService, ReportTypeService>();
builder.Services.AddSingleton<IGroupMemberService, GroupMemberService>();
builder.Services.AddSingleton<IPlaylistGroupService, PlaylistGroupService>();
builder.Services.AddSingleton<IEmojiShowService, EmojiShowService>();

// Build the app
var app = builder.Build();

// Ensure roles are created at startup
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await EnsureRolesAsync(serviceProvider);
}

// Middleware Configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseIdentityServer();   // IdentityServer middleware
app.UseAuthentication();   // Authentication middleware
app.UseAuthorization();    // Authorization middleware

app.MapControllers();      // Map Controller routes

app.Run();

// Helper method to ensure roles exist
async Task EnsureRolesAsync(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "admin", "user", "superuser", "guest" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
