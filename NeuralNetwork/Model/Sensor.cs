﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Model
{
    public class Sensor : ObservableObject
    {
        private static int LENGHT = 50;

        #region Properties
        private int _originX;
        public int OriginX
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

        private int _originY;
        public int OriginY
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

        private int _endLineX;
        public int EndLineX
        {
            get { return _endLineX; }
        }

        private int _endLineY;
        public int EndLineY
        {
            get { return _endLineY; }
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

        public Sensor(int _X, int _Y, double _ang)
        {
            this.OriginX = _X;
            this.OriginY = _Y;
            this.Angle = _ang;
        }
    }
}
