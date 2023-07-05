using LAB_13;
using System.Diagnostics;

Console.WriteLine("--------- 1.1");
int xMin = 0, xMax = 35, a = -1, b = 1, p = 751;
for (int x = xMin; x <= xMax; x++)
{
    Console.WriteLine($"x = {x}, y = {Math.Sqrt((x * x * x - x + b) % p)}");
}

Console.WriteLine("--------- 1.2");
int k = 8;
int[] P = { 58, 139 }, Q = { 67, 667 }, R = { 82, 481 };
Console.WriteLine($" P({P[0]}, {P[1]}), Q({Q[0]}, {Q[1]}), R({R[0]}, {R[1]})");
int[] scalarMultiple = Eliptic.scalarMultiple(k, P, a, p);
int[] lQ = Eliptic.scalarMultiple(k, Q, a, p);
Console.WriteLine($"a) scalarMultiple = 9P = {scalarMultiple.Select(el => el.ToString()).Aggregate((prev, current) => "R(" + prev + ", " + current + ")")}");
Console.WriteLine($"b) P + Q = {Eliptic.CalculateSum(P, Q, p).Select(el => el.ToString()).Aggregate((prev, current) => "R(" + prev + ", " + current + ")")}");
Console.WriteLine($"c) scalarMultiple + lQ - R = 9P + 7Q - R = {Eliptic.CalculateSum(Eliptic.CalculateSum(scalarMultiple, lQ, p), Eliptic.InversePoint(R), p).Select(el => el.ToString()).Aggregate((prev, current) => "R(" + prev + ", " + current + ")")}");
Console.WriteLine($"d) P - Q + R = {Eliptic.CalculateSum(Eliptic.CalculateSum(P, Eliptic.InversePoint(Q), p), R, p).Select(el => el.ToString()).Aggregate((prev, current) => "R(" + prev + ", " + current + ")")}");
Console.WriteLine();
Console.WriteLine("--------- 2");
int d = 41;
string text = "Белицкий".ToLower();
Console.WriteLine($"Text: {text}");
var stopwatch = Stopwatch.StartNew();
int[,] encrText = Eliptic.Encrypt(text, new int[] { 0, 1 }, a, p, d);
stopwatch.Stop();
Console.WriteLine($"Encrypted text: {string.Join(" ", encrText.Cast<int>())}");
Console.WriteLine($"Encryption time: {stopwatch.ElapsedMilliseconds} ms");
stopwatch.Restart();
Console.WriteLine($"Decrypted text: {text}");
stopwatch.Stop();
Console.WriteLine($"Decryption time: {stopwatch.ElapsedMilliseconds} ms");

Console.ReadKey();
