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
        private DispatcherTimer sessionTimer = null;
        private SessionManager sessionManager;
        private GeneticManager geneticManager;
        private Random randomCoord;


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

            this.randomCoord = new Random();

            this.sessionManager = new SessionManager();
            this.geneticManager = new GeneticManager();
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

        private ICommand _onStopCommand;
        public ICommand OnStopCommand
        {
            get
            {
                return _onStopCommand ?? (_onStopCommand = new RelayCommand(() => OnStop()));
            }
        }
        #endregion

        #region Function called by command

        private void OnGenerateAI()
        {
            this.sessionManager.GenerateIA();
            this.DisplayCanvas();
            this.sessionManager.StartTimer();

            this.StartSessionTimer();
        }

        private void OnStop()
        {
            this.sessionManager.StopTimer();
        }
        #endregion

        #region private function

        private void DisplayCanvas()
        {

            foreach (Bee _bee in this.sessionManager.Bees)
            {
                foreach (Sensor sensor in _bee.Sensors)
                {
                    this.MainCanvas.Children.Add(sensor.Display);
                }
            }
            foreach (Honey _honey in this.sessionManager.Honeys)
            {
                this.MainCanvas.Children.Add(_honey.Rectangle);
            }
        }

        private void StartSessionTimer()
        {
            this.sessionTimer = new DispatcherTimer();
            this.sessionTimer.Interval = new TimeSpan(0, 0, 0, 0, ApplicationConfig.TIME_SESSION_EVENT);
            this.sessionTimer.Tick += session_timer_Tick;
            this.sessionTimer.Start();
            this.sessionTimer.IsEnabled = true;
        }

        private void session_timer_Tick(object sender, EventArgs e)
        {
            this.sessionManager.StopTimer();
            this.geneticManager.GenerateNewGenome(this.sessionManager.Bees);

            for(int i = 0; i < ApplicationConfig.NUMBER_OF_AI; i++)
            {
                if(i > 1) // i Don't mutate the 2 best fitness
                {
                    this.geneticManager.Mutate(this.geneticManager.NewGenome[i]);
                }
            }

            this.sessionManager.ReGenerateIA(this.geneticManager.NewGenome);

            foreach (Bee bee in this.sessionManager.Bees)
                bee.Fitness = 0;

            foreach (Honey honey in this.sessionManager.Honeys)
            {
                honey.Rectangle.SetValue(Canvas.LeftProperty, this.randomCoord.NextDouble() * ApplicationConfig.MAX_WIDTH_PANEL);
                honey.Rectangle.SetValue(Canvas.TopProperty, this.randomCoord.NextDouble() * ApplicationConfig.MAX_HEIGHT_PANEL);
            }

            this.geneticManager.ResetData();

            this.sessionManager.StartTimer();

        }

        #endregion

    }
}