using System;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            int c = 0;
            while(true)
            {
                Console.WriteLine("Введите номер задания:");
                Console.WriteLine("1- НОД двух чисел");
                Console.WriteLine("2- НОД трёх чисел");
                Console.WriteLine("3- Поиск простых чисел из диапазона");
                Console.WriteLine("4- Каноническая форма");
                Console.WriteLine("5- Проверим на простое");
                Console.WriteLine("6- Решето Эратосфена");

                if (!int.TryParse(Console.ReadLine(), out c))
                {
                    c = -1;
                }
                switch(c)
                {
                    case 1:
                        {
                            int x = 0, y = 0;
                            Console.Write("Введите первое число: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            Console.Write("Введите второе число: ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            Console.WriteLine($"НОД двух чисел ({x},{y}) равен: {NOD.Compute(x,y)} ");
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        }

                    case 2:
                        {
                            int x = 0, y = 0, z = 0;
                            Console.Write("Введите первое число: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            Console.Write("Введите второе число: ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            Console.Write("Введите третье число: ");
                            if (!int.TryParse(Console.ReadLine(), out z))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }

                            Console.WriteLine($"НОД трёх чисел ({x},{y},{z}) равен: {NOD.Compute(z,NOD.Compute(x, y))} ");
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        }
                    case 3:
                        {
                            int x = 0, y = 0;
                            Console.Write("Введите первое число: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            Console.Write("Введите второе число, больше первого: ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            NOD.FindSimple(x, y);
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        }
                    case 4:
                        {
                            int x = 0;
                            Console.Write("Введите число: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            
                          
                         NOD.Canon(x);
                           
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        }
                    case 5:
                        Console.Clear();
                        int maybeSimple = 0;
                        Console.Write("Введите число: ");
                        if (!int.TryParse(Console.ReadLine(), out maybeSimple))
                        {
                            Console.Write("Ошибка!");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                        if(NOD.IsSimple(maybeSimple) == true)
                        {
                            Console.WriteLine("Простое");
                        }
                        else
                        {
                            Console.WriteLine("Не простое");
                        }

                        break;

                        case 6:
                        Console.Clear();
                        Console.Write("Первое число:");
                        var m = Convert.ToUInt32(Console.ReadLine());
                        Console.Write("Второе число: ");
                        var n = Convert.ToUInt32(Console.ReadLine());
                        var primeNumbers = NOD.SieveEratosthenes(m, n);
                        Console.WriteLine("Простые числа до заданного {0}:", n);
                        Console.WriteLine(string.Join(", ", primeNumbers));
                        Console.ReadLine();                    
                        break;
                    case -1:
                        {
                            Console.Clear();
                            break;
                        }
                    default:
                        {
                            Console.Clear();
                            break;
                        }
                }
            }
        }
    }
}
