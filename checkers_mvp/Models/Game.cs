using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using checkers_mvp.Misc;
using Newtonsoft.Json;

namespace checkers_mvp.Models
{
    public enum Direction
    {
        Invalid,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight
    }
    public class Game
    {
        public Board board { get; set; }
        public Player player1 { get; set; }
        public Player player2 { get; set; }
        public Player currentPlayer { get; set; }
        public Cell selectedCell { get; set; }
        public Direction lastMoveDirection { get; set; }
        public bool allowMultipleJumps { get; set; }



        public Game(bool allowMJ)
        {
            board = new Board();
            player1 = new Player(Status.Black, 12);
            player2 = new Player(Status.Red, 12);  
            currentPlayer = player1;
            allowMultipleJumps = allowMJ;
        }

        [JsonConstructor]
        public Game(Board board, Player player1, Player player2, Player currentPlayer, Cell selectedCell, Direction lastMoveDirection)
        {
            this.board = board;
            this.player1 = player1;
            this.player2 = player2;
            this.currentPlayer = currentPlayer;
            this.selectedCell = selectedCell;
            this.lastMoveDirection = lastMoveDirection;
        }

        public void saveGame(string path)
        {
            string json = JsonConvert.SerializeObject(this);
            System.IO.File.WriteAllText(path, json);
        }

        public static Game loadGame(string path)
        { 
            string json = System.IO.File.ReadAllText(path);
            Game game = JsonConvert.DeserializeObject<Game>(json);

            if(game.selectedCell != null)
            {
                game.selectedCell = game.board._cells[game.selectedCell.Row, game.selectedCell.Column];
            }
            
            return game;
        }

        public static Game newGame(bool allowMultipleJumps)
        {
           return new Game(allowMultipleJumps);
        }

        public bool isInBounds(int row, int column)
        {
            return row >= 0 && row < 8 && column >= 0 && column < 8;
        }

        public void switchPlayer()
        {
            if(selectedCell != null)
            {
                selectedCell.Status = currentPlayer.Status;
                selectedCell = null;
            }

            currentPlayer = currentPlayer.Status == player1.Status ? player2 : player1;
        }

        public bool selectPiece(int row, int column)
        {
            if(currentPlayer.Status == board._cells[row, column].Status)
            {
                if(selectedCell != null)
                {
                    selectedCell.Status = currentPlayer.Status;
                }

                selectedCell = board._cells[row, column];
                selectedCell.Status = Status.Selected;
                return true;
            }

            return false;
        }

        public bool movePiece(int row, int column)
        {
            if(selectedCell == null)
            {
                return false;
            }

            if (isValidRegularMove(row,column))
            {
                moveSelectedPiece(row, column);

                makeKingIfNeeded(row, column);

                switchPlayer();

                return true;
            }

            lastMoveDirection = getDirectionOfMove(row, column);

            if(isCaptureMove(row, column, lastMoveDirection))
            {
                lastMoveDirection = getDirectionOfMove(row, column);

                moveSelectedPiece(row, column);

                capturePiece(lastMoveDirection ,row, column);

                makeKingIfNeeded(row, column);

                if(canStillCapture(row, column) && allowMultipleJumps)
                {

                    return true;
                }
                else
                {
                    switchPlayer();
                }

                return true;
            }

            if(isKingMove(row, column))
            {
                moveSelectedPiece(row, column);

                switchPlayer();

                return true;
            }

            if(isKingCaptureMove(row, column, lastMoveDirection))
            {
                moveSelectedPiece(row, column);

                capturePiece(lastMoveDirection, row, column);

                if (canStillCapture(row, column) && allowMultipleJumps)
                {
                    return true;
                }
                else
                {
                    switchPlayer();
                }

                return true;
            }

            return false;
        }

        public void moveSelectedPiece(int row, int column)
        {
            board._cells[row, column].Status = selectedCell.Status;
            board._cells[row, column].Status = currentPlayer.Status;
            board._cells[row, column].IsKing = selectedCell.IsKing;

            selectedCell.Status = Status.Empty;
            selectedCell.IsKing = false;
            selectedCell = null;
        }

        public void capturePiece(Direction dir, int row, int column)
        {
            if(dir == Direction.UpLeft)
            {
                board._cells[row + 1, column + 1].Status = Status.Empty;
                board._cells[row + 1, column + 1].IsKing = false;
            }

            else if(dir == Direction.UpRight)
            {
                board._cells[row + 1, column - 1].Status = Status.Empty;
                board._cells[row + 1, column - 1].IsKing = false;
            }

            else if(dir == Direction.DownLeft)
            {
                board._cells[row - 1, column + 1].Status = Status.Empty;
                board._cells[row - 1, column + 1].IsKing = false;
            }

            else if(dir == Direction.DownRight)
            {
                board._cells[row - 1, column - 1].Status = Status.Empty;
                board._cells[row - 1, column - 1].IsKing = false;
            }

            getOpponent().NumOfPieces--;
        }


        public bool isCaptureMove(int row, int column, Direction dir)
        {

            if(isInBounds(row, column) == false)
            {
                return false;
            }

            if (board._cells[row, column].Status != Status.Empty)
            {
                return false;
            }

            if(currentPlayer.Status == Status.Black)
            {
                if(row - selectedCell.Row != 2 || Math.Abs(column - selectedCell.Column) != 2)
                {
                    return false;
                }
            }

            if (currentPlayer.Status == Status.Red)
            {
                if (row - selectedCell.Row != -2 || Math.Abs(column - selectedCell.Column) != 2)
                {
                    return false;
                }
            }

            Point pointToCheck = dirToIndex(dir, row, column);

            if (board._cells[(int)pointToCheck.X, (int)pointToCheck.Y].Status == getOpponent().Status)
            {
                return true;
            }

            return false;
        }

        public bool isKingCaptureMove(int row, int column, Direction dir)
        {
            if (isInBounds(row, column) == false)
            {
                return false;
            }

            if(selectedCell.IsKing == false)
            {
                return false;
            }

            if (board._cells[row, column].Status != Status.Empty)
            {
                return false;
            }

            if (Math.Abs(row - selectedCell.Row) != 2 || Math.Abs(column - selectedCell.Column) != 2)
            {
                return false;
            }

            Point pointToCheck = dirToIndex(dir, row, column);

            if (board._cells[(int)pointToCheck.X, (int)pointToCheck.Y].Status == getOpponent().Status)
            {
                return true;
            }

            return false;
        }   
        
        public void makeKingIfNeeded(int row, int column)
        {
            if(currentPlayer.Status == Status.Black && row == 7)
            {
                board._cells[row, column].IsKing = true;
            }

            if (currentPlayer.Status == Status.Red && row == 0)
            {
                board._cells[row, column].IsKing = true;
            }
        }

        public bool isKingMove(int row, int column)
        {

            if (board._cells[row, column].Status != Status.Empty)
            {
                return false;
            }

            if (selectedCell.IsKing == false)
            {
                return false;
            }

            if(Math.Abs(row - selectedCell.Row) != 1 || Math.Abs(column - selectedCell.Column) != 1)
            {
                return false;
            }

            return true;
        }

        public Point dirToIndex(Direction dir, int row, int column)
        {
            if(dir == Direction.UpLeft)
            {
                return new Point(row + 1, column + 1);
            }

            else if(dir == Direction.UpRight)
            {
                return new Point(row + 1, column - 1);
            }

            else if(dir == Direction.DownLeft)
            {
                return new Point(row - 1, column + 1);
            }

            else if(dir == Direction.DownRight)
            {
                return new Point(row - 1, column - 1);
            }

            return new Point(-1, -1);
        } 

        public bool isValidRegularMove(int row, int column)
        {
            if(board._cells[row, column].Status != Status.Empty)
            {
                return false;
            }

           if(currentPlayer.Status == Status.Black)
            {
                if(row == selectedCell.Row + 1 && Math.Abs(column-selectedCell.Column) == 1)
                {
                    return true;
                }
            }

           if(currentPlayer.Status == Status.Red)
            {
                if (row == selectedCell.Row - 1 && Math.Abs(column - selectedCell.Column) == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool canStillCapture(int row, int column) 
        {
            selectedCell = board._cells[row, column];

            if (selectedCell.IsKing)
            {
                if (isKingCaptureMove(row-2, column-2, Direction.UpLeft) || isKingCaptureMove(row - 2, column + 2, Direction.UpRight) || isKingCaptureMove(row + 2, column - 2, Direction.DownLeft) || isKingCaptureMove(row + 2, column + 2, Direction.DownRight))
                {
                    return true;
                }
            }

            if (isCaptureMove(row - 2, column - 2, Direction.UpLeft) || isCaptureMove(row - 2, column + 2, Direction.UpRight) || isCaptureMove(row + 2, column - 2, Direction.DownLeft) || isCaptureMove(row + 2, column + 2, Direction.DownRight))
            {
                return true;
            }

            selectedCell = null;
            return false;

        }

        public Direction getDirectionOfMove(int row, int column)
        {
           if(row < selectedCell.Row && column < selectedCell.Column)
            {
                return Direction.UpLeft;
            }
            else if(row < selectedCell.Row && column > selectedCell.Column)
            {
                return Direction.UpRight;
            }
            else if(row > selectedCell.Row && column < selectedCell.Column)
            {
                return Direction.DownLeft;
            }
            else if(row > selectedCell.Row && column > selectedCell.Column)
            {
                return Direction.DownRight;
            }

            return Direction.Invalid;
        }

        public Player getOpponent()
        {
            return currentPlayer == player1 ? player2 : player1;
        }

        public bool isGameOver()
        {
            return player1.NumOfPieces == 0 || player2.NumOfPieces == 0;
        }
    }
}
