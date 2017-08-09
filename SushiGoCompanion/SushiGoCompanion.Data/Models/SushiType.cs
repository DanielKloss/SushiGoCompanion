namespace SushiGoCompanion.Data.Models
{
    public class SushiType : TrackPropertyChanged
    {
        public string name { get; set; }
        public string image { get; set; }

        private int _numberOfCards;
        public int numberOfCards
        {
            get { return _numberOfCards; }
            set
            {
                _numberOfCards = value;
                OnPropertyChanged(nameof(numberOfCards));
            }
        }

        private int _wasabis;
        public int wasabis
        {
            get { return _wasabis; }
            set
            {
                _wasabis = value;
                OnPropertyChanged(nameof(wasabis));
            }
        }

        public SushiType(Sushis Name)
        {
            name = Name.ToString().Contains("Nigiri") ? Name.ToString().Insert(Name.ToString().IndexOf("Nigiri", 0), " ") : Name.ToString();
            numberOfCards = 0;
            wasabis = 0;
            image = "/Assets/Sushi/" + name + ".png";
        }
    }

    public enum Sushis
    {
        Maki,
        Dumpling,
        Tempura,
        Sashimi,
        EggNigiri,
        SquidNigiri,
        SalmonNigiri,
        Pudding
    }
}
