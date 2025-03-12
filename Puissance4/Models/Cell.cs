using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Puissance4.Models
{
    public class Cell : INotifyPropertyChanged
    {
        private Player _owner;
        public int Row { get; set; }
        public int Column { get; set; }

        public Player Owner
        {
            get => _owner;
            set
            {
                if (_owner != value)
                {
                    _owner = value;
                    OnPropertyChanged();
                }
            }
        }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static int RowHeight(Cell[,] grid, int column)
        {
            int height = 0;
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                if (grid[row, column] != null)
                {
                    height++;
                }
            }
            return height;
        }
    }
}
