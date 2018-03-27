using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.IO;
using MercuryTemplateGenerator.Controls;

using Winforms = System.Windows.Forms;
using System.Net;
using System.Windows;


namespace MercuryTemplateGenerator.Model
{
    public class Model : INotifyPropertyChanged
    {


        string _projectLocation;
        string _projectName;
        ObservableCollection<TemplateControl> _templateControls = new ObservableCollection<TemplateControl>();




        public Model() { } // Constructor

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }




        public ObservableCollection<TemplateControl> TemplateControls
        {
            get { return _templateControls; }
            set
            {
                _templateControls = value;
                OnPropertyChanged("TemplateControls");
            }
        }


        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                _projectName = UppercaseWordsAndRemoveSpaces(value);
                OnPropertyChanged("ProjectName");

            }
        }

        public string ProjectLocation
        {
            get { return _projectLocation; }
            set
            {
                _projectLocation = value;
                OnPropertyChanged("ProjectLocation");

            }
        }




        /// <summary>
        /// Get a location on the device to use
        /// </summary>
        public void GetLocation()
        {
            // get path to desktop
            var startPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Winforms.FolderBrowserDialog folderDialog = new Winforms.FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = startPath;
            Winforms.DialogResult pathResult = folderDialog.ShowDialog();


            if (pathResult == Winforms.DialogResult.OK)
            {
                ProjectLocation = folderDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Button to generate files and folders for the project
        /// </summary>
        public void GenerateFilesAndFolders()
        {
            try
            {
                string _desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string _projectRoot = $"{ProjectLocation ?? _desktopPath}\\{ProjectName}";
                string _jsResourceFiles = $"{Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))}\\Resources\\JSFiles";

            

                #region --> folders in Project root

                string _dataPath = CreateFolderInProjectRoot("Data");
                string _docPath = CreateFolderInProjectRoot("Documentation");
                string _imagePath = CreateFolderInProjectRoot("Images");
                string _jsPath = CreateFolderInProjectRoot("JS");
                string _videoPath = CreateFolderInProjectRoot("Videos");

                #endregion

                CopyFilesFromFolderToFolder(_jsResourceFiles, "Copy");

                if (!String.IsNullOrWhiteSpace(ProjectName))
                {

                    Directory.CreateDirectory(_projectRoot);
                    Debug.WriteLine(_projectRoot);
                    

                    // Check if main template directory exists
                    if (Directory.Exists(_projectRoot))
                    {
                        //Directory.CreateDirectory(_docPath);
                        //Directory.CreateDirectory(_imagePath);
                        //Directory.CreateDirectory(_dataPath);
                        //Directory.CreateDirectory(_videoPath);
                        //Directory.CreateDirectory(_jsPath);

                        if (Directory.Exists(_jsPath))
                        {
                            //CopyFilesFromFolderToFolder(_jsResourceFiles, _jsPath);
                            //DownloadRootJSFiles(_jsPath);
                        }
                    }

                    // Iterate over templates and generate folders
                    foreach (var template in TemplateControls)
                    {
                        string templateNameValue = template.TemplateModel.TemplateData.Name;
                        string templatePath = $"{_projectRoot}\\Templates\\{templateNameValue}";

                        if (!String.IsNullOrWhiteSpace(templateNameValue))
                        {
                            Directory.CreateDirectory(templatePath);
                        }
                        else
                        {
                            MessageBox.Show($"Please enter a template name for all templates");
                        }
                        
                        // Iterate over the zones and generate folders
                        foreach (var zone in template.TemplateModel.ZoneControls)
                        {
                            string zoneNameValue = zone.ZoneModel.ZoneData.Name;
                            string zonePath = $"{templatePath}\\{zoneNameValue}";

                            if (!String.IsNullOrWhiteSpace(zoneNameValue))
                            {
                                //Directory.CreateDirectory(zonePath);
                            }
                            else
                            {
                                MessageBox.Show("Please enter zone names for all zones");
                            }
                            
                        }
                        
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a project name.");
                }
                

                // Open folder project folder 
                // Process.Start(projectRoot);



                string CreateFolderInProjectRoot(string folderName)
                {
                    return $"{_projectRoot}\\{folderName}";
                }


                // close app window
                // System.Windows.Application.Current.Shutdown();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
        }
        

        /// <summary>
        /// Captilize first letter and remove spaces
        /// </summary>
        /// <param name="value">String to convert</param>
        /// <returns></returns>
        public static string UppercaseWordsAndRemoveSpaces(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return String.Empty;
            }

            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array).Replace(" ", "");
        }


        /// <summary>
        /// Download JS files for the main JS folder
        /// </summary>
        /// <param name="destinationPath"></param>
        void DownloadRootJSFiles(string destinationPath)
        {
            var JQueryLatest = new Uri(@"http://code.jquery.com/jquery-latest.min.js");
            var HandlebarsJs = new Uri(@"http://builds.handlebarsjs.com.s3.amazonaws.com/handlebars-v4.0.11.js");
            var MomentJs = new Uri(@"https://momentjs.com/downloads/moment.js");
            var VueJS = new Uri(@"https://vuejs.org/js/vue.min.js");


            DownloadWithWebCLient(JQueryLatest, destinationPath, "jquery-latest.min.js");
            DownloadWithWebCLient(HandlebarsJs, destinationPath, "handlebars-v4.0.11.js");
            DownloadWithWebCLient(MomentJs, destinationPath, "moment.js");
            DownloadWithWebCLient(VueJS, destinationPath, "vue.min.js");
        }



        /// <summary>
        /// Download files from the web using WEb Client
        /// </summary>
        /// <param name="urlPath">The URL for the file to be downloaded</param>
        /// <param name="destinationPath"></param>
        void DownloadWithWebCLient(Uri urlPath, string destinationPath, string fileName)
        {
            try
            {
                WebRequest req = WebRequest.Create(urlPath);
                WebResponse res = req.GetResponse();

                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(urlPath, $"{destinationPath}\\{fileName}");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("404"))
                {
                    Debug.WriteLine("URl is invalid");
                }

                Debug.WriteLine(ex.Message);
            }
            
        }


        /// <summary>
        /// Copy all files from one folder to another
        /// </summary>
        /// <param name="source">Source folder to copy from</param>
        /// <param name="destination">Destination folser to past to</param>
        void CopyFilesFromFolderToFolder(string source, string destination)
        {
            try
            {
                if (Directory.Exists(source))
                {
                    string fileName;
                    string destinationFile;
                    string[] files = Directory.GetFiles(source);

                    foreach (var file in files)
                    {
                        fileName = Path.GetFileName(file);
                        destinationFile = Path.Combine(destination, fileName);

                        File.Copy(file, destinationFile, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
        }







    }
}
