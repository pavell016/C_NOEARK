using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

class Cliente
{
    static void Main()
    {
        TcpClient client = new TcpClient("127.0.0.1", 50000);
        NetworkStream ns = client.GetStream();
        Console.Write("Introduce el mensaje a enviar: ");
        string mensaje = Console.ReadLine();
        string Serverpublickey = Server_Key_Await(ns);
        byte[] mensajeEncriptado = Encrypt(Serverpublickey, mensaje);
        ns.Write(mensajeEncriptado, 0, mensajeEncriptado.Length);

        Console.ReadLine();
        // Cerrar conexiones
        end(ns,client);
    }

    
    public static string Server_Key_Await(NetworkStream ns) {
        byte[] buffer = new byte[1024];
        int bytesLeidos = ns.Read(buffer, 0, buffer.Length);
        return Encoding.UTF8.GetString(buffer, 0, bytesLeidos);
    }
    public static byte[] Encrypt(string Server_pub_key, string message) {
        byte[] mensajeCifrado;
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(Server_pub_key);
            mensajeCifrado = rsa.Encrypt(Encoding.UTF8.GetBytes(message), false);
        }
        return mensajeCifrado;
    }
    public static void end(NetworkStream ns, TcpClient client) {
        ns.Close();
        client.Close();
    }
}
