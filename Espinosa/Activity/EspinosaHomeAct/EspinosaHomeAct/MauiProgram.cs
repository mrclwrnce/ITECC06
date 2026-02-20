using EspinosaHomeAct.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EspinosaHomeAct
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
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "usercontacts.db");
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
            }

            return app;
        }
    }
}
