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


namespace MercuryTemplateGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model.Model model;

        public MainWindow()
        {
            InitializeComponent();
            model = new Model.Model();
            DataContext = model;
        }

        private void projectLocationBtn_Click(object sender, RoutedEventArgs e)
        {
            model.GetLocation();
        }

        private void addTemplateBtn_Click(object sender, RoutedEventArgs e)
        {
           
            Debug.Write(model.ProjectLocation);
            Debug.Write(model.ProjectName);

        }
    }
}
