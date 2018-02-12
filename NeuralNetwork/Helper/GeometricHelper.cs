using NeuralNetwork.Config;
using NeuralNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace NeuralNetwork.Helper
{
    static class GeometricHelper
    {
        //// thx : https://stackoverflow.com/questions/5514366/how-to-know-if-a-line-intersects-a-rectangle
        /// <summary>
        /// Allows to know if a line is cutting a rectangle side
        /// </summary>
        /// <param name="p1"> start of line</param>
        /// <param name="p2"> end of line </param>
        /// <param name="r"> rectangle </param>
        /// <returns></returns>
        public static bool LineIntersectsRect(Point p1, Point p2, Rectangle r)
        {
            return SegmentIntersectRectangle((double)r.GetValue(Canvas.LeftProperty), (double)r.GetValue(Canvas.TopProperty), (double)r.GetValue(Canvas.LeftProperty) + r.Width, (double)r.GetValue(Canvas.TopProperty) + r.Height, p1.X, p1.Y, p2.X, p2.Y);
        }


        private static bool SegmentIntersectRectangle(double rectangleMinX,
                                                      double rectangleMinY,
                                                      double rectangleMaxX,
                                                      double rectangleMaxY,
                                                      double p1X,
                                                      double p1Y,
                                                      double p2X,
                                                      double p2Y)
        {

            // Find min and max X for the segment
            double minX = p1X;
            double maxX = p2X;

            if (p1X > p2X)
            {
                minX = p2X;
                maxX = p1X;
            }

            // Find the intersection of the segment's and rectangle's x-projections
            if (maxX > rectangleMaxX)
            {
                maxX = rectangleMaxX;
            }

            if (minX < rectangleMinX)
            {
                minX = rectangleMinX;
            }

            if (minX > maxX) // If their projections do not intersect return false
            {
                return false;
            }

            // Find corresponding min and max Y for min and max X we found before
            double minY = p1Y;
            double maxY = p2Y;

            double dx = p2X - p1X;

            if (Math.Abs(dx) > 0.0000001)
            {
                double a = (p2Y - p1Y) / dx;
                double b = p1Y - a * p1X;
                minY = a * minX + b;
                maxY = a * maxX + b;
            }

            if (minY > maxY)
            {
                double tmp = maxY;
                maxY = minY;
                minY = tmp;
            }

            // Find the intersection of the segment's and rectangle's y-projections
            if (maxY > rectangleMaxY)
            {
                maxY = rectangleMaxY;
            }

            if (minY < rectangleMinY)
            {
                minY = rectangleMinY;
            }

            if (minY > maxY) // If Y-projections do not intersect return false
            {
                return false;
            }

            return true;
        }


        public static bool Contain(Honey honey, Bee bee)
        {
            if (bee.X > (double)honey.Rectangle.GetValue(Canvas.LeftProperty) &&
               bee.X < ((double)honey.Rectangle.GetValue(Canvas.LeftProperty) + Honey.RECTANGLE_WIDTH))
            {
                if (bee.Y > (double)honey.Rectangle.GetValue(Canvas.TopProperty) &&
                   bee.Y < ((double)honey.Rectangle.GetValue(Canvas.TopProperty) + Honey.RECTANGLE_HEIGHT))
                {
                    return true;
                }
            }
            return false;
        }

        public static void CompareDistance(Sensor sensor,double distance)
        {
            //Console.WriteLine("DistanceToObject = " + sensor.DistanceToObject + ", " + "distance = " + distance);
            if (sensor.DistanceToObject > distance)
                sensor.DistanceToObject = distance;
        }


        /// <summary>
        /// Keep AI between [0:MAX_WIDTH_PANEL] and [0:MAX_HEIGHT_PANEL]
        /// </summary>
        /// <param name="bee"></param>
        public static void StayOnPanelRange(Bee bee)
        {
            if (bee.X > ApplicationConfig.MAX_WIDTH_PANEL)
                bee.X = 0;
            if (bee.X < 0)
                bee.X = ApplicationConfig.MAX_WIDTH_PANEL;
            if (bee.Y > ApplicationConfig.MAX_HEIGHT_PANEL)
                bee.Y = 0;
            if (bee.Y < 0)
                bee.Y = ApplicationConfig.MAX_HEIGHT_PANEL;
        }
    }
}
