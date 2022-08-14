using FlickClick.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlickClick.Dispatcher
{
    public class RealDispatcher : IDispatcher
    {
        public void Dispatch(Action action)
        {
            Application.Current.Dispatcher.BeginInvoke(action);
        }
    }
}
