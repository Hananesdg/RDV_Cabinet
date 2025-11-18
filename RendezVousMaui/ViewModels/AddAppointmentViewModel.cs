using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RendezVousMaui.Database;
using RendezVousMaui.Models;
using System;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RendezVousMaui.ViewModels;

[QueryProperty(nameof(Date), "SelectedDate")]
public partial class AddAppointmentViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    public ObservableCollection<ClientView> Clients { get; } = new();

    [ObservableProperty] private ClientView selectedClient = new();
    [ObservableProperty] private DateTime date = DateTime.Today;
    [ObservableProperty] private TimeSpan heureDebut = new(9, 0, 0);
    [ObservableProperty] private TimeSpan heureFin = new(9, 30, 0);
    [ObservableProperty] private string motif = string.Empty;

    public AddAppointmentViewModel(DatabaseService db)
    {
        _db = db;

        LoadClients();
    }

    private async void LoadClients()
    {
        var all = await _db.GetClientsAsync();
        Clients.Clear();

        foreach (var c in all)
        {
            Clients.Add(new ClientView
            {
                Id = c.Id,
                FullName = $"{c.Nom} {c.Prenom}"
            });
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        var rdv = new Appointment
        {
            ClientId = SelectedClient.Id,
            Date = Date,
            HeureDebut = HeureDebut,
            HeureFin = HeureFin,
            Motif = Motif
        };

        await _db.AddAppointmentAsync(rdv);
        await Shell.Current.GoToAsync("..");
    }
}

public class ClientView
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
}
