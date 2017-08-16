//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Text;

namespace Project.MainClasses
{
    public class ZIndexManager
    {
        #region Private variable

        private Graph graph;

        private SortedDictionary<Node,int> sortedNodes;

        #endregion

        #region Public methods

        public ZIndexManager(Graph graph)
        {
            this.graph = graph;

            sortedNodes = new SortedDictionary<Node,int>(new NodeComparer());
        }

        public void AddNode(Node node)
        {
            if (node.Shape.NodeName == "SimpleConnectorNode")
            {
                node.ZIndex = Statics.SimpleConnectorZIndex;
                return;
            }

            node.ZIndex = getMaxZIndex() + 1;
            sortedNodes.Add(node,1);
        }

        public void BringIntoFront(Node node)
        {
            if (node.Shape.NodeName == "SimpleConnectorNode")
                return;

            int max = getMaxZIndex();
            if (max != node.ZIndex)
            {
                sortedNodes.Remove(node);
                node.ZIndex = max + 1;
                sortedNodes.Add(node, 1);
            }
        }

        public void SendToBack(Node node)
        {
            if (node.Shape.NodeName == "SimpleConnectorNode")
                return;

            int min = getMinZIndex();

            if (min != node.ZIndex)
            {
                sortedNodes.Remove(node);
                node.ZIndex = min - 1;
                sortedNodes.Add(node, 1);
            }
        }

        #endregion

        /// <summary>
        /// Returns maximum ZIndex of nodes
        /// 0 if there is no Node
        /// </summary>
        private int getMaxZIndex()
        {
            int max = 0;

            if (sortedNodes.Count > 0)
            {
                IEnumerator<Node> ienumerator = sortedNodes.Keys.GetEnumerator();
                ienumerator.MoveNext();
                max = ienumerator.Current.ZIndex;
            }

            return max;
        }

        /// <summary>
        /// Returns minimum ZIndex of nodes
        /// 0 if there is no Node
        /// </summary>
        private int getMinZIndex()
        {
            int min = 0;
            if (sortedNodes.Count > 0)
            {
                IEnumerator<Node> ienumerator = sortedNodes.Keys.GetEnumerator();
                while (ienumerator.MoveNext())
                    min = ienumerator.Current.ZIndex;
            }

            return min;
        }
    }

    public class NodeComparer : IComparer<Node>
    {
        public int Compare(Node x, Node y)
        {
            if (x.ZIndex == y.ZIndex)
                return 0;
            else if (x.ZIndex < y.ZIndex)
                return 1;
            else
                return -1;
        }
    }
}
