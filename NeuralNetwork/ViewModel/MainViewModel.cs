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
using NeuralNetwork.Config;

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


        private Random RandomCoord;

        private NeuralNetworkManager _neuralNetworkManager;
        private DispatcherTimer displayTimer = null;
        private DispatcherTimer sessionTimer = null;

        public List<Bee> Bees { get; set; }

        public List<Honey> Honeys { get; set; }

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

        public MainViewModel()
        {
            this.MainCanvas = new Canvas();
            this.MainCanvas.Width = ApplicationConfig.MAX_WIDTH_PANEL;
            this.MainCanvas.Height = ApplicationConfig.MAX_HEIGHT_PANEL;
            this.MainCanvas.Background = System.Windows.Media.Brushes.AliceBlue;

            this.RandomCoord = new Random();
            this.Bees = new List<Bee>();
            this.Honeys = new List<Honey>();        
        }

        #region Command
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
            this.StartDisplayTimer();
        }
        #endregion

        #region private function
        private void GenerateIA()
        {
            for (int i = 0; i < ApplicationConfig.NUMBER_OF_IA; i++)
            {
                Bee bee = new Bee();

                bee.num = i;

                bee.X = RandomCoord.NextDouble() * ApplicationConfig.MAX_WIDTH_PANEL;
                bee.Y = RandomCoord.NextDouble() * ApplicationConfig.MAX_HEIGHT_PANEL;

                bee.NeuralNetwork = new MyNeuralNetwork(NeuralNetworkConfig.NUMBER_OF_INPUTS,
                                                        NeuralNetworkConfig.NEURON_ON_EACH_LAYER,
                                                        RandomCoord);
                bee.NeuralNetwork.GenerateNeurons();
                bee.NeuralNetwork.InitWeightsOnNetwork();

                this.Bees.Add(bee);
            }

            for (int i = 0; i < ApplicationConfig.NUMBER_OF_AREA; i++)
            {
                this.Honeys.Add(new Honey(RandomCoord.NextDouble() * ApplicationConfig.MAX_WIDTH_PANEL, RandomCoord.NextDouble() * ApplicationConfig.MAX_HEIGHT_PANEL));
            }

            this._neuralNetworkManager = new NeuralNetworkManager();

            this.DisplayCanvas();
        }

        private void DisplayCanvas()
        {

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

        private void StartDisplayTimer()
        {
            this.displayTimer = new DispatcherTimer();
            this.displayTimer.Interval = new TimeSpan(0, 0, 0, 0, ApplicationConfig.TIME_DISPLAY_EVENT);
            this.displayTimer.Tick += display_timer_Tick;
            this.displayTimer.Start();
            this.displayTimer.IsEnabled = true;
        }

        private void StartSessionTimer()
        {
            this.sessionTimer = new DispatcherTimer();
            this.sessionTimer.Interval = new TimeSpan(0, 0, 0, 0, ApplicationConfig.TIME_SESSION_EVENT);
            this.sessionTimer.Tick += session_timer_Tick;
            this.sessionTimer.Start();
            this.sessionTimer.IsEnabled = true;
        }

        /// <summary>
        /// Start new IA with the genetic algorithm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void session_timer_Tick(object sender, EventArgs e)
        {
            
        }

        private void display_timer_Tick(object sender, EventArgs e)
        {
            
            foreach (Bee bee in this.Bees)
            {
                //Console.WriteLine("----------------------------------------------------------------");
                SensorManager.Detection(bee, this.Honeys);
                this._neuralNetworkManager.ManageOutputsOfNetwork(bee);
                GeometricHelper.StayOnPanelRange(bee);

                foreach(Honey honey in Honeys)
                {
                    if (GeometricHelper.Contain(honey, bee))
                    {
                        honey.Rectangle.SetValue(Canvas.LeftProperty, this.RandomCoord.NextDouble() * ApplicationConfig.MAX_WIDTH_PANEL);
                        honey.Rectangle.SetValue(Canvas.TopProperty, this.RandomCoord.NextDouble() * ApplicationConfig.MAX_HEIGHT_PANEL);
                    }
                }
                //Console.WriteLine("----------------------------------------------------------------");
            }
        }
        #endregion

    }
}