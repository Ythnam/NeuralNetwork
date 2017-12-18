using NeuralNetwork.Helper;
using NeuralNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NeuralNetwork.BLL
{
    class SensorManager
    {
        public static void Detection(Bee _bee, List<Honey> _honeys)
        {
            foreach (Sensor sensor in _bee.Sensors)
            {
                foreach(Honey honey in _honeys)
                {
                    if (IntersectionHelper.LineIntersectsRect(new Point(sensor.OriginX, sensor.OriginY), new Point(sensor.EndLineX, sensor.EndLineY), honey.Rectangle))
                    {
                        sensor.State = 1;
                        Console.WriteLine("++++++++++++++++++++++++++++ SENSOR = 1");
                    }
                    else
                    {
                        sensor.State = 0;
                        Console.WriteLine("++++++++++++++++++++++++++++ SENSOR = 0");
                    }
                }
            }
        }
    }
}
