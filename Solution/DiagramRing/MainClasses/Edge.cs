//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Collections.ObjectModel;

namespace Project.MainClasses
{
    public class Edge
    {
        #region Properties
        
        public Graph Graph { get; private set; }
        public NodeConnection HeadConnection { get; set; }
        public NodeConnection TailConnection { get; set; }


        public EndSymbol HeadSymbol { get; set; }
        public EndSymbol TailSymbol { get; set; }

        public DirectionType DirectionType
        {
            get
            {
                if (HeadSymbol == EndSymbol.None && TailSymbol == EndSymbol.None)
                    return DirectionType.Undirected;
                else if (HeadSymbol != EndSymbol.None && TailSymbol != EndSymbol.None)
                    return DirectionType.Biderected;
                else
                    return DirectionType.Directed;
            }
        }

        public ObservableCollection<EdgePart> EdgeParts { get; private set; }
        public ObservableCollection<EdgeBreak> EdgeBreaks { get; private set; }

        public Canvas HeadSymbolCanvas { get; private set; }
        public Canvas TailSymbolCanvas { get; private set; }

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
                    foreach (EdgePart edgePart in EdgeParts)
                    {
                        if (!edgePart.IsSelected)
                        {
                            edgePart.UILine.Stroke = Brushes.Red;
                            edgePart.UILine.StrokeThickness = 3;
                        }
                    }
                }
                else
                {
                    foreach (EdgePart edgePart in EdgeParts)
                    {
                        if (!edgePart.IsSelected)
                        {
                            edgePart.UILine.Stroke = Brushes.Black;
                            edgePart.UILine.StrokeThickness = 1;
                        }
                    }
                }
            }
        }

        private EdgeBrushType edgeBrushType;
        public EdgeBrushType EdgeBrushType
        {
          get { return edgeBrushType; }
          set 
          { 
              edgeBrushType = value;

              foreach (EdgePart edgePart in EdgeParts)
                  edgePart.UpdateLineBrush();
          }
        }

        #endregion

        #region Public methods

        public Edge(Graph graph)
        {
            this.Graph = graph;

            EdgeParts = new ObservableCollection<EdgePart>();
            EdgeBreaks = new ObservableCollection<EdgeBreak>();

            IsSelected = false;
            HeadSymbolCanvas = null;
            TailSymbolCanvas = null;

            HeadSymbol = Graph.DefaultHeadSymbol;
            TailSymbol = Graph.DefaultTailSymbol;
            edgeBrushType=Graph.DefaultEdgeBrushType;
        }

        public void Remove(object sender, RoutedEventArgs e)
        {
            Graph.RemoveEdge(this);
        }
        
        public void Properties(object sender, RoutedEventArgs e)
        {
            var form = new EdgePropertiesForm(this);
            form.ShowDialog();
        }

        public void OnMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            foreach (EdgePart edgePart in EdgeParts)
            {
                edgePart.UILine.Stroke = Brushes.Red;
                edgePart.UILine.StrokeThickness = 3;
            }
        }

        public void OnMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            foreach (EdgePart edgePart in EdgeParts)
            {
                if (!IsSelected)
                {
                    edgePart.UILine.Stroke = Brushes.Black;
                    edgePart.UILine.StrokeThickness = 1;
                }
                else if (!edgePart.IsSelected)
                {
                    edgePart.UILine.Stroke = Brushes.Red;
                    edgePart.UILine.StrokeThickness = 3;
                }
                else
                {
                    edgePart.UILine.Stroke = Brushes.Blue;
                    edgePart.UILine.StrokeThickness = 3;
                }
            }
        }

        public void UpdateEvents()
        {
            foreach (EdgePart edgePart in EdgeParts)
                edgePart.UpdateEvents();
        }

        public void UpdateLabels()
        {
            foreach (EdgePart edgePart in EdgeParts)
                edgePart.UpdateLabels();
        }

        public void UpdateStructure()
        {
            int lastIndex = EdgeParts.Count - 1;

            if (HeadSymbolCanvas == null)
            {
                HeadSymbolCanvas = new Canvas();
                Graph.UICanvas.Children.Add(HeadSymbolCanvas);
                Canvas.SetZIndex(HeadSymbolCanvas, Statics.EndSymbolZIndex);
            }
            else
                HeadSymbolCanvas.Children.Clear();

            if (TailSymbolCanvas == null)
            {
                TailSymbolCanvas = new Canvas();
                Graph.UICanvas.Children.Add(TailSymbolCanvas);
                Canvas.SetZIndex(TailSymbolCanvas, Statics.EndSymbolZIndex);
            }
            else
                TailSymbolCanvas.Children.Clear();

            if (HeadSymbol != EndSymbol.None)
            {
                Line line = EdgeParts[0].UILine;

                Point startPoint = new Point(line.X1, line.Y1);
                Point linePoint = new Point(line.X2, line.Y2);
                DrawEndSymbol(HeadSymbolCanvas, HeadSymbol, startPoint, linePoint);
            }

            if (TailSymbol != EndSymbol.None)
            {
                Line line = EdgeParts[lastIndex].UILine;

                Point startPoint = new Point(line.X2, line.Y2);
                Point linePoint = new Point(line.X1, line.Y1);
                DrawEndSymbol(TailSymbolCanvas, TailSymbol, startPoint, linePoint);
            }

            EdgeBrushType=edgeBrushType;
        }

        #endregion

        #region Private methods

        private static void DrawEndSymbol(Canvas symbolCanvas, EndSymbol symbol, Point startPoint,Point linePoint)
        {
            Point p1 = new Point(startPoint.X, startPoint.Y);
            Point p2 = new Point(startPoint.X - 5, startPoint.Y - 10);
            Point p3 = new Point(startPoint.X + 5, startPoint.Y - 10);
            Point p4 = new Point(startPoint.X, startPoint.Y - 20);

            Path path = new Path();
            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = p2;
            pathFigure.IsClosed = true;
            pathFigure.IsFilled = true;
            path.Fill = Brushes.Black;

            PathSegment segment;


            segment = new LineSegment(p1, true);
            pathFigure.Segments.Add(segment);

            segment = new LineSegment(p3, true);
            pathFigure.Segments.Add(segment);

            if (symbol == EndSymbol.SimpleArrow)
            {
                pathFigure.IsClosed = false;
                pathFigure.IsFilled = false;
            }
            else if (symbol == EndSymbol.EmptyTriangle)
            {
                path.Fill = Brushes.White;
            }
            else if (symbol == EndSymbol.FilledTriangle)
            {
                path.Fill = Brushes.Black;
            }
            else if (symbol == EndSymbol.EmptyRhombus)
            {
                segment = new LineSegment(p4, true);
                pathFigure.Segments.Add(segment);
                path.Fill = Brushes.White;
            }
            else if (symbol == EndSymbol.FilledRhumbus)
            {
                segment = new LineSegment(p4, true);
                pathFigure.Segments.Add(segment);
                path.Fill = Brushes.Black;
            }

            double degree = -90;

            if (linePoint.Y != startPoint.Y)
            {
                double m;
                m = (startPoint.X - linePoint.X) / (startPoint.Y - linePoint.Y);

                m = -m;
                degree = Math.Atan(m) * 180 / Math.PI;

                if (startPoint.Y - linePoint.Y < 0)
                    degree += 180;
            }
            else if (linePoint.X > startPoint.X)
                degree = 90;

            path.RenderTransform = new RotateTransform(degree, p1.X, p1.Y);

            List<PathFigure> figures = new List<PathFigure>();
            figures.Add(pathFigure);
            path.Data = new PathGeometry(figures, FillRule.Nonzero, Transform.Identity);

            path.Stroke = Brushes.Black;
            path.StrokeThickness = 1;

            symbolCanvas.Children.Add(path);
        }

        #endregion

    }

    public enum DirectionType
    {
        Undirected,
        Directed,
        Biderected
    }

    public enum EndSymbol
    {
        None,
        SimpleArrow,
        EmptyTriangle,
        FilledTriangle,
        EmptyRhombus,
        FilledRhumbus
    }

    public enum EdgeBrushType
    {
        Solid,
        Dashed,
        Dotted
    }

    public delegate void VoidVoidDelegate();
}
