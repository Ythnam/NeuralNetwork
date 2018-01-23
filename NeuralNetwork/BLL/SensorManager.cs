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
    static class SensorManager
    {

        public static void Detection(Bee _bee, List<Honey> _honeys)
        {
            IntersectionManager intersectionManager = new IntersectionManager();
            foreach (Honey honey in _honeys)
            {
                foreach (Sensor sensor in _bee.Sensors)
                {
                    if (intersectionManager.LineIntersectsRect(new Point(sensor.OriginX, sensor.OriginY), new Point(sensor.EndLineX, sensor.EndLineY), honey.Rectangle))
                    {
                        sensor.DistanceToObject = CalculateDistance(new Point(sensor.OriginX, sensor.OriginY), new Point(intersectionManager.IntersectionX, intersectionManager.IntersectionY));
                        //Console.WriteLine("Distance sensor object = " + sensor.DistanceToObject);
                    }
                    else
                    {
                        sensor.DistanceToObject = Sensor.SENSOR_LENGHT;
                    }
                }


            }
        }

        private static double CalculateDistance(Point _origin, Point _intersection)
        {
            return Math.Abs(Math.Sqrt(Math.Pow(_origin.X - _intersection.X , 2) + Math.Pow(_origin.Y - _intersection.Y , 2)));
        }
    }
}
