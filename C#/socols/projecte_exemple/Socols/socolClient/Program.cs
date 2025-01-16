using System.Net;
using System.Net.Sockets;

namespace client
{
    class programa
    {
        static void Main(String[] args)
        {
            // (definicio socket)
            IPAddress MyIPAddress;
            MyIPAddress = IPAddress.Parse("127.0.0.1");
            int MyPort = 11000;

            //servidor
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(MyIPAddress, MyPort);

            Console.WriteLine("Connexio establerta");
            Console.ReadLine();
        }
    }
}
