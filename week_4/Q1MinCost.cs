using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q1MinCost : Processor
    {
        public Q1MinCost(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);


        public long Solve(long nodeCount, long[][] edges, long startNode, long endNode)
        {

            List<adjancy>[] adjList = new List<adjancy>[nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                adjList[i] = new List<adjancy>();
            }
            for (int i = 0; i < edges.Length; i++)
            {
                adjList[edges[i][0] - 1].Add(new adjancy(edges[i][1] - 1, edges[i][2]));

            }
            long[] dist = new long[nodeCount];
            long[] prev = new long[nodeCount];
            long[] queue = new long[nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                dist[i] = long.MaxValue;
                queue[i] = long.MaxValue;
            }
            dist[startNode - 1] = 0;
            queue[startNode - 1] = 0;

            while (checlEmpty(queue))
            {
                long minIndex = ExtractMin(queue);
                if (minIndex != -1)
                    queue[minIndex] = -1;
                foreach (var adj in adjList[minIndex])
                {
                    if (dist[adj.node] > dist[minIndex] + adj.value 
                        && dist[minIndex] <long.MaxValue)
                    {
                        dist[adj.node] = dist[minIndex] + adj.value;
                        prev[adj.node] = minIndex;
                        queue[adj.node] = dist[adj.node];
                    }
                }
            }
            if (dist[endNode - 1] == long.MaxValue)
                return -1;
            return dist[endNode - 1];
        }

        private long ExtractMin(long[] queue)
        {
            long min = long.MaxValue;
            long index = -1;
            for (int i = 0; i < queue.Length; i++)
            {
                if (queue[i] != -1)
                    if (queue[i] <= min)
                    {
                        min = queue[i];
                        index = i;
                    }
            }
            return index;
        }

        private bool checlEmpty(long[] queue)
        {
            foreach (var item in queue)
            {
                if (item != null)
                    if (item != -1)
                        return true;

            }
            return false;
            return false;
        }
    }
}
