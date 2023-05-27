using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNano3DPrinter
{
    public class PrintChannel
    {
        public Color color;
        public Point position;
        public double Speed; // 当前通道打印机移动 1um 需要的时间  mm/s
        public double OpenTime; // 当前通道喷头每次打印开启的时间 ms PulseWidth
        public double PrintDistance; // 当前通道打印间距（分辨率）um Resolution
        public double AirPress; // 当前通道比例阀气压电流值 KPa
        public double Temperature; // 当前通道温度 ℃

        public PrintChannel()
        {

        }

        public PrintChannel(Color c, Point p, double w, double r, double a)
        {
            color = c;
            position = p;
            OpenTime = w;
            PrintDistance = r;
            AirPress = a;
        }

        public static int setChannel(int index)
        {
            return FormBody.printer.SendGCode("T" + (index + 1));
        }

        public int setResolution(double r)
        {
            PrintDistance = r;
            return FormBody.printer.SendGCode("G1R" + Utilities.numToSixString(r));
        }

        public int setPulseWidth(double t)
        {
            OpenTime = t;
            return FormBody.printer.SendGCode("G1W" + Utilities.numToSixString(t));
        }

        public int setAirPress(double p)
        {
            AirPress = p;
            return FormBody.printer.SendGCode("G1P" + Utilities.numToSixString(Utilities.pressureTocurrent(p)));
        }
    }
}
