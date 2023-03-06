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
                            EntropyChecker bulgarianChecker = new EntropyChecker(bulgarianAlphabet, 0, "Болгарский");

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

                            ExcelDocumentCreator<char,double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));
                            excel.createWorksheet("first");
                            excel.addValuesFromDict(chancesItalian, "first", 0);
                            excel.addValuesFromDict(chancesBulgarian, "first", 3);
                            excel.pack.Save();
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();

                            EntropyChecker italianChecker = new EntropyChecker(new List<char>(){ '0', '1' }, 0, "Бинарный код");
                            EntropyChecker bulgarianChecker = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код");

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

                            ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));
                            excel.createWorksheet("second");
                            excel.addValuesFromDict(chancesItalian, "second", 0);
                            excel.addValuesFromDict(chancesBulgarian, "second", 3);
                            excel.pack.Save();

                            Console.ReadKey();
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();

                            EntropyChecker italianChecker = new EntropyChecker(italianAlphabet, 0, "Итальянский");
                            EntropyChecker bulgarianChecker = new EntropyChecker(bulgarianAlphabet, 0, "Болгарский");
                            EntropyChecker italianCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (итальянский)");
                            EntropyChecker bulgarianCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (болгарский)");

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

                            ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));
                            excel.createWorksheet("third");
                            excel.addValuesFromDict(chancesItalian, "third", 0);
                            excel.addValuesFromDict(chancesBulgarian, "third", 3);
                            excel.addValuesFromDict(chancesItalianBin, "third", 5);
                            excel.addValuesFromDict(chancesBulgarianBin, "third", 7);
                            excel.pack.Save();


                            Console.ReadKey();
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();

                            EntropyChecker italianCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (итальянский)");
                            EntropyChecker bulgarianCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (болгарский)");

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

                            Console.WriteLine($"Ошибка = 0.1. Количество информации сообщения. Язык - {italianCheckerBin.AlphabetName}: {italianCheckerBin.computeTextEntropyWithError(chancesItalianBin, 0.1) * binTextItalian.Length}");
                            Console.WriteLine($"Ошибка = 0.5. Количество информации сообщения. Язык - {italianCheckerBin.AlphabetName}: {italianCheckerBin.computeTextEntropyWithError(chancesItalianBin, 0.5) * binTextItalian.Length}");
                            Console.WriteLine($"Ошибка = 0.9. Количество информации сообщения. Язык - {italianCheckerBin.AlphabetName}: {italianCheckerBin.computeTextEntropyWithError(chancesItalianBin, 0.9) * binTextItalian.Length}");

                            Console.WriteLine($"Ошибка = 0.1. Количество информации сообщения. Язык - {bulgarianCheckerBin.AlphabetName}: {bulgarianCheckerBin.computeTextEntropyWithError(chancesBulgarianBin,0.1) * binTextItalian.Length}");
                            Console.WriteLine($"Ошибка = 0.5. Количество информации сообщения. Язык - {bulgarianCheckerBin.AlphabetName}: {bulgarianCheckerBin.computeTextEntropyWithError(chancesBulgarianBin, 0.5) * binTextItalian.Length}");
                            Console.WriteLine($"Ошибка = 0.9. Количество информации сообщения. Язык - {bulgarianCheckerBin.AlphabetName}: {bulgarianCheckerBin.computeTextEntropyWithError(chancesBulgarianBin, 0.9) * binTextItalian.Length}");


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
