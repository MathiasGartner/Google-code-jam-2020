using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestingDepth
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

    class Program
    {
        static void Main(string[] args)
        {
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


            #region Exeercise

            int T = lines[0].AsInt();
            for (int i = 0; i < T; i++)
            {
                string s = lines[i + 1];
                string newS = "";
                int num = 0;

                for (int c = 0; c < s.Length; c++)
                {
                    while (num < s[c].ToString().AsInt())
                    {
                        newS += "(";
                        num++;
                    }
                    while (num > s[c].ToString().AsInt())
                    {
                        newS += ")";
                        num--;
                    }
                    newS += s[c];
                }
                while (num > 0)
                {
                    newS += ")";
                    num--;
                }
                outputText.Add(String.Format("Case #{0}: {1}", i + 1, newS));
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
