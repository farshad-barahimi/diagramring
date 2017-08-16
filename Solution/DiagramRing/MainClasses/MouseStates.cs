//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

namespace Project.MainClasses
{
    public enum MouseStates
    {
        /// <summary>
        /// When mouse is normal state. in this state it will select if left mouse gets down on a shape and it is not selected otherwise goes to Moving. 
        /// and will go to SelectArea if left mouse gets down on on a empty space.
        /// </summary>
        Normal,

        /// <summary>
        /// In this state a move has started. a LastMovePosition has been saved.
        /// if left mouse gets up it is finished and will go to normal state.
        /// if mouse moves the shape is moved and LastMovePosition is updated.
        /// </summary>
        Moving,

        /// <summary>
        /// In this state mouse has begun selecting an area a Point SelectAreaStart has been saved.
        /// it will go to Normal if left mouse goes up
        /// </summary>
        SelectArea,

        /// <summary>
        /// In this state program is ready to start a link and connection points are visible.
        /// Left mouse down only works on Connection points.
        /// </summary>
        LinkNormal,

        /// <summary>
        /// In this state a link has started and a startNode and a path of link is saved.
        /// if Left mouse gets down on Connection points the link is finished otherwise a break point is added.
        /// If Right mouse gets down it will cancel
        /// </summary>
        LinkStarted,
        /// <summary>
        /// In this state a temp node is selected and moves by mouse. when mouse gets down it will go to normal state.
        /// </summary>
        InsertStarted,
        /// <summary>
        /// In this state the the node has started resizing. SelectedResizeConnector has been set.
        /// </summary>
        ResizeStarted
    }
}