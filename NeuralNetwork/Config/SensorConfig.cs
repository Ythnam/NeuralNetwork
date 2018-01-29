using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Config
{
    public static class SensorConfig
    {
        public static int SIDE_LENGHT = 50;
        public static int SENSOR_LENGHT { get; } = ((int)Math.Sqrt(2)) * SIDE_LENGHT; // Lenght of the sensor ==> lenght = 71 = sqrt(2) * SIDE_LENGHT
    }
}
