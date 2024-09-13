using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace game.Utilities
{
    internal class HMACHelper
    {
        public static byte[] GenerateKey()
        {
            byte[] key = new byte[32];  // 256 bits (32 bytes)
            RandomNumberGenerator.Fill(key);  // Llenar el array con bytes aleatorios
            return key;
        }

        // Método para calcular el HMAC usando SHA256
        public static string ComputeHmac(string message, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                byte[] hashBytes = hmac.ComputeHash(messageBytes);

                // Convertir el hash a un string hexadecimal
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        // Método opcional para convertir la clave a un string legible en hexadecimal
        public static string KeyToHex(byte[] key)
        {
            return BitConverter.ToString(key).Replace("-", "").ToLower();
        }
    }
}
