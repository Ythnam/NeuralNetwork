using NeuralNetwork.Config;
using NeuralNetwork.Helper;
using NeuralNetwork.Model;
using NeuralNetwork.NeuralNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.BLL
{
    class GeneticManager
    {
        private readonly Random rand;

        public List<MyNeuralNetwork> SelectionnedGenomes { get; set; }
        public List<MyNeuralNetwork> NewGenome { get; set; } // list of 8 genome which containt the 2 bests before, 6 child of those 2 best with sometimes a mutation

        public GeneticManager()
        {
            this.SelectionnedGenomes = new List<MyNeuralNetwork>();
            this.NewGenome = new List<MyNeuralNetwork>();
            this.rand = new Random();
        }

        public void GenerateNewGenome(List<Bee> bees)
        {
            this.SelectGenomes(bees);
            this.KeepBestGenome();
            this.Crossing();
        }

        public void ResetData()
        {
            this.SelectionnedGenomes = new List<MyNeuralNetwork>();
            this.NewGenome = new List<MyNeuralNetwork>();
        }

        #region private
        /// <summary>
        ///  Function which get best Genomes and take few worst
        /// </summary>
        /// <param name="bees"></param>
        private void SelectGenomes(List<Bee> bees)
        {
            List<Bee> sortedByFitness = bees.OrderBy(o => o.Fitness).ToList(); // sort by fitness

            // Get bests genomes
            for (int i = 0; i < GeneticConfig.MAX_BEST_FITNESS_TAKEN; i++)
            {
                this.SelectionnedGenomes.Add(sortedByFitness[ApplicationConfig.NUMBER_OF_AI - i - 1].NeuralNetwork);
            }

            this.DebugCrossing("Best genome : ", this.SelectionnedGenomes[0].Neurons);

            // Get worst genomes
            for (int i = 0; i < GeneticConfig.MAX_WORST_FITNESS_TAKEN; i++)
                this.SelectionnedGenomes.Add(sortedByFitness[i].NeuralNetwork);
        }

        /// <summary>
        /// Allows to keep the 10 best of each generations
        /// </summary>
        private void KeepBestGenome()
        {
            for(int i = 0; i < GeneticConfig.MAX_BEST_FITNESS_TAKEN; i++)
            {
                List<Neuron> neurons = this.SelectionnedGenomes[i].Neurons.Select(neuron => new Neuron(neuron)).ToList();
                this.NewGenome.Add(new MyNeuralNetwork(NeuralNetworkConfig.NUMBER_OF_INPUTS, NeuralNetworkConfig.NEURON_ON_EACH_LAYER, this.rand)
                {
                    Neurons = neurons
                });
            }           
        }

        private void Crossing()
        {
            int otherNetworkNumber;
            List<Neuron> neurons = new List<Neuron>();
            // this loop allows to keep a part of gene of each best and worst.
            // This loop create new genomes
            for(int ite = 0; ite < (ApplicationConfig.NUMBER_OF_AI - GeneticConfig.MAX_BEST_FITNESS_TAKEN); ite++)
            {
                int networkNumber;
                lock (this.rand)
                {
                    otherNetworkNumber = (int)(this.rand.NextDouble() * (GeneticConfig.MAX_BEST_FITNESS_TAKEN + GeneticConfig.MAX_WORST_FITNESS_TAKEN - 1));
                    networkNumber = (int)(this.rand.NextDouble() * (GeneticConfig.MAX_BEST_FITNESS_TAKEN + GeneticConfig.MAX_WORST_FITNESS_TAKEN - 1));
                }

                lock (this.NewGenome)
                {
                    this.NewGenome.Add(new MyNeuralNetwork(NeuralNetworkConfig.NUMBER_OF_INPUTS, NeuralNetworkConfig.NEURON_ON_EACH_LAYER, this.rand)
                    {
                        Neurons = this.CrossingGenomes(this.SelectionnedGenomes.ElementAt(otherNetworkNumber), this.SelectionnedGenomes.ElementAt(networkNumber))
                    });
                }
            }
        }

        private List<Neuron> CrossingGenomes(MyNeuralNetwork _choosenOne, MyNeuralNetwork _choosenTwo)
        {
            // Do a deepcopy. If not, referecnce will be use and change the _choosenOne neurons which converge
            List<Neuron> neurons = _choosenOne.Neurons.Select(neuron => new Neuron(neuron)).ToList();
            
            this.DebugCrossing("       Tre", neurons);

            for (int i = 0; i < neurons.Count; i++)
            {
                for(int j = 0; j < neurons.ElementAt(i).Weights.Count; j++)
                {
                    // Mutation change
                    if (this.rand.NextDouble() >= GeneticConfig.MUTATION_PERCENT)
                    {
                        double cache = neurons.ElementAt(i).Weights[j];
                        neurons.ElementAt(i).Weights[j] = this.GetWeight(this.rand.NextDouble());
                        Console.WriteLine("Mution on neurone ==> Weight : " + j + " : " + cache + " ===> " + neurons.ElementAt(i).Weights[j]);
                    }
                    else
                    {
                        // 50% chance to get gene from one or the other
                        if (this.rand.NextDouble() < 0.5)
                        {
                            neurons.ElementAt(i).Weights[j] = _choosenTwo.Neurons.ElementAt(i).Weights[j];
                        }
                    }
                }
            }
            return neurons;      
        }

        private double GetWeight(double _weight)
        {
            if (this.rand.NextDouble() <= 0.5)
                _weight = -_weight;
            return _weight;
        }

        public void Debug(List<Neuron> ne)
        {
            string debug = "";
            foreach (Neuron neur in ne)
            {
                foreach (double wei in neur.Weights)
                    debug = debug + ";" + wei;
            }
            Console.WriteLine(debug);
        }

        public void DebugCrossing(string s, List<Neuron> ne)
        {
            string debug = "";
            foreach (Neuron neur in ne)
            {
                foreach (double wei in neur.Weights)
                    debug = debug + ";" + wei;
            }
            Console.WriteLine(s + " --- " + debug);
        }
        #endregion
    }
}
