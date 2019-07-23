using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q3ExchangingMoney:Processor
    {
        public Q3ExchangingMoney(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, string[]>)Solve);


        public string[] Solve(long nodeCount, long[][] edges,long startNode)
        {
            long[] dist = new long[nodeCount];
            long[] prev = new long[nodeCount];
                for (int j = 0; j < nodeCount; j++)
            {
                dist[j] = long.MaxValue;
                prev[j] = -1;
            }
            Queue<long> v ;
            int haveCycle = HaveNegativeCycle(nodeCount, edges, dist, prev, (int)startNode, out v);
            string[] result = new string[nodeCount];
            for(int i=0;i<nodeCount;i++)
            {
                if (dist[i] == long.MaxValue)
                    result[i] = "*";
                else
                    result[i] = dist[i].ToString();
            }
            //long x = v.Last();
            //long startNodeCycle = x;
            if (haveCycle == 1)
            {
                while(v.Count != 0)
                {
                    long x = v.Dequeue();
                    result[x] = "-";
                    //for (int i = 0; i < nodeCount; i++)
                      //  if (prev[i] == x)
                        //    result[i] = "-";
                }
                /*long x;
                long start = v.Dequeue();
                x = start;
                int c= 0;
                do
                {
                    result[x] = "-";
                   // for (int i = 0; i < prev.Length; i++)
                     //   if (prev[i] == x)
                       //     result[i] = "-";
                    x = prev[x];
                } while (x != start);*/
            }
            return result;
        }
        private int HaveNegativeCycle(long nodeCount, long[][] edges, long[] dist,long[] prev, int s,out Queue<long> v)
        {
            dist[s-1] = 0;
            v = new Queue<long>();
            int ret = 0;
            for (int i = 0; i <= nodeCount+5; i++)
            {
                for (int j = 0; j < edges.Length; j++)
                {
                    long des = edges[j][1] - 1;
                    long src = edges[j][0] - 1;
                    long w = edges[j][2];
                    if (dist[(int)src] != long.MaxValue)
                    {
                        if (dist[(int)des] > dist[(int)src] + w)
                        {
                            if (i >= nodeCount - 1)
                            {
                                v.Enqueue(des);
                                //return 1;
                               ret = 1;
                            }
                            dist[(int)des] = dist[(int)src] + w;
                            prev[des] = src;
                        }
                    }
                }
            }
            
            return ret;
        }
    }
}
