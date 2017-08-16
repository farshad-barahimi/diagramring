//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Project.MainClasses
{
    public class EdgeLabel
    {
        #region Properties

        public EdgePart EdgePart { get; set; }
        public TextBlock UITextBlock { get; set; }
        public double Percent { get; set; }
        public double Distance { get; set; }

        #endregion

        #region Public methods

        public EdgeLabel(string text, EdgePart edgepart)
        {
            this.EdgePart = edgepart;

            UITextBlock = new TextBlock();
            UITextBlock.Text = text;

            Percent = 0.1;
            Distance = 3;

            Graph graph=edgepart.Edge.HeadConnection.Node.Graph;
            graph.UICanvas.Children.Add(UITextBlock);

            Update();
        }

        public void Update()
        {
            Line line=EdgePart.UILine;

            MyPoint p = Statics.CalculateLabelPosition(line.X1, line.Y1, line.X2, line.Y2, Percent, Distance);

            if (Distance >= 0)
            {
                if (line.Y2 - line.Y1 > 0)
                    p.X -= UITextBlock.ActualWidth;
                if (line.X2 - line.X1 < 0)
                    p.Y -= UITextBlock.ActualHeight;
            }
            else
            {
                if (line.Y2 - line.Y1 < 0)
                    p.X -= UITextBlock.ActualWidth;
                if (line.X2 - line.X1 > 0)
                    p.Y -= UITextBlock.ActualHeight;
            }

            UITextBlock.Margin = new Thickness(p.X, p.Y, 0, 0);
        }

        public void Remove()
        {
            Graph graph = EdgePart.Edge.HeadConnection.Node.Graph;
            graph.UICanvas.Children.Remove(UITextBlock);
            EdgePart.EdgeLabels.Remove(this);
        }

        #endregion
    }
}
