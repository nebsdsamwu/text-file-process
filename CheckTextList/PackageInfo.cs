using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTextList
{
    public class PackageInfo
    {
        public string TrackingNumber { get; set; }
        public string Month { get; set; }
        public double PackageWeight { get; set; }
        public double DimensionWeight { get; set; }
        public double ChargeableWeight { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int Zone { get; set; }
        public double LengthAndGrith { get; set; }
        public double NGTotalCharge { get; set; }
        public double FreightCharge { get; set; }
        public double FuelCharge { get; set; }

        public PackageInfo()
        {}

        public PackageInfo(string trackNo)
        {
            this.TrackingNumber = trackNo;
        }

        public string ToCSVString()
        {
            return TrackingNumber + ","
                   + Month + ","
                   + PackageWeight + ","
                   + DimensionWeight + ","
                   + ChargeableWeight + ","
                   + Length + ","
                   + Width + ","
                   + Height + ","
                   + Zone + ","
                   + LengthAndGrith + ","
                   + NGTotalCharge + ","
                   + FreightCharge + ","
                   + FuelCharge;
        }
    }
}
