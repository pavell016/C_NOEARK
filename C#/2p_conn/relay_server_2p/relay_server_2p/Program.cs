using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace servidor {
    class server {
        private static IPAddress IPServidor;
        private static Dictionary<int, TcpClient> players = new Dictionary<int, TcpClient>();
        public static void Main(String[] args) { 
            IPServidor = IPAddress.Parse("127.0.0.1");
            int conn_server_port = 50000;
            TcpListener Servidor = new TcpListener(IPServidor,conn_server_port);
            Servidor.Start();
            Console.WriteLine("Servidor Creat");
            int players_in = 0;
            while (players_in < 2) { 
                TcpClient client = Servidor.AcceptTcpClient();
                players_in++;
                players.Add(players_in,client);
                Console.WriteLine($"Player {players_in} has connected to the game");
                //todo
                Thread newClient = new Thread(() => player(client,players_in));
                newClient.Start();
            }

        }
        public static void player(TcpClient client, int playerid) {
            NetworkStream ns = client.GetStream();
            to_player(playerid, $"Wellecome player {playerid}");
            while (true) {
                try{
                    byte[] buffer = new byte[1024];

                    int bytesRebuts = ns.Read(buffer, 0, buffer.Length);
                    relay(playerid, Encoding.UTF8.GetString(buffer, 0, bytesRebuts));
                    
                }catch(Exception ex){
                    Console.WriteLine(ex.ToString());
                    Console.ReadLine();

                }
                
            }
        }

        public static void relay(int playerid, String message) {
            foreach (var player in players) {
                
                if (player.Key != playerid) {
                    try
                    {
                        NetworkStream ns = player.Value.GetStream();
                        byte[] messageBytes = Encoding.UTF8.GetBytes($"Player {playerid} said : {message}");
                        
                        ns.Write(messageBytes, 0, messageBytes.Length);
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to relay message to Player {player.Key}: {ex.Message}");
                    }
                }
            }
        }

        public static void to_player(int playerid, String message) { 
            foreach (var player in players)
            {
                if (player.Key == playerid)
                {
                    try
                    {
                        NetworkStream ns = player.Value.GetStream();
                        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                        ns.Write(messageBytes, 0, messageBytes.Length);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to send message to Player {player.Key}: {ex.Message}");
                    }
                }
            }

        }
    }
}