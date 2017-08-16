//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using Project.Shapes;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using Project.SpecialNodeProperties;

namespace Project.MainClasses
{
    public class Node
    {
        #region Properties

        public int Width { get; set; }
        public int Height { get; set; }
        public Border UIBorder { get; set; }
        public Canvas UICanvas { get; set; }
        public double Rotation { get; set; }
        public MyShape Shape { get; set; }
        public MyPoint Position { get; private set; }
        public List<Edge> edges { get; set; }
        public List<NodeConnection> NodeConnections { get; set; }
        public ResizeConnector ResizeConnector { get; set; }
        public List<string> Properties { get; set; }

        public Graph Graph { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set 
            { 
                isSelected = value;

                if (isSelected)
                {
                    UIBorder.BorderThickness = new Thickness(1);
                    UIBorder.BorderBrush = Brushes.Red;
                    if (Shape.NodeName != "SimpleConnectorNode")
                        ResizeConnector.IsVisible = true;
                }
                else
                {
                    UIBorder.BorderThickness = new Thickness(0);
                    UIBorder.BorderBrush = Brushes.Black;
                    ResizeConnector.IsVisible = false;
                }
            }
        }

        private string label;
        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
                LabelTextBlock.Text = label;
            }
        }

        public Brush Background
        {
            get
            {
                return Statics.CreateBackgroundBrush(BackgroundColor, BackgroundColorStyle);
            }
        }

        public Color BackgroundColor { get; set; }
        public BackgroundColorStyle BackgroundColorStyle { get; set; }

        public TextBlock LabelTextBlock { get; set; }

        private double fontSize;
        public double FontSize
        {
            get
            {
                return fontSize;
            }
            set
            {
                fontSize = value;
                LabelTextBlock.FontSize = fontSize;
            }
        }

        private bool isFontBold;
        public bool IsFontBold
        {
            get { return isFontBold; }
            set 
            { 
                isFontBold = value;

                if (isFontBold)
                    LabelTextBlock.FontWeight = FontWeights.Bold;
                else
                    LabelTextBlock.FontWeight = FontWeights.Normal;
            }
        }
        
        private bool isFontItalic;
        public bool IsFontItalic
        {
            get { return isFontItalic; }
            set 
            { 
                isFontItalic = value;

                if (isFontItalic)
                    LabelTextBlock.FontStyle = FontStyles.Italic;
                else
                    LabelTextBlock.FontStyle = FontStyles.Normal;
            }
        }

        private FontDecoration fontDecoration;
        public FontDecoration FontDecoration
        {
            get { return fontDecoration; }
            set 
            { 
                fontDecoration = value;
                if (fontDecoration == MainClasses.FontDecoration.Underline)
                    LabelTextBlock.TextDecorations = TextDecorations.Underline;
                else if (fontDecoration == MainClasses.FontDecoration.Stroke)
                    LabelTextBlock.TextDecorations = TextDecorations.Strikethrough;
                else
                    LabelTextBlock.TextDecorations=null;
            }
        }
        
        private Color foreGroundColor;
        public Color ForeGroundColor
        {
            get
            {
                return foreGroundColor;
            }
            set
            {
                foreGroundColor = value;
                LabelTextBlock.Foreground = new SolidColorBrush(foreGroundColor);
            }
        }

        private int zIndex;
        public int ZIndex
        {
            get
            {
                return zIndex;
            }
            set
            {
                zIndex = value;
                Canvas.SetZIndex(UIBorder, zIndex);
            }
        }

        // right to left implemented using node properties to avoid changing file structure.
        public bool IsRightToLeft
        {
            get
            {
                if (this.Properties.Contains("RightToLeft"))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    if (!this.Properties.Contains("RightToLeft"))
                        this.Properties.Add("RightToLeft");
                    this.LabelTextBlock.FlowDirection = FlowDirection.RightToLeft;
                }
                else
                {
                    if (this.Properties.Contains("RightToLeft"))
                        this.Properties.Remove("RightToLeft");
                    this.LabelTextBlock.FlowDirection = FlowDirection.LeftToRight;
                }
            }
        }

        #endregion

        #region Public methods

        public Node(Graph graph,int width,int height,MyShape shape,MyPoint position)
        {
            this.Graph = graph;
            this.Width = width;
            this.Height = height;
            this.Shape = shape;
            this.Position = position;

            UIBorder = new Border
                           {
                               Width = Width + 2, 
                               Height = Height + 2
                           };

            UICanvas = new Canvas();

            Grid UIGrid = new Grid();
            UIGrid.Children.Add(UICanvas);
            UIBorder.Child = UIGrid;

            LabelTextBlock = new TextBlock
                                 {
                                     VerticalAlignment = VerticalAlignment.Center,
                                     HorizontalAlignment = HorizontalAlignment.Center,
                                     TextAlignment = TextAlignment.Center,
                                     IsHitTestVisible = false
                                 };

            UIGrid.Children.Add(LabelTextBlock);

            this.Label = "Label";
            this.FontSize = 16;
            this.ForeGroundColor = Colors.Black;
            this.IsFontBold = false;
            this.IsFontItalic = false;
            this.FontDecoration = FontDecoration.None;
            
            edges = new List<Edge>();
            
            NodeConnections = new List<NodeConnection>();
            foreach (MyPoint p in shape.ConnectionPoints)
                new NodeConnection(p, this);

            BackgroundColor = Color.FromArgb(255,31, 73, 125);
            BackgroundColorStyle = BackgroundColorStyle.Radial;

            Position.Changed += onPositionChanged;

            Properties = new List<string>();

            this.ZIndex = 0;

            initialDraw();

            ResizeConnector = new ResizeConnector(this);
            updateResizeConnector();
            
        }

        public void Remove(object sender, RoutedEventArgs e)
        {
            Graph.RemoveNode(this);
        }

        public void Duplicate(object sender, RoutedEventArgs e)
        {
            var position = new MyPoint(this.Position.X + 10, this.Position.Y + 10);

            Node node = new Node(this.Graph, this.Width, this.Height, this.Shape, position);

            node.BackgroundColor = this.BackgroundColor;
            node.BackgroundColorStyle = this.BackgroundColorStyle;
            node.FontDecoration = this.FontDecoration;
            node.FontSize = this.FontSize;
            node.ForeGroundColor = this.ForeGroundColor;
            node.IsFontBold = this.IsFontBold;
            node.IsFontItalic = this.IsFontItalic;
            
            node.Label = this.Label;
            node.Rotation = this.Rotation;

            foreach (var s in this.Properties)
                node.Properties.Add(s);

            Graph.AddNode(node);

            node.BringIntoFront(null, null);

            node.UpdateShape();

            Graph.SelectionManager.ClearSelection();
            Graph.SelectionManager.AddSelectedNode(node);
        }

        public void BringIntoFront(object sender, RoutedEventArgs e)
        {
            Graph.ZIndexManager.BringIntoFront(this);
        }

        public void SendToBack(object sender, RoutedEventArgs e)
        {
            Graph.ZIndexManager.SendToBack(this);
        }

        public void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Graph.OnNodeMouseDown(this, e);
        }

        public void UpdateBorderPosition()
        {
            UIBorder.RenderTransform = new TranslateTransform(Position.X, Position.Y);
        }

        public virtual void PropertiesWindow(object sender, RoutedEventArgs e)
        {
            if (Shape.NodeName == "NormalNode")
            {
                var form = new NodePropertiesForm(this);
                form.ShowDialog();
            }
            else if (Shape.NodeName == "TextNode")
            {
                var form = new NodePropertiesForm(this);
                form.ShowDialog();
            }
            else if (Shape.NodeName == "UMLActorNode")
            {
                var form = new NodePropertiesForm(this);
                form.ShowDialog();
            }
            else if (Shape.NodeName == "UMLPackageNode")
            {
                var form = new NodePropertiesForm(this);
                form.ShowDialog();
            }
            else if (Shape.NodeName == "PieChartNode")
            {
                var form = new ChartPropertiesForm(this);
                form.ShowDialog();
            }
            else if (Shape.NodeName == "BarChartNode")
            {
                var form = new ChartPropertiesForm(this);
                form.ShowDialog();
            }
            else if (Shape.NodeName == "HorizontalLineNode")
            {
                var form = new LinePropertiesForm(this);
                form.ShowDialog();
            }
            else if (Shape.NodeName == "VerticalLineNode")
            {
                var form = new LinePropertiesForm(this);
                form.ShowDialog();
            }
            else if (Shape.NodeName == "UMLClassNode")
            {
                var form = new UMLClassPropertiesForm(this);
                form.ShowDialog();
            }
        }

        public void UpdateShape()
        {
            double lastWidth = UIBorder.Width - 2;
            double lastHeight = UIBorder.Height - 2;

            UIBorder.Width = Width+2;
            UIBorder.Height = Height+2;
            UICanvas.Children.Clear();


            draw();

            foreach (NodeConnection nodeConnection in NodeConnections)
                nodeConnection.Update();

            double scaleX=Width/lastWidth;
            double scaleY=Height/lastHeight;

            foreach (Edge edge in edges)
            {
                if (edge.HeadConnection.Node == this)
                {
                    double relatedX = edge.EdgeParts[0].UILine.X1 - Position.X;
                    double relatedY = edge.EdgeParts[0].UILine.Y1 - Position.Y;
                    edge.EdgeParts[0].UILine.X1 += (scaleX * relatedX - relatedX);
                    edge.EdgeParts[0].UILine.Y1 += (scaleY * relatedY - relatedY);
                }
                if (edge.TailConnection.Node == this)
                {
                    int lastIndex = edge.EdgeParts.Count - 1;
                    double relatedX = edge.EdgeParts[lastIndex].UILine.X2 - Position.X;
                    double relatedY = edge.EdgeParts[lastIndex].UILine.Y2 - Position.Y;
                    edge.EdgeParts[lastIndex].UILine.X2 += (scaleX * relatedX - relatedX);
                    edge.EdgeParts[lastIndex].UILine.Y2 += (scaleY * relatedY - relatedY);
                }

                edge.UpdateLabels();
                edge.UpdateStructure();
            }

            updateResizeConnector();
        }

        private void draw()
        {
            Shape.DrawNode(this);
        }

        public bool IsSizeAcceptable(int width, int height)
        {
            if (Shape.NodeName == "UMLActorNode")
            {
                if (width < Shape.DefaultWidth || height < Shape.DefaultHeight)
                    return false;
                return true;
            }
            else if (Shape.NodeName == "HorizontalLineNode")
            {
                if (height != 10)
                    return false;
                if (width < 20)
                    return false;
                return true;
            }

            else if (Shape.NodeName == "VerticalLineNode")
            {
                if (width != 10)
                    return false;
                if (height < 20)
                    return false;
                return true;
            }
            else if (Shape.NodeName == "PieChartNode")
            {
                if (width < 20 || height < 20)
                    return false;
                else
                    return width > height + 100;
            }
            else if (Shape.NodeName == "BarChartNode")
            {
                if (width < 200 || height < 200)
                    return false;
                else return true;
            }
            else if (width < 20 || height < 20)
                return false;
            else
                return true;
        }

        #endregion

        #region Private methods

        private void onPositionChanged(double lastX, double lastY)
        {
            foreach (Edge edge in edges)
            {
                int lastIndex = edge.EdgeParts.Count - 1;

                EdgePart firstPart = edge.EdgeParts[0];
                EdgePart lastPart = edge.EdgeParts[lastIndex];

                if (edge.HeadConnection.Node == this)
                {
                    firstPart.UILine.X1 += Position.X - lastX;
                    firstPart.UILine.Y1 += Position.Y - lastY;
                }
                if (edge.TailConnection.Node == this)
                {
                    lastPart.UILine.X2 += Position.X - lastX;
                    lastPart.UILine.Y2 += Position.Y - lastY;
                }

                edge.UpdateLabels();
                edge.UpdateStructure();
            }

            foreach (NodeConnection nodeConnection in NodeConnections)
                nodeConnection.Update();
        }
        private void updateResizeConnector()
        {
            if (Shape.NodeName == "SimpleConnectorNode")
                ResizeConnector.IsVisible = false;
            ResizeConnector.UIRectangle.Margin = new Thickness(Width - 5, Height - 5, 0, 0);
            UICanvas.Children.Add(ResizeConnector.UIRectangle);
        }
        private void initialDraw()
        {
            draw();
            UpdateBorderPosition();
        }

        #endregion
    }

    public enum BackgroundColorStyle
    {
        Solid,
        Radial,
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop,
        TopLeftToBottomRight,
        BottomRightToTopLeft
    }

    public enum FontDecoration
    {
        None,
        Underline,
        Stroke
    }
}
