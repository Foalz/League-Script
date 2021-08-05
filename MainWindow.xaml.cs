﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace LC_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {       
            InitializeComponent();
            Execute.Focusable = false;
            Execute.IsEnabled = true;
            Debug.WriteLine(Directory.GetCurrentDirectory());


        }

        public void Execute_Program(string filePath, string button_state, Process process)
        {      
            process.Start();
            Execute.Content = "Close the script";
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            var filePath = $@"scripts\main.py";
            var button_state = Execute.Content.ToString();

            try 
            {  
               if (button_state == "Execute League")
               {
                    Process process = new Process();
                    Debug.WriteLine(filePath);
                    process.StartInfo.FileName = "python.exe";
                    process.StartInfo.Arguments = filePath;
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.ErrorDialog = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    Execute_Program(filePath, button_state, process);
               }

               else if (button_state == "Close the script")
               {
                    foreach (var newprocess in Process.GetProcessesByName("Python"))
                    {
                        Execute.IsEnabled = false;
                        newprocess.WaitForExitAsync();
                        newprocess.Kill();
                        Execute.Content = "Execute League";
                        Execute.IsEnabled = true;
                        break;
                    }
               }      
            }

            catch
            {
                Debug.WriteLine(filePath);
            }


        }
    }
}