using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress serverIPAddress = IPAddress.Parse("127.0.0.1");
            int serverPort = 11000;

            // Connect to the server
            TcpClient client = new TcpClient();
            client.Connect(serverIPAddress, serverPort);
            Console.WriteLine("Connected to server!");

            // Get the network stream for communication
            NetworkStream ns = client.GetStream();

            // Read the first handshake from the server
            byte[] buffer = new byte[256];
            int bytesRead = ns.Read(buffer, 0, buffer.Length);
            string serverMessage = Encoding.Unicode.GetString(buffer, 0, bytesRead);
            Console.WriteLine(serverMessage);

            // Respond to the server
            byte[] secondHandshakeBytes = Encoding.Unicode.GetBytes("Yes, I am here.");
            ns.Write(secondHandshakeBytes, 0, secondHandshakeBytes.Length);
            Console.WriteLine(secondHandshakeBytes);

            // Read the third handshake from the server
            bytesRead = ns.Read(buffer, 0, buffer.Length);
            string finalMessage = Encoding.Unicode.GetString(buffer, 0, bytesRead);
            Console.WriteLine(finalMessage);

            // Clean up resources
            ns.Close();
            client.Close();
            Console.WriteLine("Client disconnected.");
        }
    }
}
