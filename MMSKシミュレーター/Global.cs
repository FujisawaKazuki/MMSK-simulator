using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSKシミュレーター
{
    public class Global
    {
        //シミュレーション時間を保持
        public static double simtime = 0.0;
        public static double prevsimtime = 0.0;
        public static MyEvent Pop(List<MyEvent> list)
        {
            if (list.Count == 0) return null;
            list.Sort((x, y) =>
            {
                if (x.time > y.time) { return 1; }
                else if (x.time < y.time) { return -1; }
                else { return 0; };
            });
            MyEvent ret_value = new MyEvent(list[0].time, list[0].where, list[0].who, list[0].action);
            list.RemoveAt(0);
            return ret_value;
        }
    }
}
