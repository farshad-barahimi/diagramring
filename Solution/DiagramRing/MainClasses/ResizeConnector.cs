//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace Project.MainClasses
{
    public class ResizeConnector
    {
        #region Properties

        public Rectangle UIRectangle { get; private set; }
        public Node Owner { get; private set; }
        public bool IsVisible
        {
            set
            {
                if (value == true)
                    UIRectangle.Visibility = Visibility.Visible;
                else
                    UIRectangle.Visibility = Visibility.Hidden;
            }
        }

        #endregion

        #region Public methods

        public ResizeConnector(Node owner)
        {
            this.Owner = owner;
            UIRectangle = new Rectangle();

            UIRectangle.Width = 10;
            UIRectangle.Height = 10;
            UIRectangle.Fill = Brushes.White;
            UIRectangle.Stroke = Brushes.Black;
            UIRectangle.StrokeThickness = 1;
            UIRectangle.MouseDown += onMouseDown;
            UIRectangle.MouseEnter += onMouseEnter;
            UIRectangle.MouseLeave += onMouseLeave;
        }

        #endregion 

        #region Private methods

        private void onMouseEnter(object sender, MouseEventArgs e)
        {
            UIRectangle.Fill = Brushes.Yellow;
        }

        private void onMouseLeave(object sender, MouseEventArgs e)
        {
            UIRectangle.Fill = Brushes.White;
        }

        private void onMouseDown(object sender, MouseButtonEventArgs e)
        {
            Owner.Graph.OnResizeConnectorMouseDown(this,e);
        }

        #endregion
    }
}
