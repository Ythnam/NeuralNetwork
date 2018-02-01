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

    }
}
