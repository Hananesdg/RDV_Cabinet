using RendezVousMaui.ViewModels;

namespace RendezVousMaui.Views;

public partial class EditClientPage : ContentPage
{
    public EditClientPage(EditClientViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
