using System;
using System.IO;
using System.Security.Cryptography;

namespace Warehouse.Models
{
    public class EncryptionProcess
    {
        // Задаем ключ и IV напрямую
        private static readonly byte[] Key = Convert.FromBase64String("NclL9UaBZGjELvK83bE4C6yExUyOlDaJpjGCo3W7Opk=");
        private static readonly byte[] Iv = Convert.FromBase64String("myS9F/tt9T95M07gDN273w==");

        public static string Encrypt(string plainText)
        {
            // Проверка длины ключа
            if (Key.Length != 16 && Key.Length != 24 && Key.Length != 32)
            {
                Console.WriteLine(Convert.ToBase64String(Key));
                throw new ArgumentException("Ключ должен быть 16, 24 или 32 байта в длину.");
            }

            // Проверка длины IV
            if (Iv.Length != 16)
            {
                throw new ArgumentException("IV должен быть 16 байт в длину.");
            }

            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = Iv;

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}