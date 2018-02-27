using NeuralNetwork.Config;
using NeuralNetwork.Event;
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

        public event NewMyTimerTickEvent New_MyTimerTick;
        private double ticktimer;

        private NeuralNetworkManager _neuralNetworkManager;
        private DispatcherTimer timer = null;

        public List<Bee> Bees { get; set; }

        public List<Honey> Honeys { get; set; }

        public SessionManager()
        {
            this.Bees = new List<Bee>();
            this.Honeys = new List<Honey>();
            this.RandomCoord = new Random();

            this.ticktimer = 0;

            this._neuralNetworkManager = new NeuralNetworkManager();
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
        }

        public void GenerateTrainedIA()
        {
             Bee bee = new Bee();

             bee.X = ApplicationConfig.AI_X;
             bee.Y = ApplicationConfig.AI_Y;

             bee.NeuralNetwork = new MyNeuralNetwork(NeuralNetworkConfig.NUMBER_OF_INPUTS,
                                                     NeuralNetworkConfig.NEURON_ON_EACH_LAYER,
                                                     RandomCoord);
             bee.NeuralNetwork.GenerateNeurons();

            // trained weights
            List<double> weights = new List<double> { -0.255130714855683, -0.955834815258083, -0.783108153279455, 0.592491513394048, -0.784140992343491, 0.98408665926386, 0.967099378335802, -0.442148500328021, -0.632802178912238, -0.985582368907324, 0.734380449044695, 0.889736337070231, -0.96948987616668, -0.0441478486378434, -0.424890779622314, -0.875818291155537, -0.49081501294431, 0.928610841244744, -0.733780128757367, 0.901407715352908, 0.422541373140431, -0.859910866180393, -0.00569380400967496, -0.319530605021646, -0.153766952526647, -0.787081397039388, -0.351918173652104, 0.0480898516476573, 0.776869147912073, 0.146528345135287, 0.533762837077381, 0.653492134834403, 0.978287229769997, -0.545320124619324, 0.629172365008468, 0.701040347898863, 0.792735186774626, 0.88193937338979, -0.88227203203471, -0.593355259203052, -0.945778405268573, 0.00502261147136922, -0.613798863074649, 0.736431643709741, 0.296256835710377, -0.0655902545273259, 0.569690959793372, -0.803169875779734, 0.713029795192662, -0.809193837367554, -0.168522292826568, -0.0133588630768279, -0.776029413461699, 0.456884687513525, 0.638324160891736, -0.197333722467224, 0.305246607076957, 0.900872538285736, -0.199626027233724, -0.710890946775158, 0.953108638037512, 0.805236903394217, 0.735971083741622, 0.00928217964679104, -0.742394056982544, 0.885624664782372, 0.958955365679672, 0.866589023669525, -0.911685968708101, -0.561104091611274, 0.89031102596331, 0.953664542154253, 0.379058851571315, -0.616981185794334, -0.754791849644292, 0.801609388460223, 0.306108725865422, -0.331175773093093, -0.577235685930231, -0.485617822262281, 0.667881177583654 };

             bee.NeuralNetwork.CopyWeightsOnNetwork(weights);

            this.Bees.Add(bee);

            for (int i = 0; i < ApplicationConfig.NUMBER_OF_AREA; i++)
            {
                this.Honeys.Add(new Honey(RandomCoord.NextDouble() * ApplicationConfig.MAX_WIDTH_PANEL, RandomCoord.NextDouble() * ApplicationConfig.MAX_HEIGHT_PANEL));
            }
        }

        public void ReGenerateIA(List<MyNeuralNetwork> neuralNetwork)
        {
            Console.WriteLine("nbr of neuralnetwork generated = " + neuralNetwork.Count);
            for (int i = 0; i < ApplicationConfig.NUMBER_OF_AI; i++)
            {
                this.Bees[i].NeuralNetwork = neuralNetwork[i];
                this.Bees[i].X = ApplicationConfig.AI_X;
                this.Bees[i].Y = ApplicationConfig.AI_Y;
                this.Bees[i].Fitness = 0;
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
            this.timer.Stop();
            this.ticktimer = 0;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.ticktimer += ApplicationConfig.TIME_DISPLAY_EVENT * 10;
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

 


            if (this.New_MyTimerTick != null)
                New_MyTimerTick(this, new NewMyTimerTickEventArgs(this.ticktimer));
        }
        #endregion
    }
}
