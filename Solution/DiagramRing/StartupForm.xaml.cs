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
using System.Diagnostics;

namespace Project
{
    /// <summary>
    /// Interaction logic for StartupForm.xaml
    /// </summary>
    public partial class StartupForm : Window
    {
        private MainWindow owner;
        public StartupForm(MainWindow owner)
        {
            InitializeComponent();

            this.owner = owner;
            this.Background = Statics.FormBackground;

            Statics.AddImageToButton(CloseButton, "Tango Icons\\emblem-unreadable.png");

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            var image = (Image)CloseButton.Content;
            CloseButton.Content = stackPanel;
            stackPanel.Children.Add(image);
            var label = new Label();
            label.Content = CloseButton.ToolTip.ToString();
            label.VerticalAlignment = VerticalAlignment.Center;
            stackPanel.Children.Add(label);
            CloseButton.Width = 100;
            CloseButton.Background = Brushes.White;

            if (Statics.IsShowStratupScreen)
                DontShowCheckBox.IsChecked = false;
            else
                DontShowCheckBox.IsChecked = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NewDiagramLink_Click(object sender, RoutedEventArgs e)
        {
            owner.NewFile();
            this.Close();
        }

        private void OpenDiagramLink_Click(object sender, RoutedEventArgs e)
        {
            owner.OpenFile();
            this.Close();
        }

        private void Sample1Link_Click(object sender, RoutedEventArgs e)
        {
            string path = Statics.AppStartPath + "\\Samples\\" + "flowchart.drx";
            owner.OpenPath(path, true);
            this.Close();
        }

        private void Sample2Link_Click(object sender, RoutedEventArgs e)
        {
            string path = Statics.AppStartPath + "\\Samples\\" + "classDiagram.drx";
            owner.OpenPath(path, true);
            this.Close();
        }

        private void Sample3Link_Click(object sender, RoutedEventArgs e)
        {
            string path = Statics.AppStartPath + "\\Samples\\" + "useCaseDiagram.drx";
            owner.OpenPath(path, true);
            this.Close();
        }

        private void Sample4Link_Click(object sender, RoutedEventArgs e)
        {
            string path = Statics.AppStartPath + "\\Samples\\" + "orgChart.drx";
            owner.OpenPath(path, true);
            this.Close();
        }

        private void Sample5Link_Click(object sender, RoutedEventArgs e)
        {
            string path = Statics.AppStartPath + "\\Samples\\" + "DFD.drx";
            owner.OpenPath(path, true);
            this.Close();
        }

        private void Sample6Link_Click(object sender, RoutedEventArgs e)
        {
            string path = Statics.AppStartPath + "\\Samples\\" + "ERD.drx";
            owner.OpenPath(path, true);
            this.Close();
        }

        private void Sample7Link_Click(object sender, RoutedEventArgs e)
        {
            string path = Statics.AppStartPath + "\\Samples\\" + "pieChart.drx";
            owner.OpenPath(path, true);
            this.Close();
        }

        private void Sample8Link_Click(object sender, RoutedEventArgs e)
        {
            string path = Statics.AppStartPath + "\\Samples\\" + "barChart.drx";
            owner.OpenPath(path, true);
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DontShowCheckBox.IsChecked.Value)
                Statics.IsShowStratupScreen = false;
            else
                Statics.IsShowStratupScreen = true;
            Statics.WriteOptions();

        }

        private void HelpLink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Statics.AppStartPath + "\\Help.chm");
        }
    }
}
