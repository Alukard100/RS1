using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;
using VideoStreamingPlatform.Service;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<VideoStreamingPlatformContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<VideoStreamingPlatformContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5001";  // Set your IdentityProvider URL here
        options.Audience = "api1";                    // Your API's audience identifier
        options.RequireHttpsMetadata = false;         // Set to true in production
    });


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


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();   
app.UseAuthorization();    

app.MapControllers();      


//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    await EnsureRolesAsync(services);
//}

app.Run();

//async Task EnsureRolesAsync(IServiceProvider serviceProvider)
//{
//    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//    var roles = new[] { "admin", "user", "superuser", "guest" };

//    foreach (var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//        {
//            await roleManager.CreateAsync(new IdentityRole(role));
//        }
//    }
//}
