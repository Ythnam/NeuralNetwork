using NeuralNetwork.Config;
using NeuralNetwork.Helper;
using NeuralNetwork.Model;
using NeuralNetwork.NeuralNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace NeuralNetwork.BLL
{
    public class SessionManager
    {
        private Random RandomCoord;

        private NeuralNetworkManager _neuralNetworkManager;
        private DispatcherTimer timer = null;

        public List<Bee> Bees { get; set; }

        public List<Honey> Honeys { get; set; }

        public SessionManager()
        {
            this.Bees = new List<Bee>();
            this.Honeys = new List<Honey>();
            this.RandomCoord = new Random();
        }

        public void GenerateIA()
        {
            for (int i = 0; i < ApplicationConfig.NUMBER_OF_AI; i++)
            {
                Bee bee = new Bee();

                bee.Number = i;

                bee.X = ApplicationConfig.AI_X;
                bee.Y = ApplicationConfig.AI_Y;

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
        }

        public void ReGenerateIA(List<MyNeuralNetwork> neuralNetwork)
        {
            for (int i = 0; i < ApplicationConfig.NUMBER_OF_AI; i++)
            {
                this.Bees[i].NeuralNetwork = neuralNetwork[i];
                this.Bees[i].X = ApplicationConfig.AI_X;
                this.Bees[i].Y = ApplicationConfig.AI_Y;
            }
        }

        #region Timer
        public void StartTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, ApplicationConfig.TIME_DISPLAY_EVENT);
            this.timer.Tick += timer_Tick;
            this.timer.Start();
            this.timer.IsEnabled = true;
        }

        public void StopTimer()
        {
            this.timer.Tick -= timer_Tick;
        }

        private void timer_Tick(object sender, EventArgs e)
        {

            foreach (Bee bee in this.Bees)
            {
                SensorManager.Detection(bee, this.Honeys);
                this._neuralNetworkManager.ManageOutputsOfNetwork(bee);
                GeometricHelper.StayOnPanelRange(bee);

                foreach (Honey honey in Honeys)
                {
                    if (GeometricHelper.Contain(honey, bee))
                    {
                        honey.Rectangle.SetValue(Canvas.LeftProperty, this.RandomCoord.NextDouble() * ApplicationConfig.MAX_WIDTH_PANEL);
                        honey.Rectangle.SetValue(Canvas.TopProperty, this.RandomCoord.NextDouble() * ApplicationConfig.MAX_HEIGHT_PANEL);
                        bee.Fitness = bee.Fitness + 1;
                    }
                }
            }
        }
        #endregion
    }
}
