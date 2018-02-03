using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Helper
{
    public static class NeuralHelper
    {
        public static int NbrOfNeurons()
        {
            int nbr = 0;

            for (int i = 0; i < Config.NeuralNetworkConfig.NEURON_ON_EACH_LAYER.Length; i++)
            {
                nbr = nbr + Config.NeuralNetworkConfig.NEURON_ON_EACH_LAYER[i];
            }

            return nbr;
        }
    }
}
