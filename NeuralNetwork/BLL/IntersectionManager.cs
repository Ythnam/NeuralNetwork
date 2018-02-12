using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace NeuralNetwork.BLL
{
    class IntersectionManager
    {
        #region Intersection Rectangle Line

        public double IntersectionX { get; private set; }
        public double IntersectionY { get; private set; }

        public IntersectionManager() { }

        public bool LineIntersectsRect(Point p1, Point p2, Rectangle r)
        {
            return SegmentIntersectRectangle((double)r.GetValue(Canvas.LeftProperty), 
                                             (double)r.GetValue(Canvas.TopProperty), 
                                             (double)r.GetValue(Canvas.LeftProperty) + r.Width, 
                                             (double)r.GetValue(Canvas.TopProperty) + r.Height, 
                                             p1.X,
                                             p1.Y, 
                                             p2.X,
                                             p2.Y);
        }

        public bool LineIntersectsRect(Line line, Rectangle r)
        {
            return SegmentIntersectRectangle((double)r.GetValue(Canvas.LeftProperty),
                                             (double)r.GetValue(Canvas.TopProperty),
                                             (double)r.GetValue(Canvas.LeftProperty) + r.Width,
                                             (double)r.GetValue(Canvas.TopProperty) + r.Height,
                                             line.X1,
                                             line.Y1,
                                             line.X2,
                                             line.Y2);
        }

        private bool SegmentIntersectRectangle(double rectangleMinX,
                                                      double rectangleMinY,
                                                      double rectangleMaxX,
                                                      double rectangleMaxY,
                                                      double p1X,
                                                      double p1Y,
                                                      double p2X,
                                                      double p2Y)
        {

            // Thx https://gist.github.com/ChickenProp/3194723

            this.IntersectionX = Double.NaN;
            this.IntersectionY = Double.NaN;

            bool isIntersected = true;
            var u1 = Double.NegativeInfinity;
            var u2 = Double.PositiveInfinity;

            // List which contain Delta between origin point of segment and end point
            List<double> deltas = new List<double>();
            double deltaX = p2X - p1X;
            double deltaY = p2Y - p1Y;
            deltas.Add(-deltaX);
            deltas.Add(deltaX);
            deltas.Add(-deltaY);
            deltas.Add(deltaY);

            List<double> q = new List<double>();
            q.Add((p1X - rectangleMinX));
            q.Add((rectangleMaxX - p1X));
            q.Add((p1Y - rectangleMinY));
            q.Add((rectangleMaxY - p1Y));

            for (int i = 0; i < 4; i++)
            {
                if (deltas[i] == 0)
                {
                    if (q[i] < 0)
                        return false;
                }
                else
                {
                    var t = q[i] / deltas[i];
                    if (deltas[i] < 0 && u1 < t)
                        u1 = t;
                    else if (deltas[i] > 0 && u2 > t)
                        u2 = t;
                }
            }

            if (u1 > u2 || u1 > 1 || u1 < 0)
                return false;

            this.IntersectionX = p1X + u1 * deltaX;
            this.IntersectionY = p2Y + u1 * deltaY;

            return true;
        }
        #endregion
    }
}
