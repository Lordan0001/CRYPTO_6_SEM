using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_5
{
    public class SecondCipher
    {
        public static string Encrypt2(string plaintext, string rowKey, string columnKey)
        {
            int rows = rowKey.Length;
            int columns = columnKey.Length;
            int length = rows * columns;

            char[,] matrix = new char[rows, columns];

            int index = 0;

            for (int j = 0; j < columns; j++)
            {
                int k = columnKey.IndexOf(columnKey[j]);

                for (int i = 0; i < rows; i++)
                {
                    int l = rowKey.IndexOf(rowKey[i]);

                    if (index < plaintext.Length)
                    {
                        matrix[l, k] = plaintext[index];
                        index++;
                    }
                    else
                    {
                        matrix[l, k] = ' ';
                    }
                }
            }

            string ciphertext = "";

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    ciphertext += matrix[i, j];
                }
            }

            return ciphertext;
        }

      public  static string Decrypt2(string ciphertext, string rowKey, string columnKey)
        {
            int rows = rowKey.Length;
            int columns = columnKey.Length;
            int length = rows * columns;

            char[,] matrix = new char[rows, columns];

            // заполнение матрицы по столбцам
            int index = 0;
            for (int j = 0; j < columns; j++)
            {
                int k = columnKey.IndexOf(columnKey[j]);
                for (int i = 0; i < rows; i++)
                {
                    int l = rowKey.IndexOf(rowKey[i]);
                    matrix[l, k] = ciphertext[index];
                    index++;
                }
            }

            // считывание зашифрованного сообщения по строкам
            string plaintext = "";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    plaintext += matrix[i, j];
                }
            }

            return plaintext.TrimEnd(); // удаляем лишние пробелы в конце строки
        }
    }
    }
