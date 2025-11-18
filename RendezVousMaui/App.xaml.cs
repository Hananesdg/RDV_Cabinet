using RendezVousMaui.Database;

namespace RendezVousMaui;

public partial class App : Application
{
    private readonly DatabaseService _db;

    public App(DatabaseService db)
    {
        InitializeComponent();
        _db = db;
    }

    // MAUI .NET 9 → CreateWindow = point d’entrée réel
    protected override Window CreateWindow(IActivationState? activationState)
    {
        // Initialiser la base AVANT d’afficher l’UI
        Task.Run(async () => await _db.InitAsync()).Wait();

        return new Window(new AppShell());
    }
}
