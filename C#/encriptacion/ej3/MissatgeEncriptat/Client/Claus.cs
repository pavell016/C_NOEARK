using System;
using System.Security.Cryptography;
using System.Text;

namespace MissatgeEncriptat
{
    class Claus
    {
        public byte[] PublicKey { get; set; } = Array.Empty<byte>();
        public string Username { get; set; } = string.Empty;

        public Claus() { }
        public Claus(byte[] publicKey, string username)
        {
            PublicKey = publicKey;
            Username = username;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Username: {Username}");
            sb.AppendLine("Clave Pública: " + Convert.ToBase64String(PublicKey));
            return sb.ToString();
        }
    }
}