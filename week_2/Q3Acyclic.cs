using System;
using System.Collections.Generic;
using TestCommon;

namespace A12
{
    public class Q3Acyclic : Processor
    {
        public Q3Acyclic(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            List<long>[] adjancylist = new List<long>[nodeCount + 1];
            bool[] visited = new bool[nodeCount + 1];
            bool[] recursion = new bool[nodeCount + 1];
            for (int i = 0; i <= nodeCount; i++)
            {
                adjancylist[i] = new List<long>();
                visited[i] = false;
                recursion[i] = false;
            }

            foreach (var g in edges)
            {
                adjancylist[g[0]].Add(g[1]);
        
            }

            for(int i=1;i <= nodeCount;i++)
            {
                if (cycle(i,adjancylist,visited,recursion))
                    return 1;
            }
            return 0;
        }

        private bool cycle(long n, List<long>[] adjancylist, bool[] visited ,bool[] recursion)
        {
            if (recursion[n])
                return true;
            if (visited[n])
                return false;
            visited[n] = true;
            recursion[n] = true;
            foreach(var i in adjancylist[n])
            {
                if (cycle(i, adjancylist, visited, recursion))
                    return true;
            }
            recursion[n] = false;
            return false;

        }
        
    }
}