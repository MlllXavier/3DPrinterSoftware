using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNano3DPrinter
{
    public class Printer
    {
        public double X = 0;//X轴
        public double Y = 0;//Y轴
        public double Z = 0;//Z轴
        public double maxX = 0;//X的最大范围
        public double maxY = 0;//Y的最大范围
        public double maxZ = 0;//Z的最大范围
        public int printerState = 0;//打印机状态 0：空闲 1.：打印中 2.暂停
        public PrintLayer PrintLayer = new PrintLayer();//当前打印层
        public PrintChannel printChannel = new PrintChannel();//当前打印通道

        public Printer(double mx, double my, double mz)
        {
            maxX = mx;
            maxY = my;
            maxZ = mz;
        }

        public int SendGCode(string str)//0：正确，-1：异常，1：串口未打开，-2：超出范围
        {
            return FormBody.serial.sendGCode(str);
        }

        public int moveToAbsolute(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            return SendGCode(Utilities.pointToGCode("G0", X, Y, Z));
        }

        public int moveToRelative(double x, double y, double z)
        {
            if (x > maxX || y > maxY || z > maxZ || x < 0 || y < 0 || z < 0)
            {
                return -2;
            }
            X = x;
            Y = y;
            Z = z;
            return SendGCode(Utilities.pointToGCode("G0", X - printChannel.position.X, Y - printChannel.position.Y, Z));
        }

        public int printToAbsolute(double x, double y, double z)
        {
            if (x > maxX || y > maxY || z > maxZ || x < 0 || y < 0 || z < 0)
            {
                return -2;
            }
            X = x;
            Y = y;
            Z = z;
            return SendGCode(Utilities.pointToGCode("G1", X, Y, Z));
        }

        public int printToRelative(double x, double y, double z)
        {
            if (x > maxX || y > maxY || z > maxZ || x < 0 || y < 0 || z < 0)
            {
                return -2;
            }
            X = x;
            Y = y;
            Z = z;
            return SendGCode(Utilities.pointToGCode("G1", X - printChannel.position.X, Y - printChannel.position.Y, Z));
        }

        public int toPoint(Point3D point)
        {
            if (point.X > maxX || point.Y > maxY || point.Z > maxZ || point.X < 0 || point.Y < 0 || point.Z < 0)
            {
                return -2;
            }
            X = point.X;
            Y = point.Y;
            Z = point.Z;
            if (point.mode == 0)
            {
                return moveToRelative(X, Y, Z);
            }
            else
            {
                return printToRelative(X, Y, Z);
            }
        }

        public int Pause()//暂停
        {
            printerState = 3;
            return SendGCode("G4P");
        }

        public int Continue()//继续
        {
            printerState = 2;
            return SendGCode("G4C");
        }

        public int x_moveUp(double step)
        {
            X += step;
            return moveToAbsolute(X, Y, Z);
        }

        public int x_moveDown(double step)
        {
            X -= step;
            return moveToAbsolute(X, Y, Z);
        }

        public int y_moveUp(double step)
        {
            Y += step;
            return moveToAbsolute(X, Y, Z);
        }

        public int y_moveDown(double step)
        {
            Y -= step;
            return moveToAbsolute(X, Y, Z);
        }

        public int z_moveUp(double step)
        {
            Z += step;
            return moveToAbsolute(X, Y, Z);
        }

        public int z_moveDown(double step)
        {
            Z -= step;
            return moveToAbsolute(X, Y, Z);
        }

        public int moveToSolidifyZero()
        {
            //此x,y为固化灯相对喷头1的位置
            double x = 20;
            double y = 40;
            return moveToAbsolute(PrintLayer.x_start + PrintLayer.x_size / 2 - x, PrintLayer.y_start + PrintLayer.y_size / 2 - y, 0);
        }
    }
}
