using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using MercuryTemplateGenerator.Model;
using System.Diagnostics;

namespace MercuryTemplateGenerator.Controls
{
    /// <summary>
    /// Interaction logic for TemplateControl.xaml
    /// </summary>
    public partial class TemplateControl : UserControl
    {
        Model.Model _parentModel;
        public TemplateModel TemplateModel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ParentModel"></param>
        public TemplateControl(Model.Model ParentModel)
        {
            _parentModel = ParentModel;
            InitializeComponent();
            TemplateModel = (TemplateModel)DataContext;
        }



        /// <summary>
        /// Remove a template from screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeTemplate_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgResult = MessageBox.Show("Are you sure?", "Delete template", MessageBoxButton.YesNo);

            if (msgResult == MessageBoxResult.Yes)
            {
                if (_parentModel != null)
                {
                    _parentModel.TemplateControls.Remove(this);
                }
            }


        }


        /// <summary>
        /// Add a Zone to screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addZoneBtn_Click(object sender, RoutedEventArgs e)
        {
            TemplateModel.ZoneControls.Add(new ZoneControl(TemplateModel));
        }
    }

    /// <summary>
    /// TEmplate Model Class
    /// </summary>
    public class TemplateModel : INotifyPropertyChanged
    {
        ObservableCollection<ZoneControl> _ZoneControls = new ObservableCollection<ZoneControl>();

        CommonElements _TemplateData = new CommonElements();


        public CommonElements TemplateData
        {
            get
            {
                return _TemplateData;
            }
            set
            {
                _TemplateData = value;
                if (_TemplateData != value)
                {
                    OnPropertyChanged("TemplateData");
                }
            }
        }



        public ObservableCollection<ZoneControl> ZoneControls
        {
            get { return _ZoneControls; }
            set
            {
                _ZoneControls = value;
                OnPropertyChanged("ZoneControls");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
