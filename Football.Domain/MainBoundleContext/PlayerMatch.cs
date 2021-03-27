namespace Football.Domain.MainBoundleContext
{
    public class PlayerMatch : Entity
    {
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public string Type { get; set; }
    }

    public static class PlayerMatchType
    {
        public static string AwayPlayer = "AwayPlayer";
        public static string HousePlayer = "HousePlayer";
    }
}
