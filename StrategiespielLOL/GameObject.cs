using StrategiespielLOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StrategiespielLOL
{
    abstract class GameObject
    {
        double x;
        double y;
        double vx;
        double vy;
        

        public double X
        { get { return x; }  set { this.x = value; } }

        public double Y
        { get { return y; } set { this.y = value; } }

        public double VX
        { get { return vx; } set { vx = value; } }

        public double VY
        { get { return vy; } set { vy = value; } }

        public GameObject(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public GameObject(double x, double y, double vx, double vy)
        {
            this.x = x;
            this.y = y;
            this.vx = vx;
            this.vy = vy;
        }

        public abstract void Zeichne(Canvas zeichenfläche);

        public bool Animiere(TimeSpan intervall, Canvas zeichenfläche)
        {
            x += vx * intervall.TotalSeconds;
            y += vy * intervall.TotalSeconds;

            bool überDenRandGegangen = false;

            if (x < 0.0)
            {
                überDenRandGegangen = true;
                x = zeichenfläche.ActualWidth;
            }
            else if (x > zeichenfläche.ActualWidth)
            {
                überDenRandGegangen = true;
                x = 0.0;
            }

            if (y < 0.0)
            {
                überDenRandGegangen = true;
                y = zeichenfläche.ActualHeight;
            }
            else if (y > zeichenfläche.ActualHeight)
            {
                überDenRandGegangen = true;
                y = 0.0;
            }

            return überDenRandGegangen;
        }

        /*public bool EnthältPunkt(double x, double y)
        {
            //return umriss.RenderedGeometry.FillContains(new System.Windows.Point(x - X, y - Y));
        }*/
    }
}
