namespace RendezVousMaui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("AddClientPage", typeof(RendezVousMaui.Views.AddClientPage));
        Routing.RegisterRoute("EditClientPage", typeof(RendezVousMaui.Views.EditClientPage));
        Routing.RegisterRoute("AddAppointmentPage", typeof(RendezVousMaui.Views.AddAppointmentPage));
        Routing.RegisterRoute("EditAppointmentPage", typeof(RendezVousMaui.Views.EditAppointmentPage));

    }
}
