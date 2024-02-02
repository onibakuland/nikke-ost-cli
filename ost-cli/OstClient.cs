using Grpc.Core;

namespace ost_cli;

internal class OstClient
{
    private readonly AudioControlService.AudioControlServiceClient _audioControlClient;
    private readonly MediaInfoService.MediaInfoServiceClient _mediaInfoClient;
    private readonly OstUpdateService.OstUpdateServiceClient _ostUpdateClient;
    private static bool _inputtingCommand;

    public OstClient(string host, int port)
    {
        var channel = new Channel(host, port, ChannelCredentials.Insecure);
        
        _audioControlClient = new AudioControlService.AudioControlServiceClient(channel);
        _mediaInfoClient = new MediaInfoService.MediaInfoServiceClient(channel);
        _ostUpdateClient = new OstUpdateService.OstUpdateServiceClient(channel);
    }

    private static async Task SendCommand(Func<AsyncUnaryCall<ServerResponse>> command)
    {
        try
        {
            var response = await command();
            Console.Write('\r' + response.Message);
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"RPC Exception: {ex.Status.Detail}");
        }
    }
    
    private static async Task Subscribe(Func<CancellationToken, AsyncServerStreamingCall<OstUpdateEvent>> subscribeMethod)
    {
        try
        {
            var cancellationTokenSource = new CancellationTokenSource();
            using var call = subscribeMethod(cancellationTokenSource.Token);
            // Read updates from the server stream
            while (await call.ResponseStream.MoveNext())
            {
                var update = call.ResponseStream.Current;
                if (_inputtingCommand) continue;
                if (update.SongInfo == null) continue;
                var currentTime = update.TotalTime * update.CurrentProgress;
                var currentTimeText = TimeSpan.FromSeconds(currentTime).ToString("mm\\:ss");
                var totalTimeText = TimeSpan.FromSeconds(update.TotalTime).ToString("mm\\:ss");
                Console.Write($"\rPlaying: {update.SongInfo.Title} - {currentTimeText}/{totalTimeText}");
            }
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"Error: {ex.Status.StatusCode} - {ex.Status.Detail}");
        }
    }
    
    public Task Exit() => SendCommand( () =>  _audioControlClient.ExitAsync(new ExitCommand()) );
    public Task Play() => SendCommand( () => _audioControlClient.PlayAsync(new PlayCommand()) );
    public Task Stop() => SendCommand( () => _audioControlClient.PauseAsync(new PauseCommand()) );
    public Task NextSong() => SendCommand( () => _audioControlClient.NextSongAsync(new NextSongCommand()) );
    public Task NextSonglet() => SendCommand( () => _audioControlClient.NextSongletAsync(new NextSongletCommand()) );
    public Task NextClip() => SendCommand( () => _audioControlClient.NextClipAsync(new NextClipCommand()) );
    public Task PrevSong() => SendCommand( () => _audioControlClient.PrevSongAsync(new PrevSongCommand()) );
    public Task PrevSonglet() => SendCommand( () => _audioControlClient.PrevSongletAsync(new PrevSongletCommand()) );
    public Task PrevClip() => SendCommand( () => _audioControlClient.PrevClipAsync(new PrevClipCommand()) );
    public Task SetVolume(float volume) => SendCommand(() => _audioControlClient.SetVolumeAsync(new SetVolumeCommand { Percent = volume }));
    public Task SetTimeScrub(float percent) => SendCommand(() => _audioControlClient.SetVolumeAsync(new SetVolumeCommand { Percent = percent }));
    public Task SetAlbum(int index) => SendCommand(() => _audioControlClient.SetAlbumAsync(new SetAlbumCommand() { AlbumId = index }));
    public Task SetSong(int index) => SendCommand(() => _audioControlClient.SetSongAsync(new SetSongCommand() { SongId = index }));
    public Task SetSonglet(int index) => SendCommand(() => _audioControlClient.SetSongletAsync(new SetSongletCommand() { SongletId = index }));
    public Task SetClip(int index) => SendCommand(() => _audioControlClient.SetClipAsync(new SetClipCommand() { ClipId = index }));
    public async Task QueryOstInfo()
    {
        try
        {
            var response = await _mediaInfoClient.GetOstInfoAsync(new Empty());
            foreach (var album in response.Albums)
                Console.WriteLine($"{album.AlbumId} \t- {album.Title}");
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"RPC Exception: {ex.Status.Detail}");
        }
    }

    public async Task QueryAlbumInfo(int index)
    {
        try
        {
            var response = await _mediaInfoClient.GetAlbumInfoAsync(new AlbumInfoRequest { AlbumId = index });
            Console.WriteLine($"Title: {response.AlbumInfo.Title}");
            Console.WriteLine($"Quote: {response.AlbumInfo.Quote}");
            foreach (var song in response.Songs)
                Console.WriteLine($"{song.SongId} \t- {song.Title} - {song.Artist}");
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"RPC Exception: {ex.Status.Detail}");
        }
    }
    
    public async Task QuerySongInfo(int index)
    {
        try
        {
            var response = await _mediaInfoClient.GetSongInfoAsync(new SongInfoRequest { SongId = index });
            Console.WriteLine($"Title: {response.SongInfo.Title}");
            Console.WriteLine($"Artist: {response.SongInfo.Artist}");
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"RPC Exception: {ex.Status.Detail}");
        }
    }

    public async void SubscribeToUpdates() => await Subscribe((cts) =>
            _ostUpdateClient.SubscribeToUpdates(new Empty(), cancellationToken: cts));

    public static void SetInputtingCommand(bool b)
    {
        _inputtingCommand = b;
    }
}