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
        private Random rand;

        public MyNeuralNetwork BestGenome1 { get; set; }
        public MyNeuralNetwork BestGenome2 { get; set; }
        public List<MyNeuralNetwork> NewGenome { get; set; } // list of 8 genome which containt the 2 bests before, 6 child of those 2 best with sometimes a mutation

        public GeneticManager()
        {
            this.NewGenome = new List<MyNeuralNetwork>();
            this.rand = new Random();
        }

        public void GenerateNewGenome(List<Bee> bees)
        {
            this.CreateBestGenomes(bees);
            this.LeftRightCrossingGenomes();
            this.OddEvenCrossingGenomes();
            this.GenerateLastGenomes();
            this.Mutation();
        }

        public void ResetData()
        {
            this.BestGenome1 = new MyNeuralNetwork(NeuralNetworkConfig.NUMBER_OF_INPUTS,
                                                   NeuralNetworkConfig.NEURON_ON_EACH_LAYER);
            this.BestGenome2 = new MyNeuralNetwork(NeuralNetworkConfig.NUMBER_OF_INPUTS,
                                                   NeuralNetworkConfig.NEURON_ON_EACH_LAYER);
            this.NewGenome = new List<MyNeuralNetwork>();
        }

        // Function which get 2 best Genome. I have to update it to be better
        private void CreateBestGenomes(List<Bee> bees)
        {
            Bee best = new Bee();
            Bee second = new Bee();

            foreach (Bee bee in bees)
            {
                if (bee.Fitness >= second.Fitness)
                {
                    if (bee.Fitness >= best.Fitness)
                    {
                        second = best;
                        best = bee;
                    }
                    else
                        second = bee;
                }
            }

            Console.WriteLine("Best Bee Fitness : " + best.Fitness);
            Console.WriteLine("Second Bee Fitness : " + second.Fitness);

            this.BestGenome1 = best.NeuralNetwork;
            this.BestGenome2 = second.NeuralNetwork;

            this.NewGenome.Add(this.BestGenome1);
            this.NewGenome.Add(this.BestGenome2);
        }

        private void LeftRightCrossingGenomes()
        {
            int NetworkLayerNbr = NeuralNetworkConfig.NEURON_ON_EACH_LAYER.Length;
            int splitLayerForCrossing = NetworkLayerNbr / 2;

            List<Neuron> bestNeurons1Left = new List<Neuron>();
            List<Neuron> bestNeurons1Right = new List<Neuron>();

            foreach (Neuron neuron in BestGenome1.Neurons)
            {
                // -1 because we are on List which go from 0 to N-1
                if (neuron.NeuralPosition.Item1 <= (splitLayerForCrossing - 1))
                    bestNeurons1Left.Add(neuron);
                else
                    bestNeurons1Right.Add(neuron);
            }

            List<Neuron> bestNeurons2Left = new List<Neuron>();
            List<Neuron> bestNeurons2Right = new List<Neuron>();

            foreach (Neuron neuron in BestGenome2.Neurons)
            {
                if (neuron.NeuralPosition.Item1 <= splitLayerForCrossing - 1)
                    bestNeurons2Left.Add(neuron);
                else
                    bestNeurons2Right.Add(neuron);
            }

            // Add to the left of one network the right of the other
            bestNeurons1Left.AddRange(bestNeurons2Right);
            bestNeurons2Left.AddRange(bestNeurons1Right);

            this.NewGenome.Add(new MyNeuralNetwork(NeuralNetworkConfig.NUMBER_OF_INPUTS, NeuralNetworkConfig.NEURON_ON_EACH_LAYER) { Neurons = bestNeurons1Left });
            this.NewGenome.Add(new MyNeuralNetwork(NeuralNetworkConfig.NUMBER_OF_INPUTS, NeuralNetworkConfig.NEURON_ON_EACH_LAYER) { Neurons = bestNeurons2Left });
        }

        private void OddEvenCrossingGenomes()
        {
            List<Neuron> even = new List<Neuron>();
            List<Neuron> odd = new List<Neuron>();

            for(int i = 0; i < NeuralHelper.NbrOfNeurons(); i++)
            {
                if(this.BestGenome1.Neurons.ElementAt(i).NeuralPosition.Item1 % 2 == 0)
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
                foreach(Neuron neur in mnn.Neurons)
                {
                    for(int i = 0; i < neur.Weights.Count; i++)
                    {
                        if (this.rand.NextDouble() >= GeneticConfig.MUTATION_PERCENT)
                            neur.Weights[i] = this.rand.NextDouble();
                    }
                }
            }
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
    }
}
