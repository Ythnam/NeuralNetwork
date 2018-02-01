using NeuralNetwork.Config;
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
        public MyNeuralNetwork BestGenome1 { get; set; }
        public MyNeuralNetwork BestGenome2 { get; set; }
        public List<MyNeuralNetwork> NewGenome { get; set; } // list of 8 genome which containt the 2 bests before, 6 child of those 2 best with sometimes a mutation

        public GeneticManager()
        {

        }


        // Function which get 2 best Genome. I have to update it to be better
        public void GetBestGenomes(List<Bee> bees)
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
                    {
                        second = bee;
                    }
                }
                Console.WriteLine("Fitness " + bee.num + " : " + bee.Fitness);
            }

            Console.WriteLine("Best Bee Fitness : " + best.Fitness);
            Console.WriteLine("Second Bee Fitness : " + second.Fitness);

            this.BestGenome1 = best.NeuralNetwork;
            this.BestGenome2 = second.NeuralNetwork;
        }

        private void CrossingGenomes()
        {
            int NetworkLayerNbr = NeuralNetworkConfig.NEURON_ON_EACH_LAYER.Length;
            int splitLayerForCrossing = NetworkLayerNbr / 2;

            List<Neuron> bestNeurons1Left = new List<Neuron>();
            List<Neuron> bestNeurons1Right = new List<Neuron>();

            foreach (Neuron neuron in BestGenome1.Neurons)
            {
                if (neuron.NeuralPosition.Item1 <= splitLayerForCrossing)
                    bestNeurons1Left.Add(neuron);
                else
                    bestNeurons1Right.Add(neuron);
            }

            List<Neuron> bestNeurons2Left = new List<Neuron>();
            List<Neuron> bestNeurons2Right = new List<Neuron>();

            foreach (Neuron neuron in BestGenome2.Neurons)
            {
                if (neuron.NeuralPosition.Item1 <= splitLayerForCrossing)
                    bestNeurons2Left.Add(neuron);
                else
                    bestNeurons2Right.Add(neuron);
            }

            bestNeurons1Left.AddRange(bestNeurons2Right);
            bestNeurons2Left.AddRange(bestNeurons1Right);
        }

        /// <summary>
        /// Mutate neural network weights
        /// </summary>
        //public void MutateGenome()
        //{
        //    for (int i = 0; i < weights.Length; i++)
        //    {
        //        for (int j = 0; j < weights[i].Length; j++)
        //        {
        //            for (int k = 0; k < weights[i][j].Length; k++)
        //            {
        //                float weight = weights[i][j][k];

        //                //mutate weight value 
        //                float randomNumber = UnityEngine.Random.Range(0f, 100f);

        //                if (randomNumber <= 2f)
        //                { //if 1
        //                  //flip sign of weight
        //                    weight *= -1f;
        //                }
        //                else if (randomNumber <= 4f)
        //                { //if 2
        //                  //pick random weight between -1 and 1
        //                    weight = UnityEngine.Random.Range(-0.5f, 0.5f);
        //                }
        //                else if (randomNumber <= 6f)
        //                { //if 3
        //                  //randomly increase by 0% to 100%
        //                    float factor = UnityEngine.Random.Range(0f, 1f) + 1f;
        //                    weight *= factor;
        //                }
        //                else if (randomNumber <= 8f)
        //                { //if 4
        //                  //randomly decrease by 0% to 100%
        //                    float factor = UnityEngine.Random.Range(0f, 1f);
        //                    weight *= factor;
        //                }

        //                weights[i][j][k] = weight;
        //            }
        //        }
        //    }
        //}

    }
}
