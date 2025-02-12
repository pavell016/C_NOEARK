using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress myIPAddress = IPAddress.Parse("127.0.0.1");
            int myPort = 11000;

            // Initialize the server
            TcpListener server = new TcpListener(myIPAddress, myPort);
            server.Start();
            Console.WriteLine("Server started. Waiting for a connection...");

            // Accept a client connection
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Client connected!");

            // Get the network stream for communication
            NetworkStream ns = client.GetStream();

            // First handshake: Send a message to the client
            byte[] firstHandshakeBytes = Encoding.Unicode.GetBytes("Client, are you there?");
            ns.Write(firstHandshakeBytes, 0, firstHandshakeBytes.Length);
            Console.WriteLine(firstHandshakeBytes);

            // Read the client's response
            byte[] buffer = new byte[256];
            int bytesRead = ns.Read(buffer, 0, buffer.Length);
            string clientResponse = Encoding.Unicode.GetString(buffer, 0, bytesRead);
            Console.WriteLine(clientResponse);

            // Third handshake: Send a final message to the client
            byte[] thirdHandshakeBytes = Encoding.Unicode.GetBytes("good");
            ns.Write(thirdHandshakeBytes, 0, thirdHandshakeBytes.Length);
           

            // Clean up resources
            ns.Close();
            client.Close();
            server.Stop();
            Console.WriteLine("Server stopped.");
        }
    }
}
