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
using System.Data;

namespace Project.SpecialNodeProperties
{
    /// <summary>
    /// Interaction logic for ChartPropertiesForm.xaml
    /// </summary>
    public partial class ChartPropertiesForm : Window
    {
        DataTable dataTable=null;
        Node node;

        public ChartPropertiesForm(Node node)
        {
            InitializeComponent();

            Statics.AddImageToButtonWithText(SaveButton, "checkmark48.png");
            Statics.AddImageToButtonWithText(AddButton, "Tango Icons\\list-add.png");
            Statics.AddImageToButtonWithText(UpButton, "Tango Icons\\go-up.png");
            Statics.AddImageToButtonWithText(RemoveButton, "Tango Icons\\edit-delete.png");
            Statics.AddImageToButtonWithText(EditButton, "Tango Icons\\view-refresh.png");

            this.node = node;
            this.Background = Statics.FormBackground;

            dataTable = new DataTable();
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Value");

            int i;
            for (i = 0; i < node.Properties.Count; i += 2)
            {
                DataRow row = dataTable.NewRow();
                row["Name"]=node.Properties[i];
                row["Value"]=node.Properties[i+1].ToString();
                dataTable.Rows.Add(row);
            }

            listView1.DataContext = dataTable;

            NameTextBox.Focus();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double.Parse(ValueTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Please enter value in correct format");
                return;
            }

            DataRow row = dataTable.NewRow();
            row["Name"]=NameTextBox.Text;
            row["Value"] = ValueTextBox.Text;
            dataTable.Rows.Add(row);

            NameTextBox.Text = "";
            ValueTextBox.Text = "";
            NameTextBox.Focus();
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            int index = listView1.SelectedIndex;

            if(index<1)
                return;

            string temp1,temp2;

            temp1 = dataTable.Rows[index]["Name"].ToString();
            temp2 = dataTable.Rows[index-1]["Name"].ToString();
            dataTable.Rows[index]["Name"]=temp2;
            dataTable.Rows[index-1]["Name"] = temp1;

            temp1 = dataTable.Rows[index]["Value"].ToString();
            temp2 = dataTable.Rows[index - 1]["Value"].ToString();
            dataTable.Rows[index]["Value"] = temp2;
            dataTable.Rows[index - 1]["Value"] = temp1;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            node.Properties.Clear();
            int i;
            for (i = 0; i < dataTable.Rows.Count; i++)
            {
                node.Properties.Add(dataTable.Rows[i].ItemArray[0].ToString());
                node.Properties.Add(dataTable.Rows[i].ItemArray[1].ToString());
            }
            node.UpdateShape();

            this.Close();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            int index = listView1.SelectedIndex;

            if (index < 0)
                return;

            dataTable.Rows.RemoveAt(index);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            int index = listView1.SelectedIndex;

            if (index < 0)
                return;

            try
            {
                double.Parse(EditValueTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Please enter value in correct format");
                return;
            }

            dataTable.Rows[index]["Name"]= EditNameTextBox.Text;
            dataTable.Rows[index]["Value"] = EditValueTextBox.Text;
        }
    }
}
