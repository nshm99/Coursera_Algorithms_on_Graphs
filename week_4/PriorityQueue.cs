using System;

namespace A3
{
    internal class PriorityQueue
    {
        public void makeQueue(Nodes[] dist, int[] forwpriorityQ, int source, int target)
        {
            swap(dist, forwpriorityQ, 0, source);
        }


        public long extractMin(Nodes[] dist, int[] priorityQ, int extractNum)
        {
            int vertex = priorityQ[0];
            int size = priorityQ.Length - 1 - extractNum;
            swap(dist, priorityQ, 0, size);
            SiftDown(0, dist, priorityQ, size);
            return vertex;
        }

        public void SiftDown(int index, Nodes[] dist, int[] priorityQ, int size)
        {
            int min = index;
            if (2 * index + 1 < size && dist[priorityQ[index]].dist > dist[priorityQ[2 * index + 1]].dist)
            {
                min = 2 * index + 1;
            }
            if (2 * index + 2 < size && dist[priorityQ[min]].dist > dist[priorityQ[2 * index + 2]].dist)
            {
                min = 2 * index + 2;
            }
            if (min != index)
            {
                swap(dist, priorityQ, min, index);
                SiftDown(min, dist, priorityQ, size);
            }
        }

        public void swap(Nodes[] dist, int[] priorityQ, int index1, int index2)
        {
            int temp = priorityQ[index1];

            priorityQ[index1] = priorityQ[index2];
            dist[priorityQ[index2]].queuePos = index1;

            priorityQ[index2] = temp;
            dist[temp].queuePos = index2;

        }

        public void changePriority(Nodes[] dist, int[] priorityQ, int index)
        {
            if ((index - 1) / 2 > -1
                && dist[priorityQ[index]].dist < dist[priorityQ[(index - 1) / 2]].dist)
            {
                swap(dist, priorityQ, index, (index - 1) / 2);
                changePriority(dist, priorityQ, (index - 1) / 2);
            }
        }
    }
}