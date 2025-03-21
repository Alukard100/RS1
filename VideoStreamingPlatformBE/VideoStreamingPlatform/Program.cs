using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database;
using VideoStreamingPlatform.Service;

var builder = WebApplication.CreateBuilder(args);

// Load JWT secret key
var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
var keyBytes = Encoding.ASCII.GetBytes(jwtKey);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'"
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
                new string[] {}
            }
        });
});

// Configure EF Core with SQL Server
builder.Services.AddDbContext<VideoStreamingPlatformContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<VideoStreamingPlatformContext>()
    .AddDefaultTokenProviders();

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Register custom services
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

builder.Services.AddHttpClient();

var app = builder.Build();

// Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseRouting();
app.UseHttpsRedirection();
app.UseRouting();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();


