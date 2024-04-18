using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using checkers_mvp.Commands;
using checkers_mvp.Misc;
using checkers_mvp.Models;

namespace checkers_mvp.ViewModels
{
    public class GameViewModel: INotifyPropertyChanged
    {
        private Game game { get; set; }
        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        private int _blackPieces;
        private int _redPieces;
        private string currentPlayer;
        private string allowMultipleJumps;
        private Brush allowMultipleJumpsColor;

        public int BlackPieces { get { return _blackPieces ; } set { _blackPieces = value; OnPropertyChanged("BlackPieces"); } }
        public int RedPieces { get { return _redPieces; } set { _redPieces = value; OnPropertyChanged("RedPieces"); } }
        public string CurrentPlayer { get { return currentPlayer; } set { currentPlayer = value; OnPropertyChanged("CurrentPlayer"); } }

        public string AllowMultipleJumps { get { return allowMultipleJumps; } set { allowMultipleJumps = value; OnPropertyChanged("AllowMultipleJumps"); } }

        public Brush AllowMultipleJumpsColor { get { return allowMultipleJumpsColor; } set { allowMultipleJumpsColor = value; OnPropertyChanged("AllowMultipleJumpsColor"); } }



        public ICommand EndTurn { get; set; }

        
        public GameViewModel()
        {
           EndTurn = new RelayCommand(endTurn, (object obj) => { return true; });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void updateCells()
        {
            Cells.Clear();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Cells.Add(new CellViewModel(game.board.Cells[i, j]));
                }
            }
        }

       public void processClick(int row, int column)
        {
            if(game.selectedCell == null || game.board._cells[row, column].Status == game.currentPlayer.Status)
            {
                game.selectPiece(row, column);
            }
            else if (game.selectedCell != null)
            {
                game.movePiece(row, column);
            }

            if (game.isGameOver())
            {
                string winner = game.getOpponent() == game.player1 ? "Black" : "Red";
                int nPieces = game.getOpponent() == game.player1 ? game.player1.NumOfPieces : game.player2.NumOfPieces;
                MessageBox.Show("Game Over! " + winner + " wins!");
                WinningStats.saveWinningStats(winner, nPieces);
                newGame(game.allowMultipleJumps);
            }

            this.updateFromModel();
        }

        public void endTurn(object obj)
        {
            game.switchPlayer();
            this.updateFromModel();
        }

        public void saveGame(string path)
        {  
            game.saveGame(path);
        }

        public void loadGame(string path)
        {
            this.game =  Game.loadGame(path);
            updateCells();
            updateStats();
        }

        public void newGame(bool allowMultipleJumps)
        {
            this.game = Game.newGame(allowMultipleJumps);
            updateCells();
            updateStats();
        }

       public void updateFromModel()
        {
            foreach (CellViewModel cellViewModel in Cells)
            {
                cellViewModel.updateFromModel();
            }

            updateStats();
        }


        private void updateStats()
        {
            BlackPieces = game.player1.NumOfPieces;
            RedPieces = game.player2.NumOfPieces;
            CurrentPlayer = game.currentPlayer == game.player1 ? "Black" : "Red";
            AllowMultipleJumps = game.allowMultipleJumps ? "On" : "Off";
            AllowMultipleJumpsColor = game.allowMultipleJumps ? Brushes.Green : Brushes.Red;
        }
    }
}
