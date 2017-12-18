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
        public void ManageOutputsOfNetwork(List<Bee> bees)
        {
            foreach(Bee bee in bees)
            {
                Random rand = new Random();

                List<double> inputs = new List<double>();

                foreach (Sensor sensor in bee.Sensors)
                    inputs.Add(sensor.State);

                List<double> coord = bee.NeuralNetwork.ExecuteNetwork(inputs);
                if (coord[0] > 0.5)
                {
                    bee.X = bee.X + 1;
                    if (bee.Angle >= (2 * Math.PI))
                        bee.Angle = bee.Angle - (2 * Math.PI);
                    bee.Angle += Math.PI / 16;
                }
                else
                {
                    bee.X = bee.X - 1;
                    if (bee.Angle >= (2 * Math.PI))
                        bee.Angle = bee.Angle - (2 * Math.PI);
                    bee.Angle += Math.PI / 16;
                }

                if (coord[1] > 0.5)
                {
                    bee.Y = bee.Y + 1;
                }
                else
                {
                    bee.Y = bee.Y - 1;
                }

                bee.Angle = coord[1] * 2 * Math.PI; // 360 degree is 2Pi then i m between 0 and 2PI

                //Reprint cause Proc the on preperty change (HAVE TO CHANGE THAT BUT I DON'T REALLY KNOW HOW)
                foreach (Sensor sensor in bee.Sensors)
                    sensor.Display2DReprensation();

                Console.WriteLine("Bee angle : " + bee.Angle + ", Bee corrd[1] = " + coord[1]);
            }
        }
    }
}
