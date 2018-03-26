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

namespace MercuryTemplateGenerator.Controls
{
    /// <summary>
    /// Interaction logic for ZoneControl.xaml
    /// </summary>
    public partial class ZoneControl : UserControl
    {
        TemplateModel _parentModel;
        


        public ZoneControl(TemplateModel ParentModel)
        {
            _parentModel = ParentModel;
            InitializeComponent();
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
}
