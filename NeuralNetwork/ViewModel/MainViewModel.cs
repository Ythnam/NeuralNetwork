using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System;
using NeuralNetwork.Model;
using System.Windows.Media.Imaging;
using NeuralNetwork.Helper;
using System.Drawing;
using NeuralNetwork.NeuralNet;

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
            

        }

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

        private void OnGenerateNeuralForTest()
        {
            MyNeuralNetwork mnn = new MyNeuralNetwork();
            int[] neurons = { 2, 3, 2 };
            mnn.GenerateNeurons(neurons);
            mnn.InitWeightsOnNetwork();
        }

        private void OnLoadMainWindow()
        {
           
        }
    }
}