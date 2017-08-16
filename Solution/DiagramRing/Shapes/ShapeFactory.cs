//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;

using System.Text;
using Project.MainClasses;
using System.Windows.Media;
using System.Windows;

namespace Project.Shapes
{
    public static class ShapeFactory
    {
        public static MyShape CreateEllipse()
        {
            MyShape shape = new MyShape("Ellipse",200,100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.Childs.Add(new EllipseElement(new MyPoint(100, 50), 100, 50));

            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 50));

            shape.ConnectionPoints.Add(new MyPoint(40, 10));
            shape.ConnectionPoints.Add(new MyPoint(160, 10));
            
            shape.ConnectionPoints.Add(new MyPoint(40, 90));
            shape.ConnectionPoints.Add(new MyPoint(160, 90));
            return shape;
        }

        public static MyShape CreateRectangle()
        {
            MyShape shape = new MyShape("Rectangle", 200, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;
            
            shape.ConnectionPoints.Add(new MyPoint(0,0));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 0));
            
            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(150, 100));

            shape.ConnectionPoints.Add(new MyPoint(0, 25));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(0, 75));

            shape.ConnectionPoints.Add(new MyPoint(200, 25));
            shape.ConnectionPoints.Add(new MyPoint(200, 50));
            shape.ConnectionPoints.Add(new MyPoint(200, 75));

            
            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 0);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(0, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 0), null);
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateRhombus()
        {
            MyShape shape = new MyShape("Rhombus", 100, 100);

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 50));
            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));

            shape.ConnectionPoints.Add(new MyPoint(25, 25));
            shape.ConnectionPoints.Add(new MyPoint(75, 25));
            shape.ConnectionPoints.Add(new MyPoint(25, 75));
            shape.ConnectionPoints.Add(new MyPoint(75, 75));

            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(50, 0);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(100, 50), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(50, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(0, 50), null);
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateTriangle()
        {
            MyShape shape = new MyShape("Triangle", 100, 50);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 100;

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 50));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));

            shape.ConnectionPoints.Add(new MyPoint(25, 25));
            shape.ConnectionPoints.Add(new MyPoint(75, 25));
            shape.ConnectionPoints.Add(new MyPoint(50, 50));

            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(50, 0);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(100, 50), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(0, 50), null);
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateParallelogram()
        {
            MyShape shape = new MyShape("Parallelogram", 250, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));
            shape.ConnectionPoints.Add(new MyPoint(250, 0));

            shape.ConnectionPoints.Add(new MyPoint(125, 0));
            shape.ConnectionPoints.Add(new MyPoint(175, 0));

            shape.ConnectionPoints.Add(new MyPoint(75, 100));
            shape.ConnectionPoints.Add(new MyPoint(125, 100));

            shape.ConnectionPoints.Add(new MyPoint(25, 50));

            shape.ConnectionPoints.Add(new MyPoint(225, 50));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(50, 0);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(0, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(250, 0), null);
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateDFDSymbol1()
        {
            MyShape shape = new MyShape("DFDSymbol1", 200, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(0, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(150, 100));

            shape.ConnectionPoints.Add(new MyPoint(0, 25));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(0, 75));

            shape.ConnectionPoints.Add(new MyPoint(200, 25));
            shape.ConnectionPoints.Add(new MyPoint(200, 50));
            shape.ConnectionPoints.Add(new MyPoint(200, 75));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 0);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(0, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 0), null);
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 10);
            command = new CommandElement(CommandType.Line, new MyPoint(200, 10), null);
            element.Commands.Add(command);
            element.IsClosed = false;
            element.LineBrush = Brushes.Black;
            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 90);
            command = new CommandElement(CommandType.Line, new MyPoint(200, 90), null);
            element.Commands.Add(command);
            element.IsClosed = false;
            element.LineBrush = Brushes.Black;
            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateDFDSymbol2()
        {
            MyShape shape = new MyShape("DFDSymbol2", 200, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 150;

            shape.LeftSideHeight = 35;
            shape.LeftSideWidth = 35;

            shape.Childs.Add(new EllipseElement(new MyPoint(100, 50), 100, 50));

            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 50));

            shape.ConnectionPoints.Add(new MyPoint(40, 10));
            shape.ConnectionPoints.Add(new MyPoint(160, 10));

            shape.ConnectionPoints.Add(new MyPoint(40, 90));
            shape.ConnectionPoints.Add(new MyPoint(160, 90));

            return shape;
        }

        public static MyShape CreateFlowchartSymbol1()
        {
            MyShape shape = new MyShape("FlowchartSymbol1", 200, 120);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(0, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 112));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(150, 88));

            shape.ConnectionPoints.Add(new MyPoint(0, 25));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(0, 75));

            shape.ConnectionPoints.Add(new MyPoint(200, 25));
            shape.ConnectionPoints.Add(new MyPoint(200, 50));
            shape.ConnectionPoints.Add(new MyPoint(200, 75));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 0);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(0, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.SimpleCurve, new MyPoint(100, 100), new MyPoint(50,120));
            element.Commands.Add(command);

            command = new CommandElement(CommandType.SimpleCurve, new MyPoint(200, 100), new MyPoint(150, 80));
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 0), null);
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateFlowchartSymbol2()
        {
            MyShape shape = new MyShape("FlowchartSymbol2", 200, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(0, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(150, 100));

            shape.ConnectionPoints.Add(new MyPoint(0, 25));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(0, 75));

            shape.ConnectionPoints.Add(new MyPoint(200, 25));
            shape.ConnectionPoints.Add(new MyPoint(200, 50));
            shape.ConnectionPoints.Add(new MyPoint(200, 75));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 0);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(0, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 0), null);
            element.Commands.Add(command);

            element.IsClosed = true;
            element.LineBrush = Brushes.Black;

            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(20, 0);
            command = new CommandElement(CommandType.Line, new MyPoint(20, 100), null);
            element.Commands.Add(command);
            element.LineBrush = Brushes.Black;
            element.IsClosed = false;
            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(180, 0);
            command = new CommandElement(CommandType.Line, new MyPoint(180, 100), null);
            element.Commands.Add(command);
            element.LineBrush = Brushes.Black;
            element.IsClosed = false;
            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateFlowchartSymbol3()
        {
            MyShape shape = new MyShape("FlowchartSymbol3", 230, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(30, 0));
            shape.ConnectionPoints.Add(new MyPoint(30, 100));
            shape.ConnectionPoints.Add(new MyPoint(230, 100));
            shape.ConnectionPoints.Add(new MyPoint(230, 0));

            shape.ConnectionPoints.Add(new MyPoint(80, 0));
            shape.ConnectionPoints.Add(new MyPoint(130, 0));
            shape.ConnectionPoints.Add(new MyPoint(180, 0));

            shape.ConnectionPoints.Add(new MyPoint(80, 100));
            shape.ConnectionPoints.Add(new MyPoint(130, 100));
            shape.ConnectionPoints.Add(new MyPoint(180, 100));

            shape.ConnectionPoints.Add(new MyPoint(4, 50));

            shape.ConnectionPoints.Add(new MyPoint(204, 50));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(30, 0);

            CommandElement command = new CommandElement(CommandType.SimpleCurve, new MyPoint(30, 100), new MyPoint(-20,50));
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(230, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.SimpleCurve, new MyPoint(230, 0), new MyPoint(180,50));
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateFlowchartSymbol4()
        {
            MyShape shape = new MyShape("FlowchartSymbol4", 200, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(0, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(150, 100));

            shape.ConnectionPoints.Add(new MyPoint(0, 25));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(0, 75));

            shape.ConnectionPoints.Add(new MyPoint(200, 25));
            shape.ConnectionPoints.Add(new MyPoint(200, 50));
            shape.ConnectionPoints.Add(new MyPoint(200, 75));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 0);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(0, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 0), null);
            element.Commands.Add(command);

            element.IsClosed = true;
            element.LineBrush = Brushes.Black;


            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(20, 0);
            command = new CommandElement(CommandType.Line, new MyPoint(20, 100), null);
            element.Commands.Add(command);
            element.LineBrush = Brushes.Black;
            element.IsClosed = false;
            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 20);
            command = new CommandElement(CommandType.Line, new MyPoint(200, 20), null);
            element.Commands.Add(command);
            element.LineBrush = Brushes.Black;
            element.IsClosed = false;
            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateFlowchartSymbol5()
        {
            MyShape shape = new MyShape("FlowchartSymbol5", 200, 100);
            shape.DefaultWidth = 100;
            shape.DefaultHeight = 100;

            shape.LeftSideHeight = 35;
            shape.LeftSideWidth = 35;

            EllipseElement ellipseElement = new EllipseElement(new MyPoint(100, 50), 100, 50);
            ellipseElement.LineBrush = Brushes.Black;
            ellipseElement.LineBrushThickness = 1;
            shape.Childs.Add(ellipseElement);

            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 50));

            shape.ConnectionPoints.Add(new MyPoint(40, 10));
            shape.ConnectionPoints.Add(new MyPoint(160, 10));

            shape.ConnectionPoints.Add(new MyPoint(40, 90));
            shape.ConnectionPoints.Add(new MyPoint(160, 90));

            shape.ConnectionPoints.Add(new MyPoint(200, 100));

            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(100, 100);
            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);
            element.LineBrush = Brushes.Black;
            element.IsClosed = false;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateFlowchartSymbol6()
        {
            MyShape shape = new MyShape("FlowchartSymbol6", 260, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(30, 0));
            shape.ConnectionPoints.Add(new MyPoint(30, 100));
            shape.ConnectionPoints.Add(new MyPoint(230, 100));
            shape.ConnectionPoints.Add(new MyPoint(230, 0));

            shape.ConnectionPoints.Add(new MyPoint(80, 0));
            shape.ConnectionPoints.Add(new MyPoint(130, 0));
            shape.ConnectionPoints.Add(new MyPoint(180, 0));

            shape.ConnectionPoints.Add(new MyPoint(80, 100));
            shape.ConnectionPoints.Add(new MyPoint(130, 100));
            shape.ConnectionPoints.Add(new MyPoint(180, 100));

            shape.ConnectionPoints.Add(new MyPoint(4, 50));

            shape.ConnectionPoints.Add(new MyPoint(254, 50));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(30, 0);

            CommandElement command = new CommandElement(CommandType.SimpleCurve, new MyPoint(30, 100), new MyPoint(-20, 50));
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(230, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.SimpleCurve, new MyPoint(230, 0), new MyPoint(280, 50));
            element.Commands.Add(command);

            element.IsClosed = true;
            element.LineBrush = Brushes.Black;

            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(230, 100);
            command = new CommandElement(CommandType.SimpleCurve, new MyPoint(230, 0), new MyPoint(180, 50));
            element.Commands.Add(command);
            element.IsClosed = false;
            element.LineBrush = Brushes.Black;
            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateFlowchartSymbol7()
        {
            MyShape shape = new MyShape("FlowchartSymbol7", 200, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(0, 30));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 22));
            shape.ConnectionPoints.Add(new MyPoint(100, 15));
            shape.ConnectionPoints.Add(new MyPoint(150, 8));

            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(150, 100));

            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(0, 75));

            shape.ConnectionPoints.Add(new MyPoint(200, 25));
            shape.ConnectionPoints.Add(new MyPoint(200, 50));
            shape.ConnectionPoints.Add(new MyPoint(200, 75));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 30);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(0, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 0), null);
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateFlowchartSymbol8()
        {
            MyShape shape = new MyShape("FlowchartSymbol8", 200, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(25, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(150, 100));

            shape.ConnectionPoints.Add(new MyPoint(0, 25));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(0, 75));

            shape.ConnectionPoints.Add(new MyPoint(200, 25));
            shape.ConnectionPoints.Add(new MyPoint(200, 50));
            shape.ConnectionPoints.Add(new MyPoint(200, 75));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 25);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(0, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 0), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(25, 0), null);
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateFlowchartSymbol9()
        {
            MyShape shape = new MyShape("FlowchartSymbol9", 200, 140);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(0, 20));
            shape.ConnectionPoints.Add(new MyPoint(0, 120));
            shape.ConnectionPoints.Add(new MyPoint(200, 120));
            shape.ConnectionPoints.Add(new MyPoint(200, 20));

            shape.ConnectionPoints.Add(new MyPoint(50, 32));
            shape.ConnectionPoints.Add(new MyPoint(100, 20));
            shape.ConnectionPoints.Add(new MyPoint(150, 12));

            shape.ConnectionPoints.Add(new MyPoint(50, 132));
            shape.ConnectionPoints.Add(new MyPoint(100, 120));
            shape.ConnectionPoints.Add(new MyPoint(150, 112));

            shape.ConnectionPoints.Add(new MyPoint(0, 70));

            shape.ConnectionPoints.Add(new MyPoint(200, 70));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 20);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(0, 120), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.SimpleCurve, new MyPoint(100, 120), new MyPoint(50, 140));
            element.Commands.Add(command);

            command = new CommandElement(CommandType.SimpleCurve, new MyPoint(200, 120), new MyPoint(150, 100));
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 20), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.SimpleCurve, new MyPoint(100, 20), new MyPoint(150, 0));
            element.Commands.Add(command);

            command = new CommandElement(CommandType.SimpleCurve, new MyPoint(0, 20), new MyPoint(50, 40));
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateFlowchartSymbol10()
        {
            MyShape shape = new MyShape("FlowchartSymbol10", 200, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(0, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(170, 100));
            shape.ConnectionPoints.Add(new MyPoint(170, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(150, 100));

            shape.ConnectionPoints.Add(new MyPoint(0, 25));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(0, 75));

            shape.ConnectionPoints.Add(new MyPoint(195, 50));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 0);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(0, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(170, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.SimpleCurve, new MyPoint(170, 0), new MyPoint(220,50));
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateFlowchartSymbol11()
        {
            MyShape shape = new MyShape("FlowchartSymbol11", 250, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 0));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));
            shape.ConnectionPoints.Add(new MyPoint(250, 0));

            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));
            shape.ConnectionPoints.Add(new MyPoint(200, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(150, 100));

            shape.ConnectionPoints.Add(new MyPoint(25, 50));

            shape.ConnectionPoints.Add(new MyPoint(225, 50));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 0);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(50, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(250, 0), null);
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateFlowchartSymbol12()
        {
            MyShape shape = new MyShape("FlowchartSymbol12", 250, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));

            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));
            shape.ConnectionPoints.Add(new MyPoint(200, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(150, 100));

            shape.ConnectionPoints.Add(new MyPoint(0, 50));

            shape.ConnectionPoints.Add(new MyPoint(250, 50));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(50, 0);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(0, 50), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(50, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(250, 50), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 0), null);
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateFlowchartSymbol13()
        {
            MyShape shape = new MyShape("FlowchartSymbol13", 200, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(25, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));
            shape.ConnectionPoints.Add(new MyPoint(175, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(150, 100));

            shape.ConnectionPoints.Add(new MyPoint(0, 25));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(0, 75));

            shape.ConnectionPoints.Add(new MyPoint(200, 25));
            shape.ConnectionPoints.Add(new MyPoint(200, 50));
            shape.ConnectionPoints.Add(new MyPoint(200, 75));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 25);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(0, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 25), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(175, 0), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(25, 0), null);
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateFlowchartSymbol14()
        {
            MyShape shape = new MyShape("FlowchartSymbol14", 250, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(30, 0));
            shape.ConnectionPoints.Add(new MyPoint(30, 100));
            shape.ConnectionPoints.Add(new MyPoint(230, 100));
            shape.ConnectionPoints.Add(new MyPoint(230, 0));

            shape.ConnectionPoints.Add(new MyPoint(80, 0));
            shape.ConnectionPoints.Add(new MyPoint(130, 0));
            shape.ConnectionPoints.Add(new MyPoint(180, 0));

            shape.ConnectionPoints.Add(new MyPoint(80, 100));
            shape.ConnectionPoints.Add(new MyPoint(130, 100));
            shape.ConnectionPoints.Add(new MyPoint(180, 100));

            shape.ConnectionPoints.Add(new MyPoint(4, 50));

            shape.ConnectionPoints.Add(new MyPoint(254, 50));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(50, 0);

            CommandElement command = new CommandElement(CommandType.SimpleCurve, new MyPoint(50, 100), new MyPoint(-40, 50));
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.SimpleCurve, new MyPoint(200, 0), new MyPoint(290, 50));
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateFlowchartSymbol15()
        {
            MyShape shape = new MyShape("FlowchartSymbol15", 260, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(230, 100));
            shape.ConnectionPoints.Add(new MyPoint(230, 0));

            shape.ConnectionPoints.Add(new MyPoint(80, 0));
            shape.ConnectionPoints.Add(new MyPoint(130, 0));
            shape.ConnectionPoints.Add(new MyPoint(180, 0));

            shape.ConnectionPoints.Add(new MyPoint(80, 100));
            shape.ConnectionPoints.Add(new MyPoint(130, 100));
            shape.ConnectionPoints.Add(new MyPoint(180, 100));

            shape.ConnectionPoints.Add(new MyPoint(4, 50));

            shape.ConnectionPoints.Add(new MyPoint(254, 50));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 50);

            CommandElement command = new CommandElement(CommandType.SimpleCurve, new MyPoint(90, 100), new MyPoint(20, 84));
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(230, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.SimpleCurve, new MyPoint(230, 0), new MyPoint(280, 50));
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(90, 0), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.SimpleCurve, new MyPoint(0, 50), new MyPoint(20, 20));
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateFlowchartSymbol16()
        {
            MyShape shape = new MyShape("FlowchartSymbol16", 100, 100);

            shape.LeftSideHeight = 35;
            shape.LeftSideWidth = 35;

            shape.ConnectionPoints.Add(new MyPoint(0, 0));
            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 50));
            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));

            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 0);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(100, 0), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(100, 50), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(50, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(0, 50), null);
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateUMLUseCase()
        {
            MyShape shape = new MyShape("UMLUseCase", 200, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            EllipseElement element = new EllipseElement(new MyPoint(100, 50), 100, 50);
            element.LineBrush = Brushes.Black;
            element.LineBrushThickness = 1;
            shape.Childs.Add(element);

            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 50));

            shape.ConnectionPoints.Add(new MyPoint(40, 10));
            shape.ConnectionPoints.Add(new MyPoint(160, 10));

            shape.ConnectionPoints.Add(new MyPoint(40, 90));
            shape.ConnectionPoints.Add(new MyPoint(160, 90));

            return shape;
        }

        public static MyShape CreateUMLNote()
        {
            MyShape shape = new MyShape("UMLNote", 200, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(0, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(150, 100));

            shape.ConnectionPoints.Add(new MyPoint(0, 25));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(0, 75));

            shape.ConnectionPoints.Add(new MyPoint(200, 50));
            shape.ConnectionPoints.Add(new MyPoint(200, 75));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 0);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(0, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 35), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(165, 0), null);
            element.Commands.Add(command);

            element.IsClosed = true;
            element.LineBrush = Brushes.Black;
            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(165, 0);

            command = new CommandElement(CommandType.Line, new MyPoint(165, 35), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 35), null);
            element.Commands.Add(command);

            element.IsClosed = false;
            element.LineBrush = Brushes.Black;
            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateUMLNode()
        {
            MyShape shape = new MyShape("UMLNode", 200, 200);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 150;

            shape.ConnectionPoints.Add(new MyPoint(0, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 200));
            shape.ConnectionPoints.Add(new MyPoint(200, 200));
            shape.ConnectionPoints.Add(new MyPoint(200, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 200));
            shape.ConnectionPoints.Add(new MyPoint(100, 200));
            shape.ConnectionPoints.Add(new MyPoint(150, 200));

            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(0, 150));

            shape.ConnectionPoints.Add(new MyPoint(200, 50));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 150));

            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 20);
            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(20, 0), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(200, 0), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(200, 180), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(180, 200), null);
            element.Commands.Add(command);
            element.LineBrush = Brushes.Black;
            element.IsClosed = false;
            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 20);
            command = new CommandElement(CommandType.Line, new MyPoint(180, 20), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(180, 200), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(0, 200), null);
            element.Commands.Add(command);
            element.LineBrush = Brushes.Black;
            element.IsClosed = true;
            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(180, 20);
            command = new CommandElement(CommandType.Line, new MyPoint(200, 0), null);
            element.Commands.Add(command);
            element.LineBrush = Brushes.Black;
            element.IsClosed = false;
            shape.Childs.Add(element);

            

            return shape;
        }

        public static MyShape CreateUMLComponent()
        {
            MyShape shape = new MyShape("UMLComponent", 200, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(0, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(150, 100));

            shape.ConnectionPoints.Add(new MyPoint(0, 25));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(0, 75));

            shape.ConnectionPoints.Add(new MyPoint(200, 25));
            shape.ConnectionPoints.Add(new MyPoint(200, 50));
            shape.ConnectionPoints.Add(new MyPoint(200, 75));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(30, 0);
            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(30, 100), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(200, 0), null);
            element.Commands.Add(command);
            element.LineBrush = Brushes.Black;
            element.IsClosed = true;
            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 20);
            command = new CommandElement(CommandType.Line, new MyPoint(0, 40), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(60, 40), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(60, 20), null);
            element.Commands.Add(command);
            element.LineBrush = Brushes.Black;
            element.IsClosed = true;
            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 60);
            command = new CommandElement(CommandType.Line, new MyPoint(0, 80), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(60, 80), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(60, 60), null);
            element.Commands.Add(command);
            element.LineBrush = Brushes.Black;
            element.IsClosed = true;
            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateERDSymbol1()
        {
            MyShape shape = new MyShape("ERDSymbol1", 200, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.Childs.Add(new EllipseElement(new MyPoint(100, 50), 100, 50));

            EllipseElement element = new EllipseElement(new MyPoint(100, 50), 90, 45);
            element.LineBrush = Brushes.White;
            element.LineBrushThickness = 1;
            element.FillBrush = Brushes.Transparent;
            element.UseNodeBackgroud = false;
            shape.Childs.Add(element);

            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 50));

            shape.ConnectionPoints.Add(new MyPoint(40, 10));
            shape.ConnectionPoints.Add(new MyPoint(160, 10));

            shape.ConnectionPoints.Add(new MyPoint(40, 90));
            shape.ConnectionPoints.Add(new MyPoint(160, 90));

            return shape;
        }

        public static MyShape CreateERDSymbol2()
        {
            MyShape shape = new MyShape("ERDSymbol2", 200, 100);
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.ConnectionPoints.Add(new MyPoint(0, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(150, 100));

            shape.ConnectionPoints.Add(new MyPoint(0, 25));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(0, 75));

            shape.ConnectionPoints.Add(new MyPoint(200, 25));
            shape.ConnectionPoints.Add(new MyPoint(200, 50));
            shape.ConnectionPoints.Add(new MyPoint(200, 75));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 0);
            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(0, 100), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(200, 0), null);
            element.Commands.Add(command);
            element.IsClosed = true;
            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(7, 7);
            command = new CommandElement(CommandType.Line, new MyPoint(7, 93), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(193, 93), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(193, 7), null);
            element.Commands.Add(command);
            element.IsClosed = true;
            element.LineBrush = Brushes.White;
            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateERDSymbol3()
        {
            MyShape shape = new MyShape("ERDSymbol3", 100, 100);

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 50));
            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));

            shape.ConnectionPoints.Add(new MyPoint(25, 25));
            shape.ConnectionPoints.Add(new MyPoint(75, 25));
            shape.ConnectionPoints.Add(new MyPoint(25, 75));
            shape.ConnectionPoints.Add(new MyPoint(75, 75));

            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(50, 0);
            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(100, 50), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(50, 100), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(0, 50), null);
            element.Commands.Add(command);
            element.IsClosed = true;
            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(50, 7);
            command = new CommandElement(CommandType.Line, new MyPoint(93, 50), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(50, 93), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(7, 50), null);
            element.Commands.Add(command);
            element.LineBrush = Brushes.White;
            element.IsClosed = true;
            shape.Childs.Add(element);

            return shape;
        }

        #region Special Nodes

        public static MyShape CreatePieChart()
        {
            MyShape shape = new MyShape("PieChart", 200, 200);
            shape.NodeName = "PieChartNode";
            shape.DefaultWidth = 350;
            shape.DefaultHeight = 200;

            shape.LeftSideWidth = 35;
            shape.LeftSideHeight = 35;

            shape.ConnectionPoints.Add(new MyPoint(0, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 200));
            shape.ConnectionPoints.Add(new MyPoint(200, 200));
            shape.ConnectionPoints.Add(new MyPoint(200, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 200));
            shape.ConnectionPoints.Add(new MyPoint(100, 200));
            shape.ConnectionPoints.Add(new MyPoint(150, 200));

            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(0,100));
            shape.ConnectionPoints.Add(new MyPoint(0,150));

            shape.ConnectionPoints.Add(new MyPoint(200, 50));
            shape.ConnectionPoints.Add(new MyPoint(200,100));
            shape.ConnectionPoints.Add(new MyPoint(200,150));


            EllipseElement ellipseElement = new EllipseElement(new MyPoint(100, 100), 100, 100);
            shape.Childs.Add(ellipseElement);

            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(100, 100);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.SimpleCurve, new MyPoint(100, 0), new MyPoint(200,0));
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(100, 100), null);
            element.Commands.Add(command);

            element.FillBrush = Brushes.Red;
            element.LineBrush = Brushes.White;
            element.UseNodeBackgroud = false;
            element.IsClosed = true;

            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(100, 100);

            command = new CommandElement(CommandType.Line, new MyPoint(0, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.SimpleCurve, new MyPoint(100, 0), new MyPoint(0, 0));
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(100, 100), null);
            element.Commands.Add(command);

            element.FillBrush = Brushes.Yellow;
            element.LineBrush = Brushes.White;
            element.UseNodeBackgroud = false;
            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateBarChart()
        {
            MyShape shape = new MyShape("BarChart", 200, 200);
            shape.NodeName = "BarChartNode";
            shape.DefaultWidth = 500;
            shape.DefaultHeight = 300;

            shape.LeftSideWidth = 35;
            shape.LeftSideHeight = 35;

            shape.ConnectionPoints.Add(new MyPoint(0, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 200));
            shape.ConnectionPoints.Add(new MyPoint(200, 200));
            shape.ConnectionPoints.Add(new MyPoint(200, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 200));
            shape.ConnectionPoints.Add(new MyPoint(100, 200));
            shape.ConnectionPoints.Add(new MyPoint(150, 200));

            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(0, 150));

            shape.ConnectionPoints.Add(new MyPoint(200, 50));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 150));

            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(20, 200);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(20, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(60, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(60, 200), null);
            element.Commands.Add(command);

            element.FillBrush = Brushes.Red;
            element.LineBrush = Brushes.Black;
            element.UseNodeBackgroud = false;
            element.IsClosed = true;

            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(80, 200);

            command = new CommandElement(CommandType.Line, new MyPoint(80, 150), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(120, 150), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(120, 200), null);
            element.Commands.Add(command);

            element.FillBrush = Brushes.Yellow;
            element.LineBrush = Brushes.Black;
            element.UseNodeBackgroud = false;
            element.IsClosed = true;

            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(140, 200);

            command = new CommandElement(CommandType.Line, new MyPoint(140, 50), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(180, 50), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(180, 200), null);
            element.Commands.Add(command);

            element.FillBrush = Brushes.Blue;
            element.LineBrush = Brushes.Black;
            element.UseNodeBackgroud = false;
            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateHorizontalLine()
        {
            MyShape shape = new MyShape("HorizontalLine", 200, 100);
            shape.NodeName = "HorizontalLineNode";
            shape.DefaultWidth = 300;
            shape.DefaultHeight = 10;

            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 45);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(200, 45), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 55), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(0, 55), null);
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateVerticalLine()
        {
            MyShape shape = new MyShape("VerticalLine", 200, 100);
            shape.NodeName = "VerticalLineNode";
            shape.DefaultWidth = 10;
            shape.DefaultHeight = 300;

            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(95, 0);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(105, 0), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(105, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(95, 100), null);
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateSimpleConnector()
        {
            MyShape shape = new MyShape("SimpleConnector", 40, 40);
            shape.NodeName = "SimpleConnectorNode";
            shape.DefaultWidth = 6;
            shape.DefaultHeight = 6;

            shape.LeftSideWidth = 10;
            shape.LeftSideHeight = 10;

            EllipseElement element = new EllipseElement(new MyPoint(20, 20), 20, 20);
            element.LineBrush = Brushes.Black;
            element.LineBrushThickness = 2;
            element.FillBrush = Brushes.Black;
            element.UseNodeBackgroud = false;

            shape.Childs.Add(element);

            shape.ConnectionPoints.Add(new MyPoint(20, 20));

            return shape;
        }

        public static MyShape CreateText()
        {
            MyShape shape = new MyShape("Text", 200, 100);
            shape.NodeName = "TextNode";
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;
            
            shape.LeftSideWidth = 30;
            shape.LeftSideHeight = 35;

            shape.ConnectionPoints.Add(new MyPoint(0, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 0));

            shape.ConnectionPoints.Add(new MyPoint(100, 0));

            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            
            shape.ConnectionPoints.Add(new MyPoint(0, 50));

            shape.ConnectionPoints.Add(new MyPoint(200, 50));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 20);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(200, 20), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 30), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(0, 30), null);
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(85, 25);

            command = new CommandElement(CommandType.Line, new MyPoint(115, 25), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(115, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(85, 100), null);
            element.Commands.Add(command);

            element.IsClosed = true;

            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateUMLClass()
        {
            MyShape shape = new MyShape("UMLClass", 200, 100);
            shape.NodeName = "UMLClassNode";
            shape.DefaultWidth = 200;
            shape.DefaultHeight = 200;

            shape.LeftSideWidth = 35;
            shape.LeftSideHeight = 30;

            shape.ConnectionPoints.Add(new MyPoint(0, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(150, 100));

            shape.ConnectionPoints.Add(new MyPoint(0, 25));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(0, 75));

            shape.ConnectionPoints.Add(new MyPoint(200, 25));
            shape.ConnectionPoints.Add(new MyPoint(200, 50));
            shape.ConnectionPoints.Add(new MyPoint(200, 75));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 0);

            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(0, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);

            command = new CommandElement(CommandType.Line, new MyPoint(200, 0), null);
            element.Commands.Add(command);

            element.IsClosed = true;
            element.FillBrush = Brushes.Yellow;
            element.LineBrush = Brushes.Black;

            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 22);
            command = new CommandElement(CommandType.Line, new MyPoint(200, 22), null);
            element.Commands.Add(command);
            element.IsClosed = false;
            element.LineBrush = Brushes.Black;
            element.UseNodeBackgroud = false;
            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 42);
            command = new CommandElement(CommandType.Line, new MyPoint(200, 42), null);
            element.Commands.Add(command);
            element.IsClosed = false;
            element.LineBrush = Brushes.Black;
            element.UseNodeBackgroud = false;
            shape.Childs.Add(element);


            return shape;
        }

        public static MyShape CreateUMLActor()
        {
            MyShape shape = new MyShape("UMLActor", 100, 200);
            shape.NodeName = "UMLActorNode";
            shape.DefaultWidth = 50;
            shape.DefaultHeight = 100;

            shape.LeftSideWidth = 23;
            shape.LeftSideHeight = 35;

            shape.ConnectionPoints.Add(new MyPoint(0, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 200));
            shape.ConnectionPoints.Add(new MyPoint(100, 200));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));

            shape.ConnectionPoints.Add(new MyPoint(10, 100));

            shape.ConnectionPoints.Add(new MyPoint(90, 100));

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(50, 200));

            EllipseElement ellipseElement = new EllipseElement(new MyPoint(50, 30), 25, 25);
            ellipseElement.FillBrush = Brushes.Transparent;
            ellipseElement.UseNodeBackgroud = false;
            ellipseElement.LineBrush = Brushes.Black;
            ellipseElement.LineBrushThickness = 1;
            shape.Childs.Add(ellipseElement);

            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(50, 55);
            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(50, 120), null);
            element.Commands.Add(command);
            element.LineBrush = Brushes.Black;
            element.UseNodeBackgroud = false;
            element.IsClosed = false;
            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(50, 120);
            command = new CommandElement(CommandType.Line, new MyPoint(10, 160), null);
            element.Commands.Add(command);
            element.LineBrush = Brushes.Black;
            element.UseNodeBackgroud = false;
            element.IsClosed = false;
            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(50, 120);
            command = new CommandElement(CommandType.Line, new MyPoint(90, 160), null);
            element.Commands.Add(command);
            element.LineBrush = Brushes.Black;
            element.UseNodeBackgroud = false;
            element.IsClosed = false;
            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(20, 78);
            command = new CommandElement(CommandType.Line, new MyPoint(80, 78), null);
            element.Commands.Add(command);
            element.LineBrush = Brushes.Black;
            element.UseNodeBackgroud = false;
            element.IsClosed = false;
            shape.Childs.Add(element);

            return shape;
        }

        public static MyShape CreateUMLPackage()
        {
            MyShape shape = new MyShape("UMLPackage", 200, 100);
            shape.NodeName = "UMLPackageNode";
            shape.DefaultWidth = 150;
            shape.DefaultHeight = 75;

            shape.LeftSideWidth = 50;
            shape.LeftSideHeight = 35;

            shape.ConnectionPoints.Add(new MyPoint(0, 0));
            shape.ConnectionPoints.Add(new MyPoint(0, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 100));
            shape.ConnectionPoints.Add(new MyPoint(200, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 0));
            shape.ConnectionPoints.Add(new MyPoint(100, 0));
            shape.ConnectionPoints.Add(new MyPoint(150, 0));

            shape.ConnectionPoints.Add(new MyPoint(50, 100));
            shape.ConnectionPoints.Add(new MyPoint(100, 100));
            shape.ConnectionPoints.Add(new MyPoint(150, 100));

            shape.ConnectionPoints.Add(new MyPoint(0, 25));
            shape.ConnectionPoints.Add(new MyPoint(0, 50));
            shape.ConnectionPoints.Add(new MyPoint(0, 75));

            shape.ConnectionPoints.Add(new MyPoint(200, 25));
            shape.ConnectionPoints.Add(new MyPoint(200, 50));
            shape.ConnectionPoints.Add(new MyPoint(200, 75));


            PathElement element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 30);
            CommandElement command = new CommandElement(CommandType.Line, new MyPoint(0, 100), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(200, 100), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(200, 30), null);
            element.Commands.Add(command);
            element.IsClosed = true;
            element.FillBrush = Brushes.White;
            element.LineBrush = Brushes.Black;
            shape.Childs.Add(element);

            element = new PathElement(true);
            element.StartPoint = new MyPoint(0, 0);
            command = new CommandElement(CommandType.Line, new MyPoint(70, 0), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(70, 30), null);
            element.Commands.Add(command);
            command = new CommandElement(CommandType.Line, new MyPoint(0, 30), null);
            element.Commands.Add(command);
            element.IsClosed = true;
            element.FillBrush = Brushes.White;
            element.LineBrush = Brushes.Black;
            shape.Childs.Add(element);

            return shape;
        }


        #endregion
    }
}
