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
            Random rand = new Random();

            List<double> inputs = new List<double>();

            foreach (Sensor sensor in bee.Sensors)
                inputs.Add(sensor.DistanceToObject);

            List<double> coord = bee.NeuralNetwork.ExecuteNetwork(inputs);

            // coord[0] = speed
            // coord[1] = left
            // coord[2] = right

            bee.X = bee.X + Math.Cos(bee.Angle) * 2 * coord[0]; // Test
            bee.Y = bee.Y + Math.Sin(bee.Angle) * 2 * coord[0];

            // rotationRate > 0 ==> angle[T0] < angle[T] ==> on trigonometric we are going to the left
            // rotationRate < 0 ==> angle[T0] < angle[T] ==> on trigonometric we are going to the right
            double rotationRate = coord[1] - coord[2];
            bee.Angle = bee.Angle + rotationRate * Math.PI / 18;






            //Reprint cause Proc the on preperty change (HAVE TO CHANGE THAT BUT I DON'T REALLY KNOW HOW)
            foreach (Sensor sensor in bee.Sensors)
                sensor.Display2DReprensation();

            //Console.WriteLine("Bee angle : " + bee.Angle + ", Bee corrd[1] = " + coord[0]);
        }
    }
}
