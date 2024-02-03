// Copyright 2006 Alp Toker <alp@atoker.com>
// This software is made available under the MIT License
// See COPYING for details

using DBus;
using ost_cli.Dbus;

// Hand-written interfaces for bootstrapping

namespace org.freedesktop
{
    public struct ServerInformation
    {
        public string Name;
        public string Vendor;
        public string Version;
        public string SpecVersion;
    }
    
    public class NotificationDbusImpl
    {
        public static void Initialize(Bus bus)
        {
            var nf = bus.GetObject<INotificationsDbus> ("org.freedesktop.Notifications", new ObjectPath ("/org/freedesktop/Notifications"));

            Console.WriteLine ();
            Console.WriteLine ("Capabilities:");
            foreach (var cap in nf.GetCapabilities ())
                Console.WriteLine ("\t" + cap);

            var si = nf.GetServerInformation ();

            //TODO: ability to pass null
            var hints = new Dictionary<string,object> ();

            var message = String.Format ("Brought to you using {0} {1} (implementing spec version {2}) from {3}.", si.Name, si.Version, si.SpecVersion, si.Vendor);

            var handle = nf.Notify ("D-Bus# Notifications Demo", 0, "warning", "Managed D-Bus# says 'Hello'!", message, new string[0], hints, 0);

            Console.WriteLine ();
            Console.WriteLine ("Got handle " + handle);
        }
    }
}