//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;

using System.Text;
using Project.MainClasses;

namespace Project.Shapes
{
    class CommandElement
    {
        public CommandType Type { get; private set; }
        public MyPoint EndPoint { get; private set; }
        public MyPoint ControlPoint { get; private set; }

        public CommandElement(CommandType type, MyPoint endPoint, MyPoint controlPoint)
        {
            this.Type = type;
            this.EndPoint = endPoint;
            this.ControlPoint = controlPoint;
        }
    }

    enum CommandType
    {
        Line,
        SimpleCurve
    }
}
