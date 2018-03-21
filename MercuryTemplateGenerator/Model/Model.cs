using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using Winforms = System.Windows.Forms;

namespace MercuryTemplateGenerator.Model
{
    class Model : INotifyPropertyChanged
    {

        string _projectLocation;
        string _projectName;
        ObservableCollection<Template> _templates;
        ObservableCollection<Zone> _zones;

       public Model()  {}

        public ObservableCollection<Zone> Zones
        {
            get { return _zones; }
            set {
                _zones = value;
                RaisePropertyChanged("Zones");
            }
        }

        public ObservableCollection<Template> Templates
        {
            get { return _templates; }
            set {
                _templates = value;
                RaisePropertyChanged("Templates");
            }
        }
        

        public string ProjectName
        {
            get { return _projectName; }
            set {
                _projectName = value;
                RaisePropertyChanged("ProjectName");
            }
        }
        
        public string ProjectLocation
        {
            get { return _projectLocation; }
            set {
                if(_projectLocation != null)
                {
                    _projectLocation = value;
                    RaisePropertyChanged("ProjectLocation");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

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
                _projectLocation = folderDialog.SelectedPath;
            }
        }
        
    }
}
