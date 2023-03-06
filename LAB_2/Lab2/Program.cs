using Lab2.DocumentReader;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab2
{
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
                            EntropyChecker italianChecker = new EntropyChecker(italianAlphabet, 0, "Итальянский");
                            EntropyChecker ukranianChecker = new EntropyChecker(bulgarianAlphabet, 0, "Болгарский");

                            string italianText = italianChecker.OpenDocument("italian.txt").ReadToEnd().ToLower();
                            string ukranianText = italianChecker.OpenDocument("bulgarian.txt").ReadToEnd().ToLower();

                            Regex regex = new Regex(@"\W");
                            italianText = regex.Replace(italianText, "");
                            ukranianText = regex.Replace(ukranianText, "");

                            Dictionary<char, int> italianDict = italianChecker.alphabetListToDictionary();
                            Dictionary<char, int> ukranianDict = ukranianChecker.alphabetListToDictionary();

                            italianChecker.getSymbolsCounts(italianText, italianDict);
                            ukranianChecker.getSymbolsCounts(ukranianText, ukranianDict);

                            Dictionary<char, double> chancesGolland = italianChecker.getSymbolsChances(italianText, italianDict);
                            Dictionary<char, double> chancesUkranian = ukranianChecker.getSymbolsChances(ukranianText, ukranianDict);

                            italianChecker.computeTextEntropy(chancesGolland);
                            ukranianChecker.computeTextEntropy(chancesUkranian);


                            italianChecker.printAlphabet();
                            italianChecker.printChances(chancesGolland);
                            italianChecker.printAlhabetEntropy();


                            ukranianChecker.printAlphabet();
                            ukranianChecker.printChances(chancesUkranian);
                            ukranianChecker.printAlhabetEntropy();

                            double sumGolland = 0;
                            double sumUkranian = 0;
                            foreach (KeyValuePair<char, double> x in chancesGolland)
                            {
                                sumGolland += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesUkranian)
                            {
                                sumUkranian += x.Value;
                            }

                            Console.WriteLine($"Сумма шансов для болгарского языка: {sumUkranian}");
                            Console.WriteLine($"Сумма шансов для итальянского языка: {sumGolland}");

                            ExcelDocumentCreator<char,double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));
                            excel.createWorksheet("first");
                            excel.addValuesFromDict(chancesGolland, "first", 0);
                            excel.addValuesFromDict(chancesUkranian, "first", 3);
                            excel.pack.Save();
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();

                            EntropyChecker italianChecker = new EntropyChecker(new List<char>(){ '0', '1' }, 0, "Бинарный код");
                            EntropyChecker ukranianChecker = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код");

                            string italianText = italianChecker.OpenDocument("italian.txt").ReadToEnd().ToLower();
                            string ukranianText = italianChecker.OpenDocument("bulgarian.txt").ReadToEnd().ToLower();

                            Regex regex = new Regex(@"\W");
                            italianText = regex.Replace(italianText, "");
                            ukranianText = regex.Replace(ukranianText, "");

                            string binTextItalian = "";
                            string binTextUkrainian = "";

                            var textChr = Encoding.UTF8.GetBytes(italianText);
                            foreach (int chr in textChr)
                            {
                                binTextItalian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            textChr = Encoding.UTF8.GetBytes(ukranianText);
                            foreach (int chr in textChr)
                            {
                                binTextUkrainian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            Dictionary<char, int> golladnDict = italianChecker.alphabetListToDictionary();
                            Dictionary<char, int> ukranianDict = ukranianChecker.alphabetListToDictionary();

                            italianChecker.getSymbolsCounts(binTextItalian, golladnDict);
                            ukranianChecker.getSymbolsCounts(binTextUkrainian, ukranianDict);

                            Dictionary<char, double> chancesGolland = italianChecker.getSymbolsChances(binTextItalian, golladnDict);
                            Dictionary<char, double> chancesUkranian = ukranianChecker.getSymbolsChances(binTextUkrainian, ukranianDict);

                            italianChecker.computeTextEntropy(chancesGolland);
                            ukranianChecker.computeTextEntropy(chancesUkranian);


                            italianChecker.printAlphabet();
                            italianChecker.printChances(chancesGolland);
                            italianChecker.printAlhabetEntropy();


                            ukranianChecker.printAlphabet();
                            ukranianChecker.printChances(chancesUkranian);
                            ukranianChecker.printAlhabetEntropy();

                            double sumGolland = 0;
                            double sumUkranian = 0;
                            foreach (KeyValuePair<char, double> x in chancesGolland)
                            {
                                sumGolland += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesUkranian)
                            {
                                sumUkranian += x.Value;
                            }

                            Console.WriteLine($"Сумма шансов для болгарского языка: {sumUkranian}");
                            Console.WriteLine($"Сумма шансов для итальянского языка: {sumGolland}");

                            ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));
                            excel.createWorksheet("second");
                            excel.addValuesFromDict(chancesGolland, "second", 0);
                            excel.addValuesFromDict(chancesUkranian, "second", 3);
                            excel.pack.Save();

                            Console.ReadKey();
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();

                            EntropyChecker italianChecker = new EntropyChecker(italianAlphabet, 0, "Итальянский");
                            EntropyChecker ukranianChecker = new EntropyChecker(bulgarianAlphabet, 0, "Болгарский");
                            EntropyChecker italianCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (итальянский)");
                            EntropyChecker ukranianCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (болгарский)");

                            string italianText = "belitskyvladislavdmitrievich";
                            string ukranianText = "белицкивладиславдмиртиевич";

                            string binTextItalian = "";
                            string binTextUkrainian = "";

                            var textChr = Encoding.UTF8.GetBytes(italianText);
                            foreach (int chr in textChr)
                            {
                                binTextItalian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            textChr = Encoding.UTF8.GetBytes(ukranianText);
                            foreach (int chr in textChr)
                            {
                                binTextUkrainian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            Dictionary<char, int> golladnDict = italianChecker.alphabetListToDictionary();
                            Dictionary<char, int> ukranianDict = ukranianChecker.alphabetListToDictionary();
                            Dictionary<char, int> golladnDictBin = italianCheckerBin.alphabetListToDictionary();
                            Dictionary<char, int> ukranianDictBin = ukranianCheckerBin.alphabetListToDictionary();

                            italianChecker.getSymbolsCounts(italianText, golladnDict);
                            ukranianChecker.getSymbolsCounts(ukranianText, ukranianDict);
                            italianCheckerBin.getSymbolsCounts(binTextItalian, golladnDictBin);
                            ukranianCheckerBin.getSymbolsCounts(binTextUkrainian, ukranianDictBin);

                            Dictionary<char, double> chancesItalian = italianChecker.getSymbolsChances(italianText, golladnDict);
                            Dictionary<char, double> chancesUkranian = ukranianChecker.getSymbolsChances(ukranianText, ukranianDict);
                            Dictionary<char, double> chancesItalianBin = italianCheckerBin.getSymbolsChances(binTextItalian, golladnDictBin);
                            Dictionary<char, double> chancesUkranianBin = ukranianCheckerBin.getSymbolsChances(binTextUkrainian, ukranianDictBin);

                            italianChecker.computeTextEntropy(chancesItalian);
                            ukranianChecker.computeTextEntropy(chancesUkranian);
                            italianCheckerBin.computeTextEntropy(chancesItalianBin);
                            ukranianCheckerBin.computeTextEntropy(chancesUkranianBin);


                            italianChecker.printAlphabet();
                            italianChecker.printChances(chancesItalian);
                            italianChecker.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {italianChecker.AlphabetName}: {italianChecker.AlphabetEntropy *italianText.Length}");

                            ukranianChecker.printAlphabet();
                            ukranianChecker.printChances(chancesUkranian);
                            ukranianChecker.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {ukranianChecker.AlphabetName}: {ukranianChecker.AlphabetEntropy * ukranianText.Length}");

                            italianCheckerBin.printAlphabet();
                            italianCheckerBin.printChances(chancesItalianBin);
                            italianCheckerBin.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {italianCheckerBin.AlphabetName}: {italianCheckerBin.AlphabetEntropy * binTextItalian.Length}");

                            ukranianCheckerBin.printAlphabet();
                            ukranianCheckerBin.printChances(chancesUkranianBin);
                            ukranianCheckerBin.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {ukranianCheckerBin.AlphabetName}: {ukranianCheckerBin.AlphabetEntropy * binTextUkrainian.Length}");


                            double sumGolland = 0;
                            double sumUkranian = 0;
                            double sumItalianBin = 0;
                            double sumUkranianBin = 0;
                            foreach (KeyValuePair<char, double> x in chancesItalian)
                            {
                                sumGolland += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesUkranian)
                            {
                                sumUkranian += x.Value;
                            }

                            foreach (KeyValuePair<char, double> x in chancesItalianBin)
                            {
                                sumItalianBin += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesUkranianBin)
                            {
                                sumUkranianBin += x.Value;
                            }

                            Console.WriteLine($"Сумма шансов для болгарского языка: {sumUkranian}");
                            Console.WriteLine($"Сумма шансов для итальянского языка: {sumGolland}");
                            Console.WriteLine($"Сумма шансов для болгарского языка (бинарный): {sumUkranianBin}");
                            Console.WriteLine($"Сумма шансов для итальянского языка (бинарный): {sumItalianBin}");

                            ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));
                            excel.createWorksheet("third");
                            excel.addValuesFromDict(chancesItalian, "third", 0);
                            excel.addValuesFromDict(chancesUkranian, "third", 3);
                            excel.addValuesFromDict(chancesItalianBin, "third", 5);
                            excel.addValuesFromDict(chancesUkranianBin, "third", 7);
                            excel.pack.Save();


                            Console.ReadKey();
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();

                            EntropyChecker italianCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (итальянский)");
                            EntropyChecker ukranianCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (болгарский)");

                            string italianText = "belitskyvladislavdmitrievich";
                            string ukranianText = "белицкивладиславдмиртиевич";

                            string binTextItalian = "";
                            string binTextUkrainian = "";

                            var textChr = Encoding.UTF8.GetBytes(italianText);
                            foreach (int chr in textChr)
                            {
                                binTextItalian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            textChr = Encoding.UTF8.GetBytes(ukranianText);
                            foreach (int chr in textChr)
                            {
                                binTextUkrainian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            Dictionary<char, int> golladnDictBin = italianCheckerBin.alphabetListToDictionary();
                            Dictionary<char, int> ukranianDictBin = ukranianCheckerBin.alphabetListToDictionary();

                            italianCheckerBin.getSymbolsCounts(binTextItalian, golladnDictBin);
                            ukranianCheckerBin.getSymbolsCounts(binTextUkrainian, ukranianDictBin);

                            Dictionary<char, double> chancesGollandBin = italianCheckerBin.getSymbolsChances(binTextItalian, golladnDictBin);
                            Dictionary<char, double> chancesUkranianBin = ukranianCheckerBin.getSymbolsChances(binTextUkrainian, ukranianDictBin);

                            italianCheckerBin.printAlphabet();
                            italianCheckerBin.printChances(chancesGollandBin);
                            italianCheckerBin.printAlhabetEntropy();
                            ukranianCheckerBin.printAlphabet();
                            ukranianCheckerBin.printChances(chancesUkranianBin);
                            ukranianCheckerBin.printAlhabetEntropy();

                            Console.WriteLine($"Ошибка = 0.1. Количество информации сообщения. Язык - {italianCheckerBin.AlphabetName}: {italianCheckerBin.computeTextEntropyWithError(chancesGollandBin, 0.1) * binTextItalian.Length}");
                            Console.WriteLine($"Ошибка = 0.5. Количество информации сообщения. Язык - {italianCheckerBin.AlphabetName}: {italianCheckerBin.computeTextEntropyWithError(chancesGollandBin, 0.5) * binTextItalian.Length}");
                            Console.WriteLine($"Ошибка = 0.9. Количество информации сообщения. Язык - {italianCheckerBin.AlphabetName}: {italianCheckerBin.computeTextEntropyWithError(chancesGollandBin, 0.9) * binTextItalian.Length}");

                            Console.WriteLine($"Ошибка = 0.1. Количество информации сообщения. Язык - {ukranianCheckerBin.AlphabetName}: {ukranianCheckerBin.computeTextEntropyWithError(chancesUkranianBin,0.1) * binTextItalian.Length}");
                            Console.WriteLine($"Ошибка = 0.5. Количество информации сообщения. Язык - {ukranianCheckerBin.AlphabetName}: {ukranianCheckerBin.computeTextEntropyWithError(chancesUkranianBin, 0.5) * binTextItalian.Length}");
                            Console.WriteLine($"Ошибка = 0.9. Количество информации сообщения. Язык - {ukranianCheckerBin.AlphabetName}: {ukranianCheckerBin.computeTextEntropyWithError(chancesUkranianBin, 0.9) * binTextItalian.Length}");


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
