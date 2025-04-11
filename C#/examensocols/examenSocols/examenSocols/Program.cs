using System.Net;
using System.Net.Sockets;
using System.Text;

namespace servidor
{
    class server
    {
        private static IPAddress IPServidor;
        public static void Main(String[] args)
        {
            IPServidor = IPAddress.Parse("127.0.0.1");
            int conn_server_port = 50000;
            //connexió al servidor
            TcpClient client = new TcpClient();
            client.Connect(IPServidor, conn_server_port);
            NetworkStream ns = client.GetStream();
            Console.WriteLine("Connexió Establerta\n\n");
            byte[] wellecomebff = new byte[1024];
            int br = ns.Read(wellecomebff, 0, wellecomebff.Length);
            string wellecome = Encoding.UTF8.GetString(wellecomebff, 0, br);
            Console.WriteLine(wellecome);
            Thread chat_program = new Thread(() => chat(ns, client));
            Thread atento_a_respuesta = new Thread(() => message_available(ns));
            atento_a_respuesta.Start();
            chat_program.Start();
        }
        public static void chat(NetworkStream ns, TcpClient client)
        {
            while (true)
            {
                
                try
                {
                    byte[] send = Encoding.UTF8.GetBytes("tirarDau");
                    ns.Write(send, 0, send.Length);
                    Thread.Sleep(1000);
                }
                catch(Exception ex)
                {
                    ns.Close();
                    client.Close();

                }
            }
        }
        public static void message_available(NetworkStream ns)
        {
            while (true)
            {
                if (ns.DataAvailable)
                {
                    byte[] receiveBuffer = new byte[1024];
                    int buffer = ns.Read(receiveBuffer, 0, receiveBuffer.Length);
                    string reversedPhrase = Encoding.UTF8.GetString(receiveBuffer, 0, buffer);
                    Console.WriteLine(reversedPhrase);
                }
            }
        }

    }
}
