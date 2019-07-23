using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A2
{
    public class Q2BipartiteGraph : Processor
    {
        public Q2BipartiteGraph(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);


        public long Solve(long NodeCount, long[][] edges)
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

            bool[] recersionStack = new bool[NodeCount];

            return ColoringGraph(adjList, NodeCount);

            
        }

        private long ColoringGraph(List<long>[] adjList, long nodeCount)
        {
            long[] prevColor = new long[nodeCount];
            bool[] visited = new bool[nodeCount];
            for(int i =0;i<nodeCount;i++)
            {
                prevColor[i] = 0;
            }
            Queue<long> nodes = new Queue<long>();
            nodes.Enqueue(0);
            visited[0] = true;
            long[] color = new long[nodeCount];
            color[0] = 1;//1 for blue and -1 for red
            prevColor[0] = -1;
            
            while (nodes.Count != 0)
            {
                long item = nodes.Dequeue();
                foreach(var adj in adjList[item])
                {
                    if(visited[adj] && prevColor[adj] != 0)
                    {
                        if (color[item] == color[adj])
                            return 0;
                    }
                    if(!visited[adj])
                    {
                        nodes.Enqueue(adj);
                        visited[adj] = true;
                        prevColor[adj] = color[item];
                        color[adj] = -1 * prevColor[adj];
                        
                    }
                }
                
            }
            return 1;
        }


    }

}
