using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace checkers_mvp.Models
{
    public enum Status
    {
        Unplayable,
        Empty,
        Selected,
        Red,
        Black
    }
    public class Cell
    {
        private Status _status;
        private int _row;
        private int _column;
        private bool _isKing;
        public Cell(Status status, int row, int column, bool isKing)
        {
            _status = status;
            _row = row;
            _column = column;
            _isKing = isKing;
        }

        public Status Status { get { return _status; } set { _status = value; } }

        public int Row { get { return _row; } set { _row = value; } }

        public int Column { get { return _column; } set { _column = value; } }

        public bool IsKing { get { return _isKing; } set { _isKing = value; } }
    }
}
