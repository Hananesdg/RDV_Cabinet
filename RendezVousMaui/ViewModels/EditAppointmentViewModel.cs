using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RendezVousMaui.Models;
using RendezVousMaui.Database;

namespace RendezVousMaui.ViewModels;

[QueryProperty(nameof(Appointment), "Appointment")]
public partial class EditAppointmentViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    [ObservableProperty]
    private Appointment appointment = new();

    [ObservableProperty]
    private string clientName = string.Empty;

    public EditAppointmentViewModel(DatabaseService db)
    {
        _db = db;
    }

    partial void OnAppointmentChanged(Appointment value)
    {
        LoadClient();
    }

    private async void LoadClient()
    {
        if (Appointment.ClientId != 0)
        {
            var c = await _db.GetClientById(Appointment.ClientId);
            ClientName = $"{c.Nom} {c.Prenom}";
        }
    }

    [RelayCommand]
    private async Task Update()
    {
        await _db.UpdateAppointmentAsync(Appointment);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task Delete()
    {
        await _db.DeleteAppointmentAsync(Appointment);
        await Shell.Current.GoToAsync("..");
    }
}
