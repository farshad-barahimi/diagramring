//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;

using System.Text;
using Project.Shapes;
using System.Windows;
using Project.MainClasses;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Media;

namespace Project
{
    public class ShapeButton
    {
        public MyShape Shape;
        public MainWindow Owner;
        public Button Button;
        public Color BackgroundColor;
        public BackgroundColorStyle BackgroundStyle;
        public Color ForeGroundColor;

        public ShapeButton(MyShape shape, MainWindow owner, Color backgroundColor, Color foreGroundColor, BackgroundColorStyle backgroundStyle, WrapPanel wrapPanel)
        {
            this.Shape = shape;
            this.Owner = owner;
            this.Button = new Button(); ;
            this.BackgroundColor = backgroundColor;
            this.ForeGroundColor = foreGroundColor;
            this.BackgroundStyle = backgroundStyle;

            if(!Statics.ShapeLibrary.ContainsKey(shape.Name))
                Statics.ShapeLibrary.Add(shape.Name, shape);

            Canvas canvas = new Canvas();

            Button.Content = canvas;
            Button.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
            Button.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
            Button.Background = Brushes.White;

            Brush brush = new SolidColorBrush(Colors.SteelBlue);

            double x = (61 - shape.LeftSideWidth) / 2;
            double y = (35 - shape.LeftSideHeight) / 2;
            canvas.Margin = new Thickness(x, y, 0, 0);

            shape.Draw(shape.LeftSideWidth, shape.LeftSideHeight, new MyPoint(0, 0), brush, OnClick, canvas);
            Button.Width = 68;
            Button.Height = 45;
            Button.Padding = new Thickness(0);
            Button.Click += OnClick;
            Button.Margin = new Thickness(5);

            wrapPanel.Children.Add(Button);
        }

        public void OnClick(object sender, RoutedEventArgs e)
        {
            Graph graph = Owner.graph;

            graph.TempNode = new Node(graph, Shape.DefaultWidth, Shape.DefaultHeight, Shape, new MyPoint(100, 100));
            graph.AddNode(graph.TempNode);
            graph.TempNode.BackgroundColor = BackgroundColor;
            graph.TempNode.BackgroundColorStyle = BackgroundStyle;
            graph.TempNode.ForeGroundColor = ForeGroundColor;
            graph.TempNode.UpdateShape();
            graph.TempNode.BringIntoFront(null,null);


            graph.MouseState = MouseStates.InsertStarted;
            graph.SelectionManager.ClearSelection();
            graph.SelectionManager.AddSelectedNode(graph.TempNode);
            NativeMethods.SetCursorPos(300, 200);

            e.Handled = true;
        }
    }
}
