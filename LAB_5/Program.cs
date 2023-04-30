
using LAB_5;


string encrypted = Route.Encrypt("уладбяліцкіі", 4, 4);
Console.WriteLine("Маршрутная перестановка (маршрут: запись – по строкам, считывание – по столбцам\n");
Console.WriteLine("Зашифрованное сообщение: "+encrypted);
Console.WriteLine("Расшифрованное сообщение: "+Route.Decrypt(encrypted, 4, 4));
Console.WriteLine("__________________________________________________________\n");
Console.WriteLine("\nМножественная перестановка, ключевые слова – собственные имя и фамилия\n\n");

string plaintext = "сонца";
string rowKey = "улад";
string columnKey = "бяліцкін";

string ciphertext = SecondCipher.Encrypt2(plaintext, rowKey, columnKey);
Console.WriteLine("Зашфрованное сообщение: " + ciphertext);

string decryptedText = SecondCipher.Decrypt2(ciphertext, rowKey, columnKey);
Console.WriteLine("Расшифрованное сообщение: "+decryptedText);
