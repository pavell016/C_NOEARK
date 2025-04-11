using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace servidor
{
    class server
    {
        private static IPAddress IPServidor;
        private static Dictionary<int, TcpClient> players = new Dictionary<int, TcpClient>();
        static Random rand = new Random();

        public static void Main(String[] args)
        {
            IPServidor = IPAddress.Parse("127.0.0.1");
            int conn_server_port = 50000;
            TcpListener Servidor = new TcpListener(IPServidor, conn_server_port);
            Servidor.Start();
            Console.WriteLine("Servidor Creat");
            int players_in = 0;
            while (players_in < 500)
            {
                TcpClient client = Servidor.AcceptTcpClient();
                players_in++;
                players.Add(players_in, client);
                Console.WriteLine($"Player_ {players_in} has connected");
                //todo
                Thread newClient = new Thread(() => player(client, players_in));
                newClient.Start();
            }

        }
        public static void player(TcpClient client, int playerid)
        {
            NetworkStream ns = client.GetStream();
            while (true)
            {
                if (ns.DataAvailable)
                {
                    try
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRebuts = ns.Read(buffer, 0, buffer.Length);

                        // Print "player_<id> tira el dau"
                        Console.WriteLine($"Player_{playerid} tira el dau");

                        // Generate dice roll result
                        int diceResult = TirarDau();

                        // Relay the dice roll result to all players
                        relay(playerid, $"Player_{playerid}: {diceResult}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        Console.ReadLine();
                    }
                }
            }
        }


        public static void relay(int playerid, String message)
        {
            foreach (var player in players)
            {
                try
                {
                    NetworkStream ns = player.Value.GetStream();
                    byte[] messageBytes = Encoding.UTF8.GetBytes($"Player_{playerid} --> {message}");

                    ns.Write(messageBytes, 0, messageBytes.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to relay message to Player {player.Key}: {ex.Message}");
                }
            }
        }
        public static int TirarDau() {
            return rand.Next(1,7);
        }

    }
}