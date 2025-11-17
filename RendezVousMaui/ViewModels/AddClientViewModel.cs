using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RendezVousMaui.Database;
using RendezVousMaui.Models;

namespace RendezVousMaui.ViewModels;

public partial class AddClientViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    [ObservableProperty] private string nom;
    [ObservableProperty] private string prenom;
    [ObservableProperty] private string telephone;
    [ObservableProperty] private string email;

    public AddClientViewModel(DatabaseService db)
    {
        _db = db;
    }

    [RelayCommand]
    private async Task Save()
    {
        var client = new Client
        {
            Nom = Nom,
            Prenom = Prenom,
            Telephone = Telephone,
            Email = Email
        };

        await _db.AddClientAsync(client);
        await Shell.Current.GoToAsync("..");
    }
}
