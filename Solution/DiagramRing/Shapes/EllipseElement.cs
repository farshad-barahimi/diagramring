//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;

using System.Text;
using Project.MainClasses;
using System.Windows.Media;

namespace Project.Shapes
{
    class EllipseElement:ShapeElement
    {
        public MyPoint Center { get; private set; }
        public double Radius1 { get; private set; }
        public double Radius2 { get; private set; }
        public bool UseNodeBackgroud { get; set; }
        public Brush LineBrush { get; set; }
        public Brush FillBrush { get; set; }
        public double LineBrushThickness { get; set; }

        public EllipseElement(MyPoint center, double radius1, double radius2)
        {
            this.Center = center;
            this.Radius1 = radius1;
            this.Radius2 = radius2;
            this.LineBrush = Brushes.Transparent;
            this.FillBrush = Brushes.Red;
            this.LineBrushThickness = 0;
            this.UseNodeBackgroud = true;
        }
    }
}
