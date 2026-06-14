using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

const int RemoteDebuggingPort = 9223;
const string PrimeVideoUrl = "https://www.primevideo.com/";
const string ScriptFileName = "speed-control.js";

var edgePath = FindEdgePath();
if (edgePath is null)
{
    Console.Error.WriteLine("Microsoft Edge could not be found. Please install Microsoft Edge and try again.");
    return 1;
}

Console.WriteLine("Starting Prime Video Speed Controller...");
Console.WriteLine("Prime Video will open in a dedicated Microsoft Edge app window.");
Console.WriteLine("The speed control appears only when the video player is available.");
Console.WriteLine("Close this console window to stop the helper.");

StartEdge(edgePath);

using var httpClient = new HttpClient();
var script = LoadInjectionScript();

while (true)
{
    try
    {
        var targets = await GetTargets(httpClient);
        foreach (var target in targets)
        {
            if (IsPrimeVideoTarget(target) && target.WebSocketDebuggerUrl is not null)
            {
                await InjectSpeedControl(target.WebSocketDebuggerUrl, script);
            }
        }
    }
    catch (HttpRequestException)
    {
        // Edge can take a moment to expose the local debugging endpoint.
    }
    catch (WebSocketException)
    {
        // Prime Video can navigate while the script is being injected; the next poll retries.
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected error: {ex.Message}");
    }

    await Task.Delay(TimeSpan.FromSeconds(2));
}

static string? FindEdgePath()
{
    string[] candidates =
    [
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Microsoft", "Edge", "Application", "msedge.exe"),
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Microsoft", "Edge", "Application", "msedge.exe"),
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft", "Edge", "Application", "msedge.exe")
    ];

    return candidates.FirstOrDefault(File.Exists);
}

static string LoadInjectionScript()
{
    var scriptPath = Path.Combine(AppContext.BaseDirectory, ScriptFileName);
    if (!File.Exists(scriptPath))
    {
        throw new FileNotFoundException($"Required script file was not found: {scriptPath}");
    }

    return File.ReadAllText(scriptPath, Encoding.UTF8);
}

static void StartEdge(string edgePath)
{
    var profileDir = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "PrimeVideoSpeedController",
        "EdgeProfile");

    Directory.CreateDirectory(profileDir);

    var arguments = string.Join(
        " ",
        $"--remote-debugging-port={RemoteDebuggingPort}",
        $"--user-data-dir=\"{profileDir}\"",
        "--no-first-run",
        "--new-window",
        $"--app=\"{PrimeVideoUrl}\"");

    Process.Start(new ProcessStartInfo
    {
        FileName = edgePath,
        Arguments = arguments,
        UseShellExecute = false
    });
}

static async Task<List<DebugTarget>> GetTargets(HttpClient httpClient)
{
    using var stream = await httpClient.GetStreamAsync($"http://127.0.0.1:{RemoteDebuggingPort}/json");
    var targets = await JsonSerializer.DeserializeAsync<List<DebugTarget>>(stream, JsonOptions());
    return targets ?? [];
}

static bool IsPrimeVideoTarget(DebugTarget target)
{
    if (!string.Equals(target.Type, "page", StringComparison.OrdinalIgnoreCase))
    {
        return false;
    }

    return target.Url.Contains("primevideo.com", StringComparison.OrdinalIgnoreCase)
        || target.Url.Contains("amazon.com/gp/video", StringComparison.OrdinalIgnoreCase)
        || target.Url.Contains("amazon.com.tr/gp/video", StringComparison.OrdinalIgnoreCase)
        || target.Url.Contains("amazon.co.uk/gp/video", StringComparison.OrdinalIgnoreCase)
        || target.Url.Contains("amazon.de/gp/video", StringComparison.OrdinalIgnoreCase)
        || target.Url.Contains("amazon.fr/gp/video", StringComparison.OrdinalIgnoreCase)
        || target.Url.Contains("amazon.it/gp/video", StringComparison.OrdinalIgnoreCase)
        || target.Url.Contains("amazon.es/gp/video", StringComparison.OrdinalIgnoreCase);
}

static async Task InjectSpeedControl(string webSocketDebuggerUrl, string script)
{
    using var socket = new ClientWebSocket();
    await socket.ConnectAsync(new Uri(webSocketDebuggerUrl), CancellationToken.None);

    var payload = JsonSerializer.Serialize(new
    {
        id = 1,
        method = "Runtime.evaluate",
        @params = new
        {
            expression = script,
            awaitPromise = false,
            returnByValue = true
        }
    });

    var bytes = Encoding.UTF8.GetBytes(payload);
    await socket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);

    var buffer = new byte[4096];
    await socket.ReceiveAsync(buffer, CancellationToken.None);
}

static JsonSerializerOptions JsonOptions()
{
    return new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };
}

internal sealed class DebugTarget
{
    public string Id { get; set; } = "";
    public string Type { get; set; } = "";
    public string Url { get; set; } = "";
    public string? WebSocketDebuggerUrl { get; set; }
}
