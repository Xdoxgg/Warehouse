using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Warehouse.Classes;

public class EncryptionHelper
{
   
    private static readonly byte[] Key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("KEY") ?? string.Empty);
    private static readonly byte[] Iv = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("IV") ?? string.Empty);

    public static string Encrypt(string plainText)
    {
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