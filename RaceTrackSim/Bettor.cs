using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace RaceTrackSim
{
    class Bettor 
    {
      public string _name;

       public int _cash;

        public int Cash
        {
            get { return _cash; }
            set { _cash = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public Bet HasPlacedBet;

        public RadioButton MyRadioButton;   //My RadioButton
        public TextBlock amount;


        public Bettor()
        {
            _name = Name;
            _cash = Cash;
        }

        public bool PlaceBet(int betAmount, int houndToWin)
        {
            HasPlacedBet = new Bet { Amount = betAmount, RaceHound = houndToWin, _bettor = this};
            return (_cash >= betAmount);
        }

        public void ClearBet()
        {
            
        }

        public void Collect(int winnerHound)
        {
             if (this.HasPlacedBet != null)
            {
                Cash += HasPlacedBet.PayOut(winnerHound);
            }
        }

        public  void UpdateLabels()
        {
            
        }




    }
}
