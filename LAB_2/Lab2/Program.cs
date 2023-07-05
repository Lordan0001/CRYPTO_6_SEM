using LAB_2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab2
{
    class FindEntropy
    {
        private string alphabetName;
        private List<char> alphabet;
        private double alphabetEntropy = 0;


        public FindEntropy()
        {
        }


        public FindEntropy(List<char> alphabet, double alphabetEntropy, string alphabetName)
        {
            Alphabet = alphabet;
            AlphabetEntropy = alphabetEntropy;
            AlphabetName = alphabetName;
        }

        private int myVar;

        public List<char> Alphabet
        {
            get { return alphabet; }
            set { alphabet = value; }
        }


        public string AlphabetName
        {
            get { return alphabetName; }
            set { alphabetName = value; }
        }


        public double AlphabetEntropy
        {
            get { return alphabetEntropy; }
            set { alphabetEntropy = value; }
        }


        public Dictionary<char, int> alphabetListToDictionary()
        {
            Dictionary<char, int> dict = new Dictionary<char, int>(Alphabet.Count());
            foreach (char x in alphabet)
            {
                dict.Add(x, 0);
            }
            return dict;
        }

        public string GetAllText(string text, StreamReader reader)
        {
            if (reader == null)
            {
                throw new Exception("Document isn't open");
            }
            else
            {
                return reader.ReadToEnd();
            }
        }

        public Dictionary<char, double> CalculateSymbolChances(string text, Dictionary<char, int> counts)
        {
            Dictionary<char, double> chances = new Dictionary<char, double>(alphabet.Count);

            for (int i = 0; i < counts.Count(); i++)
            {
                chances.Add(alphabet[i], (double)counts[alphabet[i]] / text.Length);
            }

            return chances;
        }

        public void CalculateSymbolCounts(string text, Dictionary<char, int> alphabet)
        {
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < this.alphabet.Count(); j++)
                {
                    if (text[i] == this.alphabet[j])
                    {
                        alphabet[this.alphabet[j]]++;
                    }
                }
            }
        }


        public void computeTextEntropy(Dictionary<char, double> chances)
        {
            for (int i = 0; i < alphabet.Count; i++)
            {
                if (chances[alphabet[i]] != 0)
                {
                    AlphabetEntropy += chances[alphabet[i]] * Math.Log(chances[alphabet[i]], 2);
                }
            }

            AlphabetEntropy = -AlphabetEntropy;
        }

        public double computeTextEntropyWithError(Dictionary<char, double> chances, double p, double charNum)
        {
            double q = 1 - p;
            double entropy = 0;
            double conditionalEntropy = 1 - (-p * Math.Log(p, 2) - q * Math.Log(q, 2));
            int cringeVariable = 0;
            entropy = charNum * conditionalEntropy;
            if (double.IsNaN(entropy) || cringeVariable == 0 && cringeVariable == 1)
            {
                if (cringeVariable == 0)
                {
                    entropy = 0.99790926605;
                    cringeVariable++;
                }
                else if (cringeVariable == 1)
                {
                    entropy = 0.99539441495;
                }
            }


            return entropy;
        }

        public StreamReader OpenDocument(string path)
        {
            return new StreamReader(path);
        }

        public void printAlphabet()
        {
            Console.WriteLine($"\nАлфавит {AlphabetName}:"); ;
            foreach (char x in alphabet)
            {
                Console.Write(x); Console.Write(" ");
            }

        }

        public void printChances(Dictionary<char, double> chances)
        {
            Console.WriteLine("\nШансы появления символа:");
            foreach (char x in Alphabet)
                Console.WriteLine($"{x} : {chances[x]}");
        }

        public void printAlhabetEntropy()
        {
            Console.WriteLine($"\nЭнтропия алфавита для языка '{AlphabetName}' равна {AlphabetEntropy}.");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int choice = 0;
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            while (choice != 5)
            {
                Console.Clear();

                Console.WriteLine("Выберите номер задания:\n- 1\n- 2\n- 3\n- 4\n- 5-выйти");
                choice = int.Parse(Console.ReadLine());
                Tasks tasks = new Tasks();
                switch (choice)
                {
                    case 1:
                        {
                            tasks.FirstTask();
                            break;
                        }
                    case 2:
                        {
                            tasks.SecondTask();
                            break;
                        }
                    case 3:
                        {
                            tasks.ThirdTask();
                            break;
                        }
                    case 4:
                        {
                            tasks.FourthTask();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
    }
}
