namespace SushiGoCompanion.Data.Models
{
    public class SushiType
    {
        public string name { get; set; }
        public int numberOfCards { get; set; }
        public int wasabis { get; set; }
        public string image { get; set; }

        public SushiType(Sushis Name)
        {
            name = Name.ToString();
            numberOfCards = 0;
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
