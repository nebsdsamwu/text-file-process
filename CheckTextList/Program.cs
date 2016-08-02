using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CheckTextList
{
    class Program
    {
        static List<string> inLines = new List<string>();
        static Dictionary<string, int> lineCounts = new Dictionary<string, int>(); 

        static void Main(string[] args)
        {
            string path = @"E:\00_works\00_RMA\Issues\JetRMA_ReturnAmount_Too_Much\jetrmas.txt";
            string inputPath = @"E:\text\trackings.txt";
            string outputPath = @"E:\text\trackingsToSql.txt";

            ReadFile(inputPath);
            WriteFile(outputPath);
            //FindRepeat();
            //CheckCounts();
            Console.ReadKey();
        }

        static void CheckCounts()
        {
            int chkCount = 0;
            foreach (var item in lineCounts)
            {
                if (item.Value > 1)
                {
                    Console.WriteLine(item.Key + " : " + item.Value);
                    chkCount += 1;
                }
            }
            Console.WriteLine("More than once: " + chkCount);
        }

        static void FindRepeat()
        {
            foreach(string s in inLines)
            {
                int val;
                if (lineCounts.TryGetValue(s, out val))
                {
                    lineCounts[s] = val + 1;
                }
                else
                {
                    lineCounts.Add(s, 1);
                }
            }
        }

        static void ReadFile(string filepath)
        {
            int count = 0;
            using (StreamReader reader = new StreamReader(filepath))
            {
                string lineIn = reader.ReadLine();
                while (lineIn != null)
                {
                    count += 1;
                    //Console.WriteLine(count + ":" + lineIn);
                    inLines.Add(lineIn);
                    lineIn = reader.ReadLine();
                }
            }
            Console.WriteLine("Total counts: " + count);
        }

        static void WriteFile(string filepath)
        {
            if (filepath.Length == 0)
            {
                filepath = @"E:\text\tmpout.txt";
            }

            string[] lines = inLines.ToArray();
            using(StreamWriter writer = new StreamWriter(filepath))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string qline = "";
                    if (i < lines.Length-1)
                    {
                       qline = "'" + lines[i] + "',";
                    }
                    else
                    {
                        qline = "'" + lines[i] + "'";
                    }
                    writer.WriteLine(qline);
                }
            }
        }
    }
}
