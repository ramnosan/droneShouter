using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace StrategiespielLOL
{
<<<<<<< HEAD
    class Drone : GameObject//LOL
=======
    class Drone : GameObject    ////dsbvidbv
>>>>>>> bfd33935ae493e6be805cfbf1efd081fa2c11057
    {
        Ellipse el = new Ellipse();
        Line li = new Line();

        double height = 25;
        public double Height { get { return this.height; } }
        double width = 25;
        public double Width { get { return this.width; } }
        double r = 12.5;

        double memoryMoveToPos;

        bool isSelected = false;
        private double grad;

        public bool IsSelected { get { return this.isSelected; } set { this.isSelected = value; } }

        public Drone(Canvas zeichenfläche)
            : base(zeichenfläche.ActualWidth * 0.5, zeichenfläche.ActualHeight * 0.5)
        {
            li.Fill = Brushes.Black;
            li.Stroke = Brushes.Black;
            li.X1 = 12.5; li.Y1 = 0;
            li.X2 = 12.5; li.Y2 = 12.5;
            li.StrokeThickness = 2;

            el.Height = height;
            el.Width = width;
            el.Fill = Brushes.FloralWhite;
            el.StrokeThickness = 1.5;
            el.Stroke = Brushes.Black;

            Random rand = new Random();
            VX = rand.Next(-50, 50);
            VY = rand.Next(-50, 50);
        }  

        public override void Zeichne(Canvas zeichenfläche)
        {
            
            //double winkelInGrad = Math.Atan2(VY, VX) * 180.0 / Math.PI + 90.0;
            //li.RenderTransform = new RotateTransform(winkelInGrad);
            
            zeichenfläche.Children.Add(el);
            Canvas.SetLeft(el, X);
            Canvas.SetTop(el, Y);

            zeichenfläche.Children.Add(li);
            Canvas.SetLeft(li, X);
            Canvas.SetTop(li, Y);
        }

        public void changeDirection(double bogenmaß)
        {
            double lol = Math.Sin(bogenmaß); double lel = Math.Cos(bogenmaß);
            li.Y1 = r - Math.Sin(bogenmaß) * (r);
            li.X1 = r + Math.Cos(bogenmaß) * (r);
            //li.RenderTransform = new RotateTransform(bogenmaß, X, Y);
        }

        public void Beschleunige(bool beschleunige)
        {
            double faktor = beschleunige ? 1.1 : 0.9;
            VX *= faktor;
            VY *= faktor;
        }

        public void moveToPos(double x, double y)
        {

        }

        
    }
    //____________________________________________________________________________________________&&&&&&
    class LaserbeamShot : GameObject
    {
        Line li = new Line();
        public LaserbeamShot(Drone drone)
            : base(drone.X + drone.Width/2, drone.Y + drone.Height/2)
        {
            li.X1 = 12.5; li.Y1 = -2;
            li.X2 = 12.5; li.Y2 = 12.5;
            li.StrokeThickness = 2;
            li.Fill = Brushes.Red;
            li.Stroke = Brushes.Red;
        }

        public override void Zeichne(Canvas zeichenfläche)
        {
            zeichenfläche.Children.Add(li);
            Canvas.SetLeft(li, X);
            Canvas.SetTop(li, Y);
        }
    }
}
