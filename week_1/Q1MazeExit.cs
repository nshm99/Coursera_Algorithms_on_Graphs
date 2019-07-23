using System;
using System.Collections.Generic;
using TestCommon;

namespace A12
{
    public class Q1MazeExit : Processor
    {
        public Q1MazeExit(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

        public long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
            List<long>[] adjancylist = new List<long>[nodeCount+1];
            /*foreach(var g in edges)
            {
                g = new long[2];
                adjancylist[g[0]].Add(g[1]);
                adjancylist[g[1]].Add(g[0]);
            }*/
            for(int i =0;i<= nodeCount;i++)
            {
                adjancylist[i] = new List<long>();
            }

            foreach (var g in edges)
            {
                adjancylist[g[0]].Add(g[1]);
                adjancylist[g[1]].Add(g[0]);
            }
            bool[] visited = new bool[nodeCount+1];
            for (int i = 0; i < visited.Length; i++)
                visited[i] = false;

            explore(StartNode,adjancylist,visited);

            if (visited[EndNode])
                return 1;
            return 0;
        }

        private void explore(long startNode, List<long>[] adjancylist, bool[] visited)
        {
            visited[startNode] = true;
            foreach (var a in adjancylist[startNode])
                if (!visited[a])
                    explore(a, adjancylist, visited);
        }
    }
}
