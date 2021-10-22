using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace project_Bomberman
{
    class LogicMachine
    {
        Timer timer1 = new Timer();
        
        
        public void Wait(int milliseconds)
        {
            var timer = new System.Timers.Timer();
            if (milliseconds <= 0) return;

            timer.Interval = milliseconds;
            timer.Enabled = true;
            timer.Start();
            //await Task.Delay(milliseconds);
            
            //var t = Task.Delay()
            
            
        }
    }
}
