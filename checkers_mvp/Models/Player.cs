using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace checkers_mvp.Models
{
    public class Player
    {
        private Status _status;
        private int _num_of_pieces;

        public Player(Status color, int num_of_pieces)
        {
            _status = color;
            _num_of_pieces = num_of_pieces;
        }
      
        public Status Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public int NumOfPieces
        {
            get { return _num_of_pieces; }
            set { _num_of_pieces = value; }
        }
    }
}
