using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vestigium
{
    #region Helpers

    public static class Logger
    {
        public static void Log(string message)
        {
            System.Console.WriteLine(message);
            System.Diagnostics.Debug.WriteLine(message);
        }
    }

    public static class StringExtension
    {
        public static int AsInt(this String str)
        {
            return Convert.ToInt32(str);
        }

        public static double AsDouble(this String str)
        {
            //return double.Parse(str.Replace(".", ","));
            return double.Parse(str);
        }
    }

    public static class EnumerableExtension
    {
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }

        public static IEnumerable<List<T>> Partition<T>(this IList<T> source, Int32 size)
        {
            for (int i = 0; i < Math.Ceiling(source.Count / (Double)size); i++)
                yield return new List<T>(source.Skip(size * i).Take(size));
        }
    }

    public class Helpers
    {
        public static Random rand = new Random();

        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }

    #endregion

    public class Matrix
    {
        public int N { get; set; }
        public int[,] Elements { get; set; }
        public int Trace { get; set; }
        public int DoubleCols { get; set; }
        public int DoubleRows { get; set; }

        public int RowColSum { get; set; }

        public Matrix(int n)
        {
            this.N = n;
            this.Elements = new int[n, n];
            RowColSum = 0;
            for (int i = 1; i <= N; i++)
            {
                RowColSum += i;
            }
            DoubleCols = 0;
            DoubleRows = 0;
        }

        public void CalculateTrace()
        {
            int sum = 0;
            for (int i = 0; i < N; i++)
            {
                sum += this.Elements[i, i];
            }
            this.Trace = sum;
        }

        public void CalculateDoubles()
        {
            for (int i = 0; i < N; i++)
            {
                bool[] rowCheck = new bool[N];
                bool[] colCheck = new bool[N];
                for (int j = 0; j < N; j++)
                {
                    rowCheck[Elements[i, j] - 1] = true;
                    colCheck[Elements[j, i] - 1] = true;
                }
                if (rowCheck.Any(p => !p))
                {
                    DoubleRows++;
                }
                if (colCheck.Any(p => !p))
                {
                    DoubleCols++;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            //var filename = "..\\..\\input.txt";

            var lines = new List<String>();
            string line;
            while ((line = Console.ReadLine()) != null && line != "")
            {
                lines.Add(line);
            }
            List<String> outputText = new List<String>();
            int T = lines[0].AsInt();
            int currentLine = 1;

            var ms = new List<Matrix>();

            for (int t = 0; t < T; t++)
            {
                var m = new Matrix(lines[currentLine].AsInt());
                currentLine++;
                for (int j = 0; j < m.N; j++)
                {
                    int[] values = lines[currentLine].Split(' ').Select(p => p.AsInt()).ToArray();
                    for (int i = 0; i < m.N; i++)
                    {
                        m.Elements[j, i] = values[i];
                    }
                    currentLine++;
                }
                ms.Add(m);
            }
            for (int c = 0; c < ms.Count; c++)
            {
                ms[c].CalculateTrace();
                ms[c].CalculateDoubles();
                outputText.Add(String.Format("Case #{0}: {1} {2} {3}", c + 1, ms[c].Trace, ms[c].DoubleRows, ms[c].DoubleCols));
            }
            foreach (var s in outputText)
            {
                System.Console.WriteLine(s);
            }
            //Console.ReadKey();
        }
    }
}
