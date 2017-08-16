//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Project.MainClasses;

namespace Project.SpecialNodeProperties
{
    /// <summary>
    /// Interaction logic for LinePropertiesForm.xaml
    /// </summary>
    public partial class LinePropertiesForm : Window
    {
        private Node node;
        public LinePropertiesForm(Node node)
        {
            InitializeComponent();

            Statics.AddImageToButtonWithText(SaveButton, "checkmark48.png");

            this.node = node;

            if (node.Shape.NodeName == "HorizontalLineNode")
                LenghtTextBox.Text = node.Width.ToString();
            else if (node.Shape.NodeName == "VerticalLineNode")
                LenghtTextBox.Text = node.Height.ToString();

            LenghtTextBox.Focus();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int len = 0;
            try
            {
                len = int.Parse(LenghtTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Please enter length in correct format");
                return;
            }

            if (len < 20)
            {
                MessageBox.Show("Length should be at least 20");
                return;
            }

            if (node.Shape.NodeName == "HorizontalLineNode")
                node.Width = len;
            else if (node.Shape.NodeName == "VerticalLineNode")
                node.Height = len;

            node.UpdateShape();
            this.Close();
        }
    }
}
