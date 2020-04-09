using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESAbATAd
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
        public static bool GetPos(int i)
        {
            System.Console.WriteLine(i);
            var c = System.Console.ReadLine().AsInt();
            return c == 1;
        }

        public static bool Submit(bool[] values)
        {
            var s = String.Concat(values.Select(p => p ? "1" : "0").ToArray());
            System.Console.WriteLine(s);
            var c = System.Console.ReadLine();
            return c == "Y";
        }        

        static void Main(string[] args)
        {
            bool isLocal = true;

            List<String> outputText = new List<String>();
            

            #region Exercise

            string line;
            line = Console.ReadLine();            
            var props = line.Split(' ').Select(p => p.AsInt()).ToArray();
            int T = props[0];
            int B = props[1];
            int B2 = B / 2;


            for (int t = 0; t < T; t++)
            {
                for (int j = 0; j < B2; j++)
                {

                }
            }


            #endregion
                        
            if (isLocal)
            {
                Console.ReadKey();
            }
        }
    }
}
