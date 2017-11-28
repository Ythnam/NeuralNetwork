using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System;
using NeuralNetwork.Model;
using System.Windows.Media.Imaging;
using NeuralNetwork.Helper;
using NeuralNetwork.BLL;
using System.Drawing;

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
        private MyNeuralNetwork net;
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

        private BitmapImage test = BitmapHelper.Bitmap2BitmapImage(new Bitmap(NeuralNetwork.Properties.Resources.Honey));
        public BitmapImage Test
        {
            get { return test; }
        }

        public MainViewModel()
        {
            this.Bee = new Bee();
            this.Bee.X = 100;
            this.Bee.Y = 100;
            this.Honey = new Honey();
            this.Honey.X = 0;
            this.Honey.Y = 0;

            this.net = new MyNeuralNetwork();
            

        }

        private ICommand _onLoadMainWindowCommand;
        public ICommand OnLoadMainWindowCommand
        {
            get
            {
                return _onLoadMainWindowCommand ?? (_onLoadMainWindowCommand = new RelayCommand(() => OnLoadMainWindow()));
            }
        }

        private ICommand _startCommand;
        public ICommand StartCommand
        {
            get
            {
                return _startCommand ?? (_startCommand = new RelayCommand(() => Start()));
            }
        }

        private void Start()
        {
            for(int i = 0; i < 50; i++)
            {

                net.FeedForward(new float[] { 0, 1 });

                net.FeedForward(new float[] { 1, 0 });

                net.FeedForward(new float[] { 1, 1 });

                net = new MyNeuralNetwork(net);

                Console.WriteLine("----1---"+net.FeedForward(new float[] { 0, 1 })[0]);
                Console.WriteLine("----2---"+net.FeedForward(new float[] { 1, 0 })[0]);
                Console.WriteLine("----3---" + net.FeedForward(new float[] { 1, 1 })[0]);

            }



        }

        private void OnLoadMainWindow()
        {
           
        }
    }
}