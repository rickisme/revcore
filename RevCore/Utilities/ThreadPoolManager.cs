using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Utilities
{
    public class ThreadPoolManager
    {
        public static Timer Schedule(int delay, Action action)
        {
            Timer timer = new Timer(new TimerCallback(delegate(object obj)
            {
                action.Invoke();
            }), null, delay, Timeout.Infinite);
            return timer;
        }
    }
}
