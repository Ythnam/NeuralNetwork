using GalaSoft.MvvmLight;
using NeuralNetwork.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NeuralNetwork.Model
{
    public class Honey : ObservableObject
    {
        private static BitmapImage _honeyBitmap = BitmapHelper.Bitmap2BitmapImage(new Bitmap(NeuralNetwork.Properties.Resources.Honey));
        public static BitmapImage HoneyBitmap
        {
            get { return _honeyBitmap; }
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

        public Honey()
        {
        }

    }
}
