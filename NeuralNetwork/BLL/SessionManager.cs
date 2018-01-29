using NeuralNetwork.Config;
using NeuralNetwork.Helper;
using NeuralNetwork.Model;
using NeuralNetwork.NeuralNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace NeuralNetwork.BLL
{
    public class SessionManager
    {
        private Random RandomCoord;

        private NeuralNetworkManager _neuralNetworkManager;
        private DispatcherTimer timer = null;

        private List<Bee> _bees;
        public List<Bee> Bees
        {
            get { return this._bees; }
            set
            {
                if (this._bees != value)
                {
                    this._bees = value;
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
                }
            }
        }

        public SessionManager()
        {
            this.GenerateIA();
        }

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
        }

        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, ApplicationConfig.TIME_DISPLAY_EVENT);
            timer.Tick += timer_Tick;
            timer.Start();
            timer.IsEnabled = true;
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
                        //honey.Rectangle.SetValue(Canvas.LeftProperty, this.RandomCoord.NextDouble() * ApplicationConfig.MAX_WIDTH_PANEL);
                        //honey.Rectangle.SetValue(Canvas.TopProperty, this.RandomCoord.NextDouble() * ApplicationConfig.MAX_HEIGHT_PANEL);
                    }
                }
            }
        }
    }
}
