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
        private List<Sensor> _sensors;
        public List<Sensor> Sensors
        {
            get { return this._sensors; }
        }

        private int _x;
        public int X
        {
            get { return this._x; }
            set
            {
                if (this._x != value)
                {
                    this._x = value;
                    foreach(Sensor sensor in Sensors)
                    {
                        sensor.OriginX = value;
                    }
                    RaisePropertyChanged();
                }
            }
        }
        private int _y;
        public int Y
        {
            get { return this._y; }
            set
            {
                if (this._y != value)
                {
                    this._y = value;
                    foreach (Sensor sensor in Sensors)
                    {
                        sensor.OriginY = value;
                    }
                    RaisePropertyChanged();
                }
            }
        }

        private int _angle;
        public int Angle
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
            this._sensors = new List<Sensor>();
        }
    }
}
