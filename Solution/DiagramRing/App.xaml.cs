//Copyright © 2010-2013 , Farshad Barahimi . All rights reserved
//This software is licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using System.Windows;
using System.Windows.Interop;

namespace Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            new HwndSource(new HwndSourceParameters());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args != null && e.Args.Length > 0)
                this.Properties["InputFileName"] = e.Args[0];
    
            base.OnStartup(e);
        }
    }
}
