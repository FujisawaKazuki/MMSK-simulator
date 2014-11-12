using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MMSKシミュレーター
{
    class Program
    {
        static void Main(string[] args)
        {
            var lambda_list = new List<double>();
            List<Tuple<double,double>> result;
            result = new List<Tuple<double, double>>();
            for (double i = 0.1; i < 1.0; i += 0.1)
                lambda_list.Add(i);


            //呼損率を求める
                for (int seed = 0; seed < 100; seed++)
               {
                   Console.WriteLine("{0},{1}","seed",seed);
                   foreach (var lambda in lambda_list)
                   {
                    MMSKSimulation a = new MMSKSimulation(lambda, 1, 2, seed, 1000);//到着率,行列長,サーバ数,seed,やって来る客数
                    a.run();
                    //Console.WriteLine("{0},{1}", lambda, a.loss_probability());
                    //Console.WriteLine();
                    result.Add(new Tuple<double, double>(lambda, a.loss_probability()));
                    System.IO.StreamWriter sw = new System.IO.StreamWriter("result.csv");
                    result.Sort();
                    result.ForEach(j => sw.WriteLine("{0},{1}", 
                        j.Item1,
                        j.Item2));
                    sw.Close();
                    }
                }
                //System.Console.ReadKey();
        }
    }
}
