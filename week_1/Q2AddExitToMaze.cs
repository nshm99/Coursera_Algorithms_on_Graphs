using System;
using System.Collections.Generic;
using TestCommon;

namespace A12
{
    public class Q2AddExitToMaze : Processor
    {
        public Q2AddExitToMaze(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            List<long>[] adjancylist = new List<long>[nodeCount + 1];
            for (int i = 0; i <= nodeCount; i++)
            {
                adjancylist[i] = new List<long>();
            }

            foreach (var g in edges)
            {
                adjancylist[g[0]].Add(g[1]);
                adjancylist[g[1]].Add(g[0]);
            }
            bool[] visited = new bool[nodeCount + 1];
            
            
            return DFS( adjancylist, visited);
            
        }

        private long DFS( List<long>[] adjancylist, bool[] visited)
        {
            for (int i = 0; i < visited.Length; i++)
                visited[i] = false;
            int cc = 0;
            for(int i =0;i<adjancylist.Length ;i++)
            {
                if (!visited[i])
                {
                    CcExplore(i, adjancylist, visited);
                    cc++;
                }
            }
            return cc-1;
        }

        private void CcExplore(long startNode, List<long>[] adjancylist, bool[] visited)
        {
            /* visited[startNode] = true;
             foreach (var a in adjancylist[startNode])
                 if (!visited[a])
                     CcExplore(a, adjancylist, visited);*/
            Stack<long> nodes = new Stack<long>();
            nodes.Push(startNode);
            while (nodes.Count != 0)
            {
                startNode = nodes.Pop();

                if (!visited[startNode])
                    visited[startNode] = true;

                foreach (var v in adjancylist[startNode])
                    if (!visited[v])
                        nodes.Push(v);
            }

        }
    }
}
