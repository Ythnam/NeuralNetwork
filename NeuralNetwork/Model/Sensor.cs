using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Model
{
    public class Sensor : ObservableObject
    {
        private static int LENGHT = 25;
        private double _angleSensor; // Basic angle of each sensor

        #region Properties
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
        }
    }
}
