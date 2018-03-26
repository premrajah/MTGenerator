using MercuryTemplateGenerator.Model;
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
using System.ComponentModel;


namespace MercuryTemplateGenerator.Controls
{
    /// <summary>
    /// Interaction logic for ZoneControl.xaml
    /// </summary>
    public partial class ZoneControl : UserControl
    {
        TemplateModel _parentModel;
        public ZoneModel ZoneModel;


        public ZoneControl(TemplateModel ParentModel)
        {
            _parentModel = ParentModel;
            InitializeComponent();
            ZoneModel = (ZoneModel)DataContext;
        }

        /// <summary>
        /// Button to remove a zone from the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeZoneBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgResult = MessageBox.Show("Are you sure?", "Delete zone", MessageBoxButton.YesNo);

            if (msgResult == MessageBoxResult.Yes)
            {
                if (_parentModel != null)
                {
                    _parentModel.ZoneControls.Remove(this);
                }
            }
        }
    }


    public class ZoneModel : INotifyPropertyChanged
    {

        Zone _ZoneData = new Zone();

        public Zone ZoneData
        {
            get { return _ZoneData; }
            set
            {
                _ZoneData = value;
                if (_ZoneData != value)
                {
                    OnPropertyChanged("ZoneData");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
