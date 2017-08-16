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
using System.Reflection;
using Project.MainClasses;

namespace Project
{
    /// <summary>
    /// Interaction logic for Properties.xaml
    /// </summary>
    public partial class NodePropertiesForm : Window
    {
        private Node node;
        public NodePropertiesForm(Node node)
        {
            InitializeComponent();

            Statics.AddImageToButtonWithText(SaveButton, "checkmark48.png");

            this.node = node;

            WidthTextBox.Text = node.Width.ToString();
            HeightTextBox.Text = node.Height.ToString();
            LabelTextBox.Text = node.Label.ToString();

            if (node.IsFontBold)
                IsBoldCheckBox.IsChecked = true;
            
            if (node.IsFontItalic)
                IsItalicCheckBox.IsChecked = true;

            if (node.FontDecoration == FontDecoration.Underline)
                radioButton10.IsChecked = true;
            else if (node.FontDecoration == FontDecoration.Stroke)
                radioButton11.IsChecked = true;


            colorBorder1.Background = new SolidColorBrush(node.BackgroundColor);
            colorBorder2.Background = new SolidColorBrush(node.ForeGroundColor);
            updatesyleBorders();

            FontComboBox.Items.Add("10");
            FontComboBox.Items.Add("12");
            FontComboBox.Items.Add("14");
            FontComboBox.Items.Add("16");
            FontComboBox.Items.Add("18");
            FontComboBox.Items.Add("20");
            FontComboBox.Items.Add("24");
            FontComboBox.Items.Add("36");
            FontComboBox.Items.Add("72");

            FontComboBox.Text = node.FontSize.ToString();
            SetBackgroundStyleColor(node.BackgroundColorStyle);

            if (node.Properties.Contains("RightToLeft"))
            {
                LabelTextBox.FlowDirection = FlowDirection.RightToLeft;
                IsRightToLeftCheckbox.IsChecked = true;
            }

            LabelTextBox.Focus();
            LabelTextBox.CaretIndex = LabelTextBox.Text.Length;

        }

        private void IsRightToLeftCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            LabelTextBox.FlowDirection = FlowDirection.RightToLeft;
        }

        private void IsRightToLeftCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            LabelTextBox.FlowDirection = FlowDirection.LeftToRight;
        }

        private void ChangeColor1Button_Click(object sender, RoutedEventArgs e)
        {
            ColorPickerForm form = new ColorPickerForm();
            form.ShowDialog();
            if (form.IsOK)
            {
                colorBorder1.Background = new SolidColorBrush(form.SelectedColor);
                updatesyleBorders();
            }
        }

        private void updatesyleBorders()
        {
            Color color = ((SolidColorBrush)(colorBorder1.Background)).Color;

            styleBorder1.Background = Statics.CreateBackgroundBrush(color, BackgroundColorStyle.Solid);
            styleBorder2.Background = Statics.CreateBackgroundBrush(color, BackgroundColorStyle.Radial);
            styleBorder3.Background = Statics.CreateBackgroundBrush(color, BackgroundColorStyle.LeftToRight);
            styleBorder4.Background = Statics.CreateBackgroundBrush(color, BackgroundColorStyle.RightToLeft);
            styleBorder5.Background = Statics.CreateBackgroundBrush(color, BackgroundColorStyle.TopToBottom);
            styleBorder6.Background = Statics.CreateBackgroundBrush(color, BackgroundColorStyle.BottomToTop);
            styleBorder7.Background = Statics.CreateBackgroundBrush(color, BackgroundColorStyle.TopLeftToBottomRight);
            styleBorder8.Background = Statics.CreateBackgroundBrush(color, BackgroundColorStyle.BottomRightToTopLeft);
        }

        private void ChangeColor2Button_Click(object sender, RoutedEventArgs e)
        {
            ColorPickerForm form = new ColorPickerForm();
            form.ShowDialog();
            if (form.IsOK)
            {
                colorBorder2.Background = new SolidColorBrush(form.SelectedColor);
            }
        }

        private BackgroundColorStyle GetBackgroundStyleColor()
        {
            if (radioButton1.IsChecked.Value)
                return BackgroundColorStyle.Solid;
            else if (radioButton2.IsChecked.Value)
                return BackgroundColorStyle.Radial;
            else if (radioButton3.IsChecked.Value)
                return BackgroundColorStyle.LeftToRight;
            else if (radioButton4.IsChecked.Value)
                return BackgroundColorStyle.RightToLeft;
            else if (radioButton5.IsChecked.Value)
                return BackgroundColorStyle.TopToBottom;
            else if (radioButton6.IsChecked.Value)
                return BackgroundColorStyle.BottomToTop;
            else if (radioButton7.IsChecked.Value)
                return BackgroundColorStyle.TopLeftToBottomRight;
            else if (radioButton8.IsChecked.Value)
                return BackgroundColorStyle.BottomRightToTopLeft;
            else
                return BackgroundColorStyle.Solid;
        }

        private void SetBackgroundStyleColor(BackgroundColorStyle style)
        {
            switch (style)
            {
                case BackgroundColorStyle.Solid:
                    radioButton1.IsChecked = true;
                    break;
                case BackgroundColorStyle.Radial:
                    radioButton2.IsChecked = true;
                    break;
                case BackgroundColorStyle.LeftToRight:
                    radioButton3.IsChecked = true;
                    break;
                case BackgroundColorStyle.RightToLeft:
                    radioButton4.IsChecked = true;
                    break;
                case BackgroundColorStyle.TopToBottom:
                    radioButton5.IsChecked = true;
                    break;
                case BackgroundColorStyle.BottomToTop:
                    radioButton6.IsChecked = true;
                    break;
                case BackgroundColorStyle.TopLeftToBottomRight:
                    radioButton7.IsChecked = true;
                    break;
                case BackgroundColorStyle.BottomRightToTopLeft:
                    radioButton8.IsChecked = true;
                    break;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int width, height;
            try
            {
                double.Parse(FontComboBox.Text);
            }
            catch
            {
                MessageBox.Show("Please enter font size in correct format");
                return;
            }

            try
            {
                width= int.Parse(WidthTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Please enter width in correct format");
                return;
            }

            try
            {
                height = int.Parse(HeightTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Please enter height in correct format");
                return;
            }

            if(width<20)
            {
                MessageBox.Show("Width is too small. (at least 20)");
                return;
            }

            if (height < 20)
            {
                MessageBox.Show("Height is too small. (at least 20)");
                return;
            }

            node.Width = width;
            node.Height = height;
            node.FontSize = double.Parse(FontComboBox.Text);

            if (IsBoldCheckBox.IsChecked.Value)
                node.IsFontBold = true;
            else
                node.IsFontBold = false;

            if (IsItalicCheckBox.IsChecked.Value)
                node.IsFontItalic = true;
            else
                node.IsFontItalic = false;

            if (radioButton9.IsChecked.Value)
                node.FontDecoration = FontDecoration.None;
            else if (radioButton10.IsChecked.Value)
                node.FontDecoration = FontDecoration.Underline;
            else if (radioButton11.IsChecked.Value)
                node.FontDecoration = FontDecoration.Stroke;

            Color backgroundColor = ((SolidColorBrush)(colorBorder1.Background)).Color;
            Color foreGroundColor = ((SolidColorBrush)(colorBorder2.Background)).Color;
            node.BackgroundColor = backgroundColor;
            node.BackgroundColorStyle = GetBackgroundStyleColor();
            node.Label = LabelTextBox.Text;

            string s = FontComboBox.Text.ToString();
            node.FontSize = int.Parse(s);
            node.ForeGroundColor = foreGroundColor;

            if (IsRightToLeftCheckbox.IsChecked.Value)
                node.IsRightToLeft = true;
            else
                node.IsRightToLeft = false;
            
            node.UpdateShape();
            this.Close();
        }
    }
}
