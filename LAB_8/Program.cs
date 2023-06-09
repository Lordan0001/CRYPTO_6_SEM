using System;
using System.Diagnostics;
using System.Numerics;
using System.Text;

    public class RSARandomGenerator
    {
        private BigInteger p;
        private BigInteger q;
        private BigInteger n;
        private BigInteger e;
        private BigInteger x;

        public RSARandomGenerator(BigInteger p, BigInteger q, BigInteger e, BigInteger x)
        {
            this.p = p;
            this.q = q;
            this.n = p * q;
            this.e = e;
            this.x = x;
        }

        public byte GenerateRandomBit()
        {
            x = BigInteger.ModPow(x, e, n);
            byte randomBit = (byte)(x % 2);
            return randomBit;
        }
    }

class RC4
{
    private byte[] S;
    private int i;
    private int j;

    public RC4(int[] key)
    {
        byte[] keyBytes = new byte[key.Length];
        for (int k = 0; k < key.Length; k++)
        {
            keyBytes[k] = (byte)key[k];
        }
        Initialize(keyBytes);
    }

    private void Initialize(byte[] key)
    {
        S = new byte[256];
        for (int i = 0; i < 256; i++)
        {
            S[i] = (byte)i;
        }

        int j = 0;
        for (int i = 0; i < 256; i++)
        {
            j = (j + S[i] + key[i % key.Length]) % 256;
            Swap(S, i, j);
        }

        i = 0;
        j = 0;
    }

    private void Swap(byte[] array, int i, int j)
    {
        byte temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }

    public byte[] Encrypt(byte[] plaintext)
    {
        byte[] ciphertext = new byte[plaintext.Length];
        for (int k = 0; k < plaintext.Length; k++)
        {
            i = (i + 1) % 256;
            j = (j + S[i]) % 256;
            Swap(S, i, j);
            byte key = S[(S[i] + S[j]) % 256];
            ciphertext[k] = (byte)(plaintext[k] ^ key);
        }
        return ciphertext;
    }

    public byte[] Decrypt(byte[] ciphertext)
    {
        return Encrypt(ciphertext); // RC4 encryption and decryption are the same operation
    }
}


public class Program
{
    public static void Main(string[] args)
    {
        BigInteger p = BigInteger.Parse("2360139667523861780919049730149768806849074118536797203293239829693873691243143692250599323188154989384639880204871117345866072468506920689286983076629297");
        BigInteger q = BigInteger.Parse("2099249415868878243183861771260002223829476242515098190344376680691720254428421388236931165207807296505082022052132982314451803186074910384766155560684887");
        BigInteger e = BigInteger.Parse("65537");
        BigInteger x0 = BigInteger.Parse("12345");

        RSARandomGenerator generator = new RSARandomGenerator(p, q, e, x0);

        // Генерация 10 случайных битов
        Console.WriteLine("Задание 1: RSA ");
        for (int i = 0; i < 10; i++)
        {
            byte randomBit = generator.GenerateRandomBit();
            Console.WriteLine(randomBit);
        }

        Console.WriteLine("\n\nЗадание 2: RS4 ");
        int[] key = { 121, 14, 89, 15 }; // Example key

        Console.Write("Введите строку для шифрования: ");
        string input = Console.ReadLine();
        byte[] plaintext = Encoding.UTF8.GetBytes(input);

        RC4 rc4 = new RC4(key);

        // Засекаем время зашифрования
        Stopwatch encryptionTimer = Stopwatch.StartNew();
        byte[] ciphertext = rc4.Encrypt(plaintext);
        encryptionTimer.Stop();

        Console.WriteLine("Зашифровано: " + BitConverter.ToString(ciphertext).Replace("-", ""));

        // Засекаем время расшифрования
        Stopwatch decryptionTimer = Stopwatch.StartNew();
        byte[] decrypted = rc4.Decrypt(ciphertext);
        decryptionTimer.Stop();

        Console.WriteLine("Расшифровано: " + Encoding.UTF8.GetString(plaintext));

        // Выводим время зашифрования и расшифрования в консоль
        Console.WriteLine("Время зашифрования (RC4): " + encryptionTimer.Elapsed);
        Console.WriteLine("Время расшифрования (RC4): " + decryptionTimer.Elapsed);
    }
}
