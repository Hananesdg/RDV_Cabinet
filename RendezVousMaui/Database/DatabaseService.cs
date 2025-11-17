using SQLite;
using RendezVousMaui.Models;

namespace RendezVousMaui.Database;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _db;

    public DatabaseService()
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "rendezvous.db3");
        _db = new SQLiteAsyncConnection(dbPath);
    }

    public async Task InitAsync()
    {
        await _db.CreateTableAsync<Client>();
        await _db.CreateTableAsync<Appointment>();
    }

    // ------------------
    // CRUD CLIENT
    // ------------------

    public Task<List<Client>> GetClientsAsync() =>
        _db.Table<Client>().OrderBy(c => c.Nom).ToListAsync();

    public Task<int> AddClientAsync(Client client) =>
        _db.InsertAsync(client);

    public Task<int> UpdateClientAsync(Client client) =>
        _db.UpdateAsync(client);

    public Task<int> DeleteClientAsync(Client client) =>
        _db.DeleteAsync(client);

    // ------------------
    // CRUD APPOINTMENT
    // ------------------

    public Task<List<Appointment>> GetAppointmentsAsync() =>
        _db.Table<Appointment>().OrderBy(a => a.Date).ToListAsync();

    public Task<int> AddAppointmentAsync(Appointment appointment) =>
        _db.InsertAsync(appointment);

    public Task<int> UpdateAppointmentAsync(Appointment appointment) =>
        _db.UpdateAsync(appointment);

    public Task<int> DeleteAppointmentAsync(Appointment appointment) =>
        _db.DeleteAsync(appointment);

    public Task<List<Appointment>> GetAppointmentsByDate(DateTime date) =>
        _db.Table<Appointment>()
            .Where(a => a.Date.Date == date.Date)
            .OrderBy(a => a.HeureDebut)
            .ToListAsync();
}
