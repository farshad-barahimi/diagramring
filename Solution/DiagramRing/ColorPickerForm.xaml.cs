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

namespace Project
{
    /// <summary>
    /// Interaction logic for Color_picker.xaml
    /// </summary>
    public partial class ColorPickerForm : Window
    {
        public bool IsOK = false;
        public Color SelectedColor;
        public ColorPickerForm()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button=(Button)sender;
            Brush brush = button.Background;
            SolidColorBrush solidBrush = (SolidColorBrush)brush;
            SelectedColor = solidBrush.Color;
            IsOK = true;
            this.Close();
        }
    }
}
