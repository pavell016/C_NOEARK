using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace parchis
{
    class Program
    {
        static void Main(string[] args)
        {
            // Definición del servidor al que conectarse
            IPAddress direccio_server = IPAddress.Parse("127.0.0.1");
            int server_port = 50000;

            // Conexión al servidor
            TcpClient client = new TcpClient();
            client.Connect(direccio_server, server_port);
            NetworkStream ns = client.GetStream();
            Console.WriteLine("Conexión Establecida\n\n");

            // Recibir datos del servidor
            byte[] receiveBuffer = new byte[1024];
            int bytesRead = ns.Read(receiveBuffer, 0, receiveBuffer.Length);
            string jsonString = Encoding.UTF8.GetString(receiveBuffer, 0, bytesRead); // Corrección aquí

            // Convertir JSON a objeto
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true  // Add this line
                };

                Player player = JsonSerializer.Deserialize<Player>(jsonString, options);
                //Player player = JsonSerializer.Deserialize<Player>(jsonString);
                // Mostrar información del jugador
                Console.WriteLine($"Welcome Player {player.PlayerID}");
                Console.WriteLine($"Your player color is: {player.PlayerColor}");
    
                Console.ReadLine();
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message.ToString());
            }
            

            
        }
    }
}
