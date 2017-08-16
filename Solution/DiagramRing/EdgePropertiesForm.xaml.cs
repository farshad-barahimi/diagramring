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

namespace Project
{
    /// <summary>
    /// Interaction logic for edgePropertiesForm.xaml
    /// </summary>
    public partial class EdgePropertiesForm : Window
    {
        private Edge edge;
        public EdgePropertiesForm(Edge edge)
        {
            InitializeComponent();

            this.Background = Statics.FormBackground;
            this.edge = edge;

            Statics.AddImageToButtonWithText(SaveButton, "checkmark48.png");

            Statics.AddImageToComboBox(comboBox1, "arrow1.png");
            Statics.AddImageToComboBox(comboBox1, "arrow6.png");
            Statics.AddImageToComboBox(comboBox1, "arrow3.png");
            Statics.AddImageToComboBox(comboBox1, "arrow2.png");
            Statics.AddImageToComboBox(comboBox1, "arrow5.png");
            Statics.AddImageToComboBox(comboBox1, "arrow4.png");

            comboBox1.SelectedIndex = (int)edge.HeadSymbol;

            Statics.AddImageToComboBox(comboBox2, "arrow1.png");
            Statics.AddImageToComboBox(comboBox2, "arrow6.png");
            Statics.AddImageToComboBox(comboBox2, "arrow3.png");
            Statics.AddImageToComboBox(comboBox2, "arrow2.png");
            Statics.AddImageToComboBox(comboBox2, "arrow5.png");
            Statics.AddImageToComboBox(comboBox2, "arrow4.png");

            comboBox2.SelectedIndex = (int)edge.TailSymbol;

            Statics.AddImageToComboBox(comboBox3, "arrow1.png");
            Statics.AddImageToComboBox(comboBox3, "arrow8.png");
            Statics.AddImageToComboBox(comboBox3, "arrow9.png");

            comboBox3.SelectedIndex = (int)edge.EdgeBrushType;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            edge.HeadSymbol = comboBoxIndexToEndSymbol(comboBox1.SelectedIndex);
            edge.TailSymbol = comboBoxIndexToEndSymbol(comboBox2.SelectedIndex);

            if (comboBox3.SelectedIndex == 0)
                edge.EdgeBrushType = EdgeBrushType.Solid;
            else if (comboBox3.SelectedIndex == 1)
                edge.EdgeBrushType = EdgeBrushType.Dashed;
            else if (comboBox3.SelectedIndex == 2)
                edge.EdgeBrushType = EdgeBrushType.Dotted;

            edge.UpdateStructure();

            this.Close();
        }

        private EndSymbol comboBoxIndexToEndSymbol(int index)
        {
            if (index == 0)
                return EndSymbol.None;
            else if (index == 1)
                return EndSymbol.SimpleArrow;
            else if (index == 2)
                return EndSymbol.EmptyTriangle;
            else if (index == 3)
                return EndSymbol.FilledTriangle;
            else if (index == 4)
                return EndSymbol.EmptyRhombus;
            else if (index == 5)
                return EndSymbol.FilledRhumbus;

            return EndSymbol.None;
        }
    }
}
