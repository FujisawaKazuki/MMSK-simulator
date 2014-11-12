using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSKシミュレーター
{
    public static class Dice
    {
        static Random rnd = new Random(0);
        public static double ExpoentialDistribution(double lambda)
        {
            return -Math.Log(rnd.NextDouble()) * 1.0 / lambda;
        }
    }
}
