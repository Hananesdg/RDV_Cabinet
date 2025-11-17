using SQLite;

namespace RendezVousMaui.Models;

public class Appointment
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int ClientId { get; set; }

    public DateTime Date { get; set; }

    public TimeSpan HeureDebut { get; set; }
    public TimeSpan HeureFin { get; set; }

    [MaxLength(255)]
    public string Motif { get; set; } = string.Empty;

    public bool EstConfirme { get; set; } = false;
}
