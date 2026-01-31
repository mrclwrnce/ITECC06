using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ESPINOSA_MdtACT1.Model;

namespace ESPINOSA_MdtACT1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddDbContext<Data.DataContext>(options =>
                {
                    var dbPath = Path.Combine(FileSystem.AppDataDirectory, "users.db");
                    options.UseSqlite($"Filename={dbPath}");
                }
                
                );
#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Data.DataContext>();
                context.Database.EnsureCreated();

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { Name = "Alice"},
                        new User { Name = "Bob"},
                        new User { Name = "Charlie"}
                    );
                    context.SaveChanges();
                }
            }

            return app;
        }
    }
}
