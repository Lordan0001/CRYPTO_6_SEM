using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

public static class DESExample
{
    public static void Main()
    {
        Console.WriteLine("Введите сообщение для шифрования DES: ");
        string originalMessage = Console.ReadLine();

        // Generate a random key and initialization vector
        byte[] key = new byte[8];
        byte[] iv = new byte[8];
        RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetBytes(key);
        rng.GetBytes(iv);

        // Create a DES encryptor
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        des.Key = key;
        des.IV = iv;
        ICryptoTransform encryptor = des.CreateEncryptor();

        // Encrypt the message
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        MemoryStream msEncrypt = new MemoryStream();
        CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        StreamWriter swEncrypt = new StreamWriter(csEncrypt);
        swEncrypt.Write(originalMessage);
        swEncrypt.Flush();
        csEncrypt.FlushFinalBlock();
        byte[] encryptedMessage = msEncrypt.ToArray();
        stopwatch.Stop();
        Console.WriteLine("____________Время зашифрования: " + stopwatch.Elapsed + "____________");
        // Create a DES decryptor
        stopwatch.Restart();
        stopwatch.Start();
        ICryptoTransform decryptor = des.CreateDecryptor();

        // Decrypt the message
        MemoryStream msDecrypt = new MemoryStream(encryptedMessage);
        CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        StreamReader srDecrypt = new StreamReader(csDecrypt);
        string decryptedMessage = srDecrypt.ReadToEnd();
        stopwatch.Stop();

        // Print the original message and the decrypted message
        Console.WriteLine("\nЗашифрованное сообщение: " + Convert.ToBase64String(encryptedMessage));
        Console.WriteLine("\n____________Время расшифрования: " + stopwatch.Elapsed + "____________");
        Console.WriteLine("\nРасшифрованное сообщение: {0}", decryptedMessage);
    }
}
