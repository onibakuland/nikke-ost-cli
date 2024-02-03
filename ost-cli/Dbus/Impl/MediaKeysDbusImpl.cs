using System.Runtime.InteropServices;
using Tmds.DBus;

namespace ost_cli.Dbus.Impl;

public class MediaKeysDbusImpl
{
    private static IntPtr _display;
    private static IntPtr _rootWindow;

    public static void Initialize()
    {
        _display = XOpenDisplay(IntPtr.Zero);
        if (_display == IntPtr.Zero)
        {
            Console.WriteLine("Failed to open X display.");
            return;
        }
        _rootWindow = XDefaultRootWindow(_display);
        var i = XSelectInput(_display, _rootWindow, EventMask.KeyPressMask);
    }

    private static void KeyPressed(int key)
    {
        Console.WriteLine($"Media key pressed: {key}");

        switch (key)
        {
            case 186:
                Console.WriteLine("Stop key pressed");
                // Add your logic for handling the "stop" media key press event
                break;

            case 184:
                Console.WriteLine("Previous key pressed");
                // Add your logic for handling the "previous" media key press event
                break;

            case 180:
                Console.WriteLine("Play/Pause key pressed");
                // Add your logic for handling the "play/pause" media key press event
                break;

            case 153:
                Console.WriteLine("Next key pressed");
                // Add your logic for handling the "next" media key press event
                break;

            // Add cases for other media key values if needed

            default:
                // Handle other media keys or unknown keys
                Console.WriteLine($"Unknown media key pressed: {key}");
                break;
        }
    }

    public static void CheckXEvent()
    {
        var e = XNextEvent(_display, out var xEvent);

        if (xEvent.type != XEventType.KeyPress) return;
        
        var keyEvent = (XKeyEvent)(Marshal.PtrToStructure(xEvent.xkey, typeof(XKeyEvent)) ?? throw new InvalidOperationException());
        KeyPressed(keyEvent.keycode);
        // Check for the specific key codes
        switch (keyEvent.keycode)
        {
            case (int)KeyCode.XK_Stop:
                Console.WriteLine("Stop key pressed");
                break;
            case (int)KeyCode.XK_Play:
                Console.WriteLine("Play key pressed");
                break;
            case (int)KeyCode.XK_Next:
                Console.WriteLine("Next key pressed");
                break;
            case (int)KeyCode.XK_Prev:
                Console.WriteLine("Previous key pressed");
                break;
        }
    }
        
    #region X11 Functions

    [DllImport("libX11")]
    public static extern IntPtr XOpenDisplay(IntPtr display_name);

    [DllImport("libX11")]
    public static extern IntPtr XDefaultRootWindow(IntPtr display);

    [DllImport("libX11")]
    public static extern int XSelectInput(IntPtr display, IntPtr window, EventMask event_mask);

    [DllImport("libX11")]
    public static extern int XNextEvent(IntPtr display, out XEvent event_return);

    #endregion

    #region X11 Structures

    [StructLayout(LayoutKind.Sequential)]
    public struct XEvent
    {
        public XEventType type;
        public IntPtr xany;
        public IntPtr xkey;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct XKeyEvent
    {
        public XEventType type;
        public IntPtr serial;
        public int send_event;
        public IntPtr display;
        public IntPtr window;
        public IntPtr root;
        public IntPtr subwindow;
        public IntPtr time;
        public int x;
        public int y;
        public int x_root;
        public int y_root;
        public int state;
        public int keycode;
        public int same_screen;
    }

    public enum XEventType
    {
        KeyPress = 2,
        KeyRelease = 3,
        ButtonPress = 4,
        ButtonRelease = 5,
        MotionNotify = 6,
        EnterNotify = 7,
        LeaveNotify = 8,
        FocusIn = 9,
        FocusOut = 10,
        KeymapNotify = 11,
        Expose = 12,
        GraphicsExpose = 13,
        NoExpose = 14,
        VisibilityNotify = 15,
        CreateNotify = 16,
        DestroyNotify = 17,
        UnmapNotify = 18,
        MapNotify = 19,
        MapRequest = 20,
        ReparentNotify = 21,
        ConfigureNotify = 22,
        ConfigureRequest = 23,
        GravityNotify = 24,
        ResizeRequest = 25,
        CirculateNotify = 26,
        CirculateRequest = 27,
        PropertyNotify = 28,
        SelectionClear = 29,
        SelectionRequest = 30,
        SelectionNotify = 31,
        ColormapNotify = 32,
        ClientMessage = 33,
        MappingNotify = 34,
        GenericEvent = 35,
        LASTEvent = 36
    }

    public enum EventMask
    {
        KeyPressMask = (1 << 0),
        KeyReleaseMask = (1 << 1),
        ButtonPressMask = (1 << 2),
        ButtonReleaseMask = (1 << 3),
        EnterWindowMask = (1 << 4),
        LeaveWindowMask = (1 << 5),
        PointerMotionMask = (1 << 6),
        PointerMotionHintMask = (1 << 7),
        Button1MotionMask = (1 << 8),
        Button2MotionMask = (1 << 9),
        Button3MotionMask = (1 << 10),
        Button4MotionMask = (1 << 11),
        Button5MotionMask = (1 << 12),
        ButtonMotionMask = (1 << 13),
        KeymapStateMask = (1 << 14),
        ExposureMask = (1 << 15),
        VisibilityChangeMask = (1 << 16),
        StructureNotifyMask = (1 << 17),
        ResizeRedirectMask = (1 << 18),
        SubstructureNotifyMask = (1 << 19),
        SubstructureRedirectMask = (1 << 20),
        FocusChangeMask = (1 << 21),
        PropertyChangeMask = (1 << 22),
        ColormapChangeMask = (1 << 23),
        OwnerGrabButtonMask = (1 << 24)
    }

    public enum KeyCode
    {
        XK_Stop = 184,
        XK_Play = 234,
        XK_Next = 345,
        XK_Prev = 4
        // Replace YOUR_KEYCODE_HERE with actual X11 keycodes for Stop, Play, Next, Previous
    }

    #endregion
}