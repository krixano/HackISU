using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackISU_2018
{
    class Timer
    {

        public int tickAmt;
        public int currentAmt = 0;

        public Timer(int tickAmt)
        {
            this.tickAmt = tickAmt;
        }

        public bool Update()
        {
            this.currentAmt++;
            if (currentAmt == tickAmt)
            {
                this.currentAmt = 0;
                return true;
            }
            return false;
        }

    }
}
