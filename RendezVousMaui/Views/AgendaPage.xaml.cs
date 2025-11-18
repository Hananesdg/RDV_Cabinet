using RendezVousMaui.ViewModels;

namespace RendezVousMaui.Views;

public partial class AgendaPage : ContentPage
{
    public AgendaPage(AgendaViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
