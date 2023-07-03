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

        public Dictionary<char, double> getSymbolsChances(string text, Dictionary<char, int> counts)
        {
            Dictionary<char, double> chances = new Dictionary<char, double>(alphabet.Count);

            for (int i = 0; i < counts.Count(); i++)
            {
                chances.Add(alphabet[i], (double)counts[alphabet[i]] / text.Length);
            }

            return chances;
        }

        public void getSymbolsCounts(string text, Dictionary<char, int> alphabet)
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
            const string fileName = "Lab2-1.xls";

            List<char> italianAlphabet = new List<char>()
            {
                'a','b','c','d','e','f','g','h','i','m','n','o','p','q','r','s','t','u','v','z','j','k','w','x','y'
            };

            List<char> bulgarianAlphabet = new List<char>()
            {
                '\u0430','\u0431','\u0432','\u0433',
                '\u0434','\u0435','\u0436','\u0437',
                '\u0438','\u0439','\u043A',
                '\u043B','\u043C','\u043D','\u043E','\u043F',
                '\u0440','\u0441','\u0442','\u0443','\u0444',
                '\u0445','\u0446','\u0447','\u0448','\u0449',
                '\u044C','\u044E','\u044F'
            };
            while (choice != 5)
            {
                Console.Clear();

                Console.WriteLine("Выберите номер задания:\n- 1\n- 2\n- 3\n- 4\n- 5-выйти");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            Console.Clear();
                            FindEntropy italianChecker = new FindEntropy(italianAlphabet, 0, "Итальянский");
                            FindEntropy bulgarianChecker = new FindEntropy(bulgarianAlphabet, 0, "Болгарский");

                            string italianText = italianChecker.OpenDocument("italian.txt").ReadToEnd().ToLower();
                            string bulgarianText = italianChecker.OpenDocument("bulgarian.txt").ReadToEnd().ToLower();

                            Regex regex = new Regex(@"\W");
                            italianText = regex.Replace(italianText, "");
                            bulgarianText = regex.Replace(bulgarianText, "");

                            Dictionary<char, int> italianDict = italianChecker.alphabetListToDictionary();
                            Dictionary<char, int> bulgarianDict = bulgarianChecker.alphabetListToDictionary();

                            italianChecker.getSymbolsCounts(italianText, italianDict);
                            bulgarianChecker.getSymbolsCounts(bulgarianText, bulgarianDict);

                            Dictionary<char, double> chancesItalian = italianChecker.getSymbolsChances(italianText, italianDict);
                            Dictionary<char, double> chancesBulgarian = bulgarianChecker.getSymbolsChances(bulgarianText, bulgarianDict);

                            italianChecker.computeTextEntropy(chancesItalian);
                            bulgarianChecker.computeTextEntropy(chancesBulgarian);


                            italianChecker.printAlphabet();
                            italianChecker.printChances(chancesItalian);
                            italianChecker.printAlhabetEntropy();


                            bulgarianChecker.printAlphabet();
                            bulgarianChecker.printChances(chancesBulgarian);
                            bulgarianChecker.printAlhabetEntropy();

                            double sumItalian = 0;
                            double sumBulgarian = 0;
                            foreach (KeyValuePair<char, double> x in chancesItalian)
                            {
                                sumItalian += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesBulgarian)
                            {
                                sumBulgarian += x.Value;
                            }

                            Console.WriteLine($"Сумма шансов для болгарского языка: {sumBulgarian}");
                            Console.WriteLine($"Сумма шансов для итальянского языка: {sumItalian}");


                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();

                            FindEntropy italianChecker = new FindEntropy(new List<char>(){ '0', '1' }, 0, "Бинарный код");
                            FindEntropy bulgarianChecker = new FindEntropy(new List<char>() { '0', '1' }, 0, "Бинарный код");

                            string italianText = italianChecker.OpenDocument("italian.txt").ReadToEnd().ToLower();
                            string bulgarianText = italianChecker.OpenDocument("bulgarian.txt").ReadToEnd().ToLower();

                            Regex regex = new Regex(@"\W");
                            italianText = regex.Replace(italianText, "");
                            bulgarianText = regex.Replace(bulgarianText, "");

                            string binTextItalian = "";
                            string binTextBulgarian = "";

                            var textChr = Encoding.UTF8.GetBytes(italianText);
                            foreach (int chr in textChr)
                            {
                                binTextItalian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            textChr = Encoding.UTF8.GetBytes(bulgarianText);
                            foreach (int chr in textChr)
                            {
                                binTextBulgarian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            Dictionary<char, int> italianDict = italianChecker.alphabetListToDictionary();
                            Dictionary<char, int> BulgarianDict = bulgarianChecker.alphabetListToDictionary();

                            italianChecker.getSymbolsCounts(binTextItalian, italianDict);
                            bulgarianChecker.getSymbolsCounts(binTextBulgarian, BulgarianDict);

                            Dictionary<char, double> chancesItalian = italianChecker.getSymbolsChances(binTextItalian, italianDict);
                            Dictionary<char, double> chancesBulgarian = bulgarianChecker.getSymbolsChances(binTextBulgarian, BulgarianDict);

                            italianChecker.computeTextEntropy(chancesItalian);
                            bulgarianChecker.computeTextEntropy(chancesBulgarian);


                            italianChecker.printAlphabet();
                            italianChecker.printChances(chancesItalian);
                            italianChecker.printAlhabetEntropy();


                            bulgarianChecker.printAlphabet();
                            bulgarianChecker.printChances(chancesBulgarian);
                            bulgarianChecker.printAlhabetEntropy();

                            double sumItalian = 0;
                            double sumBulgarian = 0;
                            foreach (KeyValuePair<char, double> x in chancesItalian)
                            {
                                sumItalian += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesBulgarian)
                            {
                                sumBulgarian += x.Value;
                            }

                            Console.WriteLine($"Сумма шансов для болгарского языка: {sumBulgarian}");
                            Console.WriteLine($"Сумма шансов для итальянского языка: {sumItalian}");

 

                            Console.ReadKey();
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();

                            FindEntropy italianChecker = new FindEntropy(italianAlphabet, 0, "Итальянский");
                            FindEntropy bulgarianChecker = new FindEntropy(bulgarianAlphabet, 0, "Болгарский");
                            FindEntropy italianCheckerBin = new FindEntropy(new List<char>() { '0', '1' }, 0, "Бинарный код (итальянский)");
                            FindEntropy bulgarianCheckerBin = new FindEntropy(new List<char>() { '0', '1' }, 0, "Бинарный код (болгарский)");

                            string italianText = "belitskyvladislavdmitrievich";
                            string bulgarianText = "белицкивладиславдмиртиевич";

                            string binTextItalian = "";
                            string binTextBulgarian = "";

                            var textChr = Encoding.UTF8.GetBytes(italianText);
                            foreach (int chr in textChr)
                            {
                                binTextItalian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            textChr = Encoding.UTF8.GetBytes(bulgarianText);
                            foreach (int chr in textChr)
                            {
                                binTextBulgarian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            Dictionary<char, int> italianDict = italianChecker.alphabetListToDictionary();
                            Dictionary<char, int> bulgarianDict = bulgarianChecker.alphabetListToDictionary();
                            Dictionary<char, int> italianDictBin = italianCheckerBin.alphabetListToDictionary();
                            Dictionary<char, int> bulgarianDictBin = bulgarianCheckerBin.alphabetListToDictionary();

                            italianChecker.getSymbolsCounts(italianText, italianDict);
                            bulgarianChecker.getSymbolsCounts(bulgarianText, bulgarianDict);
                            italianCheckerBin.getSymbolsCounts(binTextItalian, italianDictBin);
                            bulgarianCheckerBin.getSymbolsCounts(binTextBulgarian, bulgarianDictBin);

                            Dictionary<char, double> chancesItalian = italianChecker.getSymbolsChances(italianText, italianDict);
                            Dictionary<char, double> chancesBulgarian = bulgarianChecker.getSymbolsChances(bulgarianText, bulgarianDict);
                            Dictionary<char, double> chancesItalianBin = italianCheckerBin.getSymbolsChances(binTextItalian, italianDictBin);
                            Dictionary<char, double> chancesBulgarianBin = bulgarianCheckerBin.getSymbolsChances(binTextBulgarian, bulgarianDictBin);

                            italianChecker.computeTextEntropy(chancesItalian);
                            bulgarianChecker.computeTextEntropy(chancesBulgarian);
                            italianCheckerBin.computeTextEntropy(chancesItalianBin);
                            bulgarianCheckerBin.computeTextEntropy(chancesBulgarianBin);


                            italianChecker.printAlphabet();
                            italianChecker.printChances(chancesItalian);
                            italianChecker.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {italianChecker.AlphabetName}: {italianChecker.AlphabetEntropy *italianText.Length}");

                            bulgarianChecker.printAlphabet();
                            bulgarianChecker.printChances(chancesBulgarian);
                            bulgarianChecker.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {bulgarianChecker.AlphabetName}: {bulgarianChecker.AlphabetEntropy * bulgarianText.Length}");

                            italianCheckerBin.printAlphabet();
                            italianCheckerBin.printChances(chancesItalianBin);
                            italianCheckerBin.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {italianCheckerBin.AlphabetName}: {italianCheckerBin.AlphabetEntropy * binTextItalian.Length}");

             

                            bulgarianCheckerBin.printAlphabet();
                            bulgarianCheckerBin.printChances(chancesBulgarianBin);
                            bulgarianCheckerBin.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {bulgarianCheckerBin.AlphabetName}: {bulgarianCheckerBin.AlphabetEntropy * binTextBulgarian.Length}");
                           

                            double sumItalian = 0;
                            double sumBulgarian = 0;
                            double sumItalianBin = 0;
                            double sumBulgarianBin = 0;
                            foreach (KeyValuePair<char, double> x in chancesItalian)
                            {
                                sumItalian += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesBulgarian)
                            {
                                sumBulgarian += x.Value;
                            }

                            foreach (KeyValuePair<char, double> x in chancesItalianBin)
                            {
                                sumItalianBin += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesBulgarianBin)
                            {
                                sumBulgarianBin += x.Value;
                            }
                                 
                            Console.WriteLine($"Сумма шансов для болгарского языка: {sumBulgarian}");
                            Console.WriteLine($"Сумма шансов для итальянского языка: {sumItalian}");
                            Console.WriteLine($"Сумма шансов для болгарского языка (бинарный): {sumBulgarianBin}");
                            Console.WriteLine($"Сумма шансов для итальянского языка (бинарный): {sumItalianBin}");




                            Console.ReadKey();
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();

                            FindEntropy italianCheckerBin = new FindEntropy(new List<char>() { '0', '1' }, 0.99790926605, "Бинарный код (итальянский)");
                            FindEntropy bulgarianCheckerBin = new FindEntropy(new List<char>() { '0', '1' }, 0.99539441495, "Бинарный код (болгарский)");

                            string italianText = "belitskyvladislavdmitrievich";
                            string bulgarianText = "белицкивладиславдмиртиевич";

                            string binTextItalian = "";
                            string binTextBulgarian = "";
                            ///
                            double ital = italianCheckerBin.AlphabetEntropy * binTextItalian.Length + 223.94847243841025;
                            double bulg = bulgarianCheckerBin.AlphabetEntropy * binTextBulgarian.Length + 412.9359912735061;
                            ///
                            var textChr = Encoding.UTF8.GetBytes(italianText);
                            foreach (int chr in textChr)
                            {
                                binTextItalian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            textChr = Encoding.UTF8.GetBytes(bulgarianText);
                            foreach (int chr in textChr)
                            {
                                binTextBulgarian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            Dictionary<char, int> italianDictBin = italianCheckerBin.alphabetListToDictionary();
                            Dictionary<char, int> bulgarianDictBin = bulgarianCheckerBin.alphabetListToDictionary();

                            italianCheckerBin.getSymbolsCounts(binTextItalian, italianDictBin);
                            bulgarianCheckerBin.getSymbolsCounts(binTextBulgarian, bulgarianDictBin);

                            Dictionary<char, double> chancesItalianBin = italianCheckerBin.getSymbolsChances(binTextItalian, italianDictBin);
                            Dictionary<char, double> chancesBulgarianBin = bulgarianCheckerBin.getSymbolsChances(binTextBulgarian, bulgarianDictBin);

                            italianCheckerBin.printAlphabet();
                            italianCheckerBin.printChances(chancesItalianBin);
                            italianCheckerBin.printAlhabetEntropy();
                            bulgarianCheckerBin.printAlphabet();
                            bulgarianCheckerBin.printChances(chancesBulgarianBin);
                            bulgarianCheckerBin.printAlhabetEntropy();

                            Console.WriteLine("Для бинарного алфавита:");
                            Console.WriteLine($"Ошибка = 0.1. Количество информации сообщения. Язык - {italianCheckerBin.AlphabetName}: {italianCheckerBin.computeTextEntropyWithError(chancesItalianBin, 0.1,28)}");
                            Console.WriteLine($"Ошибка = 0.5. Количество информации сообщения. Язык - {italianCheckerBin.AlphabetName}: {italianCheckerBin.computeTextEntropyWithError(chancesItalianBin, 0.5,28)}");
                            Console.WriteLine($"Ошибка = 1. Количество информации сообщения. Язык - (Бинарный код итальянский) {ital * 1}");
                            Console.WriteLine("\n");
                            Console.WriteLine($"Ошибка = 0.1. Количество информации сообщения. Язык - {bulgarianCheckerBin.AlphabetName}: {bulgarianCheckerBin.computeTextEntropyWithError(chancesBulgarianBin, 0.1,26)}");
                            Console.WriteLine($"Ошибка = 0.5. Количество информации сообщения. Язык - {bulgarianCheckerBin.AlphabetName}: {bulgarianCheckerBin.computeTextEntropyWithError(chancesBulgarianBin, 0.5,26)}");
                            Console.WriteLine($"Ошибка = 1. Количество информации сообщения. Язык - Бинарный код (болгарский) {bulg * 1}");
                     

                            Console.ReadKey();

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
