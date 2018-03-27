using MercuryTemplateGenerator.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using M = MercuryTemplateGenerator.Model;


namespace MercuryTemplateGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        M.Model _dataContext = new M.Model();

        public MainWindow()
        {
            InitializeComponent();
            _dataContext = (M.Model)DataContext;

        }

        /// <summary>
        /// Get a location for the project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void projectLocationBtn_Click(object sender, RoutedEventArgs e)
        {
            _dataContext.GetLocation();
        }

        /// <summary>
        /// Adds a template to screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addTemplateBtn_Click(object sender, RoutedEventArgs e)
        {
            _dataContext.TemplateControls.Add(new Controls.TemplateControl(_dataContext));
        }

        /// <summary>
        /// Generates files and folders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateFiles_Click(object sender, RoutedEventArgs e)
        {
            _dataContext.GenerateFilesAndFolders();
        }
    }
}
