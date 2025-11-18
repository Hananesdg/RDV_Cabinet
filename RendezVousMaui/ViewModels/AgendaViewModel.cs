using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RendezVousMaui.Database;
using RendezVousMaui.Models;
using System.Collections.ObjectModel;

namespace RendezVousMaui.ViewModels;

public partial class AgendaViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    // MODE
    [ObservableProperty] private bool isDayMode = true;
    public bool IsWeekMode => !IsDayMode;

    public string ModeButtonText => IsDayMode ? "📅 Mode Semaine" : "📅 Mode Jour";

    public List<TimeSpan> Creneaux { get; set; } = new();

    // JOUR
    public ObservableCollection<AgendaDaySlot> DaySlots { get; set; } = new();
    [ObservableProperty] private DateTime selectedDate = DateTime.Today;

    // SEMAINE
    public List<AgendaWeekSlot> WeekSlots { get; set; } = new();


    public AgendaViewModel(DatabaseService db)
    {
        _db = db;

        // Créneaux 30 min
        for (int h = 8; h < 18; h++)
        {
            Creneaux.Add(new TimeSpan(h, 0, 0));
            Creneaux.Add(new TimeSpan(h, 30, 0));
        }
    }

    // 🔵 MODE SWITCH
    [RelayCommand]
    private void SwitchMode()
    {
        IsDayMode = !IsDayMode;
        OnPropertyChanged(nameof(IsWeekMode));
        OnPropertyChanged(nameof(ModeButtonText));
    }

    // 🔵 JOUR
    public async Task LoadDay()
    {
        DaySlots.Clear();

        var rdvs = await _db.GetAppointmentsByDate(SelectedDate);

        foreach (var t in Creneaux)
        {
            var rdv = rdvs.FirstOrDefault(r => r.HeureDebut == t);

            DaySlots.Add(new AgendaDaySlot
            {
                Heure = t.ToString(@"hh\:mm"),
                Statut = rdv != null ? $"RDV - {rdv.Motif}" : "Libre",
                Appointment = rdv // Ne pas utiliser le point d'exclamation ici
            });
        }
    }

    // 🔵 SEMAINE
    public async Task LoadWeek()
    {
        WeekSlots.Clear();

        DateTime start = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);

        for (int d = 0; d < 7; d++)
        {
            var date = start.AddDays(d);
            var rdvs = await _db.GetAppointmentsByDate(date);

            foreach (var t in Creneaux)
            {
                var rdv = rdvs.FirstOrDefault(r => r.HeureDebut == t);

                WeekSlots.Add(new AgendaWeekSlot
                {
                    Date = date,
                    Heure = t,
                    IsBusy = rdv != null,
                    Text = rdv != null ? rdv.Motif : $"{t:hh\\:mm}",
                    Appointment = rdv // Ne pas utiliser le point d'exclamation ici
                });
            }
        }
    }

    public AgendaWeekSlot GetWeekSlot(int row, int col)
    {
        return WeekSlots[row + col * Creneaux.Count];
    }

    public async Task OnSlotSelected(AgendaWeekSlot slot)
    {
        if (slot.IsBusy && slot.Appointment != null)
        {
            var param = new Dictionary<string, object>
            {
                {"Appointment", slot.Appointment}
            };
            await Shell.Current.GoToAsync("EditAppointmentPage", param);
        }
        else
        {
            var param = new Dictionary<string, object>
            {
                {"SelectedDate", slot.Date},
                {"SelectedHour", slot.Heure}
            };
            await Shell.Current.GoToAsync("AddAppointmentPage", param);
        }
    }
}

public class AgendaDaySlot
{
    public required string Heure { get; set; }
    public required string Statut { get; set; }
    public Appointment? Appointment { get; set; } // Ajout du ?
}

public class AgendaWeekSlot
{
    public DateTime Date { get; set; }
    public TimeSpan Heure { get; set; }
    public bool IsBusy { get; set; }
    public required string Text { get; set; }
    public Appointment? Appointment { get; set; } // Ajout du ?
}
