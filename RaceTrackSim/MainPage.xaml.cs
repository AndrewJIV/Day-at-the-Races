using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RaceTrackSim
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public  partial class MainPage : Page
    {
        private Random _randomizer;

        private List<Greyhound> _raceHoundList;

        private Bet bet;

        private List<Bettor> _crtSelBettor;

        private DispatcherTimer _tmDanceTimer;

        int minBet = 5;

        bool BetPlaced = false;


        public MainPage()
        {
            this.InitializeComponent();

            _randomizer = new Random();

            _raceHoundList = new List<Greyhound>();

            _crtSelBettor = new List<Bettor>();

            bet = new Bet();

            _tmDanceTimer = new DispatcherTimer();
            _tmDanceTimer.Interval = TimeSpan.FromMilliseconds(10);

            //setup the event handler that is going to be called by the timer
            _tmDanceTimer.Tick += OnRaceTimerTick;

            _comboBoxHound.Items.Add("1");
            _comboBoxHound.Items.Add("2");
            _comboBoxHound.Items.Add("3");
            _comboBoxHound.Items.Add("4");
            CreateBettors();

        }

        public void CreateRaceHounds()
        {
            _tmDanceTimer.Stop();
            _raceHoundList.Insert(0, new Greyhound { img = _imgRacingHound1, StartPosition = Canvas.GetLeft(_imgRacingHound1), RaceTrackLength = _imgRaceTrack.Width - _imgRacingHound1.Width, Randomizer = _randomizer });
            _raceHoundList.Insert(1, new Greyhound { img = _imgRacingHound2, StartPosition = Canvas.GetLeft(_imgRacingHound2), RaceTrackLength = _imgRaceTrack.Width - _imgRacingHound2.Width, Randomizer = _randomizer });
            _raceHoundList.Insert(2, new Greyhound { img = _imgRacingHound3, StartPosition = Canvas.GetLeft(_imgRacingHound3), RaceTrackLength = _imgRaceTrack.Width - _imgRacingHound3.Width, Randomizer = _randomizer });
            _raceHoundList.Insert(3, new Greyhound { img = _imgRacingHound4, StartPosition = Canvas.GetLeft(_imgRacingHound4), RaceTrackLength = _imgRaceTrack.Width - _imgRacingHound4.Width, Randomizer = _randomizer });
        }

        private void CreateBettors()
        {
            _crtSelBettor.Add(new Bettor() { _name = "joe", _cash = 10, HasPlacedBet = new Bet()});
            _crtSelBettor.Add(new Bettor() { _name = "Bob", _cash = 100, HasPlacedBet = new Bet() });
            _crtSelBettor.Add(new Bettor() { _name = "Anna", _cash = 50, HasPlacedBet = new Bet() });
           
            
        }

        private void OnBettorSelectorClicked(object sender, RoutedEventArgs e)
        {
           _radioBtnJoe.Content = "Joe has "+_crtSelBettor[0]._cash+ " dollars";
           _radioBtnBob.Content = "Bob has " + _crtSelBettor[1]._cash + " dollars";
           _radioBtnAnna.Content = "Anna has " + _crtSelBettor[2]._cash + " dollars";



            if (_radioBtnJoe.IsChecked == true)
            {
                _txtSelectedBettorLabel.Text = "Joe bets: ";
            }
            else if (_radioBtnBob.IsChecked == true)
            {
                _txtSelectedBettorLabel.Text = "Bob bets: ";
            }
            else
            {
                _txtSelectedBettorLabel.Text = "Anna bets: ";
            }


        }

        private void OnPlaceBet(object sender, RoutedEventArgs e)
        {
            if (_radioBtnJoe.IsChecked == true)
            {


                int bet = int.Parse(_txtBet1.Text);
                int houndNum = int.Parse(_comboBoxHound.SelectedItem as string);
                bool betPlaced;

                _crtSelBettor[0]._name = _txtSelectedBettorLabel.Text;

                if (bet >= minBet)
                {
                    betPlaced = _crtSelBettor[0].PlaceBet(bet, houndNum);

                    if (betPlaced)
                    {
                        _crtSelBettor[0].UpdateLabels();
                        BetPlaced = true;
                    }
                }
            }

            else if (_radioBtnBob.IsChecked == true)
            {


                int bet = int.Parse(_txtBet2.Text);
                int houndNum = int.Parse(_comboBoxHound.SelectedItem as string);
                bool betPlaced;

                _crtSelBettor[1]._name = _txtSelectedBettorLabel.Text;

                if (bet >= minBet)
                {
                    betPlaced = _crtSelBettor[1].PlaceBet(bet, houndNum);

                    if (betPlaced)
                    {
                        _crtSelBettor[1].UpdateLabels();
                        BetPlaced = true;
                    }
                }
            }

            else 
            {


                int bet = int.Parse(_txtBet3.Text);
                int houndNum = int.Parse(_comboBoxHound.SelectedItem as string);
                bool betPlaced;

                _crtSelBettor[2]._name = _txtSelectedBettorLabel.Text;

                if (bet >= minBet)
                {
                    betPlaced = _crtSelBettor[2].PlaceBet(bet, houndNum);

                    if (betPlaced)
                    {
                        _crtSelBettor[2].UpdateLabels();
                        BetPlaced = true;
                    }
                }
            }


        }

        private void OnStartRace(object sender, RoutedEventArgs e)
        {
            _btnPlaceBet.IsEnabled = false;
            _btnStartRace.IsEnabled = false;
            CreateRaceHounds();
            _tmDanceTimer.Start();
           

        }

        private void OnRaceTimerTick(object sender, object e)
        {
            foreach (Greyhound hound in _raceHoundList)
            {
                if(hound.Run())
                {
                    _tmDanceTimer.Stop();

                    int winningHound = _raceHoundList.IndexOf(hound) + 1;

                    foreach(Bettor bettor in _crtSelBettor)
                    {
                        bettor.Collect(winningHound);

                    }

                    foreach(Greyhound _hound in _raceHoundList)
                    {
                        _hound.TakeStartingPosition();
                    }

                }
                
            } 
        }
    }
}
