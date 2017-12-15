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
        public void ManageOutputsOfNetwork(List<double> _sensorState, MyNeuralNetwork mnn)
        {


            Random rand = new Random();

            //Task t = Task.Factory.StartNew(() =>
            //{
            //    for (int i = 0; i < 500; i++)
            //    {
            //        //Thread.Sleep(100);
            //        List<double> inputs = new List<double>();
                    
            //        foreach(Sensor sensor in sensors)
            //            inputs.Add(sensor.)

            //        List<double> coord = mnn.ExecuteNetwork(inputs);
            //        if (coord[0] > 0.5)
            //        {
            //            this.Bee.X = this.Bee.X + 1;
            //            if (this.Bee.Angle >= (2 * Math.PI))
            //                this.Bee.Angle = this.Bee.Angle - (2 * Math.PI);
            //            this.Bee.Angle += Math.PI / 16;
            //        }
            //        else
            //        {
            //            this.Bee.X = this.Bee.X - 1;
            //            if (this.Bee.Angle >= (2 * Math.PI))
            //                this.Bee.Angle = this.Bee.Angle - (2 * Math.PI);
            //            this.Bee.Angle += Math.PI / 16;
            //        }

            //        if (coord[1] > 0.5)
            //        {
            //            this.Bee.Y = this.Bee.Y + 1;
            //        }
            //        else
            //        {
            //            this.Bee.Y = this.Bee.Y - 1;
            //        }


            //        //this.Bee.Angle = coord[1] * 2 * Math.PI; // 360 degree is 2Pi then i m between 0 and 2PI

            //        Console.WriteLine("Bee angle : " + this.Bee.Angle + ", Bee corrd[1] = " + coord[1]);
            //    }
            //});
        }
    }
}
