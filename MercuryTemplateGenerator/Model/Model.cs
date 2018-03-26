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
                _projectName = value;
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
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string projectRoot = $"{ProjectLocation ?? desktopPath}\\{ProjectName ?? "DefaultProject"}";
                

                // Iterate over templates and generate folders
                foreach (var template in TemplateControls)
                {
                    string templatePath = $"{projectRoot}\\{template.TemplateModel.TemplateData.Name}";
                    //Directory.CreateDirectory(templatePath);
                    Debug.WriteLine(templatePath);


                    // Iterate over the zones and generate folders
                    foreach (var zone in template.TemplateModel.ZoneControls)
                    {
                        string zonePath = $"{templatePath}\\{zone.ZoneModel.ZoneData.Name}";
                        //Directory.CreateDirectory(zonePath);
                        Debug.WriteLine(zone.ZoneModel.ZoneData.Name);
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
