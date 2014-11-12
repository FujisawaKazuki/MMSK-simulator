using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSKシミュレーター
{
    public class MyEvent
    {
        public double time;
        public int where;
        public int who;
        public string action;
        public MyEvent(double a, int b, int c, string d)
        {
            time = a;
            where = b;
            who = c;
            action = d;
        }
    }
}
