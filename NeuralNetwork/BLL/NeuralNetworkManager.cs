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

            //if(coord[0] < 0.5)
            //    bee.Angle = bee.Angle + (coord[0] * Math.PI / 180);
            //else
            //    bee.Angle = bee.Angle - (coord[0] * Math.PI / 180);


            // coord[0] = speed
            // coord[1] = left
            // coord[2] = right

            bee.X = bee.X + Math.Cos(bee.Angle) * 2 * coord[0]; // Test
            bee.Y = bee.Y + Math.Sin(bee.Angle) * 2 * coord[0];

            double rotationRate = coord[1] - coord[2];

            if (rotationRate > 0)
                bee.Angle = bee.Angle + rotationRate * Math.PI / 18;
            else
                bee.Angle = bee.Angle - rotationRate * Math.PI / 18;





            //Reprint cause Proc the on preperty change (HAVE TO CHANGE THAT BUT I DON'T REALLY KNOW HOW)
            foreach (Sensor sensor in bee.Sensors)
                sensor.Display2DReprensation();

            //Console.WriteLine("Bee angle : " + bee.Angle + ", Bee corrd[1] = " + coord[0]);
        }
    }
}
