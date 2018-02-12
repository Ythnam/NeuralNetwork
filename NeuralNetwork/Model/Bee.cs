using GalaSoft.MvvmLight;
using NeuralNetwork.Helper;
using NeuralNetwork.NeuralNet;
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

        //private static BitmapImage _beeBitmap = BitmapHelper.Bitmap2BitmapImage(new Bitmap(NeuralNetwork.Properties.Resources.Bee));
        //public static BitmapImage BeeBitmap
        //{
        //    get { return _beeBitmap; }
        //}

        #region Propriétés
        /// <summary>
        /// Simulate sensors
        /// </summary>
        /// 
        public int num { get; set; }

        public int Fitness { get; set; }

        private List<Sensor> _sensors;
        public List<Sensor> Sensors
        {
            get { return this._sensors; }
        }

        private MyNeuralNetwork _neuralNetwork;
        public MyNeuralNetwork NeuralNetwork
        {
            get { return this._neuralNetwork; }
            set
            {
                if (this._neuralNetwork != value)
                    this._neuralNetwork = value;
            }
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
                    foreach (Sensor sensor in Sensors)
                        sensor.OriginX = value;
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
                    foreach (Sensor sensor in Sensors)
                        sensor.OriginY = value;
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
                    foreach (Sensor sensor in Sensors)
                        sensor.Angle = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        public Bee()
        {
            this._sensors = new List<Sensor>();
            Sensor _sensor1 = new Sensor(0);
            Sensor _sensor2 = new Sensor(+Math.PI / 8);
            Sensor _sensor3 = new Sensor(-Math.PI / 8);

            this.Sensors.Add(_sensor1);
            this.Sensors.Add(_sensor2);
            this.Sensors.Add(_sensor3);
            this.Angle = 0;

            this.Fitness = 0;
        }
    }
}
