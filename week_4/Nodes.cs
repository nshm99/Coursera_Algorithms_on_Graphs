using System.Collections.Generic;

namespace A3
{
    internal class Nodes
    {

        public long dist;
        public int queuePos
        {
            get;
            set;
        }
        public Nodes(long dist,int pos )
        {
            this.dist = dist;
            queuePos = pos;

        }

    }
}