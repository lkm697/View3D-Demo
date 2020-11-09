using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace View3D_Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //set & add camera
            var myCamera = new PerspectiveCamera(new Point3D(6, 5, 4), new Vector3D(-6, -5, -4), new Vector3D(0, 1, 0), 60.00);
            myView.Camera = myCamera;

            //create light
            var lightModel = new ModelVisual3D();
            var lights = new DirectionalLight(Color.FromRgb(255, 255, 158), new Vector3D(-15, -5, -10));
            lightModel.Content = lights;


            //shapes
            var shapeModel = new ModelVisual3D();
            var geoModel = new GeometryModel3D();

            var mesh3d = new MeshGeometry3D();

            //https://www.youtube.com/watch?v=09Fv5cNdbnA

            //mesh3d.Positions = Point3DCollection.Parse("0 0 0  1 0 0  0 1 0  1 1 0  0 0 1  1 0 1  0 1 1  0 1 1");
            mesh3d.Positions = Point3DCollection.Parse("0 0 0  1 0 0  0 1 0  0 0 1  -1 0 0  0 -1 0  0 0 -1");
            //mesh3d.Normals = Vector3DCollection.Parse("0 0 1  0 0 1  0 0 1  0 0 1");
            //mesh3d.TextureCoordinates = PointCollection.Parse("0 1  1 1  0 0  1 0");
            //mesh3d.TriangleIndices = Int32Collection.Parse("2 3 1  3 1 0  7 1 3  7 5 1  6 5 7  6 4 5  6 2 0  2 0 4  2 7 3  2 6 7  0 1 5  0 5 4");
            mesh3d.TriangleIndices = Int32Collection.Parse("0 1 2  0 1 3  0 2 3  1 2 3  0 4 5  0 4 6  0 5 6  4 5 6");
            geoModel.Geometry = mesh3d;

            var diffMat = new DiffuseMaterial();
            diffMat.Brush = new SolidColorBrush() { Color = Color.FromRgb(0, 255, 255), Opacity = 0.3 };
            geoModel.Material = diffMat;

            shapeModel.Content = geoModel;




            var top3dModel = new ModelVisual3D();

            top3dModel.Children.Add(lightModel);
            top3dModel.Children.Add(shapeModel);

            myView.Children.Add(top3dModel);


            double angletoRotate = 10;
            var rTate = new RotateTransform3D();
            var axisRotate = new AxisAngleRotation3D(new Vector3D(1, 1, 1), angletoRotate);

            bool bounce = true;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += (s, e) =>
            {
                if (bounce)
                {
                    angletoRotate++;
                    axisRotate = new AxisAngleRotation3D(new Vector3D(1, 2, 1), angletoRotate);
                    rTate.Rotation = axisRotate;
                    top3dModel.Transform = rTate;
                    if (angletoRotate > 350)
                    {
                        //bounce = false;
                    }
                }
                else
                {
                    angletoRotate--;
                    axisRotate = new AxisAngleRotation3D(new Vector3D(2, 1, 2), angletoRotate);
                    rTate.Rotation = axisRotate;
                    top3dModel.Transform = rTate;
                    if (angletoRotate < 5)
                    {
                        bounce = true;
                    }
                }
            };
            timer.Interval = TimeSpan.FromMilliseconds(.5);
            timer.Start();


        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
