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
using System.Windows.Media.Effects;

namespace Project
{
    /// <summary>
    /// Interaction logic for EdgePartProperties.xaml
    /// </summary>
    public partial class EdgePartProperties : Window
    {
        private EdgePart edgePart;
        private EdgeLabel editEdgeLabel;
        public EdgePartProperties(EdgePart edgePart)
        {
            InitializeComponent();

            Statics.AddImageToButtonWithText(AddEdgeLabelButton, "Tango Icons\\list-add.png");
            Statics.AddImageToButtonWithText(CloseButton, "checkmark48.png");
            Statics.AddImageToButtonWithText(RemoveEdgeLabelButton, "Tango Icons\\edit-delete.png");
            Statics.AddImageToButtonWithText(ApplyChangesButton, "Tango Icons\\view-refresh.png");

            this.edgePart = edgePart;

            PositionSlider.Value = 50;
            PositionSlider1.Value = 50;

            groupBox1.IsEnabled = false;

            TextTextBox.Focus();
            showAll();
        }

        private void PositionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int result = (int)e.NewValue;
            if(PositionSliderLabel!=null)
                PositionSliderLabel.Content = result.ToString() + "%";
        }

        private void PositionSlider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int result = (int)e.NewValue;
            if (PositionSliderLabel1 != null)
                PositionSliderLabel1.Content = result.ToString() + "%";
        }

        private void showAll()
        {
            listBox1.Items.Clear();
            foreach (EdgeLabel edgeLabel in edgePart.EdgeLabels)
                listBox1.Items.Add(edgeLabel.UITextBlock.Text);

            groupBox1.IsEnabled = false;
        }

        private void AddEdgeLabelButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter the text");
                return;
            }

            EdgeLabel edgeLabel = new EdgeLabel(TextTextBox.Text, edgePart);
            edgeLabel.Percent = PositionSlider.Value / 100;
            edgeLabel.Distance = DistanceSlider.Value;
            if (!clockwiseCheckBox.IsChecked.Value)
                edgeLabel.Distance = -edgeLabel.Distance;
            edgeLabel.UITextBlock.UpdateLayout();
            edgePart.EdgeLabels.Add(edgeLabel);
            edgePart.UpdateLabels();

            TextTextBox.Text ="";
            showAll();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index < 0)
            {
                groupBox1.IsEnabled = false;
                return;
            }
            editEdgeLabel = edgePart.EdgeLabels[index];
            TextTextBox1.Text = editEdgeLabel.UITextBlock.Text;
            PositionSlider1.Value = editEdgeLabel.Percent * 100;
            DistanceSlider1.Value = Math.Abs(editEdgeLabel.Distance);
            if (editEdgeLabel.Distance < 0)
                clockwiseCheckBox1.IsChecked = false;
            else
                clockwiseCheckBox1.IsChecked = true;
            groupBox1.IsEnabled = true;
        }

        private void ApplyChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextTextBox1.Text.Length == 0)
            {
                MessageBox.Show("Please enter the text");
                return;
            }

            editEdgeLabel.UITextBlock.Text = TextTextBox1.Text;
            editEdgeLabel.Percent = PositionSlider1.Value / 100;
            editEdgeLabel.Distance = DistanceSlider1.Value;
            if (!clockwiseCheckBox1.IsChecked.Value)
                editEdgeLabel.Distance = -editEdgeLabel.Distance;
            editEdgeLabel.UITextBlock.UpdateLayout();
            edgePart.UpdateLabels();

            TextTextBox1.Text = "";
            showAll();
        }

        private void RemoveEdgeLabelButton_Click(object sender, RoutedEventArgs e)
        {
            editEdgeLabel.Remove();
            TextTextBox1.Text = "";
            showAll();
        }
    }
}
