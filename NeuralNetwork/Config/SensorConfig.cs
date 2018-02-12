using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Config
{
    public static class SensorConfig
    {
        public static int SIDE_LENGHT = 100;
        public static double SENSOR_LENGHT { get; } = ((double)Math.Sqrt(2)) * SIDE_LENGHT; // Lenght of the sensor ==> lenght = sqrt(2) * SIDE_LENGHT
    }
}
