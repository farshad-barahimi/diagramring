//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace Project.MainClasses
{
    public class EdgeBreak
    {
        #region Private variables
        private readonly Graph graph;
        private readonly Brush normalBrush;
        private readonly Brush hoverBrush;
        #endregion

        #region Properties
        public Ellipse UIEllipse { get; private set; }
        public MyPoint Position { get; private set; }
        public EdgePart BeforeEdgePart { get; set; }
        public EdgePart AfterEdgePart { get; set; }

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
                    UIEllipse.Width = 7;
                    UIEllipse.Height = 7;
                    UIEllipse.Fill = hoverBrush;
                    UIEllipse.Stroke = hoverBrush;
                }
                else
                {
                    UIEllipse.Width = 5;
                    UIEllipse.Height = 5;
                    UIEllipse.Fill = normalBrush;
                    UIEllipse.Stroke = normalBrush;
                }
            }
        }

        #endregion

        #region Public methods

        public EdgeBreak(MyPoint position, EdgePart beforeEdgePart, EdgePart afterEdgePart, Graph graph)
        {
            this.Position = position;
            this.BeforeEdgePart = beforeEdgePart;
            this.AfterEdgePart = afterEdgePart;
            this.graph = graph;

            normalBrush = Brushes.Transparent;
            hoverBrush = Brushes.Red;

            UIEllipse = new Ellipse
                            {
                                Margin = new Thickness(position.X - 3, position.Y - 3, 0, 0),
                                Width = 5,
                                Height = 5,
                                Fill = normalBrush,
                                Stroke = normalBrush,
                                StrokeThickness = 1
                            };

            UIEllipse.MouseEnter += onMouseEnter;
            UIEllipse.MouseLeave += onMouseLeave;
            UIEllipse.MouseDown += onMouseDown;

            IsSelected = false;
        }

        public bool Move(double difx, double dify)
        {
            bool result = true;
            if (Position.X + difx < 0 || Position.Y+dify < 0)
                result=false;

            Position.X += difx;
            Position.Y += dify;
            UIEllipse.Margin = new Thickness(Position.X - 3, Position.Y - 3, 0, 0);
            BeforeEdgePart.UILine.X2 = Position.X;
            BeforeEdgePart.UILine.Y2 = Position.Y;

            AfterEdgePart.UILine.X1 = Position.X;
            AfterEdgePart.UILine.Y1 = Position.Y;

            BeforeEdgePart.UpdateLabels();
            AfterEdgePart.UpdateLabels();

            BeforeEdgePart.Edge.UpdateStructure();

            return result;
        }

        public bool IsRemovable()
        {
            var edge = BeforeEdgePart.Edge;
            if (edge.EdgeBreaks.Count == 1 && edge.HeadConnection == edge.TailConnection)
                    return false;
            return true;
        }

        public void Remove(object sender, RoutedEventArgs e)
        {
            BeforeEdgePart.UILine.X2 = AfterEdgePart.UILine.X2;
            BeforeEdgePart.UILine.Y2 = AfterEdgePart.UILine.Y2;

            foreach (EdgeLabel edgeLabel in BeforeEdgePart.EdgeLabels)
                edgeLabel.Percent *= 0.5f;

            foreach (EdgeLabel edgeLabel in AfterEdgePart.EdgeLabels)
            {
                edgeLabel.Percent *= 0.5f;
                edgeLabel.Percent += 0.5f;
                BeforeEdgePart.EdgeLabels.Add(edgeLabel);
                edgeLabel.EdgePart = BeforeEdgePart;
            }

            graph.UICanvas.Children.Remove(AfterEdgePart.UILine);
            foreach (EdgeBreak edgeBreak in AfterEdgePart.Edge.EdgeBreaks)
            {
                if (edgeBreak.BeforeEdgePart == AfterEdgePart)
                    edgeBreak.BeforeEdgePart = BeforeEdgePart;
            }

            BeforeEdgePart.Edge.EdgeParts.Remove(AfterEdgePart);
            BeforeEdgePart.Edge.EdgeBreaks.Remove(this);
            BeforeEdgePart.Edge.UpdateLabels();
            BeforeEdgePart.Edge.UpdateStructure();
            
            graph.UICanvas.Children.Remove(UIEllipse);

            graph.SelectionManager.ClearSelection();
        }

        #endregion

        #region Private methods

        private void onMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graph.OnEdgeBreakMouseDown(this, e);
        }

        private void onMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!IsSelected)
            {
                UIEllipse.Width = 5;
                UIEllipse.Height = 5;
                UIEllipse.Fill = normalBrush;
                UIEllipse.Stroke = normalBrush;
            }
        }

        private void onMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            UIEllipse.Width = 7;
            UIEllipse.Height = 7;
            UIEllipse.Fill = hoverBrush;
            UIEllipse.Stroke = hoverBrush;
        }

        #endregion
    }
}
