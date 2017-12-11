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
                    //this._endLineX = value 
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
                    this._endLineX = this.OriginX + LENGHT * Math.Cos(Angle);
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
                    this._endLineY = this.OriginY + LENGHT * Math.Sin(Angle);
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
                if (this._angle != value)
                {
                    this._angle = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        public Sensor()
        {
            this.OriginX = 0;
            this.OriginY = 0;
            this.Angle = 0;
        }

        public Sensor(double _ang)
        {
            this.Angle = _ang;
        }
    }
}
