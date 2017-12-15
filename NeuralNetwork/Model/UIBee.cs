using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace NeuralNetwork.Model
{
    public class UIBee : ObservableObject
    {
        public Ellipse Representation2D { get; set; }
        public Bee Bee { get; set; }

        public UIBee()
        {
            this.Representation2D = new Ellipse();
            this.Representation2D.Height = 10;
            this.Representation2D.Width = 10;
            this.Bee = new Bee();
        }

        public void SetX(int x)
        {
            this.Bee.X = x;
            this.Representation2D.SetValue(Canvas.LeftProperty,(double) x);
            RaisePropertyChanged();
        }

        public void SetY(int y)
        {
            this.Bee.Y = y;
            this.Representation2D.SetValue(Canvas.TopProperty, (double) y);
            RaisePropertyChanged();
        }
    }
}
