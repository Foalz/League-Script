using System;
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
using Newtonsoft.Json;

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
            Manage.IsEnabled = false;
            Settings.IsEnabled = false;
            ReadConfigJson();


        }


        private void ReadConfigJson()
        {
            dynamic jsonread = JsonConvert.DeserializeObject(File.ReadAllText($@"config\config.json"));
            Dictionary<string, CheckBox> server_dict = new Dictionary<string, CheckBox>()
            {
                { "na", NA },
                { "lan", LAN },
                { "euw", EUW }

            };

            Dictionary<string, CheckBox> language_dict = new Dictionary<string, CheckBox>()
            {
                { "en", EN },
                { "es", ES }

            };

            foreach (string language in language_dict.Keys)
            {
                if (language == Convert.ToString(jsonread["GUI"]["game"]["language"]))
                {
                    language_dict[language].IsChecked = true;
                }
            }

            foreach (string server in server_dict.Keys)
            {
                if (server == Convert.ToString(jsonread["GUI"]["game"]["server"]))
                {
                    server_dict[server].IsChecked = true;
                }
            }

            Debug.WriteLine($"Game language: {jsonread["GUI"]["game"]["language"]}");
            
        }

        private void Execute_Scripts(string filePath, string button_state, Process process)
        {
            
            dynamic jsonread = JsonConvert.DeserializeObject(File.ReadAllText($@"config\config.json"));
            Dictionary<string, string> server_dict = new Dictionary<string, string>()
            {
                { "NA", "na"},
                { "LAN", "lan" },
                { "EUW", "euw" }

            };

            Dictionary<string, string> language_dict = new Dictionary<string, string>()
            {
                { "English",  "en" } ,
                { "Spanish",  "es" } 

            };


            foreach (CheckBox language in new List<CheckBox>{ EN, ES })
            {
                if (language.IsChecked == true)
                {
                    jsonread["GUI"]["game"]["language"] = language_dict[Convert.ToString(language.Content)];
                }
            }

            foreach (CheckBox server in new List<CheckBox> { NA, LAN, EUW })
            {
                if (server.IsChecked == true)
                {
                    jsonread["GUI"]["game"]["server"] = server_dict[Convert.ToString(server.Content)];
                }
            }
            string jsonwrite = JsonConvert.SerializeObject(jsonread, Formatting.Indented);
            File.WriteAllText($@"config\config.json", jsonwrite);

            process.Start();
            Execute.Content = "Close the script";
        }

        public void Execute_Button(object sender, RoutedEventArgs e)
        {
            var filePath = $@"scripts\main.py";
            var button_state = Execute.Content.ToString();

            try 
            {  
               if (button_state == "Execute League")
               {
                    Process process = new Process();
                    process.StartInfo.FileName = "python.exe";
                    process.StartInfo.Arguments = filePath;
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.ErrorDialog = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    Execute_Scripts(filePath, button_state, process);
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
                
            }


        }

        private void CheckBox_Servers(object sender, RoutedEventArgs e)
        {
        
            CheckBox[] server_list = { NA, LAN, EUW };
            

            foreach (var i in server_list)
            {
                if (sender != i)
                {
                    i.IsChecked = false;
                }
            }


        }

        private void CheckBox_Languages(object sender, RoutedEventArgs e)
        {
            CheckBox[] language_list = {EN, ES};

            foreach (var i in language_list)
            {
                if (sender != i)
                {
                    i.IsChecked = false;
                }
            }



        }

        private void Manage_Accs_Button(object sender, RoutedEventArgs e)
        {   

            //Manage_Accs window = new Manage_Accs();
            //window.Show();   
        }

        private void Settings_Button(object sender, RoutedEventArgs e)
        {
            
            //Settings window = new Settings();
            //window.Show();       
            
        }

    }
}
