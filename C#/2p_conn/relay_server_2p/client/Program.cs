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
            /// string a enviar al servidor
            bool client_functionality = true;
            byte[] wellecomebff = new byte[1024];
            int br = ns.Read(wellecomebff, 0, wellecomebff.Length);
            string wellecome = Encoding.UTF8.GetString(wellecomebff, 0, br);
            Console.WriteLine(wellecome);
            while (client_functionality)
            {
                Console.WriteLine("Disli algo al altre jugador");
                string phrase_to_reverse = Console.ReadLine();
                if (phrase_to_reverse != "")
                {
                    byte[] send = Encoding.UTF8.GetBytes(phrase_to_reverse);
                    ns.Write(send, 0, send.Length);
                    
                    byte[] receiveBuffer = new byte[1024];
                    br = ns.Read(receiveBuffer, 0, receiveBuffer.Length);
                    string reversedPhrase = Encoding.UTF8.GetString(receiveBuffer, 0, br);

                    Console.WriteLine($"L'altre jugador ha dit: {reversedPhrase}\n");

                }
                else
                {
                    byte[] send = Encoding.UTF8.GetBytes(phrase_to_reverse);
                    ns.Write(send, 0, send.Length);
                    ns.Close();
                    client.Close();
                    client_functionality = false;
                }
            }



        }
        

    }
}