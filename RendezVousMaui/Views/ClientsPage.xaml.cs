using RendezVousMaui.ViewModels;
using RendezVousMaui.Models;

namespace RendezVousMaui.Views;

public partial class ClientsPage : ContentPage
{
    private ClientsViewModel ViewModel => (ClientsViewModel)BindingContext;

    public ClientsPage(ClientsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ViewModel.LoadClients();
    }

    private void OnSelectClient(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Client client)
        {
            ViewModel.SelectClientCommand.Execute(client);
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
