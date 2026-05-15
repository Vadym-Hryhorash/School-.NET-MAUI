using Lab3_Programming.Data;
using Lab3_Programming.Repositories;
using Lab3_Programming.ViewModels;
using Lab3_Programming.Views;
using Microsoft.Extensions.Logging;

namespace Lab3_Programming
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
            builder.Services.AddSingleton<StudentDbContext>();
            builder.Services.AddSingleton<IStudentRepository, StudentRepository>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<StudentDetailsPage>();
            builder.Services.AddTransient<StudentDetailsViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
