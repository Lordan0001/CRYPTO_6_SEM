
using LAB_5;
using System.Diagnostics;

var sw = new Stopwatch();
sw = null;
sw = new Stopwatch();
sw.Start();
string encrypted = Route.Encrypt("уладбяліцкіі", 4, 4);
sw.Stop();
Console.WriteLine("Маршрутная перестановка (маршрут: запись – по строкам, считывание – по столбцам\n");
Console.WriteLine("Время шифрования: " + sw.Elapsed);
Console.WriteLine("Зашифрованное сообщение: "+encrypted);
sw = null;
sw = new Stopwatch();
sw.Start();
Console.WriteLine("Расшифрованное сообщение: "+Route.Decrypt(encrypted, 4, 4));
sw.Stop();
Console.WriteLine("Время расшифрования: " + sw.Elapsed);
Console.WriteLine("__________________________________________________________\n");
Console.WriteLine("\nМножественная перестановка, ключевые слова – собственные имя и фамилия\n\n");

string plaintext = "сонца";
string rowKey = "улад";
string columnKey = "бяліцкін";
sw = null;
sw = new Stopwatch();
sw.Start();
string ciphertext = SecondCipher.Encrypt2(plaintext, rowKey, columnKey);
sw.Stop();
Console.WriteLine("Зашфрованное сообщение: " + ciphertext);
Console.WriteLine("Время шифрования: " + sw.Elapsed);
sw = null;
sw = new Stopwatch();
sw.Start();
string decryptedText = SecondCipher.Decrypt2(ciphertext, rowKey, columnKey);
sw.Stop();
Console.WriteLine("Расшифрованное сообщение: "+decryptedText);
Console.WriteLine("Время расшифрования: " + sw.Elapsed);