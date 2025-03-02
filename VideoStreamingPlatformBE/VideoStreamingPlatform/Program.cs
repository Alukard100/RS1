using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VideoStreamingPlatform.Commons.DTOs.Requests.Video;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database;
using VideoStreamingPlatform.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => { policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyMethod().AllowAnyHeader(); });
});

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

builder.Services.Configure<VideoSettings>(builder.Configuration.GetSection("VideoSettings"));
builder.WebHost.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = long.MaxValue);
builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IBlogService, BlogService>();
builder.Services.AddTransient<IWalletService, WalletService>();
builder.Services.AddTransient<IUserTypeService, UserTypeService>();
builder.Services.AddTransient<IMessageBodyService, MessageBodyService>();
builder.Services.AddTransient<IAdvertisementService, AdvertisementService>();
builder.Services.AddTransient<ISynchronizationService, SynchronizationService>();
builder.Services.AddTransient<IPlaylistService, PlaylistService>();
builder.Services.AddTransient<IActivePromoCodesService, ActivePromoCodesService>();
builder.Services.AddTransient<ISupportService, SupportService>();
builder.Services.AddTransient<IUserValuesService, UserValuesService>();
builder.Services.AddTransient<ICardPaymentService, CardPaymentService>();
builder.Services.AddTransient<IMembershipService, MembershipService>();
builder.Services.AddTransient<INotificationService, NotificationsService>();
builder.Services.AddTransient<INotificationTypeService, NotificationTypeService>();
builder.Services.AddTransient<IReportService, ReportService>();
builder.Services.AddTransient<IReportTypeService, ReportTypeService>();
builder.Services.AddTransient<IGroupMemberService, GroupMemberService>();
builder.Services.AddTransient<IPlaylistGroupService, PlaylistGroupService>();
builder.Services.AddTransient<IEmojiShowService, EmojiShowService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IVideoService, VideoService>();
builder.Services.AddTransient<IVideoStatisticService, VideoStatisticService>();
builder.Services.AddTransient<IRatingSystemVideoService, RatingSystemVideoService>();
builder.Services.AddTransient<IThumbnailInfoService, ThumbnailInfoService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IRatingSystemCommentService, RatingSystemCommentService>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseCors();

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
