using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNano3DPrinter
{
    public class PrintLayer
    {
        public int mode = 0;
        public int channel = 1;
        public int state = 0;
        public double interval = 0;
        public double x_size = 0;
        public double y_size = 0;
        public double x_start = 0;
        public double y_start = 0;
        public double z_start = 0;
        public int solidfyTime = 0;
        public List<Point3D> points = new List<Point3D>();

        public PrintLayer()
        {

        }

        public PrintLayer(int mode, int channel, double interval, double x_size, double y_size, double x_start, double y_start, double z_start, int solidfyTime)
        {
            this.mode = mode;
            this.channel = channel;
            this.interval = interval;
            this.x_size = x_size;
            this.y_size = y_size;
            this.x_start = x_start;
            this.y_start = y_start;
            this.z_start = z_start;
            this.solidfyTime = solidfyTime;
            switch (this.mode)
            {
                case 0:
                    points = lineation();
                    break;
                case 1:
                    points = lineation2();
                    break;
                case 2:
                    points = lineation3();
                    break;
                case 3:
                    break;
            }
        }

        public List<Point3D> lineation()//划线算法，根据次数、宽度、高度、起点求；返回值为一个点集
        {
            List<Point3D> arrPath = new List<Point3D>();
            arrPath.Add(new Point3D(x_start, y_start, z_start, 0));
            double x = x_start;
            double y = y_start;
            int times = (int)(y_size / interval);
            for (int i = 0; i < times + 1; i++)
            {
                if (i % 2 == 0)
                {
                    x += x_size;
                    arrPath.Add(new Point3D(x, y, z_start, 1));
                }
                else
                {
                    x -= x_size;
                    arrPath.Add(new Point3D(x, y, z_start, 1));
                }
                if(i == times)
                {
                    break;
                }
                y += interval;
                arrPath.Add(new Point3D(x, y, z_start, 1));
            }
            return arrPath;
        }

        public List<Point3D> lineation2()//第二种划线算法，矩形宽度、高度、划线次数
        {
            List<Point3D> arrPath = new List<Point3D>();
            arrPath.Add(new Point3D(x_start, y_start, z_start, 0));
            double x = x_start;
            double y = y_start;
            x += x_size;
            arrPath.Add(new Point3D(x, y, z_start, 1));
            bool tag = true;
            int num = 0;
            while ((y_size - interval * num) > 0 && (x_size - interval * num) > 0)
            {
                if (tag)
                {
                    y += y_size - interval * num;
                    arrPath.Add(new Point3D(x, y, z_start, 1));
                    x -= x_size - interval * num;
                    arrPath.Add(new Point3D(x, y, z_start, 1));
                }
                else
                {
                    y -= y_size - interval * num;
                    arrPath.Add(new Point3D(x, y, z_start, 1));
                    x += x_size - interval * num;
                    arrPath.Add(new Point3D(x, y, z_start, 1));
                }
                tag = !tag;
                num++;
            }
            return arrPath;
        }

        public List<Point3D> lineation3()//第三种划线算法，矩形宽度、高度、划线次数
        {
            List<Point3D> arrPath = new List<Point3D>();
            arrPath.Add(new Point3D(x_start, y_start, z_start, 0));
            double x = x_start;
            double y = y_start;
            int times_x = (int)(x_size / interval);
            for (int i = 0; i < times_x + 1; i++)
            {
                if(i % 2 == 0)
                {
                    y = y + y_size;
                }
                else
                {
                    y = y - y_size;
                }
                arrPath.Add(new Point3D(x, y, z_start, 1));
                if(i == times_x)
                {
                    break;
                }
                x = x + interval;
                arrPath.Add(new Point3D(x, y, z_start, 0));
            }
            int times_y = (int)(y_size / interval);
            for(int i = 0; i < times_y + 1; i++)
            {
                if (i % 2 == 0)
                {
                    x = x - x_size;
                }
                else
                {
                    x = x + x_size;
                }
                arrPath.Add(new Point3D(x, y, z_start, 1));
                if (i == times_y)
                {
                    break;
                }
                if (times_x % 2 != 0)
                {
                    y = y + interval;
                }
                else
                {
                    y = y - interval;
                }
                arrPath.Add(new Point3D(x, y, z_start, 0));
            }
            return arrPath;
        }
    }
}
