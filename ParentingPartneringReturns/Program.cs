using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentingPartneringReturns
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


            #region Exercise

            int T = lines[0].AsInt();

            int cl = 1;
            for (int i = 0; i < T; i++)
            {
                var acts = new List<Activity>();
                int N = lines[cl].AsInt();
                cl++;
                for (int n = 0; n < N; n++)
                {
                    var times = lines[cl].Split(' ').Select(p => p.AsInt()).ToArray();
                    var act = new Activity() { Start = times[0], End = times[1], Id = n};
                    acts.Add(act);
                    cl++;
                }
                acts = acts.OrderBy(p => p.Start).ToList();
                bool isC = true;
                int occupiedUntilC = 0;
                int occupiedUntilJ = 0;
                bool impossible = false;
                foreach (var a in acts)
                {
                    if (isC)
                    {
                        if (a.Start >= occupiedUntilC)
                        {
                            a.DoneBy = 'C';
                            occupiedUntilC = a.End;
                        }
                        else if (a.Start >= occupiedUntilJ)
                        {
                            a.DoneBy = 'J';
                            occupiedUntilJ = a.End;
                            isC = false;
                        }
                        else
                        {
                            impossible = true;
                            break;
                        }
                    }
                    else
                    {
                        if (a.Start >= occupiedUntilJ)
                        {
                            a.DoneBy = 'J';
                            occupiedUntilJ = a.End;
                        }
                        else if (a.Start >= occupiedUntilC)
                        {
                            a.DoneBy = 'C';
                            occupiedUntilC = a.End;
                            isC = true;
                        }
                        else
                        {
                            impossible = true;
                            break;
                        }
                    }
                }
                if (impossible)
                {
                    outputText.Add(String.Format("Case #{0}: {1}", i + 1, "IMPOSSIBLE"));
                }
                else
                {
                    outputText.Add(String.Format("Case #{0}: {1}", i + 1, String.Concat(acts.OrderBy(p => p.Id).Select(p => p.DoneBy).ToArray())));
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

    public class Activity
    {
        public int Id { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public char DoneBy { get; set; }
    }
}
