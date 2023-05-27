using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNano3DPrinter
{
    class PathDrawer
    {
        WpfControlLibrary3D.MainControl mainControl;

        public void setMainControl(WpfControlLibrary3D.MainControl m)
        {
            mainControl = m;
        }

        public void initPath(List<Point3D> points, PrintChannel printChannel, FormBody formBody)
        {
            //formBody.Init3DControl();
            Point3D begin = new Point3D(points[0].X - printChannel.position.X, points[0].Y - printChannel.position.Y, points[0].Z, points[0].mode);
            for (int i = 0; i < points.Count; i++)
            {
                Point3D next = new Point3D(points[i].X, points[i].Y, points[i].Z, points[0].mode);
                if (points[i].mode == 1)
                {
                    mainControl.addLine(new System.Windows.Media.Media3D.Point3D(begin.X, begin.Y, begin.Z), new System.Windows.Media.Media3D.Point3D(next.X, next.Y, next.Z), 1, System.Windows.Media.Brushes.Gray);
                }
                begin = next;
            }
        }

        public void changeHead(double x, double y, double z)
        {
            mainControl.changeHeadLocation(x, y, z);
        }

        public void draw(Point3D begin, Point3D next)
        {
            
        }
    }
}
