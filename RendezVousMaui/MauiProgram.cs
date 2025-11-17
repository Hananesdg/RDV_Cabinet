using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using RendezVousMaui.Database;

namespace RendezVousMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>();

        // SERVICES
        builder.Services.AddSingleton<DatabaseService>();

        return builder.Build();
    }
}
