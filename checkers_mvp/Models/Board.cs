using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace checkers_mvp.Models
{
    public class Board
    {
        public const int _size = 8;
        public Cell[,] _cells = new Cell[_size, _size];

        public Board()
        {
            initBoard();
            initPieces();
        }

        public void initBoard()
        {
            bool isPlayable = false;

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    if (isPlayable)
                    {
                        _cells[i, j] = new Cell(Status.Empty,i,j,false);
                    }
                    else
                    {
                        _cells[i, j] = new Cell(Status.Unplayable, i, j, false);
                    }

                    isPlayable = !isPlayable;
                }

                isPlayable = !isPlayable;
            }
        }

        public void initPieces()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    if (_cells[i, j].Status == Status.Empty)
                    {
                        if (i < 3)
                        {
                            _cells[i, j].Status = Status.Black;
                          
                        }
                        else if (i > 4)
                        {
                            _cells[i, j].Status = Status.Red;
                        }
                    }
                }
            }
        }

        public Cell[,] Cells
        {
            get { return _cells; }
            set { _cells = value; }
        }
    }
}
