﻿using NeuralNetwork.Config;
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
                    if (intersectionManager.LineIntersectsRect(new Point(sensor.OriginX, sensor.OriginY), new Point(sensor.EndLineX, sensor.EndLineY), honey.Rectangle))
                    {
                        GeometricHelper.CompareDistance(sensor, CalculateDistance(new Point(sensor.OriginX, sensor.OriginY), new Point(intersectionManager.IntersectionX, intersectionManager.IntersectionY)));
                        //sensor.DistanceToObject = CalculateDistance(new Point(sensor.OriginX, sensor.OriginY), new Point(intersectionManager.IntersectionX, intersectionManager.IntersectionY));
                        sensor.State = 1;
                        Console.WriteLine("Distance sensor object = " + sensor.DistanceToObject);
                    }
                    else
                    {
                        if(sensor.DistanceToObject != SensorConfig.SENSOR_LENGHT)
                            sensor.DistanceToObject = SensorConfig.SENSOR_LENGHT; 
                    }
                }

                if (sensor.State == 1)
                    sensor.Display.Stroke = Brushes.Red;
                else
                    sensor.Display.Stroke = Brushes.Black;

            }
        }

        private static double CalculateDistance(Point _origin, Point _intersection)
        {
            return Math.Abs(Math.Sqrt(Math.Pow(_origin.X - _intersection.X , 2) + Math.Pow(_origin.Y - _intersection.Y , 2)));
        }
    }
}
