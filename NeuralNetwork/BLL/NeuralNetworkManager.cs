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
            int[] neuronsOnEachLayer = { 2, 3, 2 }; // 2neurones first layer, 3 second layer and 2 3rd layer
            bee.NeuralNetwork = new MyNeuralNetwork(3, neuronsOnEachLayer);
            bee.NeuralNetwork.GenerateNeurons();
            bee.NeuralNetwork.InitWeightsOnNetwork();

            Random rand = new Random();

            Task t = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 500; i++)
                {
                    //Thread.Sleep(100);
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


                    //this.Bee.Angle = coord[1] * 2 * Math.PI; // 360 degree is 2Pi then i m between 0 and 2PI

                    Console.WriteLine("Bee angle : " + bee.Angle + ", Bee corrd[1] = " + coord[1]);
                }
            });
        }
    }
}
