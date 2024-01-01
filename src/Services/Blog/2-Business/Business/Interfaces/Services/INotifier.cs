using Business.Services.Notifications;

namespace Business.Interfaces.Services;

public interface INotifier
{
    bool HasNotification();
    List<Notification> GetNotifications();
    void Handle(Notification notification);
}