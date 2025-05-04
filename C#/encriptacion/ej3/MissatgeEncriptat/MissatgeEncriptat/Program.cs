using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

class Servidor
{
    static void Main()
    {
        TcpListener servidor = new TcpListener(IPAddress.Parse("127.0.0.1"), 50000);
        servidor.Start();
        Console.WriteLine("Servidor iniciado. Esperando conexiones...");

        TcpClient cliente = servidor.AcceptTcpClient();
        NetworkStream stream = cliente.GetStream();

        // 1. Generar par de claves RSA
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        string clavePublicaXml = rsa.ToXmlString(false); // Solo la clave pública

        // 2. Enviar la clave pública al cliente
        byte[] clavePublicaBytes = Encoding.UTF8.GetBytes(clavePublicaXml);
        stream.Write(clavePublicaBytes, 0, clavePublicaBytes.Length);

        // 3. Recibir el mensaje cifrado del cliente
        byte[] buffer = new byte[256];
        int bytesLeidos = stream.Read(buffer, 0, buffer.Length);
        byte[] mensajeCifrado = new byte[bytesLeidos];
        Array.Copy(buffer, 0, mensajeCifrado, 0, bytesLeidos);

        // 4. Descifrar el mensaje con la clave privada
        byte[] mensajeDescifrado = rsa.Decrypt(mensajeCifrado, false);
        string mensaje = Encoding.UTF8.GetString(mensajeDescifrado);
        Console.WriteLine("Mensaje recibido y descifrado: " + mensaje);

        // Cerrar conexiones
        stream.Close();
        cliente.Close();
        servidor.Stop();
    }
}
