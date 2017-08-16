//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Xml;
using Project.Shapes;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Project.MainClasses
{
    public class Graph
    {
        #region Private variables
        
        private Canvas tempEdgeCanvas;
        private Edge tempEdge;
        private MyPoint lastMovePosition;
        private MyPoint areaStartPosition;
        private Rectangle selectAreaRect;

        #endregion

        #region Properties

        private int width;
        public int Width
        {
            get { return width; }
            set 
            { 
                width = value;
                UIBorder.Width = width;
            }
        }

        private int height;
        public int Height
        {
            get { return height; }
            set 
            { 
                height = value;
                UIBorder.Height = height;
            }
        }
        
        public Canvas UICanvas { get; private set; }

        private List<Node> nodes;
        public List<Node> Nodes
        {
            get { return nodes; }
        }

        private List<Edge> edges;
        public List<Edge> Edges
        {
            get { return edges; }
            set { edges = value; }
        }

        private Node tempNode;
        public Node TempNode
        {
            get { return tempNode; }
            set { tempNode = value; }
        }

        private Window owner;

        private MouseStates mouseState;
        public MouseStates MouseState
        {
            get
            {
                return mouseState;
            }
            set
            {
                mouseState = value;

                if (MouseStateChanged != null)
                    MouseStateChanged();

                if (mouseState == MouseStates.LinkNormal)
                {
                    SelectionManager.ClearSelection();

                    foreach (Node node in nodes)
                        foreach (NodeConnection connection in node.NodeConnections)
                            connection.IsVisible = true;
                }
                else if (mouseState == MouseStates.Normal)
                {
                    foreach (Node node in nodes)
                        foreach (NodeConnection connection in node.NodeConnections)
                            if(connection.IsVisible)
                                connection.IsVisible = false;

                    owner.Cursor= Cursors.Arrow;
                }
                else if (mouseState == MouseStates.InsertStarted)
                {
                    owner.Cursor = Cursors.Pen;
                }
            }

        }

        private EndSymbol defaultHeadSymbol;
        public EndSymbol DefaultHeadSymbol
        {
            get { return defaultHeadSymbol; }
            set { defaultHeadSymbol = value; }
        }

        private EndSymbol defaultTailSymbol;
        public EndSymbol DefaultTailSymbol
        {
            get { return defaultTailSymbol; }
            set { defaultTailSymbol = value; }
        }

        private EdgeBrushType defaultEdgeBrushType;
        public EdgeBrushType DefaultEdgeBrushType
        {
            get { return defaultEdgeBrushType; }
            set { defaultEdgeBrushType = value; }
        }

        private ZIndexManager zIndexManager;
        public ZIndexManager ZIndexManager
        {
            get { return zIndexManager; }
        }

        private SelectionManager selectionManager;
        public SelectionManager SelectionManager
        {
            get { return selectionManager; }
        }

        #endregion

        public static Border UIBorder = null;
        
        public event NoArgDelegate MouseStateChanged;

        #region Public Methods

        public Graph(int width, int height,Window owner)
        {
            this.width = width;
            this.height = height;
            this.owner = owner;
            if(UIBorder==null)
                UIBorder = new Border();
            UIBorder.HorizontalAlignment = HorizontalAlignment.Left;
            UIBorder.VerticalAlignment = VerticalAlignment.Top;
            UIBorder.BorderBrush = Brushes.Black;
            UIBorder.BorderThickness = new Thickness(0, 0, 1, 1);

            UICanvas = new Canvas();
            UICanvas.Background = Brushes.White;
            UIBorder.Child = UICanvas;
            UIBorder.Width = width;
            UIBorder.Height = height;
            UIBorder.Background = Brushes.White;
            UIBorder.MouseDown += onMouseDown;
            UIBorder.MouseMove += onMouseMove;
            UIBorder.MouseUp += onMouseUp;
            UIBorder.MouseLeave += onMouseLeave;

            selectAreaRect = new Rectangle();
            selectAreaRect.Visibility = Visibility.Collapsed;
            UICanvas.Children.Add(selectAreaRect);
            selectAreaRect.Fill = Brushes.Transparent;
            selectAreaRect.Stroke = Brushes.Red;
            selectAreaRect.StrokeThickness = 1;
            Canvas.SetZIndex(selectAreaRect, Statics.SelectAreaZIndex);
            nodes = new List<Node>();
            edges = new List<Edge>();
            MouseState = MouseStates.Normal;

            defaultHeadSymbol = EndSymbol.None;
            defaultTailSymbol = EndSymbol.FilledTriangle;
            defaultEdgeBrushType = EdgeBrushType.Solid;

            tempEdgeCanvas = new Canvas();
            UICanvas.Children.Add(tempEdgeCanvas);
            Canvas.SetZIndex(tempEdgeCanvas, Statics.TempEdgeCanvasZIndex);

            zIndexManager = new ZIndexManager(this);
            selectionManager = new SelectionManager(this);
        }

        public bool MoveSelected(double difx, double dify, MyPoint newPoint)
        {
            bool isOk = true;

            foreach (Node node in SelectionManager.SelectedNodes)
            {
                if (node.Position.X + difx < 0 || node.Position.Y + dify < 0)
                    isOk = false;

                if (node.Position.X+node.Width + difx > width - 50)
                {
                    width += 200;
                    UIBorder.Width += 200;
                }

                if (node.Position.Y+node.Height + dify > height - 50)
                {
                    height += 200;
                    UIBorder.Height += 200;
                }

                node.Position.X += difx;
                node.Position.Y += dify;
                node.UpdateBorderPosition();

                foreach (Edge edge in node.edges)
                    if (edge.HeadConnection.Node == node && edge.TailConnection.Node.IsSelected == true)
                        foreach (EdgeBreak edgeBreak in edge.EdgeBreaks)
                            if(edgeBreak.Move(difx, dify)==false)
                                isOk = false;

                
            }

            if (SelectionManager.SelectedEdgeBreak != null)
            {
                if (SelectionManager.SelectedEdgeBreak.Move(difx, dify) == false)
                    isOk=false;
            }

            if (SelectionManager.SelectedEdgePart != null)
            {
                foreach (EdgeBreak edgeBreak in SelectionManager.SelectedEdgePart.Edge.EdgeBreaks)
                    if (edgeBreak.BeforeEdgePart.UILine == SelectionManager.SelectedEdgePart.UILine || edgeBreak.AfterEdgePart.UILine == SelectionManager.SelectedEdgePart.UILine)
                        if (edgeBreak.Move(difx, dify) == false)
                            isOk = false;
            }

            lastMovePosition = newPoint;

            return isOk;
        }

        public bool Save(string path)
        {
            FileStream fileStream = null;
            MemoryStream memoryStream = null;
            XmlWriter writer = null;

            try
            {
                memoryStream = new MemoryStream();

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.NewLineHandling = NewLineHandling.Entitize;

                writer = XmlTextWriter.Create(memoryStream,settings);

                writer.WriteStartElement("DiagramRing");

                writer.WriteStartAttribute("version");
                writer.WriteValue("2.0");
                writer.WriteEndAttribute();

                writer.WriteStartAttribute("GUID");
                writer.WriteValue("{C787CBC7-4158-4E76-AF26-984DE5AC163E}");
                writer.WriteEndAttribute();

                writer.WriteStartAttribute("width");
                writer.WriteValue(this.width);
                writer.WriteEndAttribute();

                writer.WriteStartAttribute("height");
                writer.WriteValue(this.height);
                writer.WriteEndAttribute();

                //Nodes

                writer.WriteStartElement("Nodes");

                writer.WriteStartAttribute("count");
                writer.WriteValue(this.nodes.Count);
                writer.WriteEndAttribute();

                foreach (Node node in this.nodes)
                {
                    writer.WriteStartElement("Node");

                    writer.WriteStartAttribute("x");
                    writer.WriteValue(node.Position.X);
                    writer.WriteEndAttribute();

                    writer.WriteStartAttribute("y");
                    writer.WriteValue(node.Position.Y);
                    writer.WriteEndAttribute();

                    writer.WriteStartElement("Width");
                    writer.WriteValue(node.Width);
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("Height");
                    writer.WriteValue(node.Height);
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("ShapeName");
                    writer.WriteValue(node.Shape.Name);
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("ZIndex");
                    writer.WriteValue(node.ZIndex);
                    writer.WriteFullEndElement();

                    // BackgroundColor
                    writer.WriteStartElement("BackgroundColor");

                    writer.WriteStartElement("Red");
                    writer.WriteValue(node.BackgroundColor.R);
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("Green");
                    writer.WriteValue(node.BackgroundColor.G);
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("Blue");
                    writer.WriteValue(node.BackgroundColor.B);
                    writer.WriteFullEndElement();

                    writer.WriteFullEndElement();
                    // End of BackgroundColor

                    writer.WriteStartElement("BackgroundStyle");
                    writer.WriteValue(node.BackgroundColorStyle.ToString());
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("FontSize");
                    writer.WriteValue(node.FontSize);
                    writer.WriteFullEndElement();

                    // FontColor
                    writer.WriteStartElement("FontColor");

                    writer.WriteStartElement("Red");
                    writer.WriteValue(node.ForeGroundColor.R);
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("Green");
                    writer.WriteValue(node.ForeGroundColor.G);
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("Blue");
                    writer.WriteValue(node.ForeGroundColor.B);
                    writer.WriteFullEndElement();

                    writer.WriteFullEndElement();
                    // End of FontColor

                    writer.WriteStartElement("IsFontBold");
                    writer.WriteValue(node.IsFontBold);
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("IsFontItalic");
                    writer.WriteValue(node.IsFontItalic);
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("FontDecoration");
                    writer.WriteValue(node.FontDecoration.ToString());
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("Label");
                    writer.WriteCData(node.Label);
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("Properties");

                    writer.WriteStartAttribute("count");
                    writer.WriteValue(node.Properties.Count);
                    writer.WriteEndAttribute();

                    foreach (string property in node.Properties)
                    {
                        writer.WriteStartElement("Property");
                        writer.WriteCData(property);
                        writer.WriteFullEndElement();
                    }
                    writer.WriteFullEndElement();

                    writer.WriteFullEndElement();
                }

                writer.WriteFullEndElement();

                //End of Nodes

                // Edges
                writer.WriteStartElement("Edges");

                writer.WriteStartAttribute("count");
                writer.WriteValue(this.edges.Count);
                writer.WriteEndAttribute();

                foreach (Edge edge in this.edges)
                {
                    writer.WriteStartElement("Edge");

                    writer.WriteStartElement("HeadNodeIndex");
                    writer.WriteValue(this.nodes.IndexOf(edge.HeadConnection.Node));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("HeadConnectionIndex");
                    writer.WriteValue(edge.HeadConnection.Node.NodeConnections.IndexOf(edge.HeadConnection));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("TailNodeIndex");
                    writer.WriteValue(this.nodes.IndexOf(edge.TailConnection.Node));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("TailConnectionIndex");
                    writer.WriteValue(edge.TailConnection.Node.NodeConnections.IndexOf(edge.TailConnection));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("HeadSymbol");
                    writer.WriteValue(edge.HeadSymbol.ToString());
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("TailSymbol");
                    writer.WriteValue(edge.TailSymbol.ToString());
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("EdgeParts");

                    writer.WriteStartAttribute("count");
                    writer.WriteValue(edge.EdgeParts.Count);
                    writer.WriteEndAttribute();


                    foreach (EdgePart edgePart in edge.EdgeParts)
                    {
                        writer.WriteStartElement("EdgePart");

                        writer.WriteStartAttribute("x1");
                        writer.WriteValue(edgePart.UILine.X1);
                        writer.WriteEndAttribute();

                        writer.WriteStartAttribute("y1");
                        writer.WriteValue(edgePart.UILine.Y1);
                        writer.WriteEndAttribute();

                        writer.WriteStartAttribute("x2");
                        writer.WriteValue(edgePart.UILine.X2);
                        writer.WriteEndAttribute();

                        writer.WriteStartAttribute("y2");
                        writer.WriteValue(edgePart.UILine.Y2);
                        writer.WriteEndAttribute();

                        writer.WriteStartElement("EdgeLabels");

                        writer.WriteStartAttribute("count");
                        writer.WriteValue(edgePart.EdgeLabels.Count);
                        writer.WriteEndAttribute();

                        foreach (EdgeLabel edgeLabel in edgePart.EdgeLabels)
                        {
                            writer.WriteStartElement("EdgeLabel");

                            writer.WriteStartAttribute("percent");
                            writer.WriteValue(edgeLabel.Percent);
                            writer.WriteEndAttribute();

                            writer.WriteStartAttribute("distance");
                            writer.WriteValue(edgeLabel.Distance);
                            writer.WriteEndAttribute();

                            writer.WriteStartElement("Text");
                            writer.WriteCData(edgeLabel.UITextBlock.Text);
                            writer.WriteFullEndElement();

                            writer.WriteFullEndElement();
                        }

                        // End of edgeLbaels
                        writer.WriteFullEndElement();

                        //End of EdgePart
                        writer.WriteFullEndElement();
                    }

                    // End of Edgeparts
                    writer.WriteFullEndElement();

                    //End of Edge
                    writer.WriteFullEndElement();
                }

                writer.WriteFullEndElement();
                //End of edges

                writer.WriteFullEndElement();

                writer.Close();

                fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

                memoryStream.Seek(0, SeekOrigin.Begin);

                while (memoryStream.Position < memoryStream.Length)
                {
                    byte b1 = (byte)memoryStream.ReadByte();
                    byte b2 = (byte)(255 - b1);
                    fileStream.WriteByte(b2);
                }

                memoryStream.Close();
                fileStream.Close();
                
                return true;
            }
            catch
            {
                if (memoryStream != null)
                    memoryStream.Close();

                if (fileStream != null)
                    fileStream.Close();

                return false;
            }
        }

        public static LoadResult Load(string path,Window owner,Dictionary<string,MyShape> shapeLibrary)
        {
            string extension=System.IO.Path.GetExtension(path);

            if (extension == ".dr1")
                return ImportVersion1(path, owner, shapeLibrary);
            else if(extension==".drx")
                return LoadDRX(path, owner, shapeLibrary);

            return new LoadResult(null,false,false);
            
        }

        public static LoadResult LoadDRX(string path, Window owner, Dictionary<string, MyShape> shapeLibrary)
        {
            FileStream fileStream=null;
            MemoryStream memoryStream=null;
            XmlReader reader=null;

            try
            {
                fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                memoryStream = new MemoryStream();

                while (fileStream.Position<fileStream.Length)
                {
                    byte b1 = (byte) fileStream.ReadByte();
                    byte b2 = (byte)(255 - b1);
                    memoryStream.WriteByte(b2);
                }

                fileStream.Close();

                memoryStream.Seek(0, SeekOrigin.Begin);


                reader = XmlReader.Create(memoryStream);

                

                LoadResult result = new LoadResult(null, false, false);

                string s, s1;
                int i, j, k;
                double x, y, d;
                int count, count1, count2;
                int width, height;
                int r, g, b;

                reader.ReadToFollowing("DiagramRing");

                reader.MoveToAttribute("version");
                s = reader.Value;
                if (s != "2.0")
                {
                    reader.Close();
                    return new LoadResult(null, false, false);
                }

                reader.MoveToAttribute("GUID");
                s = reader.Value;
                if (s != "{C787CBC7-4158-4E76-AF26-984DE5AC163E}")
                {
                    reader.Close();
                    return new LoadResult(null, false, false);
                }

                reader.MoveToAttribute("width");
                width = reader.ReadContentAsInt();

                reader.MoveToAttribute("height");
                height = reader.ReadContentAsInt();

                Graph graph = new Graph(width, height, owner);

                reader.ReadToFollowing("Nodes");

                reader.MoveToAttribute("count");
                count = reader.ReadContentAsInt();

                for (i = 0; i < count; i++)
                {
                    reader.ReadToFollowing("Node");

                    reader.MoveToAttribute("x");
                    x = reader.ReadContentAsDouble();

                    reader.MoveToAttribute("y");
                    y = reader.ReadContentAsDouble();

                    reader.ReadToFollowing("Width");
                    width = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("Height");
                    height = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("ShapeName");
                    s1 = reader.ReadElementContentAsString();

                    if (!shapeLibrary.ContainsKey(s1))
                    {
                        reader.Close();
                        return new LoadResult(null, false, false);
                    }

                    reader.ReadToFollowing("ZIndex");
                    int zIndex = reader.ReadElementContentAsInt();

                    Node node = new Node(graph, width, height, shapeLibrary[s1], new MyPoint(x, y));
                    node.ZIndex = zIndex;
                    graph.AddNode(node);

                    reader.ReadToFollowing("BackgroundColor");

                    reader.ReadToFollowing("Red");
                    r = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("Green");
                    g = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("Blue");
                    b = reader.ReadElementContentAsInt();

                    node.BackgroundColor = Color.FromRgb((byte)r, (byte)g, (byte)b);

                    reader.ReadToFollowing("BackgroundStyle");
                    s = reader.ReadElementContentAsString();

                    node.BackgroundColorStyle = (BackgroundColorStyle)Enum.Parse(typeof(BackgroundColorStyle), s);

                    reader.ReadToFollowing("FontSize");
                    d = reader.ReadElementContentAsInt();
                    node.FontSize = d;

                    reader.ReadToFollowing("FontColor");

                    reader.ReadToFollowing("Red");
                    r = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("Green");
                    g = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("Blue");
                    b = reader.ReadElementContentAsInt();

                    node.ForeGroundColor = Color.FromRgb((byte)r, (byte)g, (byte)b);

                    reader.ReadToFollowing("IsFontBold");
                    node.IsFontBold = reader.ReadElementContentAsBoolean();

                    reader.ReadToFollowing("IsFontItalic");
                    node.IsFontItalic = reader.ReadElementContentAsBoolean();

                    reader.ReadToFollowing("FontDecoration");
                    s = reader.ReadElementContentAsString();

                    node.FontDecoration = (FontDecoration)Enum.Parse(typeof(FontDecoration), s);

                    reader.ReadToFollowing("Label");
                    s = reader.ReadElementContentAsString();
                    node.Label = s;

                    reader.ReadToFollowing("Properties");

                    reader.MoveToAttribute("count");
                    count1 = reader.ReadContentAsInt();
                    bool isRightToLeft = false;

                    for (j = 0; j < count1; j++)
                    {
                        reader.ReadToFollowing("Property");
                        s = reader.ReadElementContentAsString();
                        node.Properties.Add(s);
                        if (s == "RightToLeft")
                            isRightToLeft = true;
                    }

                    node.IsRightToLeft = isRightToLeft;

                    node.UpdateShape();
                }

                // End of Nodes

                reader.ReadToFollowing("Edges");

                reader.MoveToAttribute("count");
                count = reader.ReadContentAsInt();

                for (i = 0; i < count; i++)
                {
                    reader.ReadToFollowing("Edge");

                    bool hasEdgeError = false;

                    reader.ReadToFollowing("HeadNodeIndex");
                    int headIndex = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("HeadConnectionIndex");
                    int headConnectionIndex = reader.ReadElementContentAsInt();

                    if (headIndex >= graph.Nodes.Count || headIndex < 0)
                        hasEdgeError = true;
                    else if (headConnectionIndex >= graph.Nodes[headIndex].NodeConnections.Count || headConnectionIndex < 0)
                        hasEdgeError = false;

                    reader.ReadToFollowing("TailNodeIndex");
                    int tailIndex = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("TailConnectionIndex");
                    int tailConnectionIndex = reader.ReadElementContentAsInt();

                    if (tailIndex >= graph.Nodes.Count || tailIndex < 0)
                        hasEdgeError = true;
                    else if (tailConnectionIndex >= graph.Nodes[tailIndex].NodeConnections.Count || tailConnectionIndex < 0)
                        hasEdgeError = false;

                    if (hasEdgeError)
                    {
                        result.IsErrorsSkipped = true;
                        continue;
                    }

                    Edge edge = new Edge(graph);
                    graph.tempEdge = edge;
                    edge.HeadConnection = graph.nodes[headIndex].NodeConnections[headConnectionIndex];
                    edge.TailConnection = graph.nodes[tailIndex].NodeConnections[tailConnectionIndex];

                    graph.nodes[headIndex].edges.Add(edge);
                    if (headIndex != tailIndex)
                        graph.nodes[tailIndex].edges.Add(edge);

                    reader.ReadToFollowing("HeadSymbol");
                    s = reader.ReadElementContentAsString();
                    edge.HeadSymbol = (EndSymbol)Enum.Parse(typeof(EndSymbol), s);

                    reader.ReadToFollowing("TailSymbol");
                    s = reader.ReadElementContentAsString();
                    edge.TailSymbol = (EndSymbol)Enum.Parse(typeof(EndSymbol), s);

                    reader.ReadToFollowing("EdgeParts");

                    reader.MoveToAttribute("count");
                    count1 = reader.ReadContentAsInt();

                    EdgePart lastPart = null;

                    for (j = 0; j < count1; j++)
                    {
                        reader.ReadToFollowing("EdgePart");

                        Line line = new Line();

                        reader.MoveToAttribute("x1");
                        line.X1 = reader.ReadContentAsDouble();

                        reader.MoveToAttribute("y1");
                        line.Y1 = reader.ReadContentAsDouble();

                        reader.MoveToAttribute("x2");
                        line.X2 = reader.ReadContentAsDouble();

                        reader.MoveToAttribute("y2");
                        line.Y2 = reader.ReadContentAsDouble();

                        EdgePart edgePart = new EdgePart(edge, line);
                        edge.EdgeParts.Add(edgePart);
                        graph.tempEdgeCanvas.Children.Add(edgePart.UILine);

                        if (j != 0)
                        {
                            EdgeBreak edgeBreak = new EdgeBreak(new MyPoint(line.X1, line.Y1), lastPart, edgePart, graph);
                            edge.EdgeBreaks.Add(edgeBreak);
                            graph.tempEdgeCanvas.Children.Add(edgeBreak.UIEllipse);
                            Canvas.SetZIndex(edgeBreak.UIEllipse, Statics.EdgeBreakZIndex);
                        }

                        lastPart = edgePart;

                        reader.ReadToFollowing("EdgeLabels");

                        reader.MoveToAttribute("count");
                        count2 = reader.ReadContentAsInt();

                        for (k = 0; k < count2; k++)
                        {
                            reader.ReadToFollowing("EdgeLabel");

                            reader.MoveToAttribute("percent");
                            double percent = reader.ReadContentAsDouble();

                            reader.MoveToAttribute("distance");
                            double distance = reader.ReadContentAsDouble();

                            reader.ReadToFollowing("Text");
                            s = reader.ReadElementContentAsString();

                            EdgeLabel edgeLabel = new EdgeLabel(s, edgePart);
                            edgeLabel.Percent = percent;
                            edgeLabel.Distance = distance;

                            edgePart.EdgeLabels.Add(edgeLabel);
                        }
                    }
                    edge.UpdateEvents();
                    edge.UpdateStructure();
                    graph.AddTempEdge();
                    edge.UpdateLabels();
                }

                result.Graph = graph;
                result.IsSuccessfull = true;

                reader.Close();
                memoryStream.Close();

                return result;
            }
            catch
            {
                if (reader != null && reader.ReadState != ReadState.Closed)
                        reader.Close();

                if (memoryStream != null)
                    memoryStream.Close();

                if (fileStream != null)
                    fileStream.Close();

                return new LoadResult(null, false, false); ;
            }
        }

        public static LoadResult ImportVersion1(string path, Window owner, Dictionary<string, MyShape> shapeLibrary)
        {
            LoadResult result= new LoadResult(null, false, false);
            XmlTextReader reader = null;

            string s, s1;
            int i, j, k;
            double x, y, d;
            int count, count1, count2;
            int width, height;
            int r, g, b;

            try
            {
                reader = new XmlTextReader(path);

                reader.ReadToFollowing("DiagramRing");

                reader.MoveToAttribute("version");
                s = reader.Value;
                if (s != "1.0")
                {
                    reader.Close();
                    return new LoadResult(null, false, false);
                }

                reader.MoveToAttribute("GUID");
                s = reader.Value;
                if (s != "{4B2FD325-AE73-4B3A-949A-2D5C54F9CBCA}")
                {
                    reader.Close();
                    return new LoadResult(null, false, false);
                }

                reader.MoveToAttribute("width");
                width = reader.ReadContentAsInt();

                reader.MoveToAttribute("height");
                height = reader.ReadContentAsInt();

                Graph graph = new Graph(width, height, owner);

                reader.ReadToFollowing("Nodes");

                reader.MoveToAttribute("count");
                count = reader.ReadContentAsInt();

                for (i = 0; i < count; i++)
                {
                    reader.ReadToFollowing("Node");

                    reader.MoveToAttribute("x");
                    x = reader.ReadContentAsDouble();

                    reader.MoveToAttribute("y");
                    y = reader.ReadContentAsDouble();

                    reader.ReadToFollowing("Width");
                    width = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("Height");
                    height = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("NodeName");
                    s = reader.ReadElementContentAsString();

                    reader.ReadToFollowing("ShapeName");
                    s1 = reader.ReadElementContentAsString();

                    if (!shapeLibrary.ContainsKey(s1))
                    {
                        reader.Close();
                        return new LoadResult(null, false, false);
                    }

                    reader.ReadToFollowing("ZIndex");
                    int zIndex = reader.ReadElementContentAsInt();

                    Node node = new Node(graph, width, height, shapeLibrary[s1], new MyPoint(x, y));
                    node.ZIndex = zIndex;
                    graph.AddNode(node);

                    reader.ReadToFollowing("BackgroundColor");

                    reader.ReadToFollowing("Red");
                    r = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("Green");
                    g = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("Blue");
                    b = reader.ReadElementContentAsInt();

                    node.BackgroundColor = Color.FromRgb((byte)r, (byte)g, (byte)b);

                    reader.ReadToFollowing("BackgroundStyle");
                    s = reader.ReadElementContentAsString();

                    node.BackgroundColorStyle = (BackgroundColorStyle)Enum.Parse(typeof(BackgroundColorStyle), s);

                    reader.ReadToFollowing("FontSize");
                    d = reader.ReadElementContentAsInt();
                    node.FontSize = d;

                    reader.ReadToFollowing("FontColor");

                    reader.ReadToFollowing("Red");
                    r = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("Green");
                    g = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("Blue");
                    b = reader.ReadElementContentAsInt();

                    node.ForeGroundColor = Color.FromRgb((byte)r, (byte)g, (byte)b);

                    reader.ReadToFollowing("Label");
                    s = reader.ReadElementContentAsString();
                    node.Label = s;

                    node.UpdateShape();
                }

                // End of Nodes

                reader.ReadToFollowing("Edges");

                reader.MoveToAttribute("count");
                count = reader.ReadContentAsInt();

                for (i = 0; i < count; i++)
                {
                    reader.ReadToFollowing("Edge");

                    bool hasEdgeError = false;

                    reader.ReadToFollowing("HeadNodeIndex");
                    int headIndex = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("HeadConnectionIndex");
                    int headConnectionIndex = reader.ReadElementContentAsInt();

                    if (headIndex >= graph.Nodes.Count || headIndex < 0)
                        hasEdgeError = true;
                    else if (headConnectionIndex >= graph.Nodes[headIndex].NodeConnections.Count || headConnectionIndex < 0)
                        hasEdgeError = false;

                    reader.ReadToFollowing("TailNodeIndex");
                    int tailIndex = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("TailConnectionIndex");
                    int tailConnectionIndex = reader.ReadElementContentAsInt();

                    if (tailIndex >= graph.Nodes.Count || tailIndex < 0)
                        hasEdgeError = true;
                    else if (tailConnectionIndex >= graph.Nodes[tailIndex].NodeConnections.Count || tailConnectionIndex < 0)
                        hasEdgeError = false;

                    if (hasEdgeError)
                    {
                        result.IsErrorsSkipped = true;
                        continue;
                    }

                    Edge edge = new Edge(graph);
                    graph.tempEdge = edge;
                    edge.HeadConnection = graph.nodes[headIndex].NodeConnections[headConnectionIndex];
                    edge.TailConnection = graph.nodes[tailIndex].NodeConnections[tailConnectionIndex];

                    graph.nodes[headIndex].edges.Add(edge);
                    if (headIndex != tailIndex)
                        graph.nodes[tailIndex].edges.Add(edge);

                    reader.ReadToFollowing("HeadSymbol");
                    s = reader.ReadElementContentAsString();
                    edge.HeadSymbol = (EndSymbol)Enum.Parse(typeof(EndSymbol), s);

                    reader.ReadToFollowing("TailSymbol");
                    s = reader.ReadElementContentAsString();
                    edge.TailSymbol = (EndSymbol)Enum.Parse(typeof(EndSymbol), s);

                    reader.ReadToFollowing("EdgeParts");

                    reader.MoveToAttribute("count");
                    count1 = reader.ReadContentAsInt();

                    EdgePart lastPart = null;

                    for (j = 0; j < count1; j++)
                    {
                        reader.ReadToFollowing("EdgePart");

                        Line line = new Line();

                        reader.MoveToAttribute("x1");
                        line.X1 = reader.ReadContentAsDouble();

                        reader.MoveToAttribute("y1");
                        line.Y1 = reader.ReadContentAsDouble();

                        reader.MoveToAttribute("x2");
                        line.X2 = reader.ReadContentAsDouble();

                        reader.MoveToAttribute("y2");
                        line.Y2 = reader.ReadContentAsDouble();

                        EdgePart edgePart = new EdgePart(edge, line);
                        edge.EdgeParts.Add(edgePart);
                        graph.tempEdgeCanvas.Children.Add(edgePart.UILine);

                        if (j != 0)
                        {
                            EdgeBreak edgeBreak = new EdgeBreak(new MyPoint(line.X1, line.Y1), lastPart, edgePart, graph);
                            edge.EdgeBreaks.Add(edgeBreak);
                            graph.tempEdgeCanvas.Children.Add(edgeBreak.UIEllipse);
                            Canvas.SetZIndex(edgeBreak.UIEllipse, Statics.EdgeBreakZIndex);
                        }

                        lastPart = edgePart;

                        reader.ReadToFollowing("EdgeLabels");

                        reader.MoveToAttribute("count");
                        count2 = reader.ReadContentAsInt();

                        for (k = 0; k < count2; k++)
                        {
                            reader.ReadToFollowing("EdgeLabel");

                            reader.MoveToAttribute("percent");
                            double percent = reader.ReadContentAsDouble();

                            reader.MoveToAttribute("distance");
                            double distance = reader.ReadContentAsDouble();

                            reader.ReadToFollowing("Text");
                            s = reader.ReadElementContentAsString();

                            EdgeLabel edgeLabel = new EdgeLabel(s, edgePart);
                            edgeLabel.Percent = percent;
                            edgeLabel.Distance = distance;

                            edgePart.EdgeLabels.Add(edgeLabel);
                        }
                    }
                    edge.UpdateEvents();
                    edge.UpdateStructure();
                    graph.AddTempEdge();
                    edge.UpdateLabels();
                }

                reader.Close();
                result.Graph = graph;
                result.IsSuccessfull = true;
                return result;
            }
            catch
            {
                if (reader != null && reader.ReadState != ReadState.Closed)
                    reader.Close();

                return new LoadResult(null, false, false);;
            }
        }

        public void AddTempEdge()
        {
            tempEdge.UpdateStructure();

            Stack<UIElement> stack = new Stack<UIElement>();

            for (int j = tempEdgeCanvas.Children.Count - 1; j >= 0; j--)
            {
                UIElement element = tempEdgeCanvas.Children[j];
                stack.Push(element);
            }

            tempEdgeCanvas.Children.Clear();

            while (stack.Count != 0)
            {
                UIElement element = stack.Pop();
                UICanvas.Children.Add(element);
                if (element is Ellipse)
                    Canvas.SetZIndex(element, Statics.EdgeBreakZIndex);
                else
                    Canvas.SetZIndex(element, Statics.EdgeBreakZIndex);
            }

            tempEdge.UpdateEvents();
            tempEdge.UpdateLabels();
            AddEdge(tempEdge);
            tempEdge = new Edge(this);
        }

        public void AddEdge(Edge edge)
        {
            edges.Add(edge);
        }

        public void RemoveEdge(Edge edge)
        {
            edge.HeadConnection.Node.edges.Remove(edge);
            edge.TailConnection.Node.edges.Remove(edge);

            foreach (EdgePart edgePart in edge.EdgeParts)
            {
                for (int i = edgePart.EdgeLabels.Count - 1; i >= 0; i--)
                    edgePart.EdgeLabels[i].Remove();

                UICanvas.Children.Remove(edgePart.UILine);
            }
            foreach (EdgeBreak edgeBreak in edge.EdgeBreaks)
                UICanvas.Children.Remove(edgeBreak.UIEllipse);

            UICanvas.Children.Remove(edge.HeadSymbolCanvas);
            UICanvas.Children.Remove(edge.TailSymbolCanvas);

            edges.Remove(edge);

            SelectionManager.ClearSelection();
        }

        public void AddNode(Node node)
        {
            nodes.Add(node);
            ZIndexManager.AddNode(node);
            UICanvas.Children.Add(node.UIBorder);
        }

        public void RemoveNode(Node node)
        {
            for (int j = node.edges.Count - 1; j >= 0; j--)
            {
                node.edges[j].Remove(null, null);
            }

            UICanvas.Children.Remove(node.UIBorder);
            nodes.Remove(node);
            SelectionManager.ClearSelection();
        }

        public void BreakEdgePartAtPoint(EdgePart edgePart,double x, double y)
        {
            Line line = new Line
            {
                X1 = x,
                Y1 = y,
                X2 = edgePart.UILine.X2,
                Y2 = edgePart.UILine.Y2
            };

            edgePart.UILine.X2 = x;
            edgePart.UILine.Y2 = y;

            UICanvas.Children.Add(line);

            EdgePart edgePart1 = new EdgePart(edgePart.Edge, line);
            edgePart1.UpdateEvents();
            Canvas.SetZIndex(line, Statics.EdgePartZIndex);

            foreach (EdgeBreak eb in edgePart.Edge.EdgeBreaks)
                if (eb.BeforeEdgePart == edgePart)
                    eb.BeforeEdgePart = edgePart1;

            int index = edgePart.Edge.EdgeParts.IndexOf(edgePart);
            edgePart.Edge.EdgeParts.Insert(index + 1, edgePart1);
            EdgeBreak edgeBreak = new EdgeBreak(new MyPoint(x, y), edgePart, edgePart1, this);
            edgePart.Edge.EdgeBreaks.Add(edgeBreak);
            UICanvas.Children.Add(edgeBreak.UIEllipse);
            Canvas.SetZIndex(edgeBreak.UIEllipse, Statics.EdgeBreakZIndex);
            
            SelectionManager.ClearSelection();
            edgePart.Edge.UpdateLabels();
            edgePart.Edge.UpdateStructure();
        }

        #endregion

        #region Mouse Events

        private void onMouseDown(object sender, MouseButtonEventArgs e)
        {
            bool isLeft = false;
            bool isRight = false;

            if (e.LeftButton == MouseButtonState.Pressed)
                isLeft = true;
            if (e.RightButton == MouseButtonState.Pressed)
                isRight = true;

            if (isRight)
            {
                if (mouseState == MouseStates.LinkStarted)
                {
                    MouseState = MouseStates.Normal;
                    tempEdgeCanvas.Children.Clear();
                    return;
                }
                else if (MouseState == MouseStates.InsertStarted)
                {
                    TempNode.Remove(null, null);
                    TempNode = null;
                    MouseState = MouseStates.Normal;
                }
            }

            if (isLeft)
            {
                Point p = e.GetPosition(UIBorder);

                if (MouseState == MouseStates.Normal)
                {
                    areaStartPosition = new MyPoint(p.X, p.Y);
                    MouseState = MouseStates.SelectArea;
                }
                else if (MouseState == MouseStates.InsertStarted)
                {
                    TempNode = null;
                    MouseState = MouseStates.Normal;
                }
                else if (mouseState == MouseStates.LinkStarted)
                {
                    int lastIndex = tempEdge.EdgeParts.Count - 1;

                    EdgePart lastPart = tempEdge.EdgeParts[lastIndex];

                    Line line1 = lastPart.UILine;
                    line1.Stroke = Brushes.Black;
                    line1.StrokeThickness = 1;

                    Line line2 = new Line();
                    line2.X1 = line1.X2;
                    line2.Y1 = line1.Y2;
                    line2.X2 = line2.X1;
                    line2.Y2 = line2.Y1;
                    line2.Stroke = Brushes.Black;
                    line2.StrokeThickness = 1;

                    EdgePart edgePart = new EdgePart(tempEdge, line2);
                    tempEdgeCanvas.Children.Add(line2);
                    tempEdge.EdgeParts.Add(edgePart);

                    EdgeBreak edgeBreak = new EdgeBreak(new MyPoint(line1.X2, line1.Y2), lastPart, edgePart, this);
                    tempEdge.EdgeBreaks.Add(edgeBreak);
                    tempEdgeCanvas.Children.Add(edgeBreak.UIEllipse);
                }
            }
        }

        private void onMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (MouseState == MouseStates.Moving)
            {
                MouseState = MouseStates.Normal;
            }
            else if (MouseState == MouseStates.ResizeStarted)
            {
                MouseState = MouseStates.Normal;
            }
            else if (MouseState == MouseStates.SelectArea)
            {
                selectAreaRect.Visibility = Visibility.Collapsed;
                MouseState = MouseStates.Normal;

                Point p = e.GetPosition(UIBorder);

                double minx = Math.Min(p.X, areaStartPosition.X);
                double miny = Math.Min(p.Y, areaStartPosition.Y);
                double maxx = Math.Max(p.X, areaStartPosition.X);
                double maxy = Math.Max(p.Y, areaStartPosition.Y);

                SelectionManager.ClearSelection();

                Rect selectRect = new Rect(new Point(minx, miny), new Point(maxx, maxy));
                foreach (Node node in nodes)
                {
                    Rect rect = new Rect(node.Position.ToPoint(), new Size(node.Width, node.Height));
                    if (selectRect.IntersectsWith(rect))
                        SelectionManager.AddSelectedNode(node);
                }
            }
        }

        private void onMouseMove(object sender, MouseEventArgs e)
        {
            bool isLeft = false;

            if (e.LeftButton == MouseButtonState.Pressed)
                isLeft = true;

            Point p = e.GetPosition(UIBorder);

            if (MouseState == MouseStates.LinkStarted)
            {
                int lastIndex = tempEdge.EdgeParts.Count - 1;

                Line line = tempEdge.EdgeParts[lastIndex].UILine;
                line.X2 = p.X;
                line.Y2 = p.Y;

                bool isVertical = false;
                bool isHorizantal = false;

                if (Math.Abs(line.X1 - line.X2) < Statics.Epsilon)
                    isVertical = true;

                if (Math.Abs(line.Y1 - line.Y2) < Statics.Epsilon)
                    isHorizantal = true;

                if (isVertical)
                {
                    line.X2 = p.X = line.X1;
                }

                if (isHorizantal)
                {
                    line.Y2 = p.Y = line.Y1;
                }

                if (isVertical || isHorizantal)
                {
                    line.Stroke = Brushes.LightGreen;
                    line.StrokeThickness = 3;
                }
                else
                {
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 1;
                }
            }
            else if (MouseState == MouseStates.InsertStarted)
            {
                if (tempNode.Position.X + tempNode.Width > width || tempNode.Position.Y + tempNode.Height > height)
                {
                    TempNode.Remove(null, null);
                    MouseState = MouseStates.Normal;
                    return;
                }

                TempNode.Position.X = p.X;
                TempNode.Position.Y = p.Y;
                TempNode.UpdateBorderPosition();
            }

            if (isLeft)
            {
                if (MouseState == MouseStates.ResizeStarted)
                {
                    int difx = (int)(p.X - lastMovePosition.X);
                    int dify = (int)(p.Y - lastMovePosition.Y);

                    Node node = SelectionManager.SelectedResizeConnector.Owner;

                    if(node.IsSizeAcceptable(node.Width+difx,node.Height))
                        node.Width += difx;
                    if (node.IsSizeAcceptable(node.Width, node.Height+dify))
                        node.Height += dify;

                    node.UpdateShape();

                    lastMovePosition = new MyPoint(p.X, p.Y);
                }
                if (MouseState == MouseStates.Moving)
                {
                    double difx = p.X - lastMovePosition.X;
                    double dify = p.Y - lastMovePosition.Y;
                    if (MoveSelected(difx, dify, new MyPoint(p.X, p.Y)) == false)
                    {
                        MoveSelected(-difx, -dify, new MyPoint(p.X, p.Y));
                        MouseState = MouseStates.Normal;
                        SelectionManager.ClearSelection();
                    }
                }
                else if (MouseState == MouseStates.SelectArea)
                {
                    double minx = Math.Min(p.X, areaStartPosition.X);
                    double miny = Math.Min(p.Y, areaStartPosition.Y);
                    double maxx = Math.Max(p.X, areaStartPosition.X);
                    double maxy = Math.Max(p.Y, areaStartPosition.Y);

                    selectAreaRect.Width = maxx - minx;
                    selectAreaRect.Height = maxy - miny;
                    selectAreaRect.Margin = new Thickness(minx, miny, 0, 0);
                    selectAreaRect.Visibility = Visibility.Visible;
                }
            }
        }

        private void onMouseLeave(object sender, MouseEventArgs e)
        {
            selectAreaRect.Visibility = Visibility.Collapsed;
            if (mouseState == MouseStates.InsertStarted)
                TempNode.Remove(null, null);

            MouseState = MouseStates.Normal;
            tempEdgeCanvas.Children.Clear();
        }
        
        public void OnNodeConnectionMouseDown(NodeConnection nodeConnection, MouseButtonEventArgs e)
        {
            bool isLeft = false;

            if (e.LeftButton == MouseButtonState.Pressed)
                isLeft = true;

            if (isLeft)
            {
                e.Handled = true;

                if (MouseState == MouseStates.LinkNormal)
                {

                    tempEdge = new Edge(this);

                    Edge edge = tempEdge;

                    edge.HeadConnection = nodeConnection;
                    Line line = new Line();
                    EdgePart edgePart = new EdgePart(edge, line);
                    edge.EdgeParts.Add(edgePart);
                    line.X1 = nodeConnection.UIEllipse.Margin.Left + 3;
                    line.Y1 = nodeConnection.UIEllipse.Margin.Top + 3;
                    line.X2 = line.X1;
                    line.Y2 = line.Y1;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 1;

                    tempEdgeCanvas.Children.Clear();
                    tempEdgeCanvas.Children.Add(line);

                    MouseState = MouseStates.LinkStarted;
                }
                else if (MouseState == MouseStates.LinkStarted)
                {
                    e.Handled = false;

                    int lastIndex = tempEdge.EdgeParts.Count - 1;

                    Line line = tempEdge.EdgeParts[lastIndex].UILine;

                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 1;
                    line.X2 = nodeConnection.UIEllipse.Margin.Left + 3;
                    line.Y2 = nodeConnection.UIEllipse.Margin.Top + 3;

                    Node head = tempEdge.HeadConnection.Node;
                    Node tail = nodeConnection.Node;

                    tempEdge.TailConnection = nodeConnection;
                    head.edges.Add(tempEdge);
                    if (head != tail)
                        tail.edges.Add(tempEdge);

                    AddTempEdge();

                    MouseState = MouseStates.Normal;
                }
            }
        }

        public void OnNodeMouseDown(Node node, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (e.LeftButton != MouseButtonState.Pressed)
                return;
            if (MouseState == MouseStates.Normal)
            {
                Point p = e.GetPosition(UIBorder);
                if (!node.IsSelected)
                {
                    if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl))
                        SelectionManager.ClearSelection();
                    SelectionManager.AddSelectedNode(node);
                    MouseState = MouseStates.Moving;
                    lastMovePosition = new MyPoint(p.X, p.Y);
                }
                else
                {
                    lastMovePosition = new MyPoint(p.X, p.Y);
                    MouseState = MouseStates.Moving;
                }
            }
        }

        public void OnEdgeBreakMouseDown(EdgeBreak edgeBreak, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (MouseState == MouseStates.Normal)
            {
                Point p = e.GetPosition(UIBorder);
                if (!edgeBreak.IsSelected)
                {
                    SelectionManager.SelectedgeBreak(edgeBreak);
                    MouseState = MouseStates.Moving;
                    lastMovePosition = new MyPoint(p.X, p.Y);
                }
                else
                {
                    lastMovePosition = new MyPoint(p.X, p.Y);
                    MouseState = MouseStates.Moving;
                }
            }
        }

        public void OnEdgePartMouseDown(EdgePart edgePart, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (MouseState == MouseStates.Normal)
            {
                Point p = e.GetPosition(UIBorder);
                if (!edgePart.IsSelected)
                {
                    SelectionManager.SelectedgePart(edgePart);
                    MouseState = MouseStates.Moving;
                    lastMovePosition = new MyPoint(p.X, p.Y);
                }
                else
                {
                    lastMovePosition = new MyPoint(p.X, p.Y);
                    MouseState = MouseStates.Moving;
                }
            }
        }

        public void OnResizeConnectorMouseDown(ResizeConnector resizeConnector, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(UIBorder);
            lastMovePosition = new MyPoint(p.X, p.Y);
            SelectionManager.SelectedResizeConnector = resizeConnector;
            MouseState = MouseStates.ResizeStarted;
            e.Handled = true;
        }

        #endregion
    }

    public class LoadResult
    {
        public Graph Graph;
        public bool IsSuccessfull;
        public bool IsErrorsSkipped;

        public LoadResult(Graph graph, bool isSuccessfull, bool isErrorsSkipped)
        {
            this.Graph = graph;
            this.IsSuccessfull = isSuccessfull;
            this.IsErrorsSkipped = isErrorsSkipped;
        }
    }
}
