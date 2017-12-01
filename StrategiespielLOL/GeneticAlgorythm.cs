﻿using System;
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

        //public List<Drone> drones{ get;  set; }

        public int Population { get; private set; }

        public int Generation { get; private set; }

        public float Mutationrate { get; private set; }

        public GeneticAlgorythm(int _population, float _mutationrate)
        {
            this.Population = _population;
            Mutationrate = _mutationrate;
        }

        public void pickP1AndP2(List<Drone> winnerPool)
        {

        }
    }

    public class DNA
    {
        CryptoRandom cRandom = new CryptoRandom();
        Random random = new Random();

        public List<List<double>> Genome // stores weights of a ANN(Artificial Neural Network)
        { get; private set; }

        public DNA(NeuralNetwork n)
        {
            Genome = getWeights2D(n);
        }

        public List<List<double>> crossoverFlipACoin(List<List<double>> otherGenome)
        {
            List<List<double>> newGenome = new List<List<double>>();
            for (int i = 0; i < Genome.Count; i++)
            {
                for (int j = 0; j < Genome[i].Count; j++)
                {
                    if (random.Next(0,2) == 0)
                        newGenome.ElementAt(i).Add(otherGenome.ElementAt(i).ElementAt(j));
                    else
                        newGenome.ElementAt(i).Add(Genome.ElementAt(i).ElementAt(j));
                }
            }
            return newGenome;
        }

        private List<List<double>> getWeights2D(NeuralNetwork n)
        {
            List<List<double>> weightsL = new List<List<double>>();
            List<double> weightsN;
            double weightD;
            
            for (int i = 0; i < n.LayerCount-1; i++) // i = layerposition
			{
                for (int j = 0; j < n.Layers[i].NeuronCount; j++)//j = neuronposition
                {
                    weightsN = new List<double>();
                    weightsL.Add(weightsN);
                    
                    for (int k = 0; k < n.Layers[i].Neurons[j].DendriteCount; k++)
			        {
                        weightD = n.Layers[i].Neurons[j].Dendrites[k].Weight;
                        weightsN.Add(weightD);
			        }
                }
			}
            return weightsL;
        }
    }
}