using System;

class EnigmaMachine
{
    private char[] alphabet;
    private char[] rotorL;
    private char[] rotorM;
    private char[] rotorR;
    private char[] reflector;

    public EnigmaMachine()
    {
        // Инициализация алфавита
        alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        // Инициализация роторов
        rotorL = "EKMFLGDQVZNTOWYHXUSPAIBRCJ".ToCharArray();
        rotorM = "AJDKSIRUXBLHWTMCQGZNPYFVOE".ToCharArray();
        rotorR = "BDFHJLCPRTXVZNYEIWGAKMUSQO".ToCharArray();

        // Инициализация рефлектора
        reflector = "YRUHQSLDPXNGOKMIEBFZCWVJAT".ToCharArray();
    }

    private char Substitute(char input, char[] rotor)
    {
        int index = Array.IndexOf(alphabet, input);
        return rotor[index];
    }

    public string Encrypt(string input)
    {
        char[] inputChars = input.ToCharArray();
        char[] outputChars = new char[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            char output = Substitute(inputChars[i], rotorR);
            output = Substitute(output, rotorM);
            output = Substitute(output, rotorL);
            int index = Array.IndexOf(alphabet, output);
            output = reflector[index];
            index = Array.IndexOf(rotorL, output);
            output = alphabet[index];
            index = Array.IndexOf(rotorM, output);
            output = alphabet[index];
            index = Array.IndexOf(rotorR, output);
            output = alphabet[index];

            outputChars[i] = output;
        }

        return new string(outputChars);
    }

    public string Decrypt(string input)
    {
        char[] inputChars = input.ToCharArray();
        char[] outputChars = new char[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            char output = Substitute(inputChars[i], rotorR);
            output = Substitute(output, rotorM);
            output = Substitute(output, rotorL);
            int index = Array.IndexOf(alphabet, output);
            output = reflector[index];
            index = Array.IndexOf(rotorL, output);
            output = alphabet[index];
            index = Array.IndexOf(rotorM, output);
            output = alphabet[index];
            index = Array.IndexOf(rotorR, output);
            output = alphabet[index];

            outputChars[i] = output;
        }

        return new string(outputChars);
    }
}

class Program
{
    static void Main(string[] args)
    {
        EnigmaMachine enigma = new EnigmaMachine();

        Console.WriteLine("Введите текст для шифрования: ");
        string toEncrypt = Console.ReadLine();
        Console.WriteLine("Шифруем: " + toEncrypt);
        string encrypted = enigma.Encrypt(toEncrypt);
        Console.WriteLine("Зашифрованное: " + encrypted);

/*        string toDecrypt = encrypted;
        Console.WriteLine("Расшифровываем: " + toDecrypt);
        string decrypted = enigma.Decrypt(toDecrypt);
        Console.WriteLine("Расшифрованное: " + decrypted);*/
    }
}
