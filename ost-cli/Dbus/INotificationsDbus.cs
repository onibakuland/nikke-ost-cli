using DBus;
using org.freedesktop;
using org.freedesktop.DBus;

namespace ost_cli.Dbus;

[Interface ("org.freedesktop.Notifications")]
public interface INotificationsDbus : Introspectable, Properties
{
    ServerInformation GetServerInformation ();
    string[] GetCapabilities ();
    void CloseNotification (uint id);
    uint Notify (string app_name, uint id, string icon, string summary, string body, string[] actions, IDictionary<string,object> hints, int timeout);
}
