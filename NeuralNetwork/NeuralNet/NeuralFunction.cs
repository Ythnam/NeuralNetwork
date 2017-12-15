using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.NeuralNet
{
    static class NeuralFunction
    {
        public static double Sigmoid(double _seriesImput)
        {
            return (double)(1 / (1 + Math.Exp(-_seriesImput)));
        }
    }
}
