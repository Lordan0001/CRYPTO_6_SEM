using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_5
{
    public class Route
    {
        public static string Encrypt(string input, int rows, int columns)
        {
            char[,] table = new char[rows, columns]; // создание таблицы

            // заполнение таблицы
            int index = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (index < input.Length)
                    {
                        table[i, j] = input[index];
                        index++;
                    }
                    else
                    {
                        table[i, j] = ' ';
                    }
                }
            }

            // считывание таблицы по столбцам и запись зашифрованного сообщения
            string output = "";
            for (int j = 0; j < columns; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    output += table[i, j];
                }
            }

            return output;
        }

        public static string Decrypt(string input, int rows, int columns)
        {
            char[,] table = new char[rows, columns]; // создание таблицы

            // заполнение таблицы
            int index = 0;
            for (int j = 0; j < columns; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    if (index < input.Length)
                    {
                        table[i, j] = input[index];
                        index++;
                    }
                    else
                    {
                        table[i, j] = ' ';
                    }
                }
            }

            // запись расшифрованного сообщения
            string output = "";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    output += table[i, j];
                }
            }

            return output;
        }


    }
}
