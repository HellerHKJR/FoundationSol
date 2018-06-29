using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTest.Lib
{
    public class TimeUtil
    {
        private DateTime _CurrentTime;
        public DateTime CurrentTime
        {
            set { _CurrentTime = value; }

            get
            {
                return DateTime.Now;
            }
        }

        public DateTime CurrentTimeUTC
        {
            get => DateTime.UtcNow;
        }
        
    }
}
