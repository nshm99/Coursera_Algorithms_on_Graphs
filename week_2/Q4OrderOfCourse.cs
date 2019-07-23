﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestCommon;

namespace A12
{
    public class Q4OrderOfCourse : Processor
    {
        public Q4OrderOfCourse(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long[]>)Solve);

        public long[] Solve(long nodeCount, long[][] edges)
        {
            List<long>[] adjancylist = new List<long>[nodeCount + 1];
            bool[] visited = new bool[nodeCount + 1];
            for (int i = 0; i <= nodeCount; i++)
            {
                adjancylist[i] = new List<long>();
            }

            foreach (var g in edges)
            {
                adjancylist[g[0]].Add(g[1]);

            }
            Stack<long> output = new Stack<long>();
            for (int i = 1; i <= nodeCount; i++)
                if(!visited[i])
                {
                    Order(i, adjancylist, visited,output);
                }
                    

            return output.ToArray();

        }

        private void Order(long i, List<long>[] adjancylist, bool[] visited, Stack<long> output)
        {
            visited[i] = true;
            foreach (var v in adjancylist[i])
                if (!visited[v])
                    Order(v, adjancylist, visited, output);
            output.Push(i);
        }

        



        public override Action<string, string> Verifier { get; set; } = TopSortVerifier;

        /// <summary>
        /// کد شما با متد زیر راست آزمایی میشود
        /// این کد نباید تغییر کند
        /// داده آزمایشی فقط یک جواب درست است
        /// تنها جواب درست نیست
        /// </summary>
        public static void TopSortVerifier(string inFileName, string strResult)
        {
            long[] topOrder = strResult.Split(TestTools.IgnoreChars)
                .Select(x => long.Parse(x)).ToArray();

            long count;
            long[][] edges;
            TestTools.ParseGraph(File.ReadAllText(inFileName), out count, out edges);

            // Build an array for looking up the position of each node in topological order
            // for example if topological order is 2 3 4 1, topOrderPositions[2] = 0, 
            // because 2 is first in topological order.
            long[] topOrderPositions = new long[count];
            for (int i = 0; i < topOrder.Length; i++)
                topOrderPositions[topOrder[i] - 1] = i;
            // Top Order nodes is 1 based (not zero based).

            // Make sure all direct depedencies (edges) of the graph are met:
            //   For all directed edges u -> v, u appears before v in the list
            foreach (var edge in edges)
                if (topOrderPositions[edge[0] - 1] >= topOrderPositions[edge[1] - 1])
                    throw new InvalidDataException(
                        $"{Path.GetFileName(inFileName)}: " +
                        $"Edge dependency violoation: {edge[0]}->{edge[1]}");

        }
    }
}
