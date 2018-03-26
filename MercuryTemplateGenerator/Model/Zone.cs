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
        string[] _transitions = new string[] {"FadeOutIn", "Crossfade", "Cut"};
        int _transitionDuration;

        public new string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public new int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged("Width");
            }
        }

        public new int Height
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

        public string[] Transitions
        {
            get { return _transitions; }
            set
            {
                _transitions = value;
                OnPropertyChanged("Transitions");
            }
        }

        public int TransitionDuration
        {
            get { return _transitionDuration; }
            set
            {
                _transitionDuration = value;
                OnPropertyChanged("TransitionsDuration");
            }
        }


    }
}
