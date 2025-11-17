using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RendezVousMaui.Models;
using RendezVousMaui.Database;

namespace RendezVousMaui.ViewModels;

public partial class ClientsViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    public ObservableCollection<Client> Clients { get; } = new();

    public ClientsViewModel(DatabaseService db)
    {
        _db = db;
    }

    public async Task LoadClients()
    {
        Clients.Clear();

        var clients = await _db.GetClientsAsync();
        foreach (var c in clients)
            Clients.Add(c);
    }

    [RelayCommand]
    private async Task AddClient()
    {
        await Shell.Current.GoToAsync("AddClientPage");
    }

    [RelayCommand]
    private async Task SelectClient(Client client)
    {
        var parameters = new Dictionary<string, object>
        {
            { "Client", client }
        };

        await Shell.Current.GoToAsync("EditClientPage", parameters);
    }
}
