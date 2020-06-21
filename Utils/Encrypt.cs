using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Tiendita.Utils
{
    class Encrypt
    {
        // Función obtnenida del siguiente enlace https://hdeleon.net/funcion-para-encriptar-en-sha256-en-c-net/
        // Es un algoritmo de cifrado de un camino
        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
