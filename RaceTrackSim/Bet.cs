using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceTrackSim
{
    class Bet
    {

        public int _amount;

        public int _raceHound;

        public Bettor _bettor { set; get; }


        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }



        public int RaceHound
        {
            get { return _raceHound; }
            set { _raceHound = value; }
        }



        public int PayOut(int winner)
        {

            if (_raceHound == winner)
            {
                return _amount;
            }
            return -_amount;
        }

        public static string GetDescription()
        {
            string text = ("Joe has bet $" + be + " on hound #" + houndNum);
            return _txtBet1.Text;
        }

    }
}
