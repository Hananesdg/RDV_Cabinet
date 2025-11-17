using RendezVousMaui.Database;

namespace RendezVousMaui;

public partial class App : Application
{
    private readonly DatabaseService _db;

    public App(DatabaseService db)
    {
        InitializeComponent();
        _db = db;
        InitDatabase();
    }

    private async void InitDatabase()
    {
        await _db.InitAsync();
    }

    // MAUI .NET 9 → CreateWindow obligatoire
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
