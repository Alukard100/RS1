using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace VideoStreamingPlatform.Database
{
    public class VideoStreamingPlatformContextFactory : IDesignTimeDbContextFactory<VideoStreamingPlatformContext>
    {
        public VideoStreamingPlatformContext CreateDbContext(string[] args)
        {
            // Build the configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Ensure it's pointing to the correct folder
                .AddJsonFile("appsettings.json") // Read from appsettings.json
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<VideoStreamingPlatformContext>();

            // Get the connection string from the configuration
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configure the DbContext options (SQL Server in this case)
            optionsBuilder.UseSqlServer(connectionString);
            // If you're using PostgreSQL, replace with: optionsBuilder.UseNpgsql(connectionString);

            return new VideoStreamingPlatformContext(optionsBuilder.Options);
        }
    }
}
