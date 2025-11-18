using RendezVousMaui.ViewModels;

namespace RendezVousMaui.Views;

public partial class EditAppointmentPage : ContentPage
{
    public EditAppointmentPage(EditAppointmentViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
