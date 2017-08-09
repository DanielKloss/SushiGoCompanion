using Microsoft.Toolkit.Uwp.UI.Extensions;
using MvvmDialogs;
using SushiGoCompanion.Data.Models;
using SushiGoCompanion.UI.Dialogs;
using SushiGoCompanion.UI.ViewModels;
using System.Collections.Generic;
using System;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SushiGoCompanion.UI.Views
{
    public sealed partial class ScoreboardView : Page
    {
        private IDialogService _dialogService;
        private ScoreboardViewModel viewModel;

        public ScoreboardView()
        {
            InitializeComponent();

            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
            {
                StatusBar.SetIsVisible(this, false);
            }

            _dialogService = new DialogService();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = new ScoreboardViewModel((IEnumerable<Player>)e.Parameter);
            viewModel = DataContext as ScoreboardViewModel;

            SystemNavigationManager systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            systemNavigationManager.BackRequested += ScoreboardView_BackRequestedAsync;
            systemNavigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private async void ScoreboardView_BackRequestedAsync(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;

            ContentDialogResult result = await _dialogService.ShowContentDialogAsync(new ConfirmDialogViewModel("End Game", "You are about to quit an unfinished game. Are you sure you want to quit the game without logging the stats?"));

            if (result == ContentDialogResult.Primary)
            {
                ((App)Application.Current).rootFrame.Navigate(typeof(MainMenuView));
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            SystemNavigationManager systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            systemNavigationManager.BackRequested -= ScoreboardView_BackRequestedAsync;
            systemNavigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        private void newRound_Completed(object sender, object e)
        {
            viewModel.newRound = false;
        }
    }
}
