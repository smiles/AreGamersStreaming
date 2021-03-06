﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AreGamersStreaming
{
    using AreGamersStreaming.AGS_Core;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AGS_Logic Core;

        public MainWindow()
        {
            InitializeComponent();
            Core = new AGS_Logic();
            AutoMinamizeStart();
            Core.IconDoubleClick += DoubleClickResize;
        }

        private void DoubleClickResize(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Normal;
            this.ShowInTaskbar = true;
            this.Activate();
            this.Show();
        }

        private void AutoMinamizeStart()
        {
            if (Core.StartMinimize())
            {
                MinimizeWindow();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MinimizeWindow();
            e.Cancel = true;
        }

        private void MinimizeWindow()
        {
            this.WindowState = System.Windows.WindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Hide();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(this.WindowState == System.Windows.WindowState.Minimized)
            {
                MinimizeWindow();
            }
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Minimized)
            {
                MinimizeWindow();
            }
        }
        

    }
}
