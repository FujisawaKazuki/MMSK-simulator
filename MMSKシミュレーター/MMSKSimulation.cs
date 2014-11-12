using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSKシミュレーター
{
    public class MMSKSimulation
    {
	 int numServer;//サーバー数
     int K;//待ち行列の長さ
	 double numCustomers;
     double mew = 1.0;//退出レート
     double queue_length;
     double t;
     public static int num_succ;//成功数
     public static int num_fail;//棄却数
     static Random rnd;
     List<MyEvent> elist;
     public static List<int> AvailableServer;//利用可能なサーバリスト 

     public MMSKSimulation(double a,int tmpK,int tmpnumServer,int seed,int n)
     {
         numCustomers = n;
         numServer = tmpnumServer;
         K = tmpK;
         if (a < 0.0 || a > 1.0) throw new System.Exception("負荷は1.0未満で");
         rnd = new Random(seed);
         t = 0.0;
           elist = new List<MyEvent>();
           AvailableServer = new List<int>();

           for (int i = 0; i < tmpnumServer; i++)
           {
               AvailableServer.Add(i);
           }

               //シミュレーション開始前に到着イベントのリスト作成
               for (int i = 0; i < numCustomers; i++)
               {
                   t += Dice.ExpoentialDistribution(a);
                   this.elist.Add(new MyEvent(t, 0, i, "ARRIVE"));//time,where,who,action
               }
       }
       
        //処理
        public void run(){
            queue_length = 0.0;
            Global.simtime = 0.0;
            Global.prevsimtime = 0.0;
            num_succ = 0;
            num_fail = 0;
            double customer_wait = 0;//並んでる客数
                while (true)
                {
                    MyEvent current = Global.Pop(this.elist);//発生時間が一番短いイベント
                    if (current == null) break;
                    Global.prevsimtime = Global.simtime; 
                    Global.simtime = current.time;//シミュレーション処理を進める
                    //queue長の処理（経過時間分queue長を減らす）
                    queue_length -= Global.simtime - Global.prevsimtime;
                    
                    if(queue_length < 0.0) queue_length = 0.0;
                    //Console.WriteLine("{0},{1},{2},{3}", current.time,current.who,current.where,current.action);
                    if (current.action.Equals("ARRIVE"))
                    {
                        //イベントARRIVEの処理
                        //processing(current);
                        if (AvailableServer.Count > 0){
                            num_succ++;
                            //Console.WriteLine("{0},{1}", "succ", num_succ);
                            double xtime = 0.0;
                            xtime = Dice.ExpoentialDistribution(1.0 / mew);//サービス時間
                            double wtime = queue_length + xtime;//到着してから出ていくまでの時間
                            int serverid = AvailableServer[0];
                            this.elist.Add(new MyEvent(Global.simtime + wtime, serverid, current.who, "DEPARTURE"));//離脱イベントリスト作成
                            AvailableServer.RemoveAt(0);
                            queue_length += xtime;
                            customer_wait++;
                        }
                        else if (customer_wait < this.K)
                        {
                            /*num_succ++;
                            double xtime = 0.0;
                            xtime = Dice.ExpoentialDistribution(1.0 / mew);//サービス時間
                            //ServerUtilization[serverid] += xtime;//使い道？
                            double wtime = queue_length + xtime;//到着してから出ていくまでの時間
                            int serverid = AvailableServer[0];
                            AvailableServer.RemoveAt(0);
                            this.elist.Add(new MyEvent(Global.simtime + wtime, serverid, current.who, "DEPARTURE"));//離脱イベントリスト作成
                            queue_length += xtime;
                           customer_wait++;*/
                        }
                        else{
                            num_fail++;
                            //Console.WriteLine("{0},{1}","fail",num_fail);
                        }
                       
                    }
                    else if (current.action.Equals("DEPARTURE"))
                    {
                        int thiserverid = current.where;//どのサーバーでサービスが終了したか
                        AvailableServer.Add(thiserverid);//空いているサーバーリストのおしりに足す
                        customer_wait--;
                    }
                }
           }

        public double loss_probability(){
            return (double)num_fail/(num_succ+num_fail);
        }
    }
}
