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
    /// Interaction logic for UMLClassPropertiesForm.xaml
    /// </summary>
    public partial class UMLClassPropertiesForm : Window
    {
        private Node node;
        public UMLClassPropertiesForm(Node node)
        {
            InitializeComponent();

            Statics.AddImageToButtonWithText(AddVariableButton, "Tango Icons\\list-add.png");
            Statics.AddImageToButtonWithText(AddFunctionButton, "Tango Icons\\list-add.png");
            Statics.AddImageToButtonWithText(VariableRemoveButton, "Tango Icons\\edit-delete.png");
            Statics.AddImageToButtonWithText(VariableUpButton, "Tango Icons\\go-up.png");
            Statics.AddImageToButtonWithText(FunctionRemoveButton, "Tango Icons\\edit-delete.png");
            Statics.AddImageToButtonWithText(FunctionUpButton, "Tango Icons\\go-up.png");
            Statics.AddImageToButtonWithText(SaveButton, "checkmark48.png");
            Statics.AddImageToButtonWithText(VariableUpdateButton, "Tango Icons\\view-refresh.png");
            Statics.AddImageToButtonWithText(FunctionUpdateButton, "Tango Icons\\view-refresh.png");
            
            this.node = node;

            if (node.Properties.Count != 0)
            {
                ClassNameTextBox.Text = node.Properties[0];
                StreoTypeTextBox.Text = node.Properties[1];
                int propertyCount = int.Parse(node.Properties[2]);
                int functionCount = int.Parse(node.Properties[3]);

                int i;
                for (i = 4; i < propertyCount + 4; i++)
                    VariableNamesListBox.Items.Add(node.Properties[i]);
                for (i = propertyCount + 4; i < propertyCount + functionCount + 4; i++)
                    FunctionNamesListBox.Items.Add(node.Properties[i]);
            }
            else
                ClassNameTextBox.Text = "Class Name";
        }

        private void AddVariableButton_Click(object sender, RoutedEventArgs e)
        {
            VariableNamesListBox.Items.Add(VariableNameTextBox.Text);
            VariableNameTextBox.Text = "";
            VariableNameTextBox.Focus();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            node.Properties.Clear();
            node.Properties.Add(ClassNameTextBox.Text);
            node.Properties.Add(StreoTypeTextBox.Text);
            node.Properties.Add(VariableNamesListBox.Items.Count.ToString());
            node.Properties.Add(FunctionNamesListBox.Items.Count.ToString());

            foreach (string s in VariableNamesListBox.Items)
                node.Properties.Add(s);

            foreach (string s in FunctionNamesListBox.Items)
                node.Properties.Add(s);

            node.UpdateShape();
            this.Close();
        }

        private void VariableNamesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = VariableNamesListBox.SelectedIndex;
            if (index >= 0)
                EditVariableNameTextBox.Text = VariableNamesListBox.Items[index].ToString();
        }

        private void VariableUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            int index = VariableNamesListBox.SelectedIndex;
            if (index >= 0)
            {
                VariableNamesListBox.Items[index] = EditVariableNameTextBox.Text;
                VariableNamesListBox.SelectedIndex = index;
            }
        }

        private void VariableRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            int index = VariableNamesListBox.SelectedIndex;
            if (index >= 0)
                VariableNamesListBox.Items.RemoveAt(index);
        }

        private void VariableUpButton_Click(object sender, RoutedEventArgs e)
        {
            int index = VariableNamesListBox.SelectedIndex;
            if (index > 0)
            {
                string temp = VariableNamesListBox.Items[index].ToString();
                VariableNamesListBox.Items[index] = VariableNamesListBox.Items[index - 1].ToString();
                VariableNamesListBox.Items[index - 1] = temp;
                VariableNamesListBox.SelectedIndex = index;
            }

        }

        private void AddFunctionButton_Click(object sender, RoutedEventArgs e)
        {
            FunctionNamesListBox.Items.Add(FunctionNameTextBox.Text);
            FunctionNameTextBox.Text = "";
            FunctionNameTextBox.Focus();
        }

        private void FunctionNamesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = FunctionNamesListBox.SelectedIndex;
            if (index >= 0)
                EditFunctionNameTextBox.Text = FunctionNamesListBox.Items[index].ToString();
        }

        private void FunctionUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            int index = FunctionNamesListBox.SelectedIndex;
            if (index >= 0)
            {
                FunctionNamesListBox.Items[index] = EditFunctionNameTextBox.Text;
                FunctionNamesListBox.SelectedIndex = index;
            }
        }

        private void FunctionRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            int index = FunctionNamesListBox.SelectedIndex;
            if (index >= 0)
                FunctionNamesListBox.Items.RemoveAt(index);

        }

        private void FunctionUpButton_Click(object sender, RoutedEventArgs e)
        {
            int index = FunctionNamesListBox.SelectedIndex;
            if (index > 0)
            {
                string temp = FunctionNamesListBox.Items[index].ToString();
                FunctionNamesListBox.Items[index] = FunctionNamesListBox.Items[index - 1].ToString();
                FunctionNamesListBox.Items[index - 1] = temp;
                FunctionNamesListBox.SelectedIndex = index;
            }
        }
    }
}
