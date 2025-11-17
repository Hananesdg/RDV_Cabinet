using RendezVousMaui.ViewModels;

namespace RendezVousMaui.Views;

public partial class AddClientPage : ContentPage
{
    public AddClientPage(AddClientViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
