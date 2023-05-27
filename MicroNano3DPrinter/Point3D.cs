using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNano3DPrinter
{
    public class Point3D
    {
        public double X;
        public double Y;
        public double Z;
        public int mode;    //移动模式：0（只移动）、1（边移动边打印）

        public Point3D(double x, double y, double z, int mode)
        {
            X = x;
            Y = y;
            Z = z;
            this.mode = mode;
        }
    }
}
