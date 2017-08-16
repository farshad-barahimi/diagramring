//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;

using System.Text;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using Project.Shapes;
using System.Windows.Media;
using Project.MainClasses;
using System.Windows.Resources;
using System.Windows.Markup;
using System.Xml;
using System.Net;
using System.IO;
using System.Windows.Media.Effects;

namespace Project
{
    public class Statics
    {
        public static string AppStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        
        public static void AddImageToButton(Button button, string imageFileName)
        {
            Image image = new Image();
            button.ToolTip = button.Content.ToString();
            button.Content = image;
            string path = "Images/" + imageFileName;
            image.Source = loadResource<ImageSource>(path);
            image.Width = 32;
            image.Height = 32;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;
            button.Width = 40;
            button.Height = 40;
            button.Background = Brushes.White;
            button.HorizontalContentAlignment = HorizontalAlignment.Center;
            button.VerticalContentAlignment = VerticalAlignment.Center;
            button.Focusable = false;
        }

        public static void AddImageToButtonWithText(Button button, string imageFileName)
        {
            button.Padding = new Thickness(0);
            StackPanel stackpanel = new StackPanel();
            stackpanel.Orientation = Orientation.Horizontal;
            Label label = new Label();
            label.Margin = new Thickness(0, -2, 0, 0);
            label.Content = button.Content;
            Image image = new Image();
            stackpanel.Children.Add(image);
            stackpanel.Children.Add(label);
            button.ToolTip = button.Content.ToString();
            button.Content = stackpanel;
            string path = "Images/" + imageFileName;
            image.Source = loadResource<ImageSource>(path);
            image.Width = 24;
            image.Height = 24;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;
            button.Height = 28;
            button.Background = Brushes.White;
            button.HorizontalContentAlignment = HorizontalAlignment.Center;
            button.VerticalContentAlignment = VerticalAlignment.Center;
            button.Focusable = false;
        }

        public static void AddImageToComboBox(ComboBox comboBox, string imageFileName)
        {
            Image image = new Image();
            comboBox.Items.Add( image);
            string path = "Images/" + imageFileName;
            image.Source = loadResource<ImageSource>(path);
            image.Width = 32;
            image.Height = 32;
            image.Margin = new Thickness(0, -4, 0, 0);
            comboBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            comboBox.VerticalContentAlignment = VerticalAlignment.Center;
        }

        public static T loadResource<T>(string path)
        {
            T c = default(T);
            StreamResourceInfo sri = Application.GetResourceStream(new Uri(path, UriKind.Relative));
            if (sri.ContentType == "application/xaml+xml")
            {
                c = (T)XamlReader.Load(sri.Stream);
            }
            else if (sri.ContentType.IndexOf("image") >= 0)
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = sri.Stream;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();
                if (typeof(T) == typeof(ImageSource))
                {
                    c = (T)((object)bi);
                }
                else if (typeof(T) == typeof(Image))
                {
                    Image img = new Image();
                    img.Source = bi;
                    c = (T)((object)img);
                }
            }
            sri.Stream.Close();
            sri.Stream.Dispose();
            return c;
        }

        public static Dictionary<string,MyShape> ShapeLibrary;

        public const int NodeConnectionZIndex = int.MaxValue-1;
        public const int SimpleConnectorZIndex = int.MaxValue - 2;
        public const int EndSymbolZIndex = int.MaxValue-3;
        public const int SelectAreaZIndex = int.MaxValue-4;
        public const int EdgeBreakZIndex = int.MaxValue - 5;
        public const int EdgePartZIndex = int.MaxValue-6;

        public const int TempEdgeCanvasZIndex = int.MinValue + 2;
        
        public const double Epsilon = 10;

        public static Brush CreateBackgroundBrush(Color color, BackgroundColorStyle style)
        {
            switch (style)
            {
                case BackgroundColorStyle.Solid:
                    return new SolidColorBrush(color);
                case BackgroundColorStyle.Radial:
                    return new RadialGradientBrush(Colors.White, color);
                case BackgroundColorStyle.LeftToRight:
                    return new LinearGradientBrush(color, Colors.White, new Point(0,0.5),new Point(1.1,0.5));
                case BackgroundColorStyle.RightToLeft:
                    return new LinearGradientBrush(color, Colors.White, new Point(1, 0.5), new Point(-0.1, 0.5));
                case BackgroundColorStyle.TopToBottom:
                    return new LinearGradientBrush(color, Colors.White, new Point(0.5, 0), new Point(0.5, 1.1));
                case BackgroundColorStyle.BottomToTop:
                    return new LinearGradientBrush(color,Colors.White, new Point(0.5, 1), new Point(0.5, -0.1));
                case BackgroundColorStyle.TopLeftToBottomRight:
                    return new LinearGradientBrush(color, Colors.White, new Point(0.0, 0.0), new Point(1.1, 1.1));
                case BackgroundColorStyle.BottomRightToTopLeft:
                    return new LinearGradientBrush(color, Colors.White, new Point(1, 1), new Point(-0.1, -0.1));
                default:
                    return new SolidColorBrush(color);
            }
        }

        public static Brush FormBackground;

        public static MyPoint CalculateLabelPosition(double x1, double y1, double x2, double y2, double percent, double distance)
        {
            double R = Math.Sqrt(Math.Pow(y2 - y1, 2) + Math.Pow(x2 - x1, 2));
            double r = percent * R;

            if (R == 0)
                return new MyPoint(x1, y1);
            
            if (x2 - x1 == 0)
            {
                double xfinal=distance;
                double yfinal=r;

                if (y2 - y1 < 0)
                    yfinal = -r;

                if (distance > 0)
                    if (y2 - y1 > 0)
                        xfinal = -xfinal;

                if (distance < 0)
                    if (y2 - y1 > 0)
                        xfinal = -xfinal;

                return new MyPoint(x1 + xfinal, y1 + yfinal);
            }

            double x6 = (x2 - x1) / R;
            double y6 = (y2 - y1) / R;

            double c = Math.Pow(y6 / x6, 2);

            double x5 = Math.Abs(distance * Math.Sqrt(c / (c + 1)));
            double y5 = Math.Abs(distance * Math.Sqrt(1- c / (c + 1)));

            if (distance >= 0)
            {
                if (x2 - x1 < 0)
                    y5 = -y5;

                if (y2 - y1 >= 0)
                    x5 = -x5;
            }
            else
            {
                if (x2 - x1 >= 0)
                    y5 = -y5;

                if (y2 - y1 < 0)
                    x5 = -x5;
            }


            double x3 = x1 + r * x6;
            double y3 = y1 + r * y6;

            return new MyPoint(x3 + x5, y3 + y5);
        }

        public static void ReadOptions()
        {
            XmlTextReader reader = null;

            string path = AppStartPath + "\\Options.xml";

            reader = new XmlTextReader(path);

            reader.ReadToFollowing("ShowStartupScreen");

            string s = reader.ReadElementContentAsString();
            if (s == "True")
                Statics.IsShowStratupScreen = true;
            else
                Statics.IsShowStratupScreen = false;

            reader.Close();

        }
        public static void WriteOptions()
        {
            FileStream fileStream = null;
            XmlWriter writer = null;

            string path = AppStartPath + "\\Options.xml";
            fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineHandling = NewLineHandling.Entitize;

            writer = XmlTextWriter.Create(fileStream, settings);

            writer.WriteStartElement("Options");

            writer.WriteStartElement("ShowStartupScreen");
            writer.WriteValue(Statics.IsShowStratupScreen.ToString());
            writer.WriteFullEndElement();

            writer.WriteFullEndElement();

            writer.Close();
            fileStream.Close();
        }

        public static bool IsShowStratupScreen;

        public static double CurrentVersion = 6.3f;

        private delegate void VoidDoubleStringDelegate(double d, string s);

        public static bool IsTwoRectCross(Rect r1, Rect r2)
        {
            if (r1.Contains(r2.TopLeft))
                return true;
            if (r1.Contains(r2.TopRight))
                return true;
            if (r1.Contains(r2.BottomLeft))
                return true;
            if (r1.Contains(r2.BottomRight))
                return true;

            if (r2.Contains(r1.TopLeft))
                return true;
            if (r2.Contains(r1.TopRight))
                return true;
            if (r2.Contains(r1.BottomLeft))
                return true;
            if (r2.Contains(r1.BottomRight))
                return true;

            if (r1.TopLeft.Y > r2.TopLeft.Y && r1.BottomLeft.Y < r2.BottomLeft.Y)
                if (r2.TopLeft.X > r1.TopLeft.X && r2.TopRight.X < r1.TopRight.X)
                    return true;

            if (r2.TopLeft.Y > r1.TopLeft.Y && r2.BottomLeft.Y < r1.BottomLeft.Y)
                if (r1.TopLeft.X > r2.TopLeft.X && r1.TopRight.X < r2.TopRight.X)
                    return true;
            return false;
        }

        public static double RectangleDistance(Rect r1, Rect r2)
        {
            double X1 = 0, X2 = 0, Y1 = 0, Y2 = 0;
            if (Statics.IsTwoRectCross(r1, r2))
            {
                return 0;
            }

            if (r1.Top > r2.Bottom && r1.Left > r2.Right)
            {
                X1 = r1.Left;
                Y1 = r1.Top;
                X2 = r2.Right;
                Y2 = r2.Bottom;
            }
            else if (r1.Top > r2.Bottom && r1.Right < r2.Left)
            {
                X1 = r1.Right;
                Y1 = r1.Top;
                X2 = r2.Left;
                Y2 = r2.Bottom;
            }
            else if (r2.Top > r1.Bottom && r2.Left > r1.Right)
            {
                X1 = r2.Left;
                Y1 = r2.Top;
                X2 = r1.Right;
                Y2 = r1.Bottom;
            }
            else if (r2.Top > r1.Bottom && r2.Right < r1.Left)
            {
                X1 = r2.Right;
                Y1 = r2.Top;
                X2 = r1.Left;
                Y2 = r1.Bottom;
            }
            else if (r1.Top > r2.Bottom)
            {
                double maxLeft = Math.Max(r1.Left, r2.Left);
                double minRight = Math.Min(r1.Right, r2.Right);
                double center = (maxLeft + minRight) / 2;
                X1 = X2 = center;
                Y1 = r1.Top;
                Y2 = r2.Bottom;
            }
            else if (r2.Top > r1.Bottom)
            {
                double maxLeft = Math.Max(r1.Left, r2.Left);
                double minRight = Math.Min(r1.Right, r2.Right);
                double center = (maxLeft + minRight) / 2;
                X1 = X2 = center;
                Y1 = r2.Top;
                Y2 = r1.Bottom;
            }
            else if (r1.Left > r2.Right)
            {
                double maxTop = Math.Max(r1.Top, r2.Top);
                double minButtom = Math.Min(r1.Bottom, r2.Bottom);
                double center = (maxTop + minButtom) / 2;
                Y1 = Y2 = center;
                X1 = r1.Left;
                X2 = r2.Right;
            }
            else if (r2.Left > r1.Right)
            {
                double maxTop = Math.Max(r1.Top, r2.Top);
                double minButtom = Math.Min(r1.Bottom, r2.Bottom);
                double center = (maxTop + minButtom) / 2;
                Y1 = Y2 = center;
                X1 = r2.Left;
                X2 = r1.Right;
            }

            return Math.Sqrt((X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1));
        }

        public static string DoubleToStringWith1Precision(double d)
        {
            d = Math.Round(d, 1);
            string result = d.ToString();
            return result;
        }
    }
}
