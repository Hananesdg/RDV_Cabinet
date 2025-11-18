using RendezVousMaui.ViewModels;

namespace RendezVousMaui.Views;

public partial class DashboardPage : ContentPage
{
    private DashboardViewModel _vm;

    public DashboardPage(DashboardViewModel vm)
    {
        this.InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _vm.LoadStats();
        DrawChart();
    }

    private void DrawChart()
    {
        ChartGrid.Children.Clear();
        ChartGrid.ColumnDefinitions.Clear();

        int days = _vm.RdvWeekChart.Count;

        for (int i = 0; i < days; i++)
            ChartGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));

        int max = _vm.RdvWeekChart.Max();

        for (int c = 0; c < days; c++)
        {
            int value = _vm.RdvWeekChart[c];
            double height = max == 0 ? 0 : (value / (double)max) * 180;

            var bar = new BoxView
            {
                HeightRequest = height,
                VerticalOptions = LayoutOptions.End,
                Color = Color.FromArgb("#3E64FF"),
                CornerRadius = 5
            };

            ChartGrid.Add(bar, c, 0);
        }
    }
}
