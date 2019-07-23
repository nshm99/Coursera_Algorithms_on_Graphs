using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A2
{
    public class Q1ShortestPath : Processor
    {
        public Q1ShortestPath(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long,long[][], long, long, long>)Solve);
        

        public long Solve(long NodeCount, long[][] edges, long StartNode,  long EndNode)
        {
            List<long>[] adjList = new List<long>[NodeCount];
            for (int i = 0; i < NodeCount; i++)
            {
                adjList[i] = new List<long>();
            }
            for (int i = 0; i < edges.Length; i++)
            {
                adjList[edges[i][0] - 1].Add(edges[i][1] - 1);
                adjList[edges[i][1] - 1].Add(edges[i][0] - 1);
            }
            bool[] visited = new bool[NodeCount];
            
            long a  = BFS(adjList, visited, StartNode, EndNode);
            return a;

        }

        private long BFS(List<long>[] adjancylist, bool[] visited, long startNode, long endNode)
        {
            
            Queue<long> nodes = new Queue<long>();
            nodes.Enqueue(startNode-1);
            long[] prevDistance = new long[visited.Length];
            prevDistance[startNode - 1] = 0;
            visited[startNode - 1] = true;
            while(nodes.Count != 0)
            {
                long item = nodes.Dequeue();
                foreach (var adj in adjancylist[item])
                {
                    if (!visited[adj])
                    {
                        nodes.Enqueue(adj);
                        prevDistance[adj] = prevDistance[item] + 1;
                        visited[adj] = true;
                        
                    }

                    if (adj == endNode - 1)
                    {
                        return prevDistance[adj];
                    }
                    
                }
                
            }
            return -1;
        }
    }
}
