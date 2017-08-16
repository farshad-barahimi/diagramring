//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project.MainClasses
{
    public class NodeConnection
    {
        #region Private variables
        
        private readonly MyPoint abstractPosition;

        #endregion

        #region Properties

        public Node Node { get; private set; }
        public Ellipse UIEllipse { get; private set; }
        
        private bool isVisible;
        public bool IsVisible
        {
            get
            {
                return isVisible;
            }
            set
            {
                isVisible = value;
                if(isVisible)
                    UIEllipse.Visibility = Visibility.Visible;
                else
                    UIEllipse.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        #region Public Methods

        public NodeConnection(MyPoint abstractPosition, Node node)
        {
            this.abstractPosition = abstractPosition;
            this.Node = node;
            this.UIEllipse = new Ellipse();
            UIEllipse.Margin = new Thickness(abstractPosition.X, abstractPosition.Y, 0, 0);
            UIEllipse.Width = 7;
            UIEllipse.Height = 7;
            UIEllipse.Fill = Brushes.White;
            UIEllipse.Stroke = Brushes.Black;
            UIEllipse.StrokeThickness = 1;
            UIEllipse.MouseEnter += onMouseEnter;
            UIEllipse.MouseLeave += onMouseLeave;
            UIEllipse.MouseDown += onMouseDown;

            node.NodeConnections.Add(this);
            node.Graph.UICanvas.Children.Add(UIEllipse);
            Canvas.SetZIndex(UIEllipse, Statics.NodeConnectionZIndex);
            Update();


            IsVisible = false ;
        }

        public void Update()
        {
            double scaleX = Node.Width / (double)Node.Shape.Width;
            double scaleY = Node.Height / (double)Node.Shape.Height;
            UIEllipse.Margin = new Thickness(Node.Position.X+abstractPosition.X * scaleX-3, Node.Position.Y+ abstractPosition.Y * scaleY-3,0,0);
        }

        #endregion

        #region Private methods

        private void onMouseDown(object sender, MouseButtonEventArgs e)
        {
            Node.Graph.OnNodeConnectionMouseDown(this, e);
        }

        private void onMouseLeave(object sender, MouseEventArgs e)
        {
            UIEllipse.Fill = Brushes.White;
            UIEllipse.Stroke = Brushes.Black;
        }

        private void onMouseEnter(object sender, MouseEventArgs e)
        {
            UIEllipse.Fill = Brushes.Black;
            UIEllipse.Stroke = Brushes.Black;
        }

        #endregion
    }
}
