//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Project.MainClasses
{
    public class EdgePart
    {
        #region Private variable
        
        private readonly Brush normalBrush;
        private readonly Brush hoverBrush;

        #endregion

        #region Properties
        
        public Line UILine { get; private set; }
        public ObservableCollection<EdgeLabel> EdgeLabels { get; private set; }
        public Edge Edge { get; private set; }

        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                if (isSelected)
                {
                    UILine.Stroke = hoverBrush;
                    UILine.StrokeThickness = 3;

                    if (Edge.EdgeBrushType != EdgeBrushType.Solid)
                        UILine.StrokeDashArray.Clear();
                }
                else
                {
                    UILine.Stroke = normalBrush;
                    UILine.StrokeThickness = 1;
                    UpdateLineBrush();
                }
            }
        }

        #endregion

        #region Public methods

        public EdgePart(Edge edge , Line line)
        {
            this.Edge = edge;
            this.UILine = line;
            normalBrush = Brushes.Black;
            hoverBrush = Brushes.Blue;
            IsSelected = false;

            EdgeLabels = new ObservableCollection<EdgeLabel>();
            UpdateLineBrush();
        }

        public void Break(object sender, RoutedEventArgs e)
        {
            MyPoint p = Statics.CalculateLabelPosition(UILine.X1, UILine.Y1, UILine.X2, UILine.Y2, 0.5f, 20);
            Edge.Graph.BreakEdgePartAtPoint(this,p.X, p.Y);
        }

        public void Properties(object sender, RoutedEventArgs e)
        {
            var form = new EdgePartProperties(this);
            form.ShowDialog();
        }

        public void UpdateEvents()
        {
            UILine.MouseEnter += Edge.OnMouseEnter;
            UILine.MouseLeave += Edge.OnMouseLeave;

            UILine.MouseEnter += onMouseEnter;
            UILine.MouseLeave += onMouseLeave;
            UILine.MouseDown += onMouseDown;
        }

        public void UpdateLabels()
        {
            foreach (EdgeLabel edgeLabel in EdgeLabels)
                edgeLabel.Update();
        }

        public void UpdateLineBrush()
        {
            UILine.StrokeDashArray.Clear();
            if(Edge.EdgeBrushType == EdgeBrushType.Dashed && !IsSelected)
            {
                UILine.StrokeDashArray.Add(8);
                UILine.StrokeDashArray.Add(8);
            }
            else if (Edge.EdgeBrushType == EdgeBrushType.Dotted && !IsSelected)
            {
                UILine.StrokeDashArray.Add(1);
                UILine.StrokeDashArray.Add(2);
            }
        }

        #endregion

        #region Private methods

        private void onMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!IsSelected)
            {
                UILine.Stroke = hoverBrush;
                UILine.StrokeThickness = 3;

                if (Edge.EdgeBrushType != EdgeBrushType.Solid)
                    UILine.StrokeDashArray.Clear();
            }
        }

        private void onMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!IsSelected)
                UpdateLineBrush();

            if (!IsSelected && !Edge.IsSelected)
            {
                UILine.Stroke = normalBrush;
                UILine.StrokeThickness = 1;
            }
        }

        private void onMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Edge.Graph.OnEdgePartMouseDown(this, e);
        }

        #endregion
    }
}
