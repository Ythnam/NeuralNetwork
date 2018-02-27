using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NeuralNetwork.BLL;
using NeuralNetwork.Config;
using NeuralNetwork.Event;
using NeuralNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace NeuralNetwork.ViewModel
{
    public class TrainingAIViewModel : ViewModelBase
    {
        private readonly SessionManager sessionManager;
        private readonly GeneticManager geneticManager;
        private readonly Random randomCoord;

        private Canvas trainingCanvas;
        public Canvas TrainingCanvas
        {
            get { return this.trainingCanvas; }
            set
            {
                if (this.trainingCanvas != value)
                {
                    this.trainingCanvas = value;
                    RaisePropertyChanged();
                }
            }
        }

        public TrainingAIViewModel()
        {
            this.TrainingCanvas = new Canvas();
            this.TrainingCanvas.Width = ApplicationConfig.MAX_WIDTH_PANEL;
            this.TrainingCanvas.Height = ApplicationConfig.MAX_HEIGHT_PANEL;
            this.TrainingCanvas.Background = System.Windows.Media.Brushes.AliceBlue;

            this.sessionManager = new SessionManager();
            this.geneticManager = new GeneticManager();
            this.randomCoord = new Random();

            this.sessionManager.New_MyTimerTick += New_My_Tick;

            MessengerInstance.Register<bool>(this, "NewGenome", (action) => NewGenome(action));
        }

        private void New_My_Tick(object source, NewMyTimerTickEventArgs e)
        {
            MessengerInstance.Send<double>(e.GetTimer(), "Timer");
        }

        private void NewGenome(bool action)
        {
            IA();
        }

        private ICommand _onLoadGenerateAICommand;
        public ICommand OnLoadGenerateAICommand
        {
            get
            {
                return _onLoadGenerateAICommand ?? (_onLoadGenerateAICommand = new RelayCommand(
                    () => OnLoadGenerateAI()));
            }
        }

        private void OnLoadGenerateAI()
        {
            this.sessionManager.GenerateIA();
            this.DisplayCanvas();
            this.sessionManager.StartTimer();
        }

        private void DisplayCanvas()
        {

            foreach (Bee _bee in this.sessionManager.Bees)
            {
                foreach (Sensor sensor in _bee.Sensors)
                {
                    this.TrainingCanvas.Children.Add(sensor.Display);
                }
            }
            foreach (Honey _honey in this.sessionManager.Honeys)
            {
                this.TrainingCanvas.Children.Add(_honey.Rectangle);
            }
        }

        // Managed by the MainView which send a trigger eache session timer tick
        public void IA()
        {
            this.sessionManager.StopTimer();
            this.geneticManager.GenerateNewGenome(this.sessionManager.Bees);

            this.sessionManager.ReGenerateIA(this.geneticManager.NewGenome);

            foreach (Honey honey in this.sessionManager.Honeys)
            {
                honey.Rectangle.SetValue(Canvas.LeftProperty, this.randomCoord.NextDouble() * ApplicationConfig.MAX_WIDTH_PANEL);
                honey.Rectangle.SetValue(Canvas.TopProperty, this.randomCoord.NextDouble() * ApplicationConfig.MAX_HEIGHT_PANEL);
            }

            this.geneticManager.ResetData();

            this.sessionManager.StartTimer();
        }
    }
}
