using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NeuralNetwork.BLL;
using NeuralNetwork.Config;
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
    public class TrainedAIViewModel : ViewModelBase
    {
        SessionManager sessionManager;

        private Canvas trainedCanvas;
        public Canvas TrainedCanvas
        {
            get { return this.trainedCanvas; }
            set
            {
                if (this.trainedCanvas != value)
                {
                    this.trainedCanvas = value;
                    RaisePropertyChanged();
                }
            }
        }

        public TrainedAIViewModel()
        {
            this.TrainedCanvas = new Canvas();
            this.TrainedCanvas.Width = ApplicationConfig.MAX_WIDTH_PANEL;
            this.TrainedCanvas.Height = ApplicationConfig.MAX_HEIGHT_PANEL;
            this.TrainedCanvas.Background = System.Windows.Media.Brushes.AliceBlue;

            this.sessionManager = new SessionManager();

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
            this.sessionManager.GenerateTrainedIA();
            this.DisplayCanvas();
            this.sessionManager.StartTimer();
        }

        private void DisplayCanvas()
        {

            foreach (Bee _bee in this.sessionManager.Bees)
            {
                foreach (Sensor sensor in _bee.Sensors)
                {
                    this.TrainedCanvas.Children.Add(sensor.Display);
                }
            }
            foreach (Honey _honey in this.sessionManager.Honeys)
            {
                this.TrainedCanvas.Children.Add(_honey.Rectangle);
            }
        }

        //  Add specific data for a good genome
        // -0,255130714855683;-0,955834815258083;-0,783108153279455;0,592491513394048;-0,784140992343491;0,98408665926386;0,967099378335802;-0,442148500328021;-0,632802178912238;-0,985582368907324;0,734380449044695;0,889736337070231;-0,96948987616668;-0,0441478486378434;-0,424890779622314;-0,875818291155537;-0,49081501294431;0,928610841244744;-0,733780128757367;0,901407715352908;0,422541373140431;-0,859910866180393;-0,00569380400967496;-0,319530605021646;-0,153766952526647;-0,787081397039388;-0,351918173652104;0,0480898516476573;0,776869147912073;0,146528345135287;0,533762837077381;0,653492134834403;0,978287229769997;-0,545320124619324;0,629172365008468;0,701040347898863;0,792735186774626;0,88193937338979;-0,88227203203471;-0,593355259203052;-0,945778405268573;0,00502261147136922;-0,613798863074649;0,736431643709741;0,296256835710377;-0,0655902545273259;0,569690959793372;-0,803169875779734;0,713029795192662;-0,809193837367554;-0,168522292826568;-0,0133588630768279;-0,776029413461699;0,456884687513525;0,638324160891736;-0,197333722467224;0,305246607076957;0,900872538285736;-0,199626027233724;-0,710890946775158;0,953108638037512;0,805236903394217;0,735971083741622;0,00928217964679104;-0,742394056982544;0,885624664782372;0,958955365679672;0,866589023669525;-0,911685968708101;-0,561104091611274;0,89031102596331;0,953664542154253;0,379058851571315;-0,616981185794334;-0,754791849644292;0,801609388460223;0,306108725865422;-0,331175773093093;-0,577235685930231;-0,485617822262281;0,667881177583654

    }
}
