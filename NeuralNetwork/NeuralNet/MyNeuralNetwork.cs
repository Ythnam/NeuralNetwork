using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.NeuralNet
{
    public class MyNeuralNetwork
    {
        public List<Neuron> Neurons { get; }
        public List<double> NetworkInputs { get; set; }

        private int[] nomberOfNeuroneForEachLayer;
        private int numberOfInputs;
        private int layer;
        private Random random;

        // _numberOfNeuronForEachLayer ==> Ex : {2,3,1} means : 1st layer => 2 neurons, 2nd layer => 3 neurons, 3rd layer => 1 neuron</param>

        public MyNeuralNetwork(int _numberOfInput, int[] _numberOfNeuronForEachLayer)
        {
            this.Neurons = new List<Neuron>();
            this.random = new Random();
            this.numberOfInputs = _numberOfInput;
            this.layer = _numberOfNeuronForEachLayer.Length;
            this.nomberOfNeuroneForEachLayer = _numberOfNeuronForEachLayer;
        }

        public MyNeuralNetwork(int _numberOfInput, int[] _numberOfNeuronForEachLayer, Random rand)
        {
            this.Neurons = new List<Neuron>();
            this.random = rand;
            this.numberOfInputs = _numberOfInput;
            this.layer = _numberOfNeuronForEachLayer.Length;
            this.nomberOfNeuroneForEachLayer = _numberOfNeuronForEachLayer;
        }

        /// <summary>
        /// Create all neuron on the network
        /// </summary>
        public void GenerateNeurons()
        {
            
            //Console.WriteLine("Nombre de neurons : " + this.nomberOfNeuroneForEachLayer);
            // Iterate on number of layer
            for(int i = 0; i < this.layer; i++)
            {
                // Iterate on number of neurons on the layer i
                for(int j = 0; j < this.nomberOfNeuroneForEachLayer[i]; j++)
                {
                    Neuron neuron = new Neuron(new Tuple<int, int>(i,j));
                    this.Neurons.Add(neuron);
                    //Console.WriteLine("Neurons => i " + i + ", j "+ j);
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
                    // GetWeight allows negativ weights
                    for(int i = 0; i < numberOfInputs; i++)
                    {
                        neuron.Weights.Add(GetWeight(this.random.NextDouble()));
                        //Console.WriteLine("Neuron ==> i = " + neuron.NeuralPosition.Item1 + ", j = " + neuron.NeuralPosition.Item2 + " ==> Weights = " + neuron.Weights[i]);
                    }
                }
                else
                {
                    // Get previous neuron and add 1 Weight for each previous neuron

                    for(int i = 0; i < this.nomberOfNeuroneForEachLayer[neuron.NeuralPosition.Item1 - 1]; i++)
                    {
                        // GetWeight allows negativ weights
                        neuron.Weights.Add(GetWeight(this.random.NextDouble()));
                        //Console.WriteLine("Neuron ==> i = " + neuron.NeuralPosition.Item1 + ", j = " + neuron.NeuralPosition.Item2 + " ==> Weight"+i+" = " + neuron.Weights[i]);

                    }
                }
            }
        }

        public List<double> ExecuteNetwork(List<double> _inputs)
        {
            //Check if lenght of the list of inputs is eguals to the number of neurons of 1st layer
            if(_inputs.Count == this.numberOfInputs)
            {
                foreach(Neuron neuron in Neurons)
                {
                    neuron.Inputs = new List<double>(); // clear the inputs
                    if(neuron.NeuralPosition.Item1 == 0)
                    {
                        // Add for 1st layer neuron input. First input is for the first neuron etc..
                        for(int i = 0; i < numberOfInputs; i++)
                        {
                            neuron.Inputs.Add(_inputs[i]);
                            //Console.WriteLine("--------------" + _inputs[i]);
                        }
                        
                        // Current neuron will get his output
                        GenerateOutput(neuron);
                    }
                    else
                    {
                        // Add all output of previous in input of current layer
                        foreach(Neuron neur in GetPreviousLayer(neuron))
                        {
                            neuron.Inputs.Add(neur.Outputs);
                        }
                        // Current neuron will get his output
                        GenerateOutput(neuron);
                    }
                }
                //Now we got all output but we only need the output of the last layer
                return GetLastLayerOutputs();
            }
            else
            {
                throw new ArgumentException("Nomber of input is different of the number of neurons on 1st layer");
            }
        }

        private List<double> GetLastLayerOutputs()
        {
            List<double> outputs = new List<double>();
            foreach(Neuron neuron in Neurons)
            {
                if (neuron.NeuralPosition.Item1 == (this.layer - 1))
                    outputs.Add(neuron.Outputs);
            }

            // test loop
            foreach (double d in outputs)
                Console.WriteLine("Outputs of network = " + d);

            return outputs;
        }

        /// <summary>
        /// Allow to easely find previous layer of the neuron
        /// </summary>
        /// <param name="neuron"></param>
        /// <returns></returns>
        public List<Neuron> GetPreviousLayer(Neuron neuron)
        {
            List<Neuron> previousLayer = new List<Neuron>();
            if(neuron.NeuralPosition.Item1 != 0)
            {
                foreach(Neuron _neuron in Neurons)
                {
                    if (_neuron.NeuralPosition.Item1 == (neuron.NeuralPosition.Item1 - 1))
                        previousLayer.Add(_neuron);
                }
            }
            return previousLayer;
        }

        #region Private Function

        /// <summary>
        /// Multiply all input with their weights and add them. Then use the Tanh function to have a nomber between -1 and 1. Add this to Output of Neuron
        /// </summary>
        /// <param name="_neuron"> Neuron that you want to get his output </param>
        private static void GenerateOutput(Neuron _neuron)
        {
            double sum = 0;
            for(int i = 0; i < _neuron.Inputs.Count; i++)
            {
                sum += _neuron.Inputs[i] * _neuron.Weights[i];
                //Console.WriteLine("Somme numéro " + i + " = " + sum);
            }
            //Console.WriteLine("Somme des input * weights = " + sum);
            _neuron.Outputs = NeuralFunction.Sigmoid(sum);
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

        #endregion

        
    }
}
