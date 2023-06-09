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

        // Modify one or more bits in the original message
        string modifiedMessage = ModifyBits(originalMessage);

        // Create a new encryptor for the modified message
        ICryptoTransform modifiedEncryptor = des.CreateEncryptor();

        // Encrypt the modified message
        stopwatch.Restart();
        stopwatch.Start();
        MemoryStream msModifiedEncrypt = new MemoryStream();
        CryptoStream csModifiedEncrypt = new CryptoStream(msModifiedEncrypt, modifiedEncryptor, CryptoStreamMode.Write);
        StreamWriter swModifiedEncrypt = new StreamWriter(csModifiedEncrypt);
        swModifiedEncrypt.Write(modifiedMessage);
        swModifiedEncrypt.Flush();
        csModifiedEncrypt.FlushFinalBlock();
        byte[] modifiedEncryptedMessage = msModifiedEncrypt.ToArray();
        stopwatch.Stop();
        Console.WriteLine("____________Время зашифрования (измененное сообщение): " + stopwatch.Elapsed + "____________");

        // Print the original and modified encrypted messages
        Console.WriteLine("\nЗашифрованное сообщение (оригинальное): " + Convert.ToBase64String(encryptedMessage));
        Console.WriteLine("\nЗашифрованное сообщение (измененное): " + Convert.ToBase64String(modifiedEncryptedMessage));

        // Create a DES decryptor
        stopwatch.Restart();
        stopwatch.Start();
        ICryptoTransform decryptor = des.CreateDecryptor();

        // Decrypt the original message
        MemoryStream msDecrypt = new MemoryStream(encryptedMessage);
        CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        StreamReader srDecrypt = new StreamReader(csDecrypt);
        string decryptedMessage = srDecrypt.ReadToEnd();
        stopwatch.Stop();
        Console.WriteLine("\n____________Время расшифрования (оригинальное сообщение): " + stopwatch.Elapsed + "____________");
        Console.WriteLine("\nРасшифрованное сообщение (оригинальное): {0}", decryptedMessage);

        // Decrypt the modified message
        MemoryStream msModifiedDecrypt = new MemoryStream(modifiedEncryptedMessage);
        CryptoStream csModifiedDecrypt = new CryptoStream(msModifiedDecrypt, decryptor, CryptoStreamMode.Read);
        StreamReader srModifiedDecrypt = new StreamReader(csModifiedDecrypt);
        string modifiedDecryptedMessage = srModifiedDecrypt.ReadToEnd();
        stopwatch.Stop();
        Console.WriteLine("\n____________Время расшифрования (измененное сообщение): " + stopwatch.Elapsed + "____________");
        Console.WriteLine("\nРасшифрованное сообщение (измененное): {0}", modifiedDecryptedMessage);
    }

    private static string ModifyBits(string message)
    {
        // Convert the message to a byte array
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(message);

        // Modify one or more bits in the byte array
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = (byte)(bytes[i] ^ 1); // Flipping the least significant bit
        }

        // Convert the modified byte array back to a string
        return System.Text.Encoding.UTF8.GetString(bytes);
    }
}
