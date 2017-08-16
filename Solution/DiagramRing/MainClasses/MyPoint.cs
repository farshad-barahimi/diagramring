//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System.Windows;
using System;

namespace Project.MainClasses
{
    public class MyPoint
    {
        private double x;
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                double last = x;
                x = value;
                if(Changed!=null)
                    Changed(last,y);
            }
        }

        private double y;
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                double last = y;
                y = value;
                if (Changed != null)
                    Changed(x,last);
            }
        }

        public event PointChangeDelegate Changed=null;

        public MyPoint(double X, double Y)
        {
            x = X; y = Y;
        }

        public Point ToPoint()
        {
            return new Point(X, Y);
        }

        public Point ToPoint(double scaleX,double scaleY)
        {
            return new Point(X*scaleX, Y*scaleY);
        }

        public double Lenght
        {
            get
            {
                return Math.Sqrt(x * x + y * y);
            }
        }

        public MyPoint Duplicate()
        {
            return new MyPoint(x, y);
        }
    }

    public delegate void PointChangeDelegate(double lastX,double lastY);
}
