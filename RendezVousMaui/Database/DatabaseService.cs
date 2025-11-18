using SQLite;
using RendezVousMaui.Models;

namespace RendezVousMaui.Database;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _db;

    public DatabaseService()
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "rendezvous.db3");
        _db = new SQLiteAsyncConnection(path);
    }

    public async Task InitAsync()
    {
        await _db.CreateTableAsync<Client>();
        await _db.CreateTableAsync<Appointment>();
    }

    // --- Client CRUD ---
    public Task<List<Client>> GetClientsAsync() =>
        _db.Table<Client>().OrderBy(c => c.Nom).ToListAsync();

    public Task<int> AddClientAsync(Client client) => _db.InsertAsync(client);
    public Task<int> UpdateClientAsync(Client client) => _db.UpdateAsync(client);
    public Task<int> DeleteClientAsync(Client client) => _db.DeleteAsync(client);

    public Task<Client> GetClientById(int id) =>
        _db.Table<Client>().Where(c => c.Id == id).FirstOrDefaultAsync();

    // --- Appointment CRUD ---
    public Task<List<Appointment>> GetAppointmentsAsync() =>
        _db.Table<Appointment>().OrderBy(a => a.Date).ToListAsync();

    public Task<List<Appointment>> GetAppointmentsByDate(DateTime date)
    {
        var start = date.Date;
        var end = date.Date.AddDays(1);

        return _db.Table<Appointment>()
                  .Where(a => a.Date >= start && a.Date < end)
                  .OrderBy(a => a.HeureDebut)
                  .ToListAsync();
    }

    public Task<int> AddAppointmentAsync(Appointment a) => _db.InsertAsync(a);
    public Task<int> UpdateAppointmentAsync(Appointment a) => _db.UpdateAsync(a);
    public Task<int> DeleteAppointmentAsync(Appointment a) => _db.DeleteAsync(a);
}
