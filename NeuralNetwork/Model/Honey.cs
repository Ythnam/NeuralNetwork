using GalaSoft.MvvmLight;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System;

namespace NeuralNetwork.Model
{
    public class Honey : ObservableObject
    {
        //private static BitmapImage _honeyBitmap = BitmapHelper.Bitmap2BitmapImage(new Bitmap(NeuralNetwork.Properties.Resources.Honey));
        //public static BitmapImage HoneyBitmap
        //{
        //    get { return _honeyBitmap; }
        //}

        public static int RECTANGLE_WIDTH = 10;
        public static int RECTANGLE_HEIGHT = 10;

        /// <summary>
        /// Usefull for the 2D representation
        /// </summary>
        private Rectangle _rect;
        public Rectangle Rectangle
        {
            get { return this._rect; }
        }

        #region Coordonnées
        private double _x;
        public double X
        {
            get { return this._x; }
            set
            {
                if (this._x != value)
                {
                    this._x = value;
                    RaisePropertyChanged();
                }
            }
        }
        private double _y;
        public double Y
        {
            get { return this._y; }
            set
            {
                if (this._y != value)
                {
                    this._y = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        public Honey(double _x, double _y)
        {
            this.X = _x;
            this.Y = _y;
            this._rect = new Rectangle();
            this._rect.SetValue(Canvas.LeftProperty, _x);
            this._rect.SetValue(Canvas.TopProperty, _y);

            this._rect.Width = RECTANGLE_WIDTH;
            this._rect.Height = RECTANGLE_HEIGHT;
            this._rect.Stroke = Brushes.Black;
        }

        public void Display2DRepresentation()
        {
            this._rect.SetValue(Canvas.LeftProperty, this.X);
            this._rect.SetValue(Canvas.TopProperty, this.Y);
            RaisePropertyChanged();
        }


    }
}
