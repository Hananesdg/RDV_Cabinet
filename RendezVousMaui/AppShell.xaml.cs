namespace RendezVousMaui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("AddClientPage", typeof(RendezVousMaui.Views.AddClientPage));
        Routing.RegisterRoute("EditClientPage", typeof(RendezVousMaui.Views.EditClientPage));
    }
}
