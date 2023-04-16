using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            int numCopy = num;
            Dictionary<int,int> CanonList = new ();
        
            for (int i = 0; num % 2 == 0; num /= 2)
            {

      
                if(CanonList.ContainsKey(2))
                {
                    CanonList[2] += 1;
                }
                else
                {
                    CanonList.Add(2, 1);
                }
            }
            for (int i = 3; i <= num;)
            {
                if (num % i == 0)
                {
 
                    if (CanonList.ContainsKey(i))
                    {
                        CanonList[i] += 1;
                    }
                    else
                    {
                        CanonList.Add(i, 1);
                    }
                    num /= i;
                }
                else
                {
                    i += 2;
                }
            }

            Console.Write(numCopy + " = 1");

            foreach (int key in CanonList.Keys)
            {
                Console.Write($" * {key}");
                if(CanonList[key] != 1)
                {
                    Console.Write($"^{CanonList[key]}");
                }
            }

        }

        public static bool IsSimple(int x)
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
            Console.WriteLine("n/ln(n) = " + n / Math.Log(n));

        }
      public  static List<uint> SieveEratosthenes(uint m, uint n)
        {
            var numbers = new List<uint>();
            //заполнение списка числами от 2 до n-1
            for (var i = 2u; i < n; i++)
            {
                numbers.Add(i);
            }

            for (var i = 0; i < numbers.Count; i++)
            {
                for (var j = 2u; j < n; j++)
                {
                    //удаляем кратные числа из списка
                    numbers.Remove(numbers[i] * j);
                }
            }
            for (int i = 0; i < numbers.Count(); i++)
            {
                if (numbers[i] < m)
                {
                    numbers.RemoveAt(i);
                    i--;
                }
            }
            numbers.Add(n);
            Console.WriteLine("Колличество чисел: "+numbers.Count);
            return numbers;
        }

       
    }
}
