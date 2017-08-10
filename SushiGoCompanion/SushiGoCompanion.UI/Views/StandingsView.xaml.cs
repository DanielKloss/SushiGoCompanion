using Microsoft.Toolkit.Uwp.UI.Extensions;
using SushiGoCompanion.Data.Models;
using SushiGoCompanion.UI.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SushiGoCompanion.UI.Views
{
    public sealed partial class StandingsView : Page
    {
        StandingsViewModel viewModel;

        public StandingsView()
        {
            InitializeComponent();

            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
            {
                StatusBar.SetIsVisible(this, false);
            }

            DataContext = new StandingsViewModel();
            viewModel = DataContext as StandingsViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            systemNavigationManager.BackRequested += StandingsView_BackRequested;
            systemNavigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            viewModel.game = e.Parameter as Game;
            viewModel.game.players = new ObservableCollection<Player>(viewModel.game.players.OrderByDescending(p => p.totalScore));
        }

        private void StandingsView_BackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            ((App)Application.Current).rootFrame.Navigate(typeof(MainMenuView));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            SystemNavigationManager systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            systemNavigationManager.BackRequested -= StandingsView_BackRequested;
            systemNavigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }
    }
}
