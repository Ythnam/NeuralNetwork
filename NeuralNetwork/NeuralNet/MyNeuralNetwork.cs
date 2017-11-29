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
        public void GenerateNetwork(int[] _nomberOfNeuroneForEachLayer)
        {
            this.layer = _nomberOfNeuroneForEachLayer.Length;
            // Iterate on number of layer
            for(int i = 0; i < this.layer; i++)
            {
                // Iterate on number of neurons on the layer i
                for(int j = 0; j < _nomberOfNeuroneForEachLayer[i]; j++)
                {
                    Neuron neuron = new Neuron(new Tuple<int, int>(i,j));
                    this.Neurons.Add(neuron);
                }
            }

            if (this.Neurons.Any())
                Console.WriteLine("Any neuron on this neural network");
        }

        public void InitWieghtsOnNetwork()
        {
            if(this.GetTypeOfNetwork().Equals("Perceptron"))
            {
                
            }
        }

        /// <summary>
        /// Allow to know if the network got more than 1 neuron
        /// </summary>
        /// <returns> String which define if it is a perceptron or not </returns>
        private string GetTypeOfNetwork()
        {
            switch (this.layer)
            {
                case 1:
                    return "Perceptron";
                case 0:
                    return "Error";
                default:
                    return "NotPerceptron";
            }
        }

        // Allow to know if a neuron is on a 1st layer, on the hidden layer or in the output layer easely
        private void GetTypeOfNeuron(Neuron neuron)
        {
            if(neuron.NeuralPosition.Item1 == 0)
            {

            }
        }

        private double GetWeight(double _weight)
        {
            if(this.random.NextDouble() <= 0.5)
            {
                _weight = - _weight;
            }

            return _weight;
        }
    }
}
