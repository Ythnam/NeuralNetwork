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
    public class Bee : ObservableObject
    {
        
        private static BitmapImage _beeBitmap = BitmapHelper.Bitmap2BitmapImage(new Bitmap(NeuralNetwork.Properties.Resources.Bee));
        public static BitmapImage BeeBitmap
        {
            get { return _beeBitmap; }
        }

        #region Propriétés
        private Sensor _sensor1;
        public Sensor Sensor1
        {
            get { return this._sensor1; }
        }

        private Sensor _sensor2;
        public Sensor Sensor2
        {
            get { return this._sensor2; }
        }

        private Sensor _sensor3;
        public Sensor Sensor3
        {
            get { return this._sensor3; }
        }

        private double _x;
        public double X
        {
            get { return this._x; }
            set
            {
                if (this._x != value)
                {
                    this._x = value;
                    _sensor1.OriginX = value;
                    _sensor1.EndLineX = value;
                    _sensor2.OriginX = value;
                    _sensor2.EndLineX = value;
                    _sensor3.OriginX = value;
                    _sensor3.EndLineX = value;
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
                    _sensor1.OriginY = value;
                    _sensor1.EndLineY = value;
                    _sensor2.OriginY = value;
                    _sensor2.EndLineY = value;
                    _sensor3.OriginY = value;
                    _sensor3.EndLineY = value;
                    RaisePropertyChanged();
                }
            }
        }

        private double _angle;
        public double Angle
        {
            get { return this._angle; }
            set
            {
                if (this._angle != value)
                {
                    this._angle = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        public Bee()
        {
            this.Angle = 0;
            this._sensor1 = new Sensor();
            this._sensor2 = new Sensor(+Math.PI/6);
            this._sensor3 = new Sensor(-Math.PI/6);
        }
    }
}
