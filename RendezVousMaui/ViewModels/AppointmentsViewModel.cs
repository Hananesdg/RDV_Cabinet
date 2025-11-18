using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RendezVousMaui.Database;
using RendezVousMaui.Models;
using System.Collections.ObjectModel;

namespace RendezVousMaui.ViewModels;

public partial class AppointmentsViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    public ObservableCollection<AppointmentView> Appointments { get; } = new();

    [ObservableProperty]
    private DateTime selectedDate = DateTime.Today;


    public AppointmentsViewModel(DatabaseService db)
    {
        _db = db;
    }

    public async Task LoadAppointments()
    {
        Appointments.Clear();

        var all = await _db.GetAppointmentsByDate(SelectedDate);

        foreach (var item in all)
        {
            var client = await _db.GetClientById(item.ClientId);

            string clientName = client != null
                ? $"{client.Nom} {client.Prenom}"
                : "Client inconnu";

            Appointments.Add(new AppointmentView
            {
                Id = item.Id,
                Date = item.Date,
                HeureDebut = item.HeureDebut,
                HeureFin = item.HeureFin,
                Motif = item.Motif,
                ClientName = clientName,
                OriginalAppointment = item
            });
        }
    }

    [RelayCommand]
    private async Task AddAppointment()
    {
        var param = new Dictionary<string, object>
        {
            { "SelectedDate", SelectedDate }
        };

        await Shell.Current.GoToAsync("AddAppointmentPage", param);
    }

    [RelayCommand]
    private async Task SelectAppointment(AppointmentView view)
    {
        var param = new Dictionary<string, object>
        {
            { "Appointment", view.OriginalAppointment }
        };

        await Shell.Current.GoToAsync("EditAppointmentPage", param);
    }
}

public class AppointmentView
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan HeureDebut { get; set; }
    public TimeSpan HeureFin { get; set; }
    public string Motif { get; set; } = "";
    public string ClientName { get; set; } = "";

    public Appointment OriginalAppointment { get; set; }
}
