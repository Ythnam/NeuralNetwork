﻿using GalaSoft.MvvmLight;
using NeuralNetwork.Config;
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
        public int Number { get; set; }

        public int Fitness { get; set; }

        public List<Sensor> Sensors { get; }

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
                    {
                        //sensor.OriginX = value;
                        sensor.Display.X1 = value;
                        sensor.Display.X2 = value + SensorConfig.SENSOR_LENGHT * Math.Cos(sensor.Angle);
                    }

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
                    {
                        sensor.Display.Y1 = value;
                        sensor.Display.Y2 = value + SensorConfig.SENSOR_LENGHT * Math.Sin(sensor.Angle);
                    }

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
            this.Sensors = new List<Sensor>();
            Sensor _sensor1 = new Sensor(0);
            Sensor _sensor2 = new Sensor(+Math.PI / 8);
            Sensor _sensor3 = new Sensor(-Math.PI / 8);
            Sensor _sensor4 = new Sensor(+Math.PI / 16);
            Sensor _sensor5 = new Sensor(-Math.PI / 16);

            this.Sensors.Add(_sensor1);
            this.Sensors.Add(_sensor2);
            this.Sensors.Add(_sensor3);
            this.Sensors.Add(_sensor4);
            this.Sensors.Add(_sensor5);
            this.Angle = 0;

            this.Fitness = 0;
        }
    }
}
