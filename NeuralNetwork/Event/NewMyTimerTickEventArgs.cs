using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Event
{
    public delegate void NewMyTimerTickEvent(object source, NewMyTimerTickEventArgs e);

    public class NewMyTimerTickEventArgs : EventArgs
    {
        private readonly double EventTime;
        public NewMyTimerTickEventArgs(double time)
        {
            EventTime = time;
        }
        public double GetTimer()
        {
            return EventTime;
        }
    }
}
