using GalaSoft.MvvmLight;
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
        private static int LENGHT = 25;
        private double _angleSensor; // Basic angle of each sensor

        #region Properties
        private double _state;
        public double State
        {
            get { return _state; }
            set
            {
                if (this._state != value)
                {
                    this._state = value;
                    RaisePropertyChanged();
                }
            }
        }

        private double _originX;
        public double OriginX
        {
            get { return _originX; }
            set
            {
                if(this._originX != value)
                {
                    this._originX = value;
                    this.EndLineX = value + LENGHT * Math.Cos(Angle);
                    RaisePropertyChanged();
                }
            }
        }

        private double _originY;
        public double OriginY
        {
            get { return _originY; }
            set
            {
                if (this._originY != value)
                {
                    this._originY = value;
                    this.EndLineY = value + LENGHT * Math.Sin(Angle);
                    RaisePropertyChanged();
                }
            }
        }

        private double _endLineX;
        public double EndLineX
        {
            get { return _endLineX; }
            set
            {
                if (this._endLineX != value)
                {
                    this._endLineX = value;
                    RaisePropertyChanged();
                }
            }
        }

        private double _endLineY;
        public double EndLineY
        {
            get { return _endLineY; }
            set
            {
                if (this._endLineY != value)
                {
                    this._endLineY = value;
                    RaisePropertyChanged();
                }
            }
        }

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
                //if (this._angle != value)
                //{
                    //if (this._angle >= (2 * Math.PI))
                    //    this._angle = this._angle - (2 * Math.PI); // Modulo 2PI
                    this._angle = this._angleSensor + value;
                    Console.WriteLine("Angle Sensor = " + this._angle);
                    RaisePropertyChanged();
                //}
            }
        }

        // Basic angle of each sensor
        //private double _angleSensor;
        //public double AngleSensor
        //{
        //    get { return _angleSensor; }
        //}
        #endregion

        public Sensor(double _ang)
        {
            this._angleSensor = _ang;
            this.Angle = 0;
            this._display = new Line();
            this.Display.Stroke = Brushes.Black;
            this.State = 0;
        }

        private void CreateLine()
        {
            this._display.X1 = this._originX;
            this._display.Y1 = this.OriginY;
            this._display.X2 = this.EndLineX;
            this._display.Y2 = this.EndLineY;
        }

        public void Display2DReprensation()
        {
            CreateLine();
            RaisePropertyChanged();
        }
    }
}
