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

        static List<PackageInfo> CarrierList = new List<PackageInfo>();
        static Dictionary<string, PackageInfo> CarrierArchive = new Dictionary<string, PackageInfo>();

        static string inputPathMonth = @"E:\00_works\Carrier-checking\Carrier-pkg-month.csv";
        static string inputPathOther = @"E:\00_works\Carrier-checking\Carrier-jack-result.csv";
        static string finalOutputPath = @"E:\00_works\Carrier-checking\result\Carrier-result.csv";
        static string repeatLogpath = @"E:\00_works\Carrier-checking\repeat\Carrier-repeat-pkg.csv";

        static void Main(string[] args)
        {
            ReadCarrierMonthCSV(inputPathMonth);
            ReadCarrierOtherCSV(inputPathOther);
            WriteCarrierInfo(finalOutputPath);
            //WriteFile(outputPath);

            //FindRepeat();
            //CheckCounts();
            Console.ReadKey();
        }

        static void WriteCarrierInfo(string outPath)
        {
            using (StreamWriter writer = File.AppendText(outPath)) 
            {
                writer.WriteLine(@"Tracking,Month,Package Weight,Dimension Weight,Chargeable Weight,Length,Width,Height,Zone,Length & Grith,NG Total Charge,Freight Charge,Fuel Charge");
                foreach (KeyValuePair<string, PackageInfo> kvp in CarrierArchive)
                {
                    Console.WriteLine(kvp.Key);
                    PackageInfo p = new PackageInfo();
                    CarrierArchive.TryGetValue(kvp.Key, out p);

                    Console.WriteLine(p.ToCSVString());
                    writer.WriteLine(p.ToCSVString());
                }
            }
        }

        static void ReadCarrierOtherCSV(string filepath)
        {
            int count = 0;
            char[] sprtr = { ',' };
            using (StreamReader reader = new StreamReader(filepath))
            {
                string lineIn = reader.ReadLine();
                while (lineIn != null)
                {
                    count += 1;
                    PackageInfo p = new PackageInfo();
                    string[] cols = lineIn.Split(sprtr);

                    foreach (string s in cols)
                    {
                        string trackingNumStr = cols[0].Trim();
                        if (CarrierArchive.TryGetValue(trackingNumStr, out p))
                        {
                            p.TrackingNumber = trackingNumStr;
                            p.PackageWeight = double.Parse(cols[1].Trim());
                            p.DimensionWeight = double.Parse(cols[2].Trim());
                            p.ChargeableWeight = double.Parse(cols[3].Trim());
                            p.Length = double.Parse(cols[4].Trim());
                            p.Width = double.Parse(cols[5].Trim());
                            p.Height = double.Parse(cols[6].Trim());
                            p.Zone = int.Parse(cols[7].Trim());
                            p.LengthAndGrith = double.Parse(cols[8].Trim());
                            p.NGTotalCharge = double.Parse(cols[9].Trim());
                            p.FreightCharge = double.Parse(cols[10].Trim());
                            p.FuelCharge = double.Parse(cols[11].Trim());
                            CarrierArchive[trackingNumStr] = p;
                        }
                        else
                        {
                            Console.WriteLine("Unmatched Tracking no: " + p.TrackingNumber);
                        }
                    }
                    lineIn = reader.ReadLine();
                }
            }
            Console.WriteLine("Total Other-Info counts: " + count);
        }

        static void ReadCarrierMonthCSV(string filepath)
        {
            int count = 0;
            char[] sprtr = { ',' };
            using (StreamReader reader = new StreamReader(filepath))
            {
                string lineIn = reader.ReadLine();
                while (lineIn != null)
                {
                    PackageInfo p = new PackageInfo();
                    count += 1;
                    string[] cols = lineIn.Split(sprtr);
                    foreach(string s in cols)
                    {
                        p.TrackingNumber = cols[0].Trim();
                        p.Month = cols[1].Trim();
                    }
                    try
                    {
                        CarrierArchive.Add(p.TrackingNumber, p);
                    }
                    catch(ArgumentException e)
                    {
                        //WriteLineTo(p.TrackingNumber, repeatLogpath);
                    }
                    lineIn = reader.ReadLine();
                }
            }
            Console.WriteLine("Total counts: " + count);
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

        static void WriteLineTo(string line, string outPath)
        {
            using (StreamWriter writer = File.AppendText(outPath)) //new StreamWriter(filepath))
            {
                writer.WriteLine(line);
            }
        }

        static void WriteQuotedLine(string filepath)
        {
            if (filepath.Length == 0)
            {
                filepath = @"E:\text\tmpout.txt";
            }

            string[] lines = inLines.ToArray();
            using(StreamWriter writer = File.AppendText(filepath)) //new StreamWriter(filepath))
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
