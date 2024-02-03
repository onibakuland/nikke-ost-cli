
using Tmds.DBus;

namespace ost_cli.Dbus
{
    [DBusInterface("org.gnome.SettingsDaemon.MediaKeys")]
    public interface IMediaKeysDbus : IDBusObject
    {
        void HandleMediaKey(string key);
    }
}