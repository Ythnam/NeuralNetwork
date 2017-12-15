using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System;
using NeuralNetwork.Model;
using System.Windows.Media.Imaging;
using NeuralNetwork.Helper;
using System.Drawing;
using NeuralNetwork.NeuralNet;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls;
using NeuralNetwork.BLL;

namespace NeuralNetwork.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        private NeuralNetworkManager _neuralNetworkManager;

        private Bee _bee;
        public Bee Bee
        {
            get { return this._bee; }
            set
            {
                if(this._bee != value)
                {
                    this._bee = value;
                    RaisePropertyChanged();
                }
            }
        }
        private Honey _honey;
        public Honey Honey
        {
            get { return this._honey; }
            set
            {
                if (this._honey != value)
                {
                    this._honey = value;
                    RaisePropertyChanged();
                }
            }
        }

        private Canvas _mainCanvas;
        public Canvas MainCanvas
        {
            get { return this._mainCanvas; }
            set
            {
                if (this._mainCanvas != value)
                {
                    this._mainCanvas = value;
                    RaisePropertyChanged();
                }
            }
        }

        //private BitmapImage test = BitmapHelper.Bitmap2BitmapImage(new Bitmap(NeuralNetwork.Properties.Resources.Honey));
        //public BitmapImage Test
        //{
        //    get { return test; }
        //}

        public MainViewModel()
        {
            this.MainCanvas = new Canvas();
            this.Bee = new Bee();
            this.Bee.X = 100;
            this.Bee.Y = 100;
            this.Honey = new Honey();
            this.Honey.X = 0;
            this.Honey.Y = 0;
            this._neuralNetworkManager = new NeuralNetworkManager();


        }

        #region Command
        private ICommand _onLoadMainWindowCommand;
        public ICommand OnLoadMainWindowCommand
        {
            get
            {
                return _onLoadMainWindowCommand ?? (_onLoadMainWindowCommand = new RelayCommand(() => OnLoadMainWindow()));
            }
        }

        private ICommand _onGenerateNeuralForTest;
        public ICommand OnGenerateNeuralForTestCommand
        {
            get
            {
                return _onGenerateNeuralForTest ?? (_onGenerateNeuralForTest = new RelayCommand(() => OnGenerateNeuralForTest()));
            }
        }
        #endregion

        #region Function called by command

        private void OnGenerateNeuralForTest()
        {

            this._neuralNetworkManager.ManageOutputsOfNetwork(this.Bee);

            // int[] neuronsOnEachLayer = { 2, 3, 2 }; // 2neurones first layer, 3 second layer and 2 3rd layer
            // MyNeuralNetwork mnn = new MyNeuralNetwork(3, neuronsOnEachLayer);

            // mnn.GenerateNeurons();
            // mnn.InitWeightsOnNetwork();

            // Random rand = new Random();

            // Task t = Task.Factory.StartNew(() =>
            //{
            //    for (int i = 0; i < 500; i++)
            //    {
            //        Thread.Sleep(100);
            //        List<double> inputs = new List<double>();
            //        //inputs.Add(rand.NextDouble());
            //        //inputs.Add(rand.NextDouble());
            //        //inputs.Add(rand.NextDouble());
            //        inputs.Add(this.Bee.Sensor1.EndLineX);
            //        inputs.Add(this.Bee.Sensor2.EndLineX);
            //        inputs.Add(this.Bee.Sensor3.EndLineX);

            //        List<double> coord = mnn.ExecuteNetwork(inputs);
            //        if (coord[0] > 0.5)
            //        {
            //            this.Bee.X = this.Bee.X + 1;
            //            if (this.Bee.Angle >= (2*Math.PI))
            //                this.Bee.Angle = this.Bee.Angle - (2 * Math.PI);
            //            this.Bee.Angle += Math.PI/16;
            //        }
            //        else
            //        {
            //            this.Bee.X = this.Bee.X - 1;
            //            if (this.Bee.Angle >= (2 * Math.PI))
            //                this.Bee.Angle = this.Bee.Angle - (2 * Math.PI);
            //            this.Bee.Angle += Math.PI / 16;
            //        }

            //        if (coord[1] > 0.5)
            //        {
            //            this.Bee.Y = this.Bee.Y + 1;
            //        }
            //        else
            //        {
            //            this.Bee.Y = this.Bee.Y - 1;
            //        }


            //        //this.Bee.Angle = coord[1] * 2 * Math.PI; // 360 degree is 2Pi then i m between 0 and 2PI

            //        Console.WriteLine("Bee angle : " + this.Bee.Angle + ", Bee corrd[1] = " + coord[1]);
            //    }
            //});
        }

        private void OnLoadMainWindow()
        {
           
        }
        #endregion

        #region private function

    #endregion

    }
}