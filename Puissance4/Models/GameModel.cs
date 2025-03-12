using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puissance4.Models
{
    public class GameModel
    {
        public Cell[,] Grid { get; }
        public int Rows { get; }
        public int Columns { get; }
        public Player CurrentPlayer { get; set; }
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        public GameModel(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Grid = new Cell[Rows, Columns];

            // Initialiser les cellules
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Grid[row, col] = new Cell(row, col);
                }
            }

            // Initialiser les joueurs
            Player1 = new Player(1) { Name = "X", Color = "Red" };
            Player2 = new Player(2) { Name = "O", Color = "Yellow" };
            CurrentPlayer = Player1;
        }

        public bool PlaceToken(int column)
        {
            if (column < 0 || column >= Columns)
                return false;

            int row = ColumnHeight(column);
            if (row < 0) // Colonne pleine
                return false;

            Grid[row, column].Owner = CurrentPlayer;

            bool victory = CheckVictory(row, column);
            if (!victory)
                SwitchPlayer();

            return true;
        }

        int ColumnHeight(int column)
        {
            // Parcourir la colonne de bas en haut pour trouver la première case vide
            for (int row = Rows - 1; row >= 0; row--)
            {
                if (Grid[row, column].Owner == null)
                    return row;
            }
            return -1; // Colonne pleine
        }

        void SwitchPlayer()
        {
            CurrentPlayer = (CurrentPlayer == Player1) ? Player2 : Player1;
        }

        bool CheckVictory(int lastRow, int lastCol)
        {
            // Directions: horizontal, vertical, diagonal montant, diagonal descendant
            int[][] directions = new int[][]
            {
                new int[] { 0, 1 },  // Horizontal
                new int[] { 1, 0 },  // Vertical
                new int[] { 1, 1 },  // Diagonale descendante
                new int[] { 1, -1 }  // Diagonale montante
            };

            foreach (var direction in directions)
            {
                int count = 1; // Le jeton placé

                // Vérifier dans une direction
                count += CountTokens(lastRow, lastCol, direction[0], direction[1]);

                // Vérifier dans la direction opposée
                count += CountTokens(lastRow, lastCol, -direction[0], -direction[1]);

                if (count >= 4)
                    return true;
            }

            return false;
        }

        private int CountTokens(int row, int col, int rowDelta, int colDelta)
        {
            int count = 0;
            int r = row + rowDelta;
            int c = col + colDelta;

            while (r >= 0 && r < Rows && c >= 0 && c < Columns && Grid[r, c].Owner == CurrentPlayer)
            {
                count++;
                r += rowDelta;
                c += colDelta;
            }

            return count;
        }

        public bool IsBoardFull()
        {
            for (int col = 0; col < Columns; col++)
            {
                if (ColumnHeight(col) >= 0)
                    return false;
            }
            return true;
        }

        public void Reset()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Grid[row, col].Owner = null;
                }
            }
            CurrentPlayer = Player1;
        }
    }
}
