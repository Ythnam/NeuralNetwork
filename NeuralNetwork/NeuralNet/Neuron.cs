﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.NeuralNet
{
    class Neuron
    {
        public List<double> Inputs { get; set; } // All input on the neuron

        public List<double> Weights { get; set; } // Weights needed for the output

        public List<double> Outputs { get; set; } // All outputs of the neuron

        public Tuple<int, int> NeuralPosition { get; } // This is the Layer of the neurone and his position on this layer

        public Neuron(List<double> _inputs, List<double> _weights, Tuple<int, int> _neuralPosition)
        {
            this.Inputs = _inputs;
            this.Weights = _weights;
            this.NeuralPosition = _neuralPosition;

            this.CheckLenght();
        }

        public Neuron(Neuron _neuron)
        {
            this.Inputs = _neuron.Inputs;
            this.Weights = _neuron.Weights;
            this.NeuralPosition = _neuron.NeuralPosition;

            this.CheckLenght();
        }

        private void CheckLenght()
        {
            if(this.Inputs.Count != this.Weights.Count)
            {
                Console.WriteLine("Le nombre d'entrée et le nombre de poids sont différents");
            }
        }
    }
}