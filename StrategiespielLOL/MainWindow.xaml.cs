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
using NeuralNet.NeuralNet;

namespace StrategiespielLOL//lol
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
        /// <summary>
        /// /IMPORTANT LISTS AND OBJECTS
        /// </summary>
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timerFPS = new DispatcherTimer(); int framesInASecond = 0;

        List<GameObject> gameobjects = new List<GameObject>();
        List<Drone> drones = new List<Drone>();
        List<Photonentorpedo> torpedoList = new List<Photonentorpedo>();
        //_________________________________________________________________________
        Random random = new Random();

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
                d.changeDirection(0);
            }
        }

        /// <summary>
        /// standard game logic//description TODO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Animiere(object sender, EventArgs e)
        {
            zeichenfläche.Children.Clear();

            //DELETE THEN COLLISSION
            List<Photonentorpedo> torpedoToDelete = new List<Photonentorpedo>();
            List<Drone> droneToDelete = new List<Drone>();
            foreach (var d in drones)
            {
                foreach (var torpedo in torpedoList)
                {
                    if (d.EnthältPunkt(torpedo.X, torpedo.Y))
                    {
                        droneToDelete.Add(d);
                        torpedoToDelete.Add(torpedo);
                    }
                }
            }
            gameobjects = gameobjects.Except(torpedoToDelete).ToList();
            torpedoList = torpedoList.Except(torpedoToDelete).ToList();
            gameobjects = gameobjects.Except(droneToDelete).ToList();
            drones = drones.Except(droneToDelete).ToList();
            foreach (var laser in torpedoToDelete)
            {
                torpedoList.Remove(laser);
                gameobjects.Remove(laser);
            }
            foreach (var drone in droneToDelete)
            {
                drones.Remove(drone);
                gameobjects.Remove(drone);
            }
            //END DELETE COLLISSION

            //ANIMIERE
            foreach (var go in gameobjects)
            {
                go.Animiere(timer.Interval, zeichenfläche);
                go.Zeichne(zeichenfläche);
            }

            framesInASecond++;
        }

        private double xMouseUp;
        private double yMouseUp;
        private void zeichenfläche_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            spawnDrone(sender, e);
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
            foreach (var drone in drones)
            {
                if (drone.IsSelected)
                {
                    drone.changeDirection(getWinkelRad(e.GetPosition(zeichenfläche).X - drone.X,
                        e.GetPosition(zeichenfläche).Y - drone.Y));
                    //
                    Photonentorpedo torpedo = new Photonentorpedo(drone); torpedo.changeDirection(drone.lookingDirection, drone);
                    torpedoList.Add(torpedo);
                    gameobjects.Add(torpedo);
                    //
                }
            }
        }
        private double getWinkelRad(double deltaX, double deltaY)//delta = Differenz bzw. Entfernung
        {
            double a = Math.Atan2(deltaY, deltaX) * -1;
            return a;
        }
        private double degreeToBogenmaß(double grad)//?
        {
            return 2*Math.PI * (grad/360);
        }

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            
        }

        /// <summary>
        /// function: two individuums are kicked in a pool too fight, the fight is simulated, the winner gets into the "breeding pool",
        /// then the whole population has fought, two individuums are picked to crossover until the max population is reached again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartGeneticAlgorythm_Click(object sender, RoutedEventArgs e)
        {
            List<Drone> popOfDrones = new List<Drone>();
            List<Drone> winningPool = new List<Drone>();
            //initialize GA with populationsize and mutationrate
            int population = 20;
            float mutationrate = 0.01f;
            GeneticAlgorythm ga = new GeneticAlgorythm(population, mutationrate);
            
            //Create new population
            for (int i = 0; i < population; i++)
            {
                popOfDrones.Add(new Drone(zeichenfläche));
                popOfDrones[i].NeuralNetwork = new NeuralNetwork(0.25, new int[] {4, 8, 4});
            }
            ga.drones = popOfDrones;//TODO: ??? i dont know yet
            
            //Let two drones fight agaist each other, the survivor gets inserted into the winningPool


        }

        private List<double> inputsForNN = new List<double>();
        private List<double> getInpuutsForNN(Drone d1, Drone d2, List<Photonentorpedo> enemyShots)
        {
            inputsForNN.Clear();
            //1.input: distance from other drone
            //double distanceFromOtherDrone = Math.Sqrt(Math.Pow((d2.X - d1.X),2) + Math.Pow((d2.Y - d1.Y), 2));
            //2.input: distance from threat
            //double distanceFromShot = 0;
            //double h = 0;
            //Calculates the shortest distance from a shot
            /*foreach (var shot in enemyShots)//TODO 
            {
                h = calculateDistance(d1.X, d1.Y, shot.X, shot.Y);
                if (h > distanceFromShot)
                {
                    distanceFromShot = h;
                    //3. input: angle of enemy shot // transform is to angle from you
                   
                }
            }*/
            inputsForNN.Add(d1.X); inputsForNN.Add(d1.Y);
            inputsForNN.Add(d2.X); inputsForNN.Add(d2.Y);
            

            return inputsForNN;
        }

        private double calculateDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }
    }
}
