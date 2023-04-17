using LAB_4;
using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        var sw = new Stopwatch();
        int option = 0;
        while (option != -1)
        {
            Console.WriteLine("1 - для шифрования Вижинера");
            Console.WriteLine("2 - для расшифрования Вижинера");
            Console.WriteLine("3 - для шифрования Цезаря");
            Console.WriteLine("4 - для расшифрования Цезаря");
            var cipher = new VigenereCipher("АБВГДЕЁЖЗІЙКЛМНОПРСТУЎФХЦЧШЫЬЭЮЯ");
            option = int.Parse(Console.ReadLine());
            var password = "БЯЛІЦКІ";
            int secretKey = 5;
            switch (option)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Введите сообщение:");
                    string inputText = Console.ReadLine().ToUpper();
                    sw = null;
                    sw = new Stopwatch();
                    sw.Start();
                    string encryptedText = cipher.Encrypt(inputText, password);
                    sw.Stop();
                    Console.WriteLine("Время шифрования: " +sw.Elapsed);
                    Console.WriteLine("Зашифрованное сообщение: {0}", encryptedText); break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Введите зашифрованное сообщение");
                    string newEncryptedText = Console.ReadLine();
                    sw = null;
                    sw = new Stopwatch();
                    sw.Start();
                    Console.WriteLine("Расшифрованное сообщение: {0}", cipher.Decrypt(newEncryptedText, password));
                    sw.Stop();
                    Console.WriteLine("Время расшифрования: " + sw.Elapsed);
                    Console.ReadLine(); break;
                case 3:
                    Console.Clear();
                    var cipherCaesar = new Caesar();
                    Console.Write("Введите текст: ");
                    var message = Console.ReadLine();
                    sw = null;
                    sw = new Stopwatch();
                    sw.Start();
                    var encryptedTextCaesar = cipherCaesar.Encrypt(message, secretKey);
                    Console.WriteLine("Время шифрования: " + sw.Elapsed);
                    Console.WriteLine("Зашифрованное сообщение: {0}", encryptedTextCaesar);

                    Console.ReadLine(); break;
                case 4:
                    Console.Clear();
                    var decCipherCaesar = new Caesar();
                    Console.Write("Введите текст: ");
                    var newEncryptedTextCaesar = Console.ReadLine();
                    sw = null;
                    sw = new Stopwatch();
                    sw.Start();
                    Console.WriteLine("Расшифрованное сообщение: {0}", decCipherCaesar.Decrypt(newEncryptedTextCaesar, secretKey));
                    Console.WriteLine("Время расшифрования: " + sw.Elapsed);
                    break;
                default:
                    option = -1;
                    break;
            }



        }
    }
}