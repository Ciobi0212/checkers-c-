using checkers_mvp.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers_mvp.ViewModels
{
    public class WinningStatsViewmodel : INotifyPropertyChanged
    {
        private int _redWins;
        private int _blackWins;
        private int _redMostPieces;
        private int _blackMostPieces;

        public WinningStatsViewmodel()
        {
            _redWins = WinningStats.stats.RedWins;
            _blackWins = WinningStats.stats.BlackWins;
            _redMostPieces = WinningStats.stats.RedMostPieces;
            _blackMostPieces = WinningStats.stats.BlackMostPieces;
        }

        public int RedWins
        {
            get { return _redWins; }
            set
            {
                _redWins = value;
                OnPropertyChanged("RedWins");
            }
        }

        public int BlackWins
        {
            get { return _blackWins; }
            set
            {
                _blackWins = value;
                OnPropertyChanged("BlackWins");
            }
        }

        public int RedMostPieces
        {
            get { return _redMostPieces; }
            set
            {
                _redMostPieces = value;
                OnPropertyChanged("RedMostPieces");
            }
        }

        public int BlackMostPieces
        {
            get { return _blackMostPieces; }
            set
            {
                _blackMostPieces = value;
                OnPropertyChanged("BlackMostPieces");
            }
        }

        public void updateFromModel()
        {
            BlackWins = WinningStats.stats.BlackWins;
            RedWins = WinningStats.stats.RedWins;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
