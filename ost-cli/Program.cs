using System.CommandLine;

namespace ost_cli;

public static class Program
{
    private static Task<int> Main(string[] args)
    {
        var hostOption = new Option<string>(
            aliases: new[] { "--host" },
            description: "The host to connect.",
            getDefaultValue: () => "localhost");

        var portOption = new Option<int>(
            aliases: new[] { "--port" },
            description: "The port to connect.",
            getDefaultValue: () => 50051);

        var rootCommand = new RootCommand("OST-WE Controller CLI for Nikke.");
        rootCommand.AddOption(hostOption);
        rootCommand.AddOption(portOption);


        rootCommand.SetHandler((host, port) =>
        {
            Console.WriteLine($"Starting ost-we cli grpc client with Host: {host} Port: {port}");
            StartInteractiveCLI( new OstClient(host, port));
        }, hostOption, portOption);

        return rootCommand.InvokeAsync(args);
    }
    
    private static void StartInteractiveCLI(OstClient client)
    {
        client?.SubscribeToUpdates();
        while (true)
        {
            if (!Console.KeyAvailable) continue;
            var key = Console.ReadKey(intercept: true).Key;
            switch (key)
            {
                case ConsoleKey.Backspace:
                    break;
                case ConsoleKey.Tab:
                    break;
                case ConsoleKey.Clear:
                    break;
                case ConsoleKey.Enter:
                    break;
                case ConsoleKey.Pause:
                    break;
                case ConsoleKey.Escape:
                    break;
                case ConsoleKey.Spacebar:
                    break;
                case ConsoleKey.PageUp:
                    break;
                case ConsoleKey.PageDown:
                    break;
                case ConsoleKey.End:
                    break;
                case ConsoleKey.Home:
                    break;
                case ConsoleKey.LeftArrow:
                    break;
                case ConsoleKey.UpArrow:
                    break;
                case ConsoleKey.RightArrow:
                    break;
                case ConsoleKey.DownArrow:
                    break;
                case ConsoleKey.Select:
                    break;
                case ConsoleKey.Print:
                    break;
                case ConsoleKey.Execute:
                    break;
                case ConsoleKey.PrintScreen:
                    break;
                case ConsoleKey.Insert:
                    break;
                case ConsoleKey.Delete:
                    break;
                case ConsoleKey.Help:
                    break;
                case ConsoleKey.D0:
                    break;
                case ConsoleKey.D1:
                    break;
                case ConsoleKey.D2:
                    break;
                case ConsoleKey.D3:
                    break;
                case ConsoleKey.D4:
                    break;
                case ConsoleKey.D5:
                    break;
                case ConsoleKey.D6:
                    break;
                case ConsoleKey.D7:
                    break;
                case ConsoleKey.D8:
                    break;
                case ConsoleKey.D9:
                    break;
                case ConsoleKey.A:
                    break;
                case ConsoleKey.B:
                    client?.PrevClip(); break;
                case ConsoleKey.C:
                    if (client != null)
                    {
                        OstClient.SetInputtingCommand(true);
                        HandleCommand(client);
                    }
                    break;
                case ConsoleKey.D:
                    break;
                case ConsoleKey.E:
                    break;
                case ConsoleKey.F:
                    break;
                case ConsoleKey.G:
                    break;
                case ConsoleKey.H:
                    break;
                case ConsoleKey.I:
                    break;
                case ConsoleKey.J:
                    break;
                case ConsoleKey.K:
                    break;
                case ConsoleKey.L:
                    break;
                case ConsoleKey.M:
                    break;
                case ConsoleKey.N:
                    client?.NextSonglet(); break;
                case ConsoleKey.O:
                    break;
                case ConsoleKey.P:
                    client?.Play(); break;
                case ConsoleKey.Q:
                    client?.Exit();
                    return;
                case ConsoleKey.R:
                    break;
                case ConsoleKey.S:
                    client?.Stop(); break;
                case ConsoleKey.T:
                    break;
                case ConsoleKey.U:
                    break;
                case ConsoleKey.V:
                    break;
                case ConsoleKey.W:
                    break;
                case ConsoleKey.X:
                    break;
                case ConsoleKey.Y:
                    break;
                case ConsoleKey.Z:
                    break;
                case ConsoleKey.LeftWindows:
                    break;
                case ConsoleKey.RightWindows:
                    break;
                case ConsoleKey.Applications:
                    break;
                case ConsoleKey.Sleep:
                    break;
                case ConsoleKey.NumPad0:
                    break;
                case ConsoleKey.NumPad1:
                    break;
                case ConsoleKey.NumPad2:
                    break;
                case ConsoleKey.NumPad3:
                    break;
                case ConsoleKey.NumPad4:
                    break;
                case ConsoleKey.NumPad5:
                    break;
                case ConsoleKey.NumPad6:
                    break;
                case ConsoleKey.NumPad7:
                    break;
                case ConsoleKey.NumPad8:
                    break;
                case ConsoleKey.NumPad9:
                    break;
                case ConsoleKey.Multiply:
                    break;
                case ConsoleKey.Add:
                    break;
                case ConsoleKey.Separator:
                    break;
                case ConsoleKey.Subtract:
                    break;
                case ConsoleKey.Decimal:
                    break;
                case ConsoleKey.Divide:
                    break;
                case ConsoleKey.F1:
                    break;
                case ConsoleKey.F2:
                    break;
                case ConsoleKey.F3:
                    break;
                case ConsoleKey.F4:
                    break;
                case ConsoleKey.F5:
                    break;
                case ConsoleKey.F6:
                    break;
                case ConsoleKey.F7:
                    break;
                case ConsoleKey.F8:
                    break;
                case ConsoleKey.F9:
                    break;
                case ConsoleKey.F10:
                    break;
                case ConsoleKey.F11:
                    break;
                case ConsoleKey.F12:
                    break;
                case ConsoleKey.F13:
                    break;
                case ConsoleKey.F14:
                    break;
                case ConsoleKey.F15:
                    break;
                case ConsoleKey.F16:
                    break;
                case ConsoleKey.F17:
                    break;
                case ConsoleKey.F18:
                    break;
                case ConsoleKey.F19:
                    break;
                case ConsoleKey.F20:
                    break;
                case ConsoleKey.F21:
                    break;
                case ConsoleKey.F22:
                    break;
                case ConsoleKey.F23:
                    break;
                case ConsoleKey.F24:
                    break;
                case ConsoleKey.BrowserBack:
                    break;
                case ConsoleKey.BrowserForward:
                    break;
                case ConsoleKey.BrowserRefresh:
                    break;
                case ConsoleKey.BrowserStop:
                    break;
                case ConsoleKey.BrowserSearch:
                    break;
                case ConsoleKey.BrowserFavorites:
                    break;
                case ConsoleKey.BrowserHome:
                    break;
                case ConsoleKey.VolumeMute:
                    break;
                case ConsoleKey.VolumeDown:
                    break;
                case ConsoleKey.VolumeUp:
                    break;
                case ConsoleKey.MediaNext:
                    client?.NextSong(); break;
                case ConsoleKey.MediaPrevious:
                    client?.PrevSong(); break;
                case ConsoleKey.MediaStop:
                    client?.Stop(); break;
                case ConsoleKey.MediaPlay:
                    client?.Play(); break;
                case ConsoleKey.LaunchMail:
                    break;
                case ConsoleKey.LaunchMediaSelect:
                    break;
                case ConsoleKey.LaunchApp1:
                    break;
                case ConsoleKey.LaunchApp2:
                    break;
                case ConsoleKey.Oem1:
                    break;
                case ConsoleKey.OemPlus:
                    break;
                case ConsoleKey.OemComma:
                    break;
                case ConsoleKey.OemMinus:
                    break;
                case ConsoleKey.OemPeriod:
                    break;
                case ConsoleKey.Oem2:
                    break;
                case ConsoleKey.Oem3:
                    break;
                case ConsoleKey.Oem4:
                    break;
                case ConsoleKey.Oem5:
                    break;
                case ConsoleKey.Oem6:
                    break;
                case ConsoleKey.Oem7:
                    break;
                case ConsoleKey.Oem8:
                    break;
                case ConsoleKey.Oem102:
                    break;
                case ConsoleKey.Process:
                    break;
                case ConsoleKey.Packet:
                    break;
                case ConsoleKey.Attention:
                    break;
                case ConsoleKey.CrSel:
                    break;
                case ConsoleKey.ExSel:
                    break;
                case ConsoleKey.EraseEndOfFile:
                    break;
                case ConsoleKey.Play:
                    break;
                case ConsoleKey.Zoom:
                    break;
                case ConsoleKey.NoName:
                    break;
                case ConsoleKey.Pa1:
                    break;
                case ConsoleKey.OemClear:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private static void HandleCommand(OstClient client)
    {
        Console.Clear();
        Console.Write("\r Enter command:");
        var command = Console.ReadLine();
        var elements = command?.Split(' ');
        int index;
        switch (elements?[0])
        {
            case "volume":
                if (float.TryParse(elements[1], out var volume));
                    client?.SetVolume(volume);
                break;
            case "scrub":
                if (float.TryParse(elements[1], out var percent));
                client?.SetTimeScrub(percent);
                break;
            case "s":
                switch (elements[1])
                {
                    case "album":
                        if (int.TryParse(elements[2], out index));
                        client?.SetAlbum(index);
                        break;
                    case "song":
                        if (int.TryParse(elements[2], out index));
                        client?.SetSong(index);
                        break;
                    case "songlet":
                        if (int.TryParse(elements[2], out index));
                        client?.SetSonglet(index);
                        break;
                    case "clip":
                        if (int.TryParse(elements[2], out index));
                        client?.SetClip(index);
                        break;
                }
                break;
            case "q":
                switch (elements[1])
                {
                    case "ost":
                        client?.QueryOstInfo();
                        break;
                    case "album":
                        if (int.TryParse(elements[2], out index));
                        client?.QueryAlbumInfo(index);
                        break;
                    case "song":
                        if (int.TryParse(elements[2], out index));
                        client?.QuerySongInfo(index);
                        break;
                }
                break;

        }
        OstClient.SetInputtingCommand(false);
    }
}