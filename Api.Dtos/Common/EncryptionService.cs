using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Api.Dtos.Common
{
    public static class EncryptionService
    {
        private static readonly string _key = "Formunica2025**"; // Clave base, ajustaremos su tamaño.

        public static string Encrypt(string strText)
        {
            // Convertir la clave a un arreglo válido de 16 bytes.
            var key = GetValidKey(_key);

            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();
                var iv = aes.IV;

                using (var encryptor = aes.CreateEncryptor(aes.Key, iv))
                {
                    var textBytes = Encoding.UTF8.GetBytes(strText);
                    var encryptedBytes = encryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
                    
                    var ivBase64 = Convert.ToBase64String(iv).Replace("+", "-").Replace("/", "_").Replace("=", "");
                    var encryptedBase64 = Convert.ToBase64String(encryptedBytes).Replace("+", "-").Replace("/", "_").Replace("=", "");

                    // Concatenar IV y datos cifrados separados por ':'
                    return ivBase64 + "." + encryptedBase64;
                }
            }
        }

        public static string Descrypt(string strEncrypText)
        {
            try
            {
                var parts = strEncrypText.Split('.');
                if (parts.Length != 2)
                    throw new ArgumentException("El texto cifrado no tiene el formato esperado.");

                var iv = Convert.FromBase64String(parts[0].Replace("-", "+").Replace("_", "/") + new string('=', (4 - parts[0].Length % 4) % 4));
                var encryptedBytes = Convert.FromBase64String(parts[1].Replace("-", "+").Replace("_", "/") + new string('=', (4 - parts[1].Length % 4) % 4));
                var key = GetValidKey(_key);

                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, iv))
                    {
                        var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                        return Encoding.UTF8.GetString(decryptedBytes);
                    }
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error de formato en Base64: {strEncrypText}");
                throw new FormatException("El texto contiene caracteres no válidos para Base64.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                throw new InvalidOperationException("Ocurrió un error al descifrar el texto.", ex);
            }
        }

        private static byte[] GetValidKey(string key)
        {
            // Asegurar que la clave tenga exactamente 16 bytes (AES-128).
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var validKey = new byte[16];
            Array.Copy(keyBytes, validKey, Math.Min(keyBytes.Length, validKey.Length));
            return validKey;
        }
    }
}
