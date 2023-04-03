// using System.Security.Cryptography;
// using System.Text;
// Si solo incluyes esto de arriba no jala, increible

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using static System.Convert;

namespace CryptographyLibrary;

public static class Protector
{
    // salt
    private static readonly byte[] salt = Encoding.Unicode.GetBytes("chinnnngado");
    private static readonly int iterations = 2000;
    public static string Encrypt(string plainText, string password)
    {
        byte[] encryptedBytes;
        byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);


        var aes = Aes.Create();
        // Ni modo carnal, te quedas desactualizado porque sino me caen los demonios
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);

        aes.Key = pbkdf2.GetBytes(32); // 256-bit key
        aes.IV = pbkdf2.GetBytes(16); // 128- bit IV

        /*      // Scoped 'using' directive
                using MemoryStream ms = new();
                using CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);

                cs.Write(plainBytes, 0, plainBytes.Length);
                encryptedBytes = ms.ToArray();
                // Esta madre está bien curseada */

        using (var ms = new MemoryStream()) {
            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write)) {
                cs.Write(plainBytes, 0, plainBytes.Length);
            }
            encryptedBytes = ms.ToArray();
        }

        return Convert.ToBase64String(encryptedBytes);
    }

    public static string Decrypt(string cryptoText, string password)
    {
        byte[] plainBytes;
        byte[] cryptoBytes = Convert.FromBase64String(cryptoText);

        var aes = Aes.Create();
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);

        aes.Key = pbkdf2.GetBytes(32);
        aes.IV = pbkdf2.GetBytes(16);

        /* using MemoryStream ms = new();
        using CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);

        cs.Write(cryptoBytes, 0, cryptoBytes.Length);
        plainBytes = ms.ToArray();
        // Igualmente curseado */

        using (var ms = new MemoryStream()) {
            using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write)) {
                cs.Write(cryptoBytes, 0, cryptoBytes.Length);
            }
            plainBytes = ms.ToArray();
        }

        return Encoding.Unicode.GetString(plainBytes);
    }
}