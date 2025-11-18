using CommunityToolkit.Mvvm.ComponentModel;
using RendezVousMaui.Database;
using System.Collections.ObjectModel;

namespace RendezVousMaui.ViewModels;

public partial class AgendaViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    [ObservableProperty]
    private DateTime selectedDate = DateTime.Today;

    public ObservableCollection<AgendaSlot> Slots { get; } = new();

    public AgendaViewModel(DatabaseService db)
    {
        _db = db;
        LoadSlots();
    }

    partial void OnSelectedDateChanged(DateTime value)
    {
        LoadSlots();
    }

    private async void LoadSlots()
    {
        Slots.Clear();

        var rdvs = await _db.GetAppointmentsByDate(SelectedDate);

        for (int h = 8; h <= 18; h++)
        {
            var heure = new TimeSpan(h, 0, 0);
            var rdv = rdvs.FirstOrDefault(r => r.HeureDebut.Hours == h);

            Slots.Add(new AgendaSlot
            {
                Heure = $"{h:00}:00",
                Statut = rdv == null ? "Libre" : "Réservé : " + rdv.Motif
            });
        }
    }
}

public class AgendaSlot
{
    public string Heure { get; set; } = "";
    public string Statut { get; set; } = "";
}
