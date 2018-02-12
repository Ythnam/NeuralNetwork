using GalaSoft.MvvmLight;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System;
using NeuralNetwork.Config;

namespace NeuralNetwork.Model
{
    public class Honey : ObservableObject
    {
        /// <summary>
        /// Usefull for the 2D representation
        /// </summary>
        private Rectangle _rect;
        public Rectangle Rectangle
        {
            get { return this._rect; }
            set
            {
                if(this._rect != value)
                {
                    this._rect = value;
                    RaisePropertyChanged();
                }
            }
        }

        public Honey(double _x, double _y)
        {
            this._rect = new Rectangle();
            this._rect.SetValue(Canvas.LeftProperty, _x);
            this._rect.SetValue(Canvas.TopProperty, _y);

            this._rect.Width = ApplicationConfig.RECTANGLE_WIDTH;
            this._rect.Height = ApplicationConfig.RECTANGLE_HEIGHT;
            this._rect.Stroke = Brushes.Black;
        }
    }
}
