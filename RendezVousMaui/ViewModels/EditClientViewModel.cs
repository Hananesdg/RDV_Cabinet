using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RendezVousMaui.Database;
using RendezVousMaui.Models;

namespace RendezVousMaui.ViewModels;

[QueryProperty(nameof(Client), "Client")]
public partial class EditClientViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    [ObservableProperty] private Client client;

    public EditClientViewModel(DatabaseService db)
    {
        _db = db;
    }

    [RelayCommand]
    private async Task Update()
    {
        await _db.UpdateClientAsync(Client);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task Delete()
    {
        await _db.DeleteClientAsync(Client);
        await Shell.Current.GoToAsync("..");
    }
}
