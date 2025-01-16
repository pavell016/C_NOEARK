using System.Net;
using System.Net.Sockets;

namespace server {
    class programa {
        static void Main(String[] args) {
            // (definicio socket)
            IPAddress MyIPAddress;
            MyIPAddress = IPAddress.Parse("127.0.0.1");
            int MyPort = 11000;

            //servidor
            TcpListener server = new TcpListener(MyIPAddress, MyPort);
            server.Start();
            Console.WriteLine("Server Iniciat");

            TcpClient Client = server.AcceptTcpClient();
            Console.ReadLine();
        }
    }
}
