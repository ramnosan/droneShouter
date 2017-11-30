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
        private int population;
        public int Population
        {
            get; private set; 
        }

        public int Generation { get; private set; }

        public int Mutationrate { get; private set; }

        public GeneticAlgorythm(int _population, int _mutationrate)
        {
            population = _population;
            Mutationrate = _mutationrate;
        }

        public void crossoverFlipACoin()
        {
            
        }
    }

    public class DNA
    {
        
        public List<double> Genome
        {
            get; set;
        }

        public DNA(NeuralNetwork n)
        {

        }
    }
}
