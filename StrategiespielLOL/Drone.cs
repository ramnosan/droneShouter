using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace StrategiespielLOL
{
    class Drone : GameObject
    {
        Ellipse el = new Ellipse();
        Line li = new Line();
        public Line LI
        {
            get{ return this.li;} set{;}
        }

        double height = 25;
        public double Height { get { return this.height; } }
        double width = 25;
        public double Width { get { return this.width; } }
        public double r = 12.5;
        public double lookingDirection;

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
            lookingDirection = bogenmaß;
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

        public void shootLaser()
        {

        }

        public bool EnthältPunkt(double x, double y)
        {
            return el.RenderedGeometry.FillContains(new System.Windows.Point(x - X, y - Y));
        }


        
    }
    //______________________________________________________________
    class Photonentorpedo : GameObject
    {
        public Photonentorpedo(Drone drone)
            : base(drone.X, drone.Y)
        {
            VX = 500 * (drone.LI.X1 - drone.LI.X2);
            VY = 500 * (drone.LI.Y1 - drone.LI.Y2);
        }

        public override void Zeichne(Canvas zeichenfläche)
        {
            Ellipse elli = new Ellipse();
            elli.Width = 5.0;
            elli.Height = 5.0;
            elli.Fill = Brushes.Red;
            zeichenfläche.Children.Add(elli);
            Canvas.SetLeft(elli, X - 0.5 * elli.Width);
            Canvas.SetTop(elli, Y - 0.5 * elli.Height);
        }

        /// <summary>
        /// ändert den Startpunkt, sodass die kugel nicht in der Drone spawnen
        /// </summary>
        /// <param name="bogenmaß"></param>
        /// <param name="d"></param>
        public void changeDirection(double bogenmaß, Drone d)
        {
            X += d.LI.X1;
            Y += d.LI.Y1;
        }
        
    }

    //____________________________________________________________________________________________&&&&&&
    class LaserbeamShot : GameObject
    {
        Line li = new Line();
        public LaserbeamShot(Drone drone)
            : base(drone.X, drone.Y)
        {
            li.X1 = 12.5; li.Y1 = -2;
            li.X2 = 12.5; li.Y2 = 12.5;
            li.StrokeThickness = 2;
            li.Fill = Brushes.Red;
            li.Stroke = Brushes.Red;


            //LET the laser fly in the direction the drones are looking
            changeDirection(drone.lookingDirection, drone);
            
            VX =  100 * (li.X1 - li.X2);
            VY =  100 * (li.Y1 - li.Y2);
        }

        public override void Zeichne(Canvas zeichenfläche)
        {
            zeichenfläche.Children.Add(li);
            Canvas.SetLeft(li, X);
            Canvas.SetTop(li, Y);
        }

        private void changeDirection(double bogenmaß, Drone d)
        {
            double lol = Math.Sin(bogenmaß); double lel = Math.Cos(bogenmaß);
            li.Y1 = d.r - Math.Sin(bogenmaß) * (d.r);
            li.X1 = d.r + Math.Cos(bogenmaß) * (d.r);
            //li.RenderTransform = new RotateTransform(bogenmaß, X, Y);
            //X += li.X1 - li.X2;//Abstand zu drone vergrößern
            //Y += li.Y1 - li.Y2;
        }

        public bool EnthältPunkt(double x, double y)
        {
            return li.RenderedGeometry.FillContains(new System.Windows.Point(x - X, y - Y));
        }
    }
}
