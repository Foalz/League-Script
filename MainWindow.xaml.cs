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
    /// 
    /// Executes the component, then it reads json file
    /// to update checkboxes based on json information
    /// 
    /// If execute league button is pressed
    ///     {
    ///         If the main.py script is not executed
    ///         {
    ///             Creates a new process, read json file and
    ///             modifies it based on the main window information,
    ///             and then executes the process (python.exe that executes
    ///             main.py file).
    ///         }
    ///         
    ///         else 
    ///         {
    ///             Kills the python.exe process.
    ///         }
    ///     }
    ///     
    /// Check boxes can only be clicked one by one.
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
            //This function reads the initial configurations of the json file, and updates the program
            //checkboxes based on this information. We are storing all the necessary info in dictionaries.
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

            //These loops updates checkboxes with start info.
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
            //This function reads again the json file, with the purpose to be modified, to execute
            //the script and the game with desired configuration.
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

            //These loops are checking what checkboxes are checked, and modifies the content of the
            //json file
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

            //Finally, we are writting in the json file the new configuration to be read by 
            //python scripts
            string jsonwrite = JsonConvert.SerializeObject(jsonread, Formatting.Indented);
            File.WriteAllText($@"config\config.json", jsonwrite);

            //Starts python script
            process.Start(); 
            Execute.Content = "Close the script";
        }

        public void Execute_Button(object sender, RoutedEventArgs e)
        {
            //Defining the main script directory
            var filePath = $@"scripts\main.py";
            var button_state = Execute.Content.ToString();

            try 
            {  
               //If the content of the button is "Execute League" it means the script
               //is not executed yet, so it proceeds to execute main.py script when is pressed.
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

               //In other case, if the content of the button is "Close the script" it means the
               //python script is already running, so this button kills the python.exe process
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
                //Do nothing
            }


        }

        private void CheckBox_Servers(object sender, RoutedEventArgs e)
        {
        
            CheckBox[] server_list = { NA, LAN, EUW };

            //Updating dinamically checkboxes, only one can be selected.
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
            //Updating dinamically checkboxes, only one can be selected.
            foreach (var i in language_list)
            {
                if (sender != i)
                {
                    i.IsChecked = false;
                }
            }



        }


        /* External windows, currently not working
  
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
        */
    }
}
