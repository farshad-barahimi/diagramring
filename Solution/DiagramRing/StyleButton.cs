//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using Project.MainClasses;
using System.Windows.Shapes;
using Project.Shapes;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace Project
{
    class StyleButton : Button
    {
        private Color backgroundColor;
        private BackgroundColorStyle backgroundStyle;
        private Color foregroundColor;
        private MainWindow owner;

        public StyleButton(MainWindow owner,Color backgroundColor, BackgroundColorStyle backgroundStyle, Color foregroundColor)
        {
            this.backgroundColor=backgroundColor;
            this.backgroundStyle=backgroundStyle;
            this.foregroundColor=foregroundColor;
            this.owner = owner;
            this.Background = Brushes.White;
            this.Width = 40;
            this.Height = 40;
            this.Margin = new System.Windows.Thickness(2);
            this.Focusable = false;

            Canvas canvas = new Canvas();
            this.Content = canvas;
            this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
            this.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;

            MyShape shape = ShapeFactory.CreateFlowchartSymbol9();
            shape.Draw(28, 28, new MyPoint(0, 0), Statics.CreateBackgroundBrush(backgroundColor,backgroundStyle), OnMouseDown, canvas);

            TextBlock textBlock = new TextBlock();
            textBlock.Foreground = new SolidColorBrush(foregroundColor);
            textBlock.Text = "A";
            textBlock.IsHitTestVisible = false;
            textBlock.Margin=new System.Windows.Thickness(10,5,0,0);

            canvas.Children.Add(textBlock);

            this.Click += OnClick;
        }

        public StyleButton Clone()
        {
            return new StyleButton(owner, backgroundColor, backgroundStyle, foregroundColor);
        }

        void OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (Node node in owner.graph.SelectionManager.SelectedNodes)
            {
                node.BackgroundColor = backgroundColor;
                node.BackgroundColorStyle = backgroundStyle;
                node.ForeGroundColor = foregroundColor;
                node.UpdateShape();
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        
    }
}
