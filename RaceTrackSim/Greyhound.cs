using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RaceTrackSim
{
    class Greyhound
    {
        private int _locationToAdd;
        public double StartPosition
        {
            get; set;
        }

        private int _location
        {
            get; set;
        }
        public double RaceTrackLength
        {
            get; set;
        }

        public Random Randomizer;

        public Image img;
  

        public Greyhound()
        {
            _location = 0;
        }

        public void TakeStartingPosition()
        {
           
            _location = 0;
            _locationToAdd = 0;
            Canvas.SetLeft(img, StartPosition);
        }

        public bool Run()
        {
            _location = Randomizer.Next(1, 10);

            _locationToAdd += _location;
            Canvas.SetLeft(img, StartPosition + _locationToAdd);

            if (Canvas.GetLeft(img) >= RaceTrackLength)
            {
                return true;
            }
            else
            {
                return false;
            }   
        }
    }
}
