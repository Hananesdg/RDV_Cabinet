using RendezVousMaui.ViewModels;

namespace RendezVousMaui.Views;

public partial class AgendaPage : ContentPage
{
    private AgendaViewModel _vm;

    public AgendaPage(AgendaViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _vm.LoadDay();
        await _vm.LoadWeek();

        BuildWeekGrid();
    }

    private void BuildWeekGrid()
    {
        WeekGrid.RowDefinitions.Clear();
        WeekGrid.ColumnDefinitions.Clear();
        WeekGrid.Children.Clear();

        int rows = _vm.Creneaux.Count;
        int cols = 7;

        // Colonnes = jours
        for (int c = 0; c < cols; c++)
            WeekGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));

        // Lignes = créneaux
        for (int r = 0; r < rows; r++)
            WeekGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));

        // Cases
        for (int c = 0; c < cols; c++)
        {
            for (int r = 0; r < rows; r++)
            {
                var slot = _vm.GetWeekSlot(r, c);

                var frame = new Frame
                {
                    Padding = 4,
                    Margin = 1,
                    CornerRadius = 5,
                    BackgroundColor = slot.IsBusy ? Colors.Red : Colors.White,
                    Content = new Label
                    {
                        Text = slot.Text,
                        FontSize = 13,
                        TextColor = Colors.Black
                    }
                };

                var tap = new TapGestureRecognizer();
                tap.Tapped += async (s, e) => await _vm.OnSlotSelected(slot);
                frame.GestureRecognizers.Add(tap);

                WeekGrid.Add(frame, c, r);
            }
        }
    }
}
