using NeuralNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace NeuralNetwork.Helper
{
    public static class ContainHelper
    {
        public static bool Contain(Honey honey, Bee bee)
        {
            if(bee.X > (double) honey.Rectangle.GetValue(Canvas.LeftProperty) && 
               bee.X < ((double) honey.Rectangle.GetValue(Canvas.LeftProperty) + Honey.RECTANGLE_WIDTH))
            {
                if(bee.Y > (double)honey.Rectangle.GetValue(Canvas.TopProperty) &&
                   bee.Y < ((double)honey.Rectangle.GetValue(Canvas.TopProperty) + Honey.RECTANGLE_HEIGHT))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
