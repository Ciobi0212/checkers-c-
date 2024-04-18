using checkers_mvp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace checkers_mvp.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        public Cell _cell;

        private Brush _color;

        public CellViewModel(Cell cell)
        {
            _cell = cell;
            updateFromModel();
        }

        public Status Status
        {
            get { return _cell.Status; }
            set { _cell.Status = value;}
        }

        public Brush Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }

        public void updateFromModel()
        {
            if(_cell.IsKing && _cell.Status != Status.Selected)
            {
                if(_cell.Status == Status.Red)
                {
                    Color = Brushes.Orange;
                }
                else
                {
                    Color = Brushes.Brown;
                }
                return;
            }

            switch (_cell.Status)
            {
                case Status.Empty:
                    Color = Brushes.Transparent;
                    break;
                case Status.Unplayable:
                    Color = Brushes.Transparent;
                    break;
                case Status.Red:
                    Color = Brushes.Red;
                    break;
                case Status.Black:
                    Color = Brushes.Black;
                    break;
                case Status.Selected:
                    Color = Brushes.LightGreen;
                    break;
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
