using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indicium
{
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
    public class Matrix
    {
        public int N { get; set; }
        public int[,] Elements { get; set; }
        public int Trace { get; set; }

        public List<int> TNums { get; set; }
        public int K { get; set; }
        public bool IsPossible { get; set; }

        public Matrix(int n)
        {
            this.N = n;
            this.Elements = new int[n, n];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    this.Elements[i, j] = ((j - i + N) % N) + 1;
                }
            }
            this.TNums = new List<int>();
        }

        public void CalcTNums2(int k)
        {
            this.K = k;
            for (int i = 0; i < N; i++)
            {
                this.TNums.Add(1);
            }
            int sum = N;
            int maxAdd = N - 1;
            int todo = k - N;
            int index = N - 1;
            while(todo > 0)
            {
                if (todo >= maxAdd)
                {
                    this.TNums[index] += maxAdd;
                    sum += maxAdd;
                    todo -= maxAdd;
                }
                else
                {
                    this.TNums[index] += todo;
                    todo = 0;
                }
                index--;
            }
            //TNums.Reverse();
            //System.Console.WriteLine(TNums.Sum(p => p));
        }

        public void CalcTNums(int k)
        {
            this.K = k;
            for (int i = 0; i < N; i++)
            {
                this.TNums.Add(1);
            }
            int todo = k - N;
            int index = N - 1;
            while (todo > 0)
            {
                this.TNums[index]++;
                index--;
                if (index < 0)
                {
                    index = N - 1;
                }
                todo--;
            }
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

        public string Line(int i)
        {
            var e = new List<int>();
            for(int j = 0; j < N; j++)
            {
                e.Add(this.Elements[i, j]);
            }
            var s = String.Join(" ", e);
            return s;
        }

        public void SwapRow(int r1, int r2)
        {
            for (int i = 0; i < N; i++)
            {
                int tmp = this.Elements[r1, i];
                this.Elements[r1, i] = this.Elements[r2, i];
                this.Elements[r2, i] = tmp;
            }
        }

        public void SwapCol(int c1, int c2)
        {
            for (int i = 0; i < N; i++)
            {
                int tmp = this.Elements[i, c1];
                this.Elements[i, c1] = this.Elements[i, c2];
                this.Elements[i, c2] = tmp;
            }
        }

        public int GetIndexOfNumInRow(int rowId, int num)
        {
            int index = 1;
            for (int i = 0; i < N; i++)
            {
                if (num == this.Elements[rowId, i])
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        public void TryRearrange()
        {
            this.IsPossible = false;

            for (int i = 0; i < TNums.Count; i++)
            {
                this.CalculateTrace();
                if (this.Trace == this.K)
                {
                    break;
                }
                int indexToSwap = GetIndexOfNumInRow(i, TNums[i]);
                if (indexToSwap == i)
                {
                    continue;
                }
                else if (indexToSwap < i)
                {
                    return;
                }
                else
                {
                    SwapCol(i, indexToSwap);
                }                
            }
            this.IsPossible = true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 2; i <= 5; i++)
            {
                for (int j = i; j <= i * i; j++)
                {
                    System.Diagnostics.Debug.WriteLine(i + " " + j);
                }
            }
            bool isLocal = true;

            List<String> outputText = new List<String>();
            var lines = new List<String>();

            if (isLocal)
            {
                var filename = "..\\..\\input.txt";
                lines = System.IO.File.ReadAllLines(filename).ToList();
            }
            else
            {
                string line;
                while ((line = Console.ReadLine()) != null && line != "")
                {
                    lines.Add(line);
                }
            }


            #region Exercise

            int T = lines[0].AsInt();
            
            for (int t = 0; t < T; t++)
            {
                var props = lines[t+1].Split(' ').Select(p => p.AsInt()).ToArray();
                int N = props[0];
                int K = props[1];

                Matrix m = new Matrix(N);
                m.CalcTNums(K);
                m.TryRearrange();

                if (m.IsPossible)
                {
                    outputText.Add(String.Format("Case #{0}: {1}", t + 1, "POSSIBLE"));
                    for (int i = 0; i < N; i++)
                    {
                        outputText.Add(m.Line(i));
                    }
                }
                else
                {
                    outputText.Add(String.Format("Case #{0}: {1}", t + 1, "IMPOSSIBLE"));
                }
            }

            #endregion

            foreach (var s in outputText)
            {
                System.Console.WriteLine(s);
            }

            if (isLocal)
            {
                Console.ReadKey();
            }
        }
    }
}
