using System;
using System.Collections.Generic;
using TestCommon;

namespace A12
{
    public class Q5StronglyConnected: Processor
    {
        public Q5StronglyConnected(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            List<long>[] adjancylist = new List<long>[nodeCount + 1];
            List<long>[] reversAdjancylist = new List<long>[nodeCount + 1];
            for (int i = 0; i < adjancylist.Length; i++)
            {
                adjancylist[i] = new List<long>();
                reversAdjancylist[i] = new List<long>();
            }

            foreach (var g in edges)
            {
                adjancylist[g[0]].Add(g[1]);
                reversAdjancylist[g[1]].Add(g[0]);
            }
            Stack<long> stack = new Stack<long>();
            int cc = 0;
            bool[] visited = new bool[nodeCount+1];
            for (int i = 0; i < visited.Length; i++)
                visited[i] = false;
            
            for (int i = 1; i < visited.Length; i++)
                if (visited[i] == false)
                    fillOrder(i, visited, stack,adjancylist);
            
            for (int i = 0; i < visited.Length; i++)
                visited[i] = false;
            
            while (stack.Count != 0)
            {
                
                long v = stack.Pop();
                
                if (visited[v] == false)
                {
                    DFS(v, visited,reversAdjancylist,cc);
                    cc++;
                }
            }
            return cc;
        }

        private void DFS(long v, bool[] visited, List<long>[] reversAdjancylist,int cc)
        {
            
            visited[v] = true;
            
            List<long> list = reversAdjancylist[v];
            foreach(var i in list)
            {
                if (!visited[i])
                {
                    DFS(i, visited, reversAdjancylist, cc);
                    
                }

            }
            
        }

        private void fillOrder(long v, bool[] visited, Stack<long> stack, List<long>[] adjancylist)
        {
            
            visited[v] = true;
            
            List<long> list = adjancylist[v];
            foreach(var i in list)
            {
                if (!visited[i])
                    fillOrder(i, visited, stack,adjancylist);

            }
            stack.Push(v);
            
        }
    }
}
