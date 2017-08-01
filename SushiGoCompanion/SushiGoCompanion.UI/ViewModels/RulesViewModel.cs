using System.Collections.ObjectModel;
using SushiGoCompanion.UI.Models;

namespace SushiGoCompanion.UI.ViewModels
{
    public class RulesViewModel : BaseViewModel
    {
        private ObservableCollection<RulesCategory> _ruleCategories;
        public ObservableCollection<RulesCategory> ruleCategories
        {
            get { return _ruleCategories; }
            set
            {
                _ruleCategories = value;
                OnPropertyChanged(nameof(ruleCategories));
            }
        }

        public RulesViewModel()
        {
            ruleCategories = new ObservableCollection<RulesCategory>
            {
                new RulesCategory
                {
                    header = "Setup",
                    instructions = new ObservableCollection<string>
                    {
                        "Shuffle and deal cards to each player",
                        "2 players - 10 cards each",
                        "3 players - 9 cards each",
                        "4 players - 8 cards each",
                        "5 players - 7 cards each"
                    }
                },
                new RulesCategory
                {
                    header = "Gameplay",
                    instructions = new ObservableCollection<string>
                    {
                        "All players play simultaneously",
                        "Choose a card from your hand to keep",
                        "Pass the rest of the cards to the player on your left",
                        "Wasabi - can only be used with the NEXT nigiri you pick up",
                        "Chopsticks - can be swapped for two cards",
                        "The round ends when all players have played their last card",
                        "The game lasts for 3 rounds"
                    }
                },
                new RulesCategory
                {
                    header = "Variants",
                    instructions = new ObservableCollection<string>
                    {
                        "PASS BOTH WAYS - Alternate the direction cards are passed. In round two pass to the right instead!",
                        "TWO PPLAYER - playing with two players requires the use of a 'dummy player'. Take turns controlling the dummy player"
                    }
                },
            };
        }
    }
}
