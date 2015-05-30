using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ChuckEng.Samples
{
    class StarControl : UserControl
    {
        private Polygon _poly;

        public StarControl()
        {
            Loaded += OnLoaded;
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            Canvas canvas = new Canvas();
            canvas.SizeChanged += CanvasOnSizeChanged;
            Content = canvas;

            _poly = new Polygon();
            _poly.Stroke = Brushes.White;
            _poly.StrokeThickness = 0.1;
            _poly.Fill = Brushes.White;
            canvas.Children.Add(_poly);

            BuildStar();
        }

        public void BuildStar()
        {
            _poly.Points.Clear();

            double outerRadius = Math.Min(ActualHeight, ActualWidth) / 2;
            double innerRadius = outerRadius / 2.5;

            for (int angle = 0 ; angle < 361 ; angle += 36) {
                Debug.Write(string.Format("angle[{0}]", angle));
                if (angle % 72 == 0) {
                    // outer circle
                    Debug.Write(" - outer");
                    Point pt = new Point(outerRadius * Math.Cos(DegreeToRadian(angle - 90)), outerRadius * Math.Sin(DegreeToRadian(angle - 90)));
                    Debug.Write(string.Format(" pt[{0}]", pt));
                    _poly.Points.Add(pt);
                }
                else {
                    // inner circle
                    Debug.Write(" - inner");
                    Point pt = new Point(innerRadius * Math.Cos(DegreeToRadian(angle - 90)), innerRadius * Math.Sin(DegreeToRadian(angle - 90)));
                    Debug.Write(string.Format(" pt[{0}]", pt));
                    _poly.Points.Add(pt);
                }

                Debug.WriteLine("");
            }
        }

        static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        void CanvasOnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            Canvas.SetLeft(_poly, args.NewSize.Width / 2);
            Canvas.SetTop(_poly, args.NewSize.Height / 2);
            BuildStar();

            Debug.WriteLine("SizeChanged[{0}]", args.NewSize);
        }
    }
}
