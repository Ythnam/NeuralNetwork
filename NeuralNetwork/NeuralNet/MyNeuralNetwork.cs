using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.NeuralNet
{
    class MyNeuralNetwork
    {
        public List<Neuron> Neurons { get; }
        public List<double> NetworkInputs { get; set; }

        private int[] nomberOfNeuroneForEachLayer;
        private int layer;
        private Random random;

        public MyNeuralNetwork()
        {
            this.Neurons = new List<Neuron>();
            this.random = new Random();
        }

        /// <summary>
        /// Create all neuron on the network
        /// </summary>
        /// <param name="_nomberOfNeuroneForEachLayer"> Ex : {2,3,1} means : 1st layer => 2 neurons, 2nd layer => 3 neurons, 3rd layer => 1 neuron</param>
        public void GenerateNeurons(int[] _nomberOfNeuroneForEachLayer)
        {
            this.layer = _nomberOfNeuroneForEachLayer.Length;
            this.nomberOfNeuroneForEachLayer = _nomberOfNeuroneForEachLayer;
            Console.WriteLine("Nombre de neurons : " + this.nomberOfNeuroneForEachLayer);
            // Iterate on number of layer
            for(int i = 0; i < this.layer; i++)
            {
                // Iterate on number of neurons on the layer i
                for(int j = 0; j < _nomberOfNeuroneForEachLayer[i]; j++)
                {
                    Neuron neuron = new Neuron(new Tuple<int, int>(i,j));
                    this.Neurons.Add(neuron);
                    Console.WriteLine("Neurons => i " + i + ", j "+ j);
                }
            }

            if (!this.Neurons.Any())
                Console.WriteLine("Any neuron on this neural network ==> "+!this.Neurons.Any());
        }

        /// <summary>
        /// Init all weight with random values
        /// Weight are on neurons for my model
        /// Those weight of a Neuron are link with the input
        /// </summary>
        public void InitWeightsOnNetwork()
        {
            // 2 case : Neuron on first layer and others
            foreach(Neuron neuron in Neurons)
            {
                if(neuron.NeuralPosition.Item1 == 0)
                {
                    // 1 entry for 1st layer neuron
                    // GetWeight allows negativ weights
                    neuron.Weights.Add(GetWeight(this.random.NextDouble()));
                    Console.WriteLine("Neuron ==> i = " + neuron.NeuralPosition.Item1 + ", j = " + neuron.NeuralPosition.Item2 + " ==> Weights = " + neuron.Weights[0]);
                }
                else
                {
                    // Get previous neuron and add 1 Weight for each previous neuron
                    for(int i = 0; i < this.nomberOfNeuroneForEachLayer[neuron.NeuralPosition.Item1 - 1]; i++)
                    {
                        // GetWeight allows negativ weights
                        neuron.Weights.Add(GetWeight(this.random.NextDouble()));
                        Console.WriteLine("Neuron ==> i = " + neuron.NeuralPosition.Item1 + ", j = " + neuron.NeuralPosition.Item2 + " ==> Weight"+i+" = " + neuron.Weights[i]);

                    }
                }
            }
        }

        public List<double> ExecuteNetwork(List<double> _inputs)
        {
            //Check if lenght of the list of inputs is eguals to the number of neurons of 1st layer
            if(_inputs.Count == this.nomberOfNeuroneForEachLayer[0])
            {
                foreach(Neuron neuron in Neurons)
                {
                    // Ajouter pour le 1er layer les inputs, faire une fonction générique. Ensuite faire propager la chose
                    return null;
                }
            }
            else
            {
                throw new ArgumentException("Nomber of input is different of the number of neurons on 1st layer");
            }
        }

        /// <summary>
        /// Allow to have negative Weights on this neural network
        /// </summary>
        /// <param name="_weight"></param>
        /// <returns></returns>
        private double GetWeight(double _weight)
        {
            if(this.random.NextDouble() <= 0.5)
                _weight = - _weight;
            return _weight;
        }
    }
}
