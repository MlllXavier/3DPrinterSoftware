using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlLibrary3D
{
    /// <summary>
    /// MainControl.xaml 的交互逻辑
    /// </summary>
    public partial class MainControl : UserControl
    {
        public MainControl()
        {
            InitializeComponent();
            RenderOptions.SetEdgeMode(main3D, EdgeMode.Aliased);
            addLine(new Point3D(0, 0, 0), new Point3D(200, 0, 0), 1, Brushes.Red);
            addLine(new Point3D(0, 0, 0), new Point3D(0, 200, 0), 1, Brushes.Red);
            addLine(new Point3D(0, 0, 0), new Point3D(0, 0, 200), 1, Brushes.Red);
            viewX.ValueChanged += ViewX_ValueChanged;
            viewY.ValueChanged += ViewY_ValueChanged;
            viewZ.ValueChanged += ViewZ_ValueChanged;
        }

        private void ViewZ_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Point3D point = cameraPer.Position;
            point.Z = e.NewValue;
            cameraPer.Position = point;
            Vector3D vector3D = new Vector3D(-point.X, -point.Y, -point.Z);
            cameraPer.LookDirection = vector3D;
        }

        private void ViewY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Point3D point = cameraPer.Position;
            point.Y = e.NewValue;
            cameraPer.Position = point;
            Vector3D vector3D = new Vector3D(-point.X, -point.Y, -point.Z);
            cameraPer.LookDirection = vector3D;
        }

        private void ViewX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Point3D point = cameraPer.Position;
            point.X = e.NewValue;
            cameraPer.Position = point;
            Vector3D vector3D = new Vector3D(-point.X, -point.Y, -point.Z);
            cameraPer.LookDirection = vector3D;
        }

        public void changeHeadLocation(double x, double y, double z)
        {
            TranslateTransform3D translateTransform3D = new TranslateTransform3D();
            translateTransform3D.OffsetX = x;
            translateTransform3D.OffsetY = y;
            translateTransform3D.OffsetZ = z;
            head.Transform = translateTransform3D;
        }

        public void addLine(Point3D start, Point3D end, double width, Brush brush)
        {
            width = width / 2.0;
            GeometryModel3D geometryModel3D = new GeometryModel3D();
            MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
            Point3DCollection point3Ds = new Point3DCollection();
            point3Ds.Add(new Point3D(start.X - width, start.Y, start.Z));
            point3Ds.Add(new Point3D(start.X + width, start.Y, start.Z));
            point3Ds.Add(new Point3D(end.X - width, end.Y, end.Z));
            point3Ds.Add(new Point3D(end.X - width, end.Y, end.Z));
            point3Ds.Add(new Point3D(end.X + width, end.Y, end.Z));
            point3Ds.Add(new Point3D(start.X + width, start.Y, start.Z));

            point3Ds.Add(new Point3D(start.X, start.Y - width, start.Z));
            point3Ds.Add(new Point3D(start.X, start.Y + width, start.Z));
            point3Ds.Add(new Point3D(end.X, end.Y - width, end.Z));
            point3Ds.Add(new Point3D(end.X, end.Y - width, end.Z));
            point3Ds.Add(new Point3D(end.X, end.Y + width, end.Z));
            point3Ds.Add(new Point3D(start.X, start.Y + width, start.Z));

            point3Ds.Add(new Point3D(start.X, start.Y, start.Z - width));
            point3Ds.Add(new Point3D(start.X, start.Y, start.Z + width));
            point3Ds.Add(new Point3D(end.X, end.Y, end.Z - width));
            point3Ds.Add(new Point3D(end.X, end.Y, end.Z - width));
            point3Ds.Add(new Point3D(end.X, end.Y, end.Z + width));
            point3Ds.Add(new Point3D(start.X, start.Y, start.Z + width));
            meshGeometry3D.Positions = point3Ds;
            geometryModel3D.Geometry = meshGeometry3D;
            geometryModel3D.Material = new DiffuseMaterial(brush);
            geometryModel3D.BackMaterial = new DiffuseMaterial(brush);
            modelGroup.Children.Add(geometryModel3D);
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            viewX.Value = 300;
            viewY.Value = 200;
            viewZ.Value = 100;
            sliX.Value = 180;
            sliY.Value = 0;
            sliZ.Value = 90;
            
        }
    }
}
