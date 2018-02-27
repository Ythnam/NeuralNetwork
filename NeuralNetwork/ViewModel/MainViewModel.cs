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

        public MainViewModel()
        {
        }

        #region Command
        private ICommand _onStartSessionTimerCommand;
        public ICommand OnStartSessionTimerCommand
        {
            get
            {
                return _onStartSessionTimerCommand ?? (_onStartSessionTimerCommand = new RelayCommand(() => OnStartSessionTimer()));
            }
        }

        private void OnStartSessionTimer()
        {
            this.StartSessionTimer();
        }
        #endregion

        #region Function called by command

        #endregion

        #region private function

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
            MessengerInstance.Send<bool>(true, "NewGenome");
        }

        #endregion

    }
}