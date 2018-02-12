using GalaSoft.MvvmLight;
using NeuralNetwork.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuralNetwork.Model
{
    public class Sensor : ObservableObject
    {
        private readonly double _angleSensor; // Basic angle of each sensor

        #region Properties
        public double State { get; set; }


        private Line _display;
        public Line Display
        {
            get { return this._display;
            }
            private set
            {
                this._display = Display;
                RaisePropertyChanged();
            }
        }

        private double _angle;
        public double Angle
        {
            get { return _angle; }
            set
            {
                this._angle = this._angleSensor + value;
                RaisePropertyChanged();
            }
        }

        private double _distanceToObject;
        public double DistanceToObject
        {
            get { return this._distanceToObject; }
            set
            {
                if (this._distanceToObject != value)
                {
                    this._distanceToObject = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        public Sensor(double _ang)
        {
            this._angleSensor = _ang;
            this.Angle = 0;
            this._display = new Line();
            this.Display.Stroke = Brushes.Black;
            this.State = 0;
            this.DistanceToObject = SensorConfig.SENSOR_LENGHT;
        }
    }
}
