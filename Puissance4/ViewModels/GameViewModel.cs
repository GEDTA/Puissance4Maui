using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Puissance4.Models;

namespace Puissance4.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {

        private GameModel _gameModel;
        public ObservableCollection<ObservableCollection<Models.Cell>> Grid { get; private set; }
        public Player CurrentPlayer => _gameModel.CurrentPlayer;
        public ICommand PlaceTokenCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public GameViewModel()
        {
            _gameModel = new GameModel(6, 7);
            Grid = new ObservableCollection<ObservableCollection<Models.Cell>>();

            for (int row = 0; row < _gameModel.Rows; row++)
            {
                var rowCollection = new ObservableCollection<Models.Cell>();
                for (int col = 0; col < _gameModel.Columns; col++)
                {
                    rowCollection.Add(_gameModel.Grid[row, col]);
                }
                Grid.Add(rowCollection);
            }

            PlaceTokenCommand = new Command<string>(PlaceToken);

            ResetCommand = new Command(Reset);
        }



        private void PlaceToken(string column2)
        {

            var column = int.Parse(column2);
            var modifiedrow= 0;
            var win = false;
            if (_gameModel.PlaceToken(column))
            {
                for (int row = 0; row < _gameModel.Rows; row++)
                {
                    Grid[row][column] = _gameModel.Grid[row, column];
                    Grid[row][column].OnPropertyChanged(nameof(Models.Cell.Owner));
                    modifiedrow = row;
                }
                OnPropertyChanged(nameof(CurrentPlayer));
            }
            
            win = _gameModel.CheckVictory(modifiedrow, column);
            if (win)
            {
                var winner = _gameModel.CurrentPlayer;
                var message = $"Le joueur {winner.Name} a gagné!";
                var title = "Victoire";
                var action = Application.Current.MainPage.DisplayAlert(title, message, "OK");
                Reset();
            }
        }



        private void Reset()
        {
            _gameModel.Reset();
            OnPropertyChanged(nameof(CurrentPlayer));
            OnPropertyChanged(nameof(Grid));
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
