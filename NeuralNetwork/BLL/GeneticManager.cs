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
            this.Crossing();
            //this.GenerateLastGenomes();
            //this.Mutation();
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
            for(int i = 0; i < GeneticConfig.MAX_BEST_FITNESS_TAKEN; i++)
            {
                this.SelectionnedGenomes.Add(sortedByFitness[ApplicationConfig.NUMBER_OF_AI - i - 1].NeuralNetwork);
                Console.WriteLine("Fitness : " + sortedByFitness[ApplicationConfig.NUMBER_OF_AI - i - 1].Fitness);
            }

            // Get worst genomes
            for (int i = 0; i < GeneticConfig.MAX_WORST_FITNESS_TAKEN; i++)
                this.SelectionnedGenomes.Add(sortedByFitness[i].NeuralNetwork);
        }

        private void Crossing()
        {
            int i = 0;
            int otherNetworkNumber;
            // this loop allows to keep a part of gene of each best and worst.
            // This loop create new genomes
            //foreach (MyNeuralNetwork mnn in this.SelectionnedGenomes)
            //{
            //    otherNetworkNumber = (int)(this.rand.NextDouble() * (GeneticConfig.MAX_BEST_FITNESS_TAKEN + GeneticConfig.MAX_WORST_FITNESS_TAKEN -1));
            //    while (otherNetworkNumber == i)
            //        otherNetworkNumber = (int)(this.rand.NextDouble() * (GeneticConfig.MAX_BEST_FITNESS_TAKEN + GeneticConfig.MAX_WORST_FITNESS_TAKEN - 1));
            //    Console.WriteLine("Network taken = " + otherNetworkNumber);

            //    this.NewGenome.Add(new MyNeuralNetwork(NeuralNetworkConfig.NUMBER_OF_INPUTS, NeuralNetworkConfig.NEURON_ON_EACH_LAYER) {
            //        Neurons = this.CrossingGenomes(mnn, this.SelectionnedGenomes.ElementAt(otherNetworkNumber)) });

            //    i++;
            //}

            for(int ite = 0; ite < (ApplicationConfig.NUMBER_OF_AI /*- (GeneticConfig.MAX_BEST_FITNESS_TAKEN + GeneticConfig.MAX_WORST_FITNESS_TAKEN)*/); ite++)
            {
                otherNetworkNumber = (int)(this.rand.NextDouble() * (GeneticConfig.MAX_BEST_FITNESS_TAKEN + GeneticConfig.MAX_WORST_FITNESS_TAKEN - 1));
                int networkNumber = (int)(this.rand.NextDouble() * (GeneticConfig.MAX_BEST_FITNESS_TAKEN + GeneticConfig.MAX_WORST_FITNESS_TAKEN - 1));
                //Console.WriteLine(" -- otherNetworkNumber = " + otherNetworkNumber + " ; networkNumber = " + networkNumber);
                while (otherNetworkNumber == networkNumber)
                    networkNumber = (int)(this.rand.NextDouble() * (GeneticConfig.MAX_BEST_FITNESS_TAKEN + GeneticConfig.MAX_WORST_FITNESS_TAKEN - 1));
                //Console.WriteLine(" ++ otherNetworkNumber = " + otherNetworkNumber + " ; networkNumber = " + networkNumber);

                this.NewGenome.Add(new MyNeuralNetwork(NeuralNetworkConfig.NUMBER_OF_INPUTS, NeuralNetworkConfig.NEURON_ON_EACH_LAYER)
                {
                    Neurons = this.CrossingGenomes(this.SelectionnedGenomes.ElementAt(otherNetworkNumber), this.SelectionnedGenomes.ElementAt(networkNumber))
                });
            }

            //Console.WriteLine("New genome count = " + this.NewGenome.Count);
        }

        private List<Neuron> CrossingGenomes(MyNeuralNetwork _choosenOne, MyNeuralNetwork _choosenTwo)
        {
            List<Neuron> neurons = _choosenOne.Neurons;
            for (int i = 0; i < _choosenOne.Neurons.Count; i++)
            {
                for(int j = 0; j < _choosenOne.Neurons.ElementAt(i).Weights.Count; j++)
                {
                    // 50% chance to get gene from one or the other
                    if(this.rand.NextDouble() < 0.5)
                    {
                        neurons.ElementAt(i).Weights[j] = _choosenTwo.Neurons.ElementAt(i).Weights[j];
                        //Console.WriteLine("ça rentre dans la boucle");
                    }
                }
            }

            return neurons;      
        }


        private void Mutate(MyNeuralNetwork mnn)
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

        public void Mutation()
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
        #endregion
    }
}
