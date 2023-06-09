﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


    class Program
    {
        static void Main(string[] args)
        {

           
                Console.WriteLine($"\nХеширование SHA256\n");
                Stopwatch stopwatch1 = new Stopwatch();
                stopwatch1.Start();

                string text = "Bialitski Vladislav";
                string salt = CreateSalt(15);
                string hash = GenerateSHA256(text, salt);

                Console.WriteLine("Text:  " + text + "\nХэш:  " + hash);
                Console.WriteLine("Time " + stopwatch1.ElapsedMilliseconds + " ms");
                Console.WriteLine();
                Console.WriteLine($"\nХеширование MD\n");
                string hash1;
                Stopwatch stopwatch2 = new Stopwatch();
                stopwatch2.Start();
                using (MD5 md5Hash = MD5.Create())
                {
                    hash1 = GetMd5Hash(md5Hash, text);


                    Console.WriteLine("Text:  " + text + "\nХэш:  " + hash1);
                }
                Console.WriteLine("Time " + stopwatch2.ElapsedMilliseconds + " ms");
            
            Console.ReadKey();
        }
        public static string CreateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string GenerateSHA256(string input, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            return ToHex(hash);
        }

        public static string ToHex(byte[] ba)
        {

            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
