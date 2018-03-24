using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HackISU_2018
{
    class Timer
    {

        public int tickAmt;
        public int currentAmt = 0;
        public bool running = false;

        public Timer(int tickAmt)
        {
            this.tickAmt = tickAmt;
        }

        public void start()
        {
            running = true;
        }

        public void stop()
        {
            running = false;
        }

        public bool Update()
        {
            if (running)
            {
                this.currentAmt++;
                if (currentAmt == tickAmt)
                {
                    this.currentAmt = 0;
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
