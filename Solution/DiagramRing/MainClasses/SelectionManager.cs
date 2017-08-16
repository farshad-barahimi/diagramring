//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;

using System.Text;
using System.Collections.ObjectModel;

namespace Project.MainClasses
{
    public class SelectionManager
    {
        #region Private variable

        private Graph graph;

        #endregion

        #region Properties

        public ObservableCollection<Node> SelectedNodes { get; private set; }

        public EdgePart SelectedEdgePart { get; private set; }

        public EdgeBreak SelectedEdgeBreak { get; private set; }

        public ResizeConnector SelectedResizeConnector { get; set; }

        #endregion

        public event NoArgDelegate SelectionChanged;

        #region Public methods

        public SelectionManager(Graph graph)
        {
            this.graph = graph;

            SelectedNodes = new ObservableCollection<Node>();
            SelectedEdgePart = null;
            SelectedEdgeBreak = null;
        }

        public void ClearSelection()
        {
            foreach (Node node in graph.Nodes)
            {
                node.IsSelected = false;
                foreach (Edge edge in node.edges)
                {
                    edge.IsSelected = false;
                    foreach (EdgePart edgePart in edge.EdgeParts)
                        edgePart.IsSelected = false;
                    foreach (EdgeBreak edgeBreak in edge.EdgeBreaks)
                        edgeBreak.IsSelected = false;
                }
            }

            SelectedNodes.Clear();
            SelectedEdgeBreak = null;
            SelectedEdgePart = null;

            SelectionChanged();
        }

        public void AddSelectedNode(Node node)
        {
            node.IsSelected = true;
            SelectedNodes.Add(node);

            if (SelectedEdgePart != null)
                SelectedEdgePart.IsSelected = false;
            if (SelectedEdgeBreak != null)
                SelectedEdgeBreak.IsSelected = false;
            SelectedEdgePart = null;
            SelectedEdgeBreak = null;

            SelectionChanged();
        }

        public void SelectedgeBreak(EdgeBreak edgeBreak)
        {
            ClearSelection();
            edgeBreak.IsSelected = true;
            SelectedEdgeBreak = edgeBreak;

            SelectionChanged();
        }

        public void SelectedgePart(EdgePart edgePart)
        {
            ClearSelection();
            edgePart.IsSelected = true;
            edgePart.Edge.IsSelected = true;
            SelectedEdgePart = edgePart;
            SelectionChanged();
        }

        public bool IsMultipleSelected()
        {
            bool isMultiple = false;

            if (SelectedNodes.Count > 1)
                isMultiple = true;
            if (SelectedNodes.Count == 1 && SelectedEdgePart != null)
                isMultiple = true;
            if (SelectedNodes.Count == 1 && SelectedEdgeBreak != null)
                isMultiple = true;
            if (SelectedEdgeBreak != null && SelectedEdgePart != null)
                isMultiple = true;

            return isMultiple;
        }

        public bool IsOnlyNodesSelected()
        {
            if (SelectedNodes.Count < 1)
                return false;
            if (SelectedEdgePart != null)
                return false;
            if (SelectedEdgeBreak != null)
                return false;

            return true;
        }

        #endregion
    }

    public delegate void NoArgDelegate();

}
