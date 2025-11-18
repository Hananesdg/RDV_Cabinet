using Plugin.LocalNotification;

namespace RendezVousMaui.Services;

public class NotificationService
{
    public void ShowNotification(string title, string message)
    {
        var request = new NotificationRequest
        {
            NotificationId = new Random().Next(1000, 9999),
            Title = title,
            Description = message,
            ReturningData = "dummy",
            BadgeNumber = 1
        };

        LocalNotificationCenter.Current.Show(request);
    }

    public void ScheduleNotification(DateTime date, string title, string message)
    {
        var request = new NotificationRequest
        {
            NotificationId = new Random().Next(1000, 9999),
            Title = title,
            Description = message,
            Schedule =
            {
                NotifyTime = date
            },
            BadgeNumber = 1
        };

        LocalNotificationCenter.Current.Show(request);
    }
}
