//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;

using System.Text;
using Project.MainClasses;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace Project.Shapes
{
    class PathElement:ShapeElement
    {
        public bool IsClosed { get; set; }
        public MyPoint StartPoint { get; set; }
        public ObservableCollection<CommandElement> Commands { get; private set; }
        public Brush FillBrush { get; set; }
        public Brush LineBrush { get; set; }
        public bool UseNodeBackgroud { get; set; }

        public PathElement(bool isClosed)
        {
            Commands = new ObservableCollection<CommandElement>();
            this.IsClosed = isClosed;
            StartPoint = new MyPoint(0, 0);
            FillBrush = Brushes.Orange;
            LineBrush = Brushes.Transparent;
            this.UseNodeBackgroud = true;
        }
    }
}
