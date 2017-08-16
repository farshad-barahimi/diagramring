//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;

using System.Text;
using System.Collections;
using System.Windows.Controls;
using Project.MainClasses;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

namespace Project.Shapes
{
    public class MyShape
    {
        #region Properties
        public ObservableCollection<ShapeElement> Childs { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int DefaultWidth { get; set; }
        public int DefaultHeight { get; set; }
        public int LeftSideWidth { get; set; }
        public int LeftSideHeight { get; set; }
        public ObservableCollection<MyPoint> ConnectionPoints { get; private set; }
        public string Name { get; private set; }
        public string NodeName { get; set; }

        #endregion

        #region Public methods

        public MyShape(string name,int width,int height)
        {
            Childs = new ObservableCollection<ShapeElement>();
            ConnectionPoints = new ObservableCollection<MyPoint>();
            this.Width = width;
            this.Height = height;
            this.Name = name;
            DefaultWidth = width;
            DefaultHeight = height;

            LeftSideWidth = 61;
            LeftSideHeight = 35;
            NodeName = "NormalNode";
        }

        public void Draw(int drawWidth, int drawHeight, MyPoint position, Brush background, MouseButtonEventHandler onMouseDown, Canvas canvas)
        {
            normalDraw(drawWidth,drawHeight,position,background,onMouseDown,canvas);
        }

        public void DrawNode(Node node)
        {
            if (NodeName == "NormalNode")
                normalDraw(node.Width, node.Height, node.Position, node.Background, node.OnMouseDown, node.UICanvas);
            else if (NodeName == "PieChartNode")
                drawPieChart(node);
            else if (NodeName == "BarChartNode")
                drawBarChart(node);
            else if (NodeName == "HorizontalLineNode")
                drawHorizontalLine(node);
            else if (NodeName == "VerticalLineNode")
                drawVerticalLine(node);
            else if (NodeName == "SimpleConnectorNode")
                drawSimpleConnector(node);
            else if (NodeName == "TextNode")
                drawText(node);
            else if (NodeName == "UMLClassNode")
                drawUMLClass(node);
            else if (NodeName == "UMLActorNode")
                drawUMLActor(node);
            else if (NodeName == "UMLPackageNode")
                drawUMLPackage(node);
        }

        private void normalDraw(int drawWidth, int drawHeight, MyPoint position, Brush background, MouseButtonEventHandler onMouseDown, Canvas canvas)
        {
            double scaleX = drawWidth / (double)Width;
            double scaleY = drawHeight / (double)Height;

            foreach (ShapeElement element in Childs)
            {
                if (element is EllipseElement)
                {
                    Ellipse ellipse = new Ellipse();
                    EllipseElement ellipseElement = (EllipseElement)element;

                    ellipse.Width = ellipseElement.Radius1 * 2*scaleX;
                    ellipse.Height = ellipseElement.Radius2 * 2*scaleY;

                    double x = ellipseElement.Center.X - ellipseElement.Radius1;
                    double y = ellipseElement.Center.Y - ellipseElement.Radius2;

                    x *= scaleX;
                    y *= scaleY;

                    ellipse.Margin = new Thickness(x, y, 0, 0);


                    if (ellipseElement.UseNodeBackgroud == true)
                        ellipse.Fill = background;
                    else
                        ellipse.Fill = ellipseElement.FillBrush;

                    ellipse.Stroke = ellipseElement.LineBrush;
                    ellipse.StrokeThickness = ellipseElement.LineBrushThickness;

                    canvas.Children.Add(ellipse);
                    ellipse.MouseDown += onMouseDown;
                }
                else if (element is PathElement)
                {
                    PathElement pathElement = (PathElement)element;

                    Path path = new Path();
                    PathFigure pathFigure = new PathFigure();
                    pathFigure.StartPoint = pathElement.StartPoint.ToPoint(scaleX,scaleY);
                    pathFigure.IsClosed = pathElement.IsClosed;
                    pathFigure.IsFilled = true;

                    foreach (CommandElement command in pathElement.Commands)
                    {
                        if (command.Type == CommandType.Line)
                        {
                            PathSegment segment = new LineSegment(command.EndPoint.ToPoint(scaleX,scaleY), true);
                            pathFigure.Segments.Add(segment);
                        }
                        else if (command.Type == CommandType.SimpleCurve)
                        {
                            PathSegment segment = new QuadraticBezierSegment(command.ControlPoint.ToPoint(scaleX,scaleY), command.EndPoint.ToPoint(scaleX,scaleY), true);
                            pathFigure.Segments.Add(segment);
                        }
                    }

                    List<PathFigure> figures = new List<PathFigure>();
                    figures.Add(pathFigure);
                    path.Data = new PathGeometry(figures, FillRule.Nonzero, Transform.Identity);

                    path.Stroke = pathElement.LineBrush;
                    path.StrokeThickness = 1;

                    if (pathElement.UseNodeBackgroud)
                        path.Fill = background;
                    else
                        path.Fill = pathElement.FillBrush;

                    canvas.Children.Add(path);
                    path.MouseDown += onMouseDown;
                }
            }
        }

        #endregion

        #region Special Nodes


        private void drawPieChart(Node node)
        {
            int i;
            double sum = 0;
            List<Pair> data = new List<Pair>();
            for (i = 0; i < node.Properties.Count; i += 2)
            {
                Pair pair = new Pair();
                pair.Name = node.Properties[i];
                pair.Value = double.Parse(node.Properties[i + 1]);
                data.Add(pair);
                sum += pair.Value;
            }

            

            Rectangle rectangle = new Rectangle();
            rectangle.Width = node.Width;
            rectangle.Height = node.Height;
            rectangle.Fill = Brushes.White;
            rectangle.Stroke = Brushes.Black;
            rectangle.StrokeThickness = 1;
            rectangle.MouseDown += node.OnMouseDown;
            node.UICanvas.Children.Add(rectangle);

            double diameter = node.Height-10;
            double radius = diameter / 2;
            double centerX = radius+5;
            double centerY = radius+5;
            double startAngle = 0;

            if (data.Count == 0)
            {
                Pair pair = new Pair();
                pair.Name = "No Data";
                pair.Value = 100;
                data.Add(pair);
                sum += pair.Value;
            }
            
            WrapPanel wrapPanel = new WrapPanel();
            wrapPanel.Orientation = Orientation.Vertical;
            node.UICanvas.Children.Add(wrapPanel);
            wrapPanel.Margin = new Thickness(diameter + 10, 0, 0, 0);
            wrapPanel.Width = node.Width - diameter - 10;
            wrapPanel.Height = node.Height;


            List<Color> colors = new List<Color>();
            colors.Add(Colors.Red);
            colors.Add(Colors.Blue);
            colors.Add(Colors.Yellow);
            colors.Add(Colors.Magenta);
            colors.Add(Colors.Green);
            colors.Add(Colors.Gray);

            for (i = 0; i < data.Count; i++)
            {
                double angle = data[i].Value / sum * 360;
                bool isMoreThan180 = false;
                if (angle > 180)
                    isMoreThan180 = true;

                Path path = new Path();
                PathFigure pathFigure = new PathFigure();
                pathFigure.StartPoint = new Point(centerX, centerY);
                pathFigure.IsClosed = true;
                pathFigure.IsFilled = true;

                double startX = radius * Math.Cos(startAngle * Math.PI / 180);
                double startY = radius * Math.Sin(startAngle * Math.PI / 180);
                startX += centerX;
                startY += centerY;

                startAngle -= angle;

                double endX = radius * Math.Cos(startAngle * Math.PI / 180);
                double endY = radius * Math.Sin(startAngle * Math.PI / 180);
                endX += centerX;
                endY += centerY;

                if (data.Count == 1)
                    endY += 0.001;


                PathSegment segment = new LineSegment(new Point(startX, startY), true);
                pathFigure.Segments.Add(segment);

                SweepDirection direction = SweepDirection.Counterclockwise;
                ArcSegment arc = new ArcSegment(new Point(endX, endY), new Size(radius, radius), 0, isMoreThan180, direction, true);
                pathFigure.Segments.Add(arc);

                segment = new LineSegment(new Point(centerX, centerY), true);
                pathFigure.Segments.Add(segment);

                List<PathFigure> figures = new List<PathFigure>();
                figures.Add(pathFigure);
                path.Data = new PathGeometry(figures, FillRule.Nonzero, Transform.Identity);

                path.Stroke = Brushes.White;
                path.StrokeThickness = 2;

                if (angle < 20)
                    path.StrokeThickness = 1;

                Color color = colors[(i % colors.Count)];
                Brush brush = new SolidColorBrush(color);
                path.Fill = brush;
                path.MouseDown += node.OnMouseDown;

                node.UICanvas.Children.Add(path);

                Border border = new Border();
                border.Background = brush;
                border.Width = 15;
                border.Height = 15;
                border.IsHitTestVisible = false;

                double percent = data[i].Value / sum * 100;
                int percentFirstPart = (int)percent;
                int percentSecondPart = (int)((percent - percentFirstPart) * 100);
                string percentString = percentFirstPart.ToString() + "." + percentSecondPart.ToString() + "%";

                Label label = new Label();
                label.Content = data[i].Name + " : " + percentString;
                label.IsHitTestVisible = false;

                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Children.Add(border);
                stackPanel.Children.Add(label);
                wrapPanel.Children.Add(stackPanel);

            }
            node.LabelTextBlock.Visibility = Visibility.Collapsed;
        }

        private void drawBarChart(Node node)
        {
            int i;
            double max = double.MinValue;
            List<Pair> data = new List<Pair>();
            for (i = 0; i < node.Properties.Count; i += 2)
            {
                Pair pair = new Pair();
                pair.Name = node.Properties[i];
                pair.Value = double.Parse(node.Properties[i + 1]);
                data.Add(pair);
                if(pair.Value>max)
                    max = pair.Value;
            }

            Rectangle rectangle = new Rectangle();
            rectangle.Width = node.Width;
            rectangle.Height = node.Height;
            rectangle.Fill = Brushes.White;
            rectangle.Stroke = Brushes.Black;
            rectangle.StrokeThickness = 1;
            rectangle.MouseDown += node.OnMouseDown;
            node.UICanvas.Children.Add(rectangle);

            double verticalSpace = 40;

            Line line = new Line();
            line.X1 = 20;
            line.Y1 = node.Height - verticalSpace;
            line.X2 = line.X1;
            line.Y2 = 20;
            line.StrokeThickness = 2;
            line.Stroke = Brushes.Black;
            line.IsHitTestVisible = false;
            node.UICanvas.Children.Add(line);

            line = new Line();
            line.X1 = 20;
            line.Y1 = node.Height - verticalSpace;
            line.X2 = node.Width-20;
            line.Y2 = line.Y1;
            line.StrokeThickness = 2;
            line.Stroke = Brushes.Black;
            line.IsHitTestVisible = false;
            node.UICanvas.Children.Add(line);

            double chartHeight = node.Height - verticalSpace-40;

            List<Color> colors = new List<Color>();
            colors.Add(Colors.Red);
            colors.Add(Colors.Blue);
            colors.Add(Colors.Yellow);
            colors.Add(Colors.Magenta);
            colors.Add(Colors.Green);
            colors.Add(Colors.Gray);

            double x = 40;
            double y = node.Height - verticalSpace;
            double barWidth = 0; 
            if(data.Count!=0)
                barWidth=(node.Width-40 - ((data.Count + 1) * 20)) / data.Count;

            for (i = 0; i < data.Count && barWidth>=0 ; i++)
            {
                double value = data[i].Value / max * chartHeight;

                Path path = new Path();
                PathFigure pathFigure = new PathFigure();
                pathFigure.StartPoint = new Point(x, y);
                pathFigure.IsClosed = true;
                pathFigure.IsFilled = true;

                PathSegment segment = new LineSegment(new Point(x, y-value),true);
                pathFigure.Segments.Add(segment);

                segment = new LineSegment(new Point(x+barWidth, y-value), true);
                pathFigure.Segments.Add(segment);

                segment = new LineSegment(new Point(x + barWidth, y), true);
                pathFigure.Segments.Add(segment);

                List<PathFigure> figures = new List<PathFigure>();
                figures.Add(pathFigure);
                path.Data = new PathGeometry(figures, FillRule.Nonzero, Transform.Identity);

                path.Stroke = Brushes.Black;
                path.StrokeThickness = 1;

                Color color = colors[(i % colors.Count)];
                Brush brush = new SolidColorBrush(color);
                path.Fill = brush;
                path.IsHitTestVisible = false;

                node.UICanvas.Children.Add(path);

                Label label = new Label();
                label.Width = barWidth;
                label.Content = data[i].Name;
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.Margin = new Thickness(x, y + 5, 0, 0);
                label.IsHitTestVisible = false;
                node.UICanvas.Children.Add(label);

                label = new Label();
                label.Width = barWidth;
                label.Content = data[i].Value;
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.Margin = new Thickness(x, y -value -25 , 0, 0);
                label.IsHitTestVisible =false;
                node.UICanvas.Children.Add(label);

                x += barWidth + 20;
            }
            node.LabelTextBlock.Visibility = Visibility.Collapsed;
        }

        private void drawHorizontalLine(Node node)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Width = node.Width;
            rectangle.Height = node.Height;
            rectangle.Fill = Brushes.White;
            rectangle.MouseDown += node.OnMouseDown;
            node.UICanvas.Children.Add(rectangle);

            Line line = new Line();
            line.X1 = 0;
            line.Y1 = 5;
            line.X2 = node.Width;
            line.Y2 = 5;
            line.Fill = Brushes.Black;
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 2;
            line.IsHitTestVisible = false;
            node.UICanvas.Children.Add(line);

            node.LabelTextBlock.Visibility = Visibility.Collapsed;
        }

        private void drawVerticalLine(Node node)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Width = node.Width;
            rectangle.Height = node.Height;
            rectangle.Fill = Brushes.White;
            rectangle.MouseDown += node.OnMouseDown;
            node.UICanvas.Children.Add(rectangle);

            Line line = new Line();
            line.X1 = 5;
            line.Y1 = 0;
            line.X2 = 5;
            line.Y2 = node.Height;
            line.Fill = Brushes.Black;
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 2;
            line.IsHitTestVisible = false;
            node.UICanvas.Children.Add(line);

            node.LabelTextBlock.Visibility = Visibility.Collapsed;
        }

        private void drawSimpleConnector(Node node)
        {
            normalDraw(node.Width, node.Height, node.Position, node.Background , node.OnMouseDown, node.UICanvas);
            node.LabelTextBlock.Visibility = Visibility.Collapsed;
        }

        private void drawUMLActor(Node node)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Width = node.Width;
            rectangle.Height = node.Height;
            rectangle.Fill = Brushes.White;
            rectangle.MouseDown += node.OnMouseDown;
            node.UICanvas.Children.Add(rectangle);

            
            normalDraw(node.Width, node.Height, node.Position, node.Background, node.OnMouseDown, node.UICanvas);


            TextBlock textBlock = new TextBlock();
            textBlock.Text = node.LabelTextBlock.Text;
            textBlock.InvalidateMeasure();
            textBlock.IsHitTestVisible = false;
            node.UICanvas.Children.Add(textBlock);

            textBlock.UpdateLayout();
            double xMargin = (node.Width - textBlock.ActualWidth) / 2;
            textBlock.Margin = new Thickness(xMargin, node.Height - 20, 0, 0);


            node.LabelTextBlock.Visibility=Visibility.Collapsed;
        }

        private void drawText(Node node)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Width = node.Width;
            rectangle.Height = node.Height;
            rectangle.Fill = node.Background;
            rectangle.MouseDown += node.OnMouseDown;
            node.UICanvas.Children.Add(rectangle);

        }

        private void drawUMLClass(Node node)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Width = node.Width;
            rectangle.Height = node.Height;
            rectangle.Fill = node.Background;
            rectangle.Stroke = Brushes.Black;
            rectangle.StrokeThickness = 1;
            rectangle.MouseDown += node.OnMouseDown;
            node.UICanvas.Children.Add(rectangle);

            string className = "Class Name";
            string streoType = "";
            List<string> VariableNames = new List<string>();
            List<string> MethodNames = new List<string>();

            if (node.Properties.Count != 0)
            {
                className = node.Properties[0];
                streoType = node.Properties[1];
                int variableCount = int.Parse(node.Properties[2]);
                int methodCount = int.Parse(node.Properties[3]);

                int i;
                for (i = 4; i < variableCount + 4; i++)
                    VariableNames.Add(node.Properties[i]);

                for (i = variableCount+4; i < variableCount+methodCount + 4; i++)
                    MethodNames.Add(node.Properties[i]);
            }

            if (VariableNames.Count == 0)
                VariableNames.Add("");

            StackPanel stackPanel = new StackPanel();
            stackPanel.Width = node.Width;
            stackPanel.Height = node.Height;
            stackPanel.Margin = new Thickness(0, 3, 0, 3);

            Label label = new Label();
            label.Content = className;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.IsHitTestVisible = false;
            label.Padding = new Thickness(10,1,10,1);
            label.Foreground = new SolidColorBrush(node.ForeGroundColor);
            label.FontWeight = FontWeights.Bold;
            stackPanel.Children.Add(label);

            if (streoType != "")
            {
                label = new Label();
                label.Content = streoType;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.IsHitTestVisible = false;
                label.Padding = new Thickness(10, 1, 10, 1);
                label.Foreground = new SolidColorBrush(node.ForeGroundColor);
                label.FontWeight = FontWeights.Bold;
                stackPanel.Children.Add(label);
            }

            rectangle = new Rectangle();
            rectangle.Height = 1;
            rectangle.Fill = Brushes.Black;
            rectangle.IsHitTestVisible = false;
            rectangle.Margin = new Thickness(0, 3, 0, 3);
            stackPanel.Children.Add(rectangle);

            foreach (string s in VariableNames)
            {
                label = new Label();
                label.Content = s;
                label.IsHitTestVisible = false;
                label.Padding = new Thickness(10, 1, 10, 1);
                label.Foreground = new SolidColorBrush(node.ForeGroundColor);
                label.FontWeight = FontWeights.Bold;
                stackPanel.Children.Add(label);
            }

            rectangle = new Rectangle();
            rectangle.Height = 1;
            rectangle.Fill = Brushes.Black;
            rectangle.IsHitTestVisible = false;
            rectangle.Margin = new Thickness(0, 3, 0, 3);
            stackPanel.Children.Add(rectangle);

            foreach (string s in MethodNames)
            {
                label = new Label();
                label.Content = s;
                label.IsHitTestVisible = false;
                label.Padding = new Thickness(10, 1, 10, 1);
                label.Foreground = new SolidColorBrush(node.ForeGroundColor);
                label.FontWeight = FontWeights.Bold;
                stackPanel.Children.Add(label);
            }

            node.UICanvas.Children.Add(stackPanel);

            node.LabelTextBlock.Visibility = Visibility.Collapsed;
        }

        private void drawUMLPackage(Node node)
        {
            normalDraw(node.Width, node.Height, node.Position, node.Background, node.OnMouseDown, node.UICanvas);

            double xMargin = node.Width - node.Width / (double)(node.Shape.Width) * 70;
            double yMargin=node.Height- node.Height/(double)(node.Shape.Height) *30;

            node.LabelTextBlock.Margin = new Thickness(0,0,xMargin,yMargin);
        }

        #endregion

        private class Pair
        {
            public string Name;
            public double Value;
        }

    }
}
