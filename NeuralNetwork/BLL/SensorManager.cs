using NeuralNetwork.Config;
using NeuralNetwork.Helper;
using NeuralNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace NeuralNetwork.BLL
{
    static class SensorManager
    {

        public static void Detection(Bee _bee, List<Honey> _honeys)
        {
            IntersectionManager intersectionManager = new IntersectionManager();
            foreach (Sensor sensor in _bee.Sensors)
            {
                sensor.State = 0;
                foreach (Honey honey in _honeys)
                {
                    if (intersectionManager.LineIntersectsRect(sensor.Display, honey.Rectangle))
                    {
                        GeometricHelper.GetClosestDistance(sensor, CalculateDistance(new Point(sensor.Display.X1, sensor.Display.Y1), new Point(intersectionManager.IntersectionX, intersectionManager.IntersectionY)));
                        sensor.State = 1; // ==> Avec un événement ça aurait été plus propre
                        //Console.WriteLine("Distance sensor object = " + sensor.DistanceToObject);
                    }
                }

                if (sensor.State == 1)
                    sensor.Display.Stroke = Brushes.Red;
                else
                {
                    sensor.Display.Stroke = Brushes.Black;
                    if (sensor.DistanceToObject != SensorConfig.SENSOR_LENGHT)
                        sensor.DistanceToObject = SensorConfig.SENSOR_LENGHT;
                }
                    

            }
        }

        private static double CalculateDistance(Point _origin, Point _intersection)
        {
            //Console.WriteLine("Distance calculated = " + Math.Abs(Math.Sqrt(Math.Pow(_origin.X - _intersection.X, 2) + Math.Pow(_origin.Y - _intersection.Y, 2))));
            return Math.Abs(Math.Sqrt(Math.Pow(_origin.X - _intersection.X , 2) + Math.Pow(_origin.Y - _intersection.Y , 2)));
        }
    }
}
