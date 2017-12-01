using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNet.NeuralNet;

namespace StrategiespielLOL
{
    public class GeneticAlgorythm
    {
        Random random = new Random();

        public List<Drone> drones{ get;  set; }

        public int Population { get; private set; }

        public int Generation { get; private set; }

        public float Mutationrate { get; private set; }

        public GeneticAlgorythm(int _population, float _mutationrate)
        {
            this.Population = _population;
            Mutationrate = _mutationrate;
            drones = new List<Drone>();
        }

        public void pickP1AndP2(List<Drone> winnerPool)
        {

        }
    }

    public class DNA
    {
        public double [,] Genome2D
        { get; set; }

        public DNA(NeuralNetwork n)
        {

        }

        public void crossoverFlipACoin()
        {

        }

        private List<List<double>> getWeights2D(Drone d)
        {
            List<List<double>> weightsL = new List<List<double>>();
            List<double> weightsN;
            double weightD;
            
            for (int i = 0; i < d.NeuralNetwork.LayerCount-1; i++) // i = layerposition
			{
                for (int j = 0; j < d.NeuralNetwork.Layers[i].NeuronCount; j++)//j = neuronposition
                {
                    weightsN = new List<double>();
                    weightsL.Add(weightsN);
                    
                    for (int k = 0; k < d.NeuralNetwork.Layers[i].Neurons[j].DendriteCount; k++)
			        {
                        weightD = d.NeuralNetwork.Layers[i].Neurons[j].Dendrites[k].Weight;
                        weightsN.Add(weightD);
			        }
                }
			}
            return weightsL;
        }
    }
}
