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
        byte[] encryptedMessage = EncryptMessage(originalMessage, encryptor);

        // Print the original encrypted message
        Console.WriteLine("\nЗашифрованное сообщение (оригинальное): " + Convert.ToBase64String(encryptedMessage));

        // Perform a brute-force attack by flipping each bit and measuring changes
        PerformAvalancheAttack(originalMessage, encryptedMessage, des);
    }

    private static byte[] EncryptMessage(string message, ICryptoTransform encryptor)
    {
        byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);

        using (MemoryStream msEncrypt = new MemoryStream())
        {
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                csEncrypt.Write(messageBytes, 0, messageBytes.Length);
            }
            return msEncrypt.ToArray();
        }
    }

    private static string DecryptMessage(byte[] encryptedMessage, ICryptoTransform decryptor)
    {
        using (MemoryStream msDecrypt = new MemoryStream(encryptedMessage))
        {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            {
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }

    private static void PerformAvalancheAttack(string originalMessage, byte[] encryptedMessage, DESCryptoServiceProvider des)
    {
        byte[] originalMessageBytes = System.Text.Encoding.UTF8.GetBytes(originalMessage);
        byte[] modifiedMessageBytes = new byte[originalMessageBytes.Length];
        Array.Copy(originalMessageBytes, modifiedMessageBytes, originalMessageBytes.Length);

        ICryptoTransform decryptor = des.CreateDecryptor();
        string decryptedMessage = DecryptMessage(encryptedMessage, decryptor);

        Console.WriteLine("\nРасшифрованное сообщение (оригинальное): {0}", decryptedMessage);
        Console.WriteLine("\n\nИзменение каждого бита в исходном сообщении:");

        for (int i = 0; i < modifiedMessageBytes.Length; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                byte bitMask = (byte)(1 << j);
                modifiedMessageBytes[i] ^= bitMask;

                encryptedMessage = EncryptMessage(System.Text.Encoding.UTF8.GetString(modifiedMessageBytes), des.CreateEncryptor());
                string modifiedDecryptedMessage = DecryptMessage(encryptedMessage, decryptor);

                Console.WriteLine("Изменение бита {0} в байте {1}: {2}", j, i, modifiedDecryptedMessage);
                modifiedMessageBytes[i] ^= bitMask; // Revert the bit change
            }
        }
    }
}
