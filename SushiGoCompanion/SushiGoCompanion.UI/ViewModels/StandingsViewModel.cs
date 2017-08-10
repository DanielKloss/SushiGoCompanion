using SushiGoCompanion.Data.Models;
using SushiGoCompanion.UI.Views;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace SushiGoCompanion.UI.ViewModels
{
    public class StandingsViewModel : BaseViewModel
    {
        private Game _game;
        public Game game
        {
            get { return _game; }
            set
            {
                _game = value;
                OnPropertyChanged(nameof(game));
            }
        }

        private ICommand _finishCommand;
        public ICommand finishCommand
        {
            get
            {
                if (_finishCommand == null)
                {
                    _finishCommand = new Command(FinishGame);
                }

                return _finishCommand;
            }
            set { _finishCommand = value; }
        }

        public void FinishGame()
        {
            ((App)Application.Current).rootFrame.Navigate(typeof(MainMenuView));
        }
    }
}
