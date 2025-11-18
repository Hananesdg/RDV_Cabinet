using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using RendezVousMaui.Database;
using RendezVousMaui.ViewModels;
using RendezVousMaui.Views;

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
        builder.Services.AddSingleton<ClientsPage>();
        builder.Services.AddTransient<AddClientPage>();
        builder.Services.AddTransient<EditClientPage>();

        builder.Services.AddSingleton<ClientsViewModel>();
        builder.Services.AddTransient<AddClientViewModel>();
        builder.Services.AddTransient<EditClientViewModel>();

        builder.Services.AddSingleton<AppointmentsPage>();
        builder.Services.AddSingleton<AppointmentsViewModel>();

        builder.Services.AddTransient<AddAppointmentPage>();
        builder.Services.AddTransient<AddAppointmentViewModel>();

        builder.Services.AddTransient<EditAppointmentPage>();
        builder.Services.AddTransient<EditAppointmentViewModel>();

        builder.Services.AddSingleton<AgendaPage>();
        builder.Services.AddSingleton<AgendaViewModel>();


        return builder.Build();
    }
}
