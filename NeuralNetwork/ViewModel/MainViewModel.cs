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

        public static int NUMBER_OF_IA = 5;

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

        private ICommand _onGenerateAICommand;
        public ICommand OnGenerateAICommand
        {
            get
            {
                return _onGenerateAICommand ?? (_onGenerateAICommand = new RelayCommand(() => OnGenerateAI()));
            }
        }
        #endregion

        #region Function called by command

        private void OnGenerateAI()
        {
            this.GenerateIA();
            this.StartTimer();
        }

        private void OnLoadMainWindow()
        {
           
        }
        #endregion

        #region private function
        private void GenerateIA()
        {
            Random RandomCoord = new Random();

            for (int i = 0; i < NUMBER_OF_IA; i++)
            {
                Bee bee = new Bee();

                bee.num = i;

                bee.X = RandomCoord.NextDouble() * 525;
                bee.Y = RandomCoord.NextDouble() * 525;

                int[] neuronsOnEachLayer = { 3, 4, 4, 3 }; // 2neurones first layer, 3 second layer and 2 3rd layer
                bee.NeuralNetwork = new MyNeuralNetwork(3, neuronsOnEachLayer, RandomCoord);
                bee.NeuralNetwork.GenerateNeurons();
                bee.NeuralNetwork.InitWeightsOnNetwork();

                this.Bees.Add(bee);
            }

            for (int i = 0; i < 20; i++)
            {
                this.Honeys.Add(new Honey(RandomCoord.NextDouble() * 525, RandomCoord.NextDouble() * 525));
            }

            this._neuralNetworkManager = new NeuralNetworkManager();

            foreach (Bee _bee in this.Bees)
            {
                foreach (Sensor sensor in _bee.Sensors)
                {
                    this.MainCanvas.Children.Add(sensor.Display);
                    sensor.Display2DReprensation();
                }
            }
            foreach (Honey _honey in this.Honeys)
            {
                this.MainCanvas.Children.Add(_honey.Rectangle);
                _honey.Display2DRepresentation();
            }
        }

        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timer.Tick += timer_Tick;
            timer.Start();
            timer.IsEnabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            
            foreach (Bee bee in this.Bees)
            {
                Console.WriteLine("----------------------------------------------------------------");
                SensorManager.Detection(bee, this.Honeys);
                this._neuralNetworkManager.ManageOutputsOfNetwork(bee);
                Console.WriteLine("----------------------------------------------------------------");
            }

        }
        #endregion

    }
}