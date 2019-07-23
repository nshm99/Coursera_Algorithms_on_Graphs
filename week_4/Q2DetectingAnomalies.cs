using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q2DetectingAnomalies : Processor
    {
        public Q2DetectingAnomalies(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);


        public long Solve(long nodeCount, long[][] edges)
        {
            bool[] visited = new bool[nodeCount];
            long[] dist = new long[nodeCount];

            for (int j = 0; j < nodeCount; j++)
            {
                dist[j] = long.MaxValue;
            }
            for(int i =0;i<nodeCount;i++)
            {
                if(!visited[i])
                {
                    if (HaveNegativeCycle(nodeCount, edges, dist, i) == 1)
                        return 1;
                    for(int j=0;j<nodeCount;j++)
                    {
                        if (dist[j] != long.MaxValue)
                            visited[j] = true;
                    }

                }
            }

            return 0;
        }

        private int HaveNegativeCycle(long nodeCount, long[][] edges, long[] dist, int s)
        {
            dist[s] = 0;
            for (int i = 0; i < nodeCount; i++)
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
                            if (i == nodeCount - 1)
                                return 1;
                            dist[(int)des] = dist[(int)src] + w;
                        }
                    }
                }
            }


            return 0;
        }
    }
}
