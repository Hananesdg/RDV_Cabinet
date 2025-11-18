using CommunityToolkit.Mvvm.ComponentModel;
using RendezVousMaui.Database;

namespace RendezVousMaui.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    [ObservableProperty] private int totalClients;
    [ObservableProperty] private int rdvToday;
    [ObservableProperty] private int rdvWeek;
    [ObservableProperty] private int upcomingRdv;

    public List<int> RdvWeekChart { get; set; } = new();

    public DashboardViewModel(DatabaseService db)
    {
        _db = db;
    }

    public async Task LoadStats()
    {
        var clients = await _db.GetClientsAsync();
        TotalClients = clients.Count;

        var today = DateTime.Today;
        var todayRdvs = await _db.GetAppointmentsByDate(today);
        RdvToday = todayRdvs.Count;

        DateTime startWeek = today.AddDays(-(int)today.DayOfWeek + 1);
        int weekCount = 0;

        RdvWeekChart.Clear();

        for (int i = 0; i < 7; i++)
        {
            var date = startWeek.AddDays(i);
            var rdvs = await _db.GetAppointmentsByDate(date);

            weekCount += rdvs.Count;
            RdvWeekChart.Add(rdvs.Count);
        }

        RdvWeek = weekCount;

        var all = await _db.GetAppointmentsAsync();
        UpcomingRdv = all.Where(a => a.Date >= today).Count();
    }
}
