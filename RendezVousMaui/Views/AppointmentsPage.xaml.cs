using RendezVousMaui.ViewModels;

namespace RendezVousMaui.Views;

public partial class AppointmentsPage : ContentPage
{
    public AppointmentsPage(AppointmentsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is AppointmentsViewModel vm)
            await vm.LoadAppointments();
    }

    private async void OnSelect(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is AppointmentView rdv)
        {
            await (BindingContext as AppointmentsViewModel)
                .SelectAppointmentCommand.ExecuteAsync(rdv);
        }
    }
}
