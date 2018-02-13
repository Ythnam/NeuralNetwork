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

        //public MyNeuralNetwork BestGenome1 { get; set; }
        //public MyNeuralNetwork BestGenome2 { get; set; }

        public List<MyNeuralNetwork> BestGenomes { get; set; }
        public List<MyNeuralNetwork> NewGenome { get; set; } // list of 8 genome which containt the 2 bests before, 6 child of those 2 best with sometimes a mutation

        public GeneticManager()
        {
            this.BestGenomes = new List<MyNeuralNetwork>();
            this.NewGenome = new List<MyNeuralNetwork>();
            this.rand = new Random();
        }

        public void GenerateNewGenome(List<Bee> bees)
        {
            this.GetBestGenomes(bees);
            this.LeftRightCrossingGenomes();
            this.OddEvenCrossingGenomes();
            //this.GenerateLastGenomes();
            //this.Mutation();
        }

        public void Mutate(MyNeuralNetwork mnn)
        {
            foreach (Neuron neur in mnn.Neurons)
            {
                for (int i = 0; i < neur.Weights.Count; i++)
                {
                    if (this.rand.NextDouble() >= GeneticConfig.MUTATION_PERCENT)
                    {
                        double cache = neur.Weights[i];
                        neur.Weights[i] = this.GetWeight(this.rand.NextDouble());
                        Console.WriteLine("Mution on neurone (" + neur.NeuralPosition.Item1 + "," + neur.NeuralPosition.Item2 + ") , Weight : " + i + " : " + cache + " ===> " + neur.Weights[i]);
                    }
                }
            }
        }

        public void ResetData()
        {
            this.BestGenomes = new List<MyNeuralNetwork>();
            this.NewGenome = new List<MyNeuralNetwork>();
        }

        #region private
        /// <summary>
        ///  Function which get best Genome. I have to update it to be better
        /// </summary>
        /// <param name="bees"></param>
        private void GetBestGenomes(List<Bee> bees)
        {
            List<Bee> sortedByFitness = bees.OrderBy(o => o.Fitness).ToList(); // sort by fitness

            for(int i = 0; i < GeneticConfig.MAX_BEST_FITNESS_TAKEN; i++)
            {
                this.BestGenomes.Add(sortedByFitness[ApplicationConfig.NUMBER_OF_AI - i].NeuralNetwork);
                Console.WriteLine("Fitness : " + sortedByFitness[ApplicationConfig.NUMBER_OF_AI - i].Fitness);
            }
        }

        private void LeftRightCrossingGenomes()
        {
            int numberOnIteration = 0;
            int splitLayerForCrossing = NeuralNetworkConfig.NEURON_ON_EACH_LAYER.Length / 2;
            List<Neuron> bestNeuronsEvenLeft = new List<Neuron>();
            List<Neuron> bestNeuronsEvenRight = new List<Neuron>();
            List<Neuron> bestNeuronsOddLeft = new List<Neuron>();
            List<Neuron> bestNeuronsOddRight = new List<Neuron>();

            foreach (MyNeuralNetwork neuralNetwork in this.BestGenomes)
            {
                if((numberOnIteration % 2) == 0)
                {
                    foreach (Neuron neuron in neuralNetwork.Neurons)
                    {
                        // -1 because we are on List which go from 0 to N-1
                        if (neuron.NeuralPosition.Item1 <= (splitLayerForCrossing - 1))
                            bestNeuronsEvenLeft.Add(neuron);
                        else
                            bestNeuronsEvenRight.Add(neuron);
                    }
                }
                else
                {
                    foreach (Neuron neuron in neuralNetwork.Neurons)
                    {
                        if (neuron.NeuralPosition.Item1 <= splitLayerForCrossing - 1)
                            bestNeuronsOddLeft.Add(neuron);
                        else
                            bestNeuronsOddRight.Add(neuron);
                    }

                    // creat new genomes
                    bestNeuronsEvenLeft.AddRange(bestNeuronsOddRight); 
                    bestNeuronsOddLeft.AddRange(bestNeuronsEvenRight);

                    this.NewGenome.Add(new MyNeuralNetwork(NeuralNetworkConfig.NUMBER_OF_INPUTS, NeuralNetworkConfig.NEURON_ON_EACH_LAYER) { Neurons = bestNeuronsEvenLeft });
                    this.NewGenome.Add(new MyNeuralNetwork(NeuralNetworkConfig.NUMBER_OF_INPUTS, NeuralNetworkConfig.NEURON_ON_EACH_LAYER) { Neurons = bestNeuronsOddLeft });

                    bestNeuronsEvenLeft = new List<Neuron>();
                    bestNeuronsEvenRight = new List<Neuron>();
                    bestNeuronsOddLeft = new List<Neuron>();
                    bestNeuronsOddRight = new List<Neuron>();
                }
                Debug(neuralNetwork.Neurons);
                numberOnIteration = numberOnIteration + 1;
            }
        }

        private void OddEvenCrossingGenomes()
        {
            List<Neuron> even = new List<Neuron>();
            List<Neuron> odd = new List<Neuron>();

            for (int i = 0; i < NeuralHelper.NbrOfNeurons(); i++)
            {
                if (this.BestGenome1.Neurons.ElementAt(i).NeuralPosition.Item1 % 2 == 0)
                {
                    even.Add(this.BestGenome1.Neurons.ElementAt(i));
                    odd.Add(this.BestGenome2.Neurons.ElementAt(i));
                }
                else
                {
                    even.Add(this.BestGenome2.Neurons.ElementAt(i));
                    odd.Add(this.BestGenome1.Neurons.ElementAt(i));
                }
            }

            this.NewGenome.Add(new MyNeuralNetwork(NeuralNetworkConfig.NUMBER_OF_INPUTS, NeuralNetworkConfig.NEURON_ON_EACH_LAYER) { Neurons = even });
            this.NewGenome.Add(new MyNeuralNetwork(NeuralNetworkConfig.NUMBER_OF_INPUTS, NeuralNetworkConfig.NEURON_ON_EACH_LAYER) { Neurons = odd });

        }

        private void GenerateLastGenomes()
        {
            while(NewGenome.Count < ApplicationConfig.NUMBER_OF_AI)
            {
                MyNeuralNetwork neuralNetwork = new MyNeuralNetwork(NeuralNetworkConfig.NUMBER_OF_INPUTS,
                                                                    NeuralNetworkConfig.NEURON_ON_EACH_LAYER);
                neuralNetwork.GenerateNeurons();
                neuralNetwork.InitWeightsOnNetwork();

                this.NewGenome.Add(neuralNetwork);
            }

            Console.WriteLine("test fin iteration ==> " + NewGenome.Count);

        }

        private void Mutation()
        {
            foreach(MyNeuralNetwork mnn in this.NewGenome)
            {
                Mutate(mnn);
            }
        }

        private double GetWeight(double _weight)
        {
            if (this.rand.NextDouble() <= 0.5)
                _weight = -_weight;
            return _weight;
        }

        private void Debug(List<Neuron> ne)
        {
            string debug = "";
            foreach (Neuron neur in ne)
            {
                foreach (double wei in neur.Weights)
                    debug = debug + ";" + wei;
            }
            Console.WriteLine(debug);
        }
        #endregion
    }
}
