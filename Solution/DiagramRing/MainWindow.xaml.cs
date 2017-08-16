// In the name of God
//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Project.Shapes;
using Project.MainClasses;
using System.Windows.Media.Effects;
using System.Threading;
using Microsoft.Win32;
using System.IO;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentFile = null;

        private SplashScreen splashScreen;
        public MainWindow()
        {
            InitializeComponent();

            init();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            updateTitle();

            Statics.ReadOptions();

            splashScreen.Close();

            if (Application.Current.Properties["InputFileName"] != null)
            {
                string inputFileName = Application.Current.Properties["InputFileName"].ToString();
                OpenPath(inputFileName,false);
                return;
            }

            if (Statics.IsShowStratupScreen)
            {
                var form = new StartupForm(this);
                form.ShowDialog();
            }
        }

        private void init()
        {
            splashScreen = new SplashScreen();
            splashScreen.Show();

            MainGrid.Margin = new Thickness(0, 5, 0, 0);

            Statics.FormBackground = this.Background;

            Statics.AddImageToButton(NewButton, "Tango Icons\\x-office-drawing.png");
            Statics.AddImageToButton(OpenButton, "Tango Icons\\document-open.png");
            Statics.AddImageToButton(SaveButton, "Tango Icons\\document-save.png");
            Statics.AddImageToButton(NewButton1, "Tango Icons\\x-office-drawing.png");
            Statics.AddImageToButton(OpenButton1, "Tango Icons\\document-open.png");
            Statics.AddImageToButton(SaveButton1, "Tango Icons\\document-save.png");
            Statics.AddImageToButton(SaveAsButton, "Tango Icons\\document-save-as.png");
            Statics.AddImageToButton(ClipDocumentButton, "Clip48.png");

            Statics.AddImageToButton(ExporttButton, "Tango Icons\\image-x-generic.png");
            Statics.AddImageToButton(WebsiteButton, "Tango Icons\\internet-web-browser.png");
            Statics.AddImageToButton(AboutButton, "Tango Icons\\dialog-information.png");
            Statics.AddImageToButton(AboutButton1, "Tango Icons\\dialog-information.png");
            Statics.AddImageToButton(HelpButton, "Tango Icons\\help-browser.png");
            Statics.AddImageToButton(HelpButton1, "Tango Icons\\help-browser.png");
            Statics.AddImageToButton(StartupButton, "Tango Icons\\go-home.png");

            DefaultEdgeTypeComboBox.ToolTip = "Default edge ends";

            Statics.AddImageToComboBox(DefaultEdgeTypeComboBox, "arrow1.png");
            Statics.AddImageToComboBox(DefaultEdgeTypeComboBox, "arrow2.png");
            Statics.AddImageToComboBox(DefaultEdgeTypeComboBox, "arrow3.png");
            Statics.AddImageToComboBox(DefaultEdgeTypeComboBox, "arrow4.png");
            Statics.AddImageToComboBox(DefaultEdgeTypeComboBox, "arrow5.png");
            Statics.AddImageToComboBox(DefaultEdgeTypeComboBox, "arrow6.png");
            Statics.AddImageToComboBox(DefaultEdgeTypeComboBox, "arrow7.png");
            DefaultEdgeTypeComboBox.SelectedIndex = 1;

            DefaultEdgeBrushComboBox.ToolTip = "Default edge brush";
            Statics.AddImageToComboBox(DefaultEdgeBrushComboBox, "arrow1.png");
            Statics.AddImageToComboBox(DefaultEdgeBrushComboBox, "arrow8.png");
            Statics.AddImageToComboBox(DefaultEdgeBrushComboBox, "arrow9.png");
            DefaultEdgeBrushComboBox.SelectedIndex = 0;

            defineShapes();
            defineStyles();

            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            newGraph();
            this.PreviewKeyDown += new KeyEventHandler(OnKeyDown);
        }

        private void defineShapes()
        {
            #region Shapes definition

            Statics.ShapeLibrary = new Dictionary<string, MyShape>();

            Color color1 = Color.FromArgb(255, 31, 73, 125);
            Color color2 = Color.FromArgb(255, 247, 150, 70);
            Color color3 = Colors.White;

            MyShape shape = ShapeFactory.CreateEllipse();
            new ShapeButton(shape, this, color1, Colors.Black, BackgroundColorStyle.Radial, MainShapesWrapPanel);
            new ShapeButton(shape, this, color1, Colors.Black, BackgroundColorStyle.Radial, FlowchartWrapPanel);
            new ShapeButton(shape, this, color1, Colors.Black, BackgroundColorStyle.Radial, ERDWrapPanel);

            shape = ShapeFactory.CreateRectangle();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, MainShapesWrapPanel);
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, FlowchartWrapPanel);
            new ShapeButton(shape, this, color2, Colors.Black, BackgroundColorStyle.Solid, DFDWrapPanel);
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, ERDWrapPanel);

            shape = ShapeFactory.CreateRhombus();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, MainShapesWrapPanel);
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, FlowchartWrapPanel);
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, ERDWrapPanel);

            shape = ShapeFactory.CreateText();
            new ShapeButton(shape, this, Colors.White, Colors.Black, BackgroundColorStyle.Solid, MainShapesWrapPanel);

            shape = ShapeFactory.CreateTriangle();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, MainShapesWrapPanel);
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreateParallelogram();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, MainShapesWrapPanel);
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreateDFDSymbol1();
            new ShapeButton(shape, this, color2, Colors.Black, BackgroundColorStyle.Solid, DFDWrapPanel);

            shape = ShapeFactory.CreateDFDSymbol2();
            new ShapeButton(shape, this, color2, Colors.Black, BackgroundColorStyle.Radial, DFDWrapPanel);

            shape = ShapeFactory.CreateFlowchartSymbol1();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreateFlowchartSymbol2();
            new ShapeButton(shape, this, color3, Colors.Black, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreateFlowchartSymbol3();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreateFlowchartSymbol4();
            new ShapeButton(shape, this, color3, Colors.Black, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreateFlowchartSymbol5();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreateFlowchartSymbol6();
            new ShapeButton(shape, this, color3, Colors.Black, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreateFlowchartSymbol7();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreateFlowchartSymbol8();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreateFlowchartSymbol9();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreateFlowchartSymbol10();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreateFlowchartSymbol11();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreateFlowchartSymbol12();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreateFlowchartSymbol13();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreateFlowchartSymbol14();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreateFlowchartSymbol15();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreateFlowchartSymbol16();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, FlowchartWrapPanel);

            shape = ShapeFactory.CreatePieChart();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, ChartsWrapPanel);

            shape = ShapeFactory.CreateBarChart();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, ChartsWrapPanel);

            shape = ShapeFactory.CreateHorizontalLine();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, MainShapesWrapPanel);

            shape = ShapeFactory.CreateVerticalLine();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, MainShapesWrapPanel);

            shape = ShapeFactory.CreateSimpleConnector();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, MainShapesWrapPanel);

            shape = ShapeFactory.CreateUMLClass();
            new ShapeButton(shape, this, Colors.White, Colors.Black, BackgroundColorStyle.Solid, UMLWrapPanel);

            shape = ShapeFactory.CreateUMLPackage();
            new ShapeButton(shape, this, Colors.White, Colors.Black, BackgroundColorStyle.Solid, UMLWrapPanel);

            shape = ShapeFactory.CreateUMLActor();
            new ShapeButton(shape, this, Colors.White, Colors.Black, BackgroundColorStyle.Solid, UMLWrapPanel);

            shape = ShapeFactory.CreateUMLUseCase();
            new ShapeButton(shape, this, Colors.White, Colors.Black, BackgroundColorStyle.Radial, UMLWrapPanel);

            shape = ShapeFactory.CreateUMLNote();
            new ShapeButton(shape, this, Colors.White, Colors.Black, BackgroundColorStyle.Solid, UMLWrapPanel);

            shape = ShapeFactory.CreateUMLComponent();
            new ShapeButton(shape, this, Colors.White, Colors.Black, BackgroundColorStyle.Solid, UMLWrapPanel);

            shape = ShapeFactory.CreateUMLNode();
            new ShapeButton(shape, this, Colors.White, Colors.Black, BackgroundColorStyle.Solid, UMLWrapPanel);

            shape = ShapeFactory.CreateERDSymbol1();
            new ShapeButton(shape, this, color1, Colors.Black, BackgroundColorStyle.Radial, ERDWrapPanel);
            
            shape = ShapeFactory.CreateERDSymbol2();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, ERDWrapPanel);

            shape = ShapeFactory.CreateERDSymbol3();
            new ShapeButton(shape, this, color1, Colors.White, BackgroundColorStyle.Solid, ERDWrapPanel);

            #endregion

        }

        private void defineStyles()
        {
            StyleButton button;

            var color1 = Color.FromArgb(255, 31, 73, 125);
            button = new StyleButton(this, color1, BackgroundColorStyle.Radial, Colors.Black);
            addStyleButton(button);

            var color2 = Color.FromArgb(255, 247, 150, 70);
            button = new StyleButton(this, color2, BackgroundColorStyle.Radial, Colors.Black);
            addStyleButton(button);

            var color3 = Color.FromArgb(255, 192, 0, 0);
            button = new StyleButton(this, color3, BackgroundColorStyle.Radial, Colors.Black);
            addStyleButton(button);

            var color4 = Color.FromArgb(255, 255, 192, 0);
            button = new StyleButton(this, color4, BackgroundColorStyle.Radial, Colors.Black);
            addStyleButton(button);

            var color5 = Color.FromArgb(255, 79, 129, 189);
            button = new StyleButton(this, color5, BackgroundColorStyle.Radial, Colors.Black);
            addStyleButton(button);

            var color6 = Color.FromArgb(255, 155, 187, 89);
            button = new StyleButton(this, color6, BackgroundColorStyle.Radial, Colors.Black);
            addStyleButton(button);

            var color7 = Color.FromArgb(255, 128, 100, 162);
            button = new StyleButton(this, color7, BackgroundColorStyle.Radial, Colors.Black);
            addStyleButton(button);


            button = new StyleButton(this, color1, BackgroundColorStyle.Solid, Colors.White);
            addStyleButton(button);

            button = new StyleButton(this, color2, BackgroundColorStyle.Solid, Colors.Black);
            addStyleButton(button);

            button = new StyleButton(this, color3, BackgroundColorStyle.Solid, Colors.White);
            addStyleButton(button);

            button = new StyleButton(this, color4, BackgroundColorStyle.Solid, Colors.Black);
            addStyleButton(button);

            button = new StyleButton(this, color5, BackgroundColorStyle.Solid, Colors.White);
            addStyleButton(button);

            button = new StyleButton(this, color6, BackgroundColorStyle.Solid, Colors.White);
            addStyleButton(button);

            button = new StyleButton(this, color7, BackgroundColorStyle.Solid, Colors.White);
            addStyleButton(button);

            
            
            button = new StyleButton(this, color1, BackgroundColorStyle.TopLeftToBottomRight, Colors.White);
            addStyleButton(button);

            button = new StyleButton(this, Colors.White, BackgroundColorStyle.Solid, Colors.Black);
            addStyleButton(button);
        }

        private void addStyleButton(StyleButton button)
        {
            StyleStackPanel.Children.Add(button);
            StyleWrapPanel.Children.Add(button.Clone());
        }

        public Graph graph;
        private void newGraph()
        {
            graph = new Graph(1000,1000,this);
            scrollViewer.Content = Graph.UIBorder;
            graph.SelectionManager.SelectionChanged += graph_SelectionChanged;
            graph.MouseStateChanged += graph_MouseStateChanged;
            graph_MouseStateChanged();
            currentFile = null;
        }

        void graph_MouseStateChanged()
        {
            if (graph.MouseState == MouseStates.LinkNormal || graph.MouseState == MouseStates.LinkStarted)
            {
                if (!ConnectModeRadioButton.IsChecked.Value)
                    ConnectModeRadioButton.IsChecked = true;
            }
            else
            {
                if (!SelectModeRadioButton.IsChecked.Value)
                    SelectModeRadioButton.IsChecked = true;
            }
        }

        void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (graph.SelectionManager.IsOnlyNodesSelected())
            {
                int difx=0,dify=0;
                Point p=Mouse.GetPosition(Graph.UIBorder);
                if (e.Key == Key.Down)
                    dify = 1;
                if (e.Key == Key.Up)
                    dify = -1;
                if (e.Key == Key.Left)
                    difx = -1;
                if (e.Key == Key.Right)
                    difx = 1;

                if (difx != 0 || dify != 0)
                {
                    if (graph.MoveSelected(difx, dify, new MyPoint(p.X, p.Y)) == false)
                        graph.MoveSelected(-difx, -dify, new MyPoint(p.X, p.Y));
                    e.Handled = true;
                }

                if (e.Key == Key.Delete)
                {
                    List<Node> temp = new List<Node>();
                    foreach (Node node in graph.SelectionManager.SelectedNodes)
                        temp.Add(node);

                    foreach(Node node in temp)
                        node.Remove(null, null);
                }
            }

            if (graph.SelectionManager.IsMultipleSelected())
                return;

            if (e.Key == Key.Delete)
            {
                var edgeBreak = graph.SelectionManager.SelectedEdgeBreak;
                if (edgeBreak != null)
                    if (edgeBreak.IsRemovable())
                        edgeBreak.Remove(null, null);

                if (graph.SelectionManager.SelectedEdgePart != null)
                    graph.SelectionManager.SelectedEdgePart.Edge.Remove(null, null);
                
            }
            if (e.Key == Key.B)
            {
                if (graph.SelectionManager.SelectedEdgePart != null)
                    graph.SelectionManager.SelectedEdgePart.Break(null, null);
            }
            if (e.Key == Key.Enter)
            {
                if (graph.SelectionManager.SelectedNodes.Count == 1)
                    graph.SelectionManager.SelectedNodes[0].PropertiesWindow(null, null);
                if (graph.SelectionManager.SelectedEdgePart != null)
                    graph.SelectionManager.SelectedEdgePart.Properties(null, null);
            }
            if (e.Key == Key.Space)
            {
                if (graph.SelectionManager.SelectedEdgePart != null)
                    graph.SelectionManager.SelectedEdgePart.Edge.Properties(null, null);
            }

            if (e.Key == Key.D)
            {
                if (graph.SelectionManager.SelectedNodes.Count == 1)
                    graph.SelectionManager.SelectedNodes[0].Duplicate(null, null);
            }
        }

        
        void graph_SelectionChanged()
        {
            FunctionsStackPanel.Children.Clear();
            StylesExpander.Visibility = System.Windows.Visibility.Hidden;
            FunctionsExpander.Visibility = System.Windows.Visibility.Collapsed;

            if (graph.SelectionManager.IsMultipleSelected())
            {
                if (graph.SelectionManager.IsOnlyNodesSelected())
                {
                    FunctionsExpander.Visibility = System.Windows.Visibility.Visible;

                    Button button = new Button();
                    button.Background = Brushes.White;
                    button.Margin = new Thickness(2);
                    button.Content = "Remove nodes (Del)";
                    foreach (Node node in graph.SelectionManager.SelectedNodes)
                        button.Click += node.Remove;

                    Statics.AddImageToButton(button, "Tango Icons\\edit-delete.png");
                    FunctionsStackPanel.Children.Add(button);

                    StylesExpander.Visibility = System.Windows.Visibility.Visible;
                }
                return;
            }
            else if (graph.SelectionManager.SelectedNodes.Count == 1)
            {
                FunctionsExpander.Visibility = System.Windows.Visibility.Visible;
                WrapPanel nodeButtonsWrapPanel = new WrapPanel() { Orientation = Orientation.Horizontal };
                FunctionsStackPanel.Children.Add(nodeButtonsWrapPanel);

                Button button = new Button();
                button.Background = Brushes.White;
                button.Margin = new Thickness(2);
                button.Content = "Node properties (Enter)";
                button.Click += graph.SelectionManager.SelectedNodes[0].PropertiesWindow;
                Statics.AddImageToButton(button, "Tango Icons\\document-properties.png");
                nodeButtonsWrapPanel.Children.Add(button);

                button = new Button();
                button.Background = Brushes.White;
                button.Margin = new Thickness(2);
                button.Content = "Remove node (Del)";
                button.Click += graph.SelectionManager.SelectedNodes[0].Remove;
                Statics.AddImageToButton(button, "Tango Icons\\edit-delete.png");
                nodeButtonsWrapPanel.Children.Add(button);

                button = new Button();
                button.Background = Brushes.White;
                button.Margin = new Thickness(2);
                button.Content = "Bring into front";
                button.Click += graph.SelectionManager.SelectedNodes[0].BringIntoFront;
                Statics.AddImageToButton(button, "Tango Icons\\go-top.png");
                nodeButtonsWrapPanel.Children.Add(button);

                button = new Button();
                button.Background = Brushes.White;
                button.Margin = new Thickness(2);
                button.Content = "Send to back";
                button.Click += graph.SelectionManager.SelectedNodes[0].SendToBack;
                Statics.AddImageToButton(button, "Tango Icons\\go-bottom.png");
                nodeButtonsWrapPanel.Children.Add(button);

                button = new Button();
                button.Background = Brushes.White;
                button.Margin = new Thickness(2);
                button.Content = "Duplicate (D)";
                button.Click += graph.SelectionManager.SelectedNodes[0].Duplicate;
                Statics.AddImageToButton(button, "Tango Icons\\edit-copy.png");
                nodeButtonsWrapPanel.Children.Add(button);

                StylesExpander.Visibility = System.Windows.Visibility.Visible;
            }
            else if (graph.SelectionManager.SelectedEdgePart != null)
            {
                FunctionsExpander.Visibility = System.Windows.Visibility.Visible;

                Label label = new Label();
                label.Content = "Whole edge functions :";
                label.Margin = new Thickness(5);
                FunctionsStackPanel.Children.Add(label);

                StackPanel edgeButtonsStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
                FunctionsStackPanel.Children.Add(edgeButtonsStackPanel);

                Button button = new Button();
                button.Background = Brushes.White;
                button.Margin = new Thickness(2);
                button.Content = "Edge properties (Space)";
                button.Click += graph.SelectionManager.SelectedEdgePart.Edge.Properties;
                Statics.AddImageToButton(button, "Tango Icons\\document-properties.png");
                edgeButtonsStackPanel.Children.Add(button);

                button = new Button();
                button.Background = Brushes.White;
                button.Margin = new Thickness(2);
                button.Content = "Remove Edge (Del)";
                button.Click += graph.SelectionManager.SelectedEdgePart.Edge.Remove;
                Statics.AddImageToButton(button, "Tango Icons\\edit-delete.png");
                edgeButtonsStackPanel.Children.Add(button);

                label = new Label();
                label.Content = "Edge part functions :";
                label.Margin = new Thickness(5);
                FunctionsStackPanel.Children.Add(label);

                StackPanel edgePartButtonsStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
                FunctionsStackPanel.Children.Add(edgePartButtonsStackPanel);

                button = new Button();
                button.Background = Brushes.White;
                button.Margin = new Thickness(2);
                button.Content = "Edge part properties (Enter)";
                button.Click += graph.SelectionManager.SelectedEdgePart.Properties;
                Statics.AddImageToButton(button, "Tango Icons\\document-properties.png");
                edgePartButtonsStackPanel.Children.Add(button);

                button = new Button();
                button.Background = Brushes.White;
                button.Margin = new Thickness(2);
                button.Content = "Break Edge part (B)";
                button.Click += graph.SelectionManager.SelectedEdgePart.Break;
                Statics.AddImageToButton(button, "Break48.png");
                edgePartButtonsStackPanel.Children.Add(button);
            }
            else if (graph.SelectionManager.SelectedEdgeBreak != null)
            {
                FunctionsExpander.Visibility = System.Windows.Visibility.Visible;
                graph.UICanvas.ContextMenu = new ContextMenu();

                var edgeBreak = graph.SelectionManager.SelectedEdgeBreak;
                if (edgeBreak.IsRemovable())
                {
                    Button button = new Button();
                    button.Background = Brushes.White;
                    button.Margin = new Thickness(2);
                    button.Content = "Remove edge break (Del)";
                    button.Click += edgeBreak.Remove;
                    Statics.AddImageToButton(button, "Tango Icons\\edit-delete.png");
                    FunctionsStackPanel.Children.Add(button);
                }
                else
                {
                    var textblock = new TextBlock();
                    textblock.Text = "It can't be removed\nTo remove it, remove the edge";
                    FunctionsStackPanel.Children.Add(textblock);
                }
            }
        }

        private MyShape getShapeByName(string name)
        {
            return Statics.ShapeLibrary[name];
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        private void ExporttButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "(*.Jpg) | *.jpg;";
            dialog.DefaultExt = ".jpg";
            if (dialog.ShowDialog().Value == true)
            {
                double maxWidth = 0;
                double maxHeight = 0;

                foreach (Node node in graph.Nodes)
                {
                    if (node.Position.X + node.Width >= maxWidth)
                        maxWidth = node.Position.X + node.Width;

                    if (node.Position.Y + node.Height >= maxHeight)
                        maxHeight = node.Position.Y + node.Height;

                    foreach(Edge edge in node.edges)
                        foreach (EdgeBreak edgeBreak in edge.EdgeBreaks)
                        {
                            if (edgeBreak.Position.X > maxWidth)
                                maxWidth = edgeBreak.Position.X;
                            if (edgeBreak.Position.Y > maxHeight)
                                maxHeight = edgeBreak.Position.Y;
                        }
                }

                maxWidth += 50;
                maxHeight += 50;

                maxWidth = Math.Min(maxWidth, graph.Width - 2);
                maxHeight = Math.Min(maxHeight, graph.Height - 2);

                int height = (int)maxHeight;
                int width = (int)maxWidth;

                RenderTargetBitmap bmp = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
                bmp.Render(graph.UICanvas);

                string file = dialog.FileName;

                BitmapEncoder encoder;
                encoder = new JpegBitmapEncoder();

                encoder.Frames.Add(BitmapFrame.Create(bmp));

                using (Stream stm = File.Create(file))
                {
                    encoder.Save(stm);
                }
            }
        }

        private void WebsiteButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.farshadoo.com/");
        }

        private void DefaultEdgeTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (graph == null)
                return;
            switch (DefaultEdgeTypeComboBox.SelectedIndex)
            {
                case 0:
                    graph.DefaultHeadSymbol = EndSymbol.None;
                    graph.DefaultTailSymbol = EndSymbol.None;
                    break;
                case 1:
                    graph.DefaultHeadSymbol = EndSymbol.None;
                    graph.DefaultTailSymbol = EndSymbol.FilledTriangle;
                    break;
                case 2:
                    graph.DefaultHeadSymbol = EndSymbol.None;
                    graph.DefaultTailSymbol = EndSymbol.EmptyTriangle;
                    break;
                case 3:
                    graph.DefaultHeadSymbol = EndSymbol.None;
                    graph.DefaultTailSymbol = EndSymbol.FilledRhumbus;
                    break;
                case 4:
                    graph.DefaultHeadSymbol = EndSymbol.None;
                    graph.DefaultTailSymbol = EndSymbol.EmptyRhombus;
                    break;
                case 5:
                    graph.DefaultHeadSymbol = EndSymbol.None;
                    graph.DefaultTailSymbol = EndSymbol.SimpleArrow;
                    break;
                case 6:
                    graph.DefaultHeadSymbol = EndSymbol.SimpleArrow;
                    graph.DefaultTailSymbol = EndSymbol.SimpleArrow;
                    break;
            }
        }

        private void DefaultEdgeBrushComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (graph == null)
                return;
            switch (DefaultEdgeBrushComboBox.SelectedIndex)
            {
                case 0:
                    graph.DefaultEdgeBrushType = EdgeBrushType.Solid;
                    break;
                case 1:
                    graph.DefaultEdgeBrushType = EdgeBrushType.Dashed;
                    break;
                case 2:
                    graph.DefaultEdgeBrushType = EdgeBrushType.Dotted;
                    break;
            }
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Any unsaved changes in this document will be lost. Are you sure?", "Warning", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            NewFile();
        }

        public void NewFile()
        {
            newGraph();
            currentFile = null;
            updateTitle();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Any unsaved changes in this document will be lost. Are you sure?", "Warning", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            OpenFile();
        }

        public void OpenFile()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Diagram Ring 2 or higher files (*.drx)|*.drx|Diagram Ring 1 files(*.dr1)|*.dr1";
            if (dialog.ShowDialog().Value)
            {
                OpenPath(dialog.FileName, false);
            }
        }

        public void OpenPath(string path,bool forceResave)
        {
            LoadResult result = Graph.Load(path, this, Statics.ShapeLibrary);
            graph = result.Graph;
            if (!result.IsSuccessfull)
            {
                newGraph();
                currentFile = null;
                updateTitle();
                MessageBox.Show("There was an error reading the file");
            }
            else
            {
                scrollViewer.Content = Graph.UIBorder;
                graph.SelectionManager.SelectionChanged += graph_SelectionChanged;
                graph.MouseStateChanged += graph_MouseStateChanged;
                graph_MouseStateChanged();

                if (forceResave)
                    currentFile = null;
                else if (System.IO.Path.GetExtension(path) == ".dr1")
                    currentFile = null;
                else
                    currentFile = path;
                updateTitle();


                foreach (Node node in graph.Nodes)
                    node.UpdateShape();

                foreach (Edge edge in graph.Edges)
                    foreach (EdgePart edgepart in edge.EdgeParts)
                    {
                        foreach (EdgeLabel edgeLabel in edgepart.EdgeLabels)
                            edgeLabel.UITextBlock.UpdateLayout();
                        edgepart.UpdateLabels();
                    }

                graph.SelectionManager.ClearSelection();
                if (result.IsErrorsSkipped)
                    MessageBox.Show("File openned successfully , but some errors skipped.");
                
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentFile == null)
                SaveAsButton_Click(sender, e);
            else
            {
                graph.Save(currentFile);
                updateTitle();
                MessageBox.Show("Saved successfully");
            }
        }

        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.DefaultExt = ".drx";
            dialog.Filter = "(*.drx)|*.drx";
            if (dialog.ShowDialog().Value)
            {
                if (graph.Save(dialog.FileName))
                {
                    currentFile = dialog.FileName;
                    updateTitle();
                    MessageBox.Show("Saved successfully");
                }
                else
                {
                    MessageBox.Show("There was an error saving the document");
                }
            }
        }

        private void updateTitle()
        {
            double d = Math.Round(Statics.CurrentVersion, 1);
            string title = "Diagram Ring " + d+"  - ";

            if (currentFile == null)
                title += "Unsaved document";
            else
            {
                title += System.IO.Path.GetFileName(currentFile);
            }

            this.Title = title;
        }

        private void ClipDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            double maxWidth = 0;
            double maxHeight = 0;

            foreach (Node node in graph.Nodes)
            {
                if (node.Position.X + node.Width >= maxWidth)
                    maxWidth = node.Position.X + node.Width;

                if (node.Position.Y + node.Height >= maxHeight)
                    maxHeight = node.Position.Y + node.Height;

                foreach (Edge edge in node.edges)
                    foreach (EdgeBreak edgeBreak in edge.EdgeBreaks)
                    {
                        if (edgeBreak.Position.X > maxWidth)
                            maxWidth = edgeBreak.Position.X;
                        if (edgeBreak.Position.Y > maxHeight)
                            maxHeight = edgeBreak.Position.Y;
                    }
            }

            maxWidth += 50;
            maxHeight += 50;

            maxWidth = Math.Max(maxWidth, 500);
            maxHeight = Math.Max(maxHeight, 500);

            graph.Width = (int)maxWidth;
            graph.Height = (int)maxHeight;
        }

        private void TopTab_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            border.Background = Brushes.Orange;
        }

        private void TopTab_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            if (border.HorizontalAlignment != HorizontalAlignment.Left)
                border.Background = Brushes.Transparent;
            else
                border.Background = Brushes.Red;
        }

        private void TopTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border selectedBorder = (Border)sender;
            
            foreach (UIElement element in TopTabStackPanel.Children)
            {
                Border border = (Border)element;
                if (border == selectedBorder)
                {
                    border.Background = Brushes.Red;
                    border.HorizontalAlignment = HorizontalAlignment.Left;
                }
                else
                {
                    border.Background = Brushes.Transparent;
                    border.HorizontalAlignment = HorizontalAlignment.Right;
                }
            }

            foreach (UIElement element in TabStackPanel.Children)
            {
                StackPanel stackPanel = (StackPanel)element;
                stackPanel.Visibility = Visibility.Collapsed;
            }

            if (selectedBorder == HomeTabBorder)
                HomeStackPanel.Visibility = Visibility.Visible;
            else if (selectedBorder == DocumentTabBorder)
                DocumentStackPanel.Visibility = Visibility.Visible;
            else if (selectedBorder == StyleTabBorder)
                StyleStackPanel.Visibility = Visibility.Visible;
            else if (selectedBorder == HelpTabBorder)
                HelpStackPanel.Visibility = Visibility.Visible;
        }

        private void LeftTab_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            border.Background = Brushes.Orange;
        }

        private void LeftTab_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            if (border.VerticalAlignment != VerticalAlignment.Top)
                border.Background = Brushes.Transparent;
            else
                border.Background = Brushes.Red;
        }

        private void LeftTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border selectedBorder = (Border)sender;

            foreach (UIElement element in LeftTabStackPanel.Children)
            {
                Border border = (Border)element;
                if (border == selectedBorder)
                {
                    border.Background = Brushes.Red;
                    border.VerticalAlignment = VerticalAlignment.Top;
                }
                else
                {
                    border.Background = Brushes.Transparent;
                    border.VerticalAlignment = VerticalAlignment.Bottom;
                }
            }

            foreach (UIElement element in LeftStackPanel.Children)
            {
                WrapPanel wrapPanel = (WrapPanel)element;
                wrapPanel.Visibility = Visibility.Collapsed;
            }

            if (selectedBorder == MainShapesTabBorder)
                MainShapesWrapPanel.Visibility = Visibility.Visible;
            else if (selectedBorder == DFDTabBorder)
                DFDWrapPanel.Visibility = Visibility.Visible;
            else if (selectedBorder == FlowchartTabBorder)
                FlowchartWrapPanel.Visibility = Visibility.Visible;
            else if (selectedBorder == ChartsTabBorder)
                ChartsWrapPanel.Visibility = Visibility.Visible;
            else if (selectedBorder == UMLTabBorder)
                UMLWrapPanel.Visibility = Visibility.Visible;
            else if (selectedBorder == ERDTabBorder)
                ERDWrapPanel.Visibility = Visibility.Visible;
        }

        private void StartupButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new StartupForm(this);
            form.ShowDialog();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Statics.AppStartPath + "\\Help.pdf");
        }

        private void SelectModeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if(graph!=null)
                graph.MouseState = MouseStates.Normal;
        }

        private void ConnectModeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (graph != null)
                graph.MouseState = MouseStates.LinkNormal;
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PasteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CutButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public partial class NativeMethods
    {
        /// Return Type: BOOL->int  
        ///X: int  
        ///Y: int  
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "SetCursorPos")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool SetCursorPos(int X, int Y);
    } 
}
