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
using System.Windows.Shapes;
using System.Windows.Threading;

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
        private DispatcherTimer timer = null;

        private List<Bee> _bees;
        public List<Bee> Bees
        {
            get { return this._bees; }
            set
            {
                if(this._bees != value)
                {
                    this._bees = value;
                    //RaisePropertyChanged();
                }
            }
        }
        private List<Honey> _honeys;
        public List<Honey> Honeys
        {
            get { return this._honeys; }
            set
            {
                if (this._honeys != value)
                {
                    this._honeys = value;
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
            this.Bees = new List<Bee>();
            this.Honeys = new List<Honey>();


            Bee bee = new Bee();
            bee.X = 100;
            bee.Y = 100;

            int[] neuronsOnEachLayer = { 2, 3, 2 }; // 2neurones first layer, 3 second layer and 2 3rd layer
            bee.NeuralNetwork = new MyNeuralNetwork(3, neuronsOnEachLayer);
            bee.NeuralNetwork.GenerateNeurons();
            bee.NeuralNetwork.InitWeightsOnNetwork();


            this.Bees.Add(bee);

            Honey honey = new Honey(150, 50);
            this.Honeys.Add(honey);
            Honey honey1 = new Honey(50, 50);
            this.Honeys.Add(honey1);
            Honey honey2 = new Honey(150, 150);
            this.Honeys.Add(honey2);
            Honey honey3 = new Honey(50, 150);
            this.Honeys.Add(honey3);

            this._neuralNetworkManager = new NeuralNetworkManager();

            foreach(Bee _bee in this.Bees)
            {
                foreach (Sensor sensor in _bee.Sensors)
                {
                    this.MainCanvas.Children.Add(sensor.Display);
                    sensor.Display2DReprensation();
                }
            }
            foreach(Honey _honey in this.Honeys)
            {
                this.MainCanvas.Children.Add(_honey.Rectangle);
                _honey.Display2DRepresentation();
            }

            

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            timer.Tick += timer_Tick;
            timer.Start();
            timer.IsEnabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach(Bee bee in this.Bees)
            {
                Console.WriteLine("----------------------------------------------------------------");
                SensorManager.Detection(bee, this.Honeys);
                this._neuralNetworkManager.ManageOutputsOfNetwork(bee);
                Console.WriteLine("----------------------------------------------------------------");
            }

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

        }

        private void OnLoadMainWindow()
        {
           
        }
        #endregion

        #region private function

    #endregion

    }
}