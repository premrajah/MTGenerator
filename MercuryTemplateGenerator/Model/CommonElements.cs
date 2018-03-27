using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MercuryTemplateGenerator.Model
{
    public class CommonElements : INotifyPropertyChanged
    {
        string _name;
        int _width;
        int _height;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = Model.UppercaseWordsAndRemoveSpaces(value);
                OnPropertyChanged("Name");
            }
        }
        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged("Width");
            }
        }
        public int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged("Height");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
    
}
