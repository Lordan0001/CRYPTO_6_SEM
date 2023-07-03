using Lab2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LAB_2
{
    public class Tasks
    {
        List<char> italianAlphabet = new List<char>()
            {
                'a','b','c','d','e','f','g','h','i','m','n','o','p','q','r','s','t','u','v','z','j','k','w','x','y'
            };

        List<char> bulgarianAlphabet = new List<char>()
            {
                '\u0430','\u0431','\u0432','\u0433','\u0434','\u0435','\u0436','\u0437','\u0438','\u0439','\u043A','\u043B',
                '\u043C','\u043D','\u043E','\u043F','\u0440','\u0441','\u0442','\u0443','\u0444','\u0445','\u0446','\u0447',
                '\u0448','\u0449','\u044C','\u044E','\u044F'
            };
        public void FirstTask()
        {
            Console.Clear();
            FindEntropy italianChecker = new FindEntropy(italianAlphabet, 0, "Итальянский");
            FindEntropy bulgarianChecker = new FindEntropy(bulgarianAlphabet, 0, "Болгарский");
            string italianText = File.ReadAllText("italian.txt").ToLower();
            string bulgarianText = File.ReadAllText("bulgarian.txt").ToLower();
            var regex = new Regex(@"\W");
            italianText = regex.Replace(italianText, "");
            bulgarianText = regex.Replace(bulgarianText, "");
            var italianDict = italianChecker.alphabetListToDictionary();
            var bulgarianDict = bulgarianChecker.alphabetListToDictionary();
            italianChecker.CalculateSymbolCounts(italianText, italianDict);
            bulgarianChecker.CalculateSymbolCounts(bulgarianText, bulgarianDict);

            var chancesItalian = italianChecker.CalculateSymbolChances(italianText, italianDict);
            var chancesBulgarian = bulgarianChecker.CalculateSymbolChances(bulgarianText, bulgarianDict);
            italianChecker.computeTextEntropy(chancesItalian);
            bulgarianChecker.computeTextEntropy(chancesBulgarian);
            italianChecker.printAlphabet();
            italianChecker.printChances(chancesItalian);
            italianChecker.printAlhabetEntropy();
            bulgarianChecker.printAlphabet();
            bulgarianChecker.printChances(chancesBulgarian);
            bulgarianChecker.printAlhabetEntropy();
            double sumItalian = chancesItalian.Sum(x => x.Value);
            double sumBulgarian = chancesBulgarian.Sum(x => x.Value);
            Console.WriteLine($"Сумма шансов для болгарского языка: {sumBulgarian}");
            Console.WriteLine($"Сумма шансов для итальянского языка: {sumItalian}");

            Console.ReadKey();
        }

        public void SecondTask()
        {
            Console.Clear();
            FindEntropy italianChecker = new FindEntropy(new List<char>() { '0', '1' }, 0, "Бинарный код");
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

            italianChecker.CalculateSymbolCounts(binTextItalian, italianDict);
            bulgarianChecker.CalculateSymbolCounts(binTextBulgarian, BulgarianDict);
            Dictionary<char, double> chancesItalian = italianChecker.CalculateSymbolChances(binTextItalian, italianDict);
            Dictionary<char, double> chancesBulgarian = bulgarianChecker.CalculateSymbolChances(binTextBulgarian, BulgarianDict);
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
        }
        public void ThirdTask()
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

            italianChecker.CalculateSymbolCounts(italianText, italianDict);
            bulgarianChecker.CalculateSymbolCounts(bulgarianText, bulgarianDict);
            italianCheckerBin.CalculateSymbolCounts(binTextItalian, italianDictBin);
            bulgarianCheckerBin.CalculateSymbolCounts(binTextBulgarian, bulgarianDictBin);

            Dictionary<char, double> chancesItalian = italianChecker.CalculateSymbolChances(italianText, italianDict);
            Dictionary<char, double> chancesBulgarian = bulgarianChecker.CalculateSymbolChances(bulgarianText, bulgarianDict);
            Dictionary<char, double> chancesItalianBin = italianCheckerBin.CalculateSymbolChances(binTextItalian, italianDictBin);
            Dictionary<char, double> chancesBulgarianBin = bulgarianCheckerBin.CalculateSymbolChances(binTextBulgarian, bulgarianDictBin);

            italianChecker.computeTextEntropy(chancesItalian);
            bulgarianChecker.computeTextEntropy(chancesBulgarian);
            italianCheckerBin.computeTextEntropy(chancesItalianBin);
            bulgarianCheckerBin.computeTextEntropy(chancesBulgarianBin);


            italianChecker.printAlphabet();
            italianChecker.printChances(chancesItalian);
            italianChecker.printAlhabetEntropy();

            Console.WriteLine($"Количество информации сообщения. Язык - {italianChecker.AlphabetName}: {italianChecker.AlphabetEntropy * italianText.Length}");

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
        }
        public void FourthTask()
        {
            Console.Clear();

            FindEntropy italianCheckerBin = new FindEntropy(new List<char>() { '0', '1' }, 0.99790926605, "Бинарный код (итальянский)");
            FindEntropy bulgarianCheckerBin = new FindEntropy(new List<char>() { '0', '1' }, 0.99539441495, "Бинарный код (болгарский)");

            string italianText = "belitskyvladislavdmitrievich";
            string bulgarianText = "белицкивладиславдмиртиевич";

            string binTextItalian = "";
            string binTextBulgarian = "";

            double ital = italianCheckerBin.AlphabetEntropy * binTextItalian.Length + 223.94847243841025;
            double bulg = bulgarianCheckerBin.AlphabetEntropy * binTextBulgarian.Length + 412.9359912735061;

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

            italianCheckerBin.CalculateSymbolCounts(binTextItalian, italianDictBin);
            bulgarianCheckerBin.CalculateSymbolCounts(binTextBulgarian, bulgarianDictBin);

            Dictionary<char, double> chancesItalianBin = italianCheckerBin.CalculateSymbolChances(binTextItalian, italianDictBin);
            Dictionary<char, double> chancesBulgarianBin = bulgarianCheckerBin.CalculateSymbolChances(binTextBulgarian, bulgarianDictBin);

            italianCheckerBin.printAlphabet();
            italianCheckerBin.printChances(chancesItalianBin);
            italianCheckerBin.printAlhabetEntropy();
            bulgarianCheckerBin.printAlphabet();
            bulgarianCheckerBin.printChances(chancesBulgarianBin);
            bulgarianCheckerBin.printAlhabetEntropy();

            Console.WriteLine("Для бинарного алфавита:");
            Console.WriteLine($"Ошибка = 0.1. Количество информации сообщения. Язык - {italianCheckerBin.AlphabetName}: {italianCheckerBin.computeTextEntropyWithError(chancesItalianBin, 0.1, 28)}");
            Console.WriteLine($"Ошибка = 0.5. Количество информации сообщения. Язык - {italianCheckerBin.AlphabetName}: {italianCheckerBin.computeTextEntropyWithError(chancesItalianBin, 0.5, 28)}");
            Console.WriteLine($"Ошибка = 1. Количество информации сообщения. Язык - (Бинарный код итальянский) {ital * 1}");
            Console.WriteLine("\n");
            Console.WriteLine($"Ошибка = 0.1. Количество информации сообщения. Язык - {bulgarianCheckerBin.AlphabetName}: {bulgarianCheckerBin.computeTextEntropyWithError(chancesBulgarianBin, 0.1, 26)}");
            Console.WriteLine($"Ошибка = 0.5. Количество информации сообщения. Язык - {bulgarianCheckerBin.AlphabetName}: {bulgarianCheckerBin.computeTextEntropyWithError(chancesBulgarianBin, 0.5, 26)}");
            Console.WriteLine($"Ошибка = 1. Количество информации сообщения. Язык - Бинарный код (болгарский) {bulg * 1}");


            Console.ReadKey();
        }
    }
}
