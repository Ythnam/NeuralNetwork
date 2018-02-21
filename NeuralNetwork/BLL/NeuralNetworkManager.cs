using NeuralNetwork.Config;
using NeuralNetwork.Model;
using NeuralNetwork.NeuralNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.BLL
{
    class NeuralNetworkManager
    {
        public void ManageOutputsOfNetwork(Bee bee)
        {
            List<double> inputs = new List<double>();

            foreach (Sensor sensor in bee.Sensors)
                inputs.Add((sensor.DistanceToObject / SensorConfig.SENSOR_LENGHT)); // the division allows to keep the value between [0-100] which allows to be easely between [0-6] for a sigmoid -> ]0-1[


            List<double> coord = bee.NeuralNetwork.ExecuteNetwork(inputs);

            //foreach(double d in coord)
            //{
            //    Console.WriteLine("Bee " + bee.Number + " =====> " + d);
            //}

            // coord[0] = speed
            // coord[1] = left
            // coord[2] = right

            bee.X = bee.X + Math.Cos(bee.Angle) * ApplicationConfig.SPEED_RATE * coord[0];
            bee.Y = bee.Y + Math.Sin(bee.Angle) * ApplicationConfig.SPEED_RATE * coord[0];

            // rotationRate > 0 ==> angle[T0] < angle[T] ==> on trigonometric we are going to the left
            // rotationRate < 0 ==> angle[T0] < angle[T] ==> on trigonometric we are going to the right
            double rotationRate = coord[1] - coord[2];
            bee.Angle = bee.Angle + rotationRate * Math.PI / 18;

            //Console.WriteLine("Bee angle : " + bee.Angle + ", Bee corrd[1] = " + coord[0]);
        }
    }
}
