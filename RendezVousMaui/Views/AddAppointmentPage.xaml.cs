using RendezVousMaui.ViewModels;

namespace RendezVousMaui.Views;

public partial class AddAppointmentPage : ContentPage
{
    public AddAppointmentPage(AddAppointmentViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
