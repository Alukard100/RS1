using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;
using VideoStreamingPlatform.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IBlogService,BlogService>();
builder.Services.AddSingleton<IWalletService,WalletService>();
builder.Services.AddSingleton<IUserTypeService, UserTypeService>();
builder.Services.AddSingleton<IMessageBodyService,MessageBodyService>();
builder.Services.AddSingleton<IAdvertisementService, AdvertisementService>();
builder.Services.AddSingleton<ISynchronizationService,SynchronizationService>();
builder.Services.AddSingleton<IPlaylistService,PlaylistService>();
builder.Services.AddSingleton<IActivePromoCodesService,ActivePromoCodesService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
