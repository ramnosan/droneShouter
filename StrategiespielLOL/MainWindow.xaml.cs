using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StrategiespielLOL
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()//amk
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(1);//60 FPS = 
            timer.Tick += Animiere;

            timerFPS.Interval = TimeSpan.FromSeconds(1);
            timerFPS.Tick += resetFramesInASecond;
            timerFPS.Start();
        }

        public void resetFramesInASecond(object sender, EventArgs e)
        {
            lblFramesPerSecond.Content = framesInASecond;
            framesInASecond = 0;
        }

        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timerFPS = new DispatcherTimer(); int framesInASecond = 0;

        List<GameObject> gameobjects = new List<GameObject>();
        List<Drone> drones = new List<Drone>();

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Drone d = new Drone(zeichenfläche);
            gameobjects.Add(d);
            drones.Add(d);
            d.Zeichne(zeichenfläche);
            timer.Start();
        }

        private void spawnDrone(object sender, MouseButtonEventArgs e)
        {
            if (radioBtnSpawnDrone.IsChecked == true)
            {
                Drone d = new Drone(zeichenfläche);
                gameobjects.Add(d);
                drones.Add(d);
                d.X = e.GetPosition(zeichenfläche).X - d.Height / 2;
                d.Y = e.GetPosition(zeichenfläche).Y - d.Width / 2;
                d.Zeichne(zeichenfläche);
            }
        }

        void Animiere(object sender, EventArgs e)
        {
            
            zeichenfläche.Children.Clear();
            foreach (var go in gameobjects)
            {
                //Thread thread = new Thread(() => go.Animiere(timer.Interval, zeichenfläche));
                //thread.Start();
                //thread.Abort();
                go.Animiere(timer.Interval, zeichenfläche);
                go.Zeichne(zeichenfläche);
                //Thread threadZeichne = new Thread(() => go.Zeichne(zeichenfläche));
                //threadZeichne.Start();
                //threadZeichne.Abort();
            }

            framesInASecond++;
        }
        Thread thread;
        void startThreadAnim(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(
   new Action(() => {
       
            if (thread == null)
            {
                thread = new Thread(() => Animiere(sender, e));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else if (thread.IsAlive == false)
            {
                
            }
   })
);
        }

       

        public double xMouseUp;
        public double yMouseUp;
        private void zeichenfläche_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SpawnDrone(sender, e);
            xMouseUp = e.GetPosition(zeichenfläche).X;
            yMouseUp = e.GetPosition(zeichenfläche).Y;
            selectObjects();
        }

        public double xMouseDown;
        public double yMouseDown;
        private void zeichenfläche_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            xMouseDown = e.GetPosition(zeichenfläche).X;
            yMouseDown = e.GetPosition(zeichenfläche).Y;
        }

        private void selectObjects()
        {
            foreach (Drone d in drones)
            {
                if (xMouseDown <= d.X && xMouseUp >= d.X)
                {
                    if (yMouseDown <= d.Y && yMouseUp >= d.Y)
                    {
                        d.IsSelected = true;
                    }
                }
                else
                {
                    d.IsSelected = false;
                }
            }
        }

        private void zeichenfläche_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in drones)
            {
                if (item.IsSelected)
                {
                    item.changeDirection(getWinkelRad(e.GetPosition(zeichenfläche).X - item.X,
                        e.GetPosition(zeichenfläche).Y - item.Y));
                    
                }
            }
        }
        private double getWinkelRad(double deltaX, double deltaY)//delta = Differenz bzw. Entfernung
        {
            //double a = Math.Atan(deltaY/deltaX);
            double a = Math.Atan2(deltaY, deltaX) * -1;
            return a;
        }
        private double degreeToBogenmaß(double grad)//?
        {
            //return grad;
            return 2*Math.PI * (grad/360);
        }
    }
}
