using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MercuryTemplateGenerator.Model
{
    public class Zone : CommonElements 
    {
        string _name;
        int _width;
        int _height;
        int _xLocation;
        int _yLocation;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
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

        
        public int XLocation
        {
            get { return _xLocation; }
            set
            {
                _xLocation = value;
                OnPropertyChanged("XLocation");
            }
        }
        public int YLocation
        {
            get { return _yLocation; }
            set
            {
                _yLocation = value;
                OnPropertyChanged("YLocation");
            }
        }
        
    }
}
