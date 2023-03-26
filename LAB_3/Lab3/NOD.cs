using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    static class NOD
    {
        public static int Compute(int x, int y)
        {
            while (x != 0 && y != 0)
            {
                if (x > y)
                {
                    x -= y;
                }
                else
                {
                    y -= x;
                }
            }
            return Math.Max(x, y);

        }

        public static  void Canon(int num)
        {
            Console.Write("{0} = 1", num);
            for (int i = 0; num % 2 == 0; num /= 2)
            {
                Console.Write(" * {0}", 2);
            }
            for (int i = 3; i <= num;)
            {
                if (num % i == 0)
                {
                    Console.Write(" * {0}", i);
                    num /= i;
                }
                else
                {
                    i += 2;
                }
            }
        }

        private static bool IsSimple(int x)
        {
            for (int i = 2; Math.Pow(i, 2) <= x; i++)
            {
                if (x % i == 0)
                {
                    return false;
                }
            }

            return true;
        }


        public static void FindSimple(int m, int n)
        {
            int counter = 0;
            if (n < m)
            {
                Console.WriteLine("Неверный промежуток");
            }

            Console.Write($"Простые числа интервала [{m},{n}]: ");

            for (int i = m; i <= n; i++)
            {
                if (IsSimple(i))
                {
                    Console.Write(i.ToString() + " ");
                    counter++;
                }
            }
            Console.WriteLine();
            Console.WriteLine($"Количество простых чисел: {counter}");

        }
    }
}
