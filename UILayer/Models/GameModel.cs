namespace UILayer.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public List<TeamModel> Teams { get; set; }
        public string Date { get; set; }
        public Result Result { get; set; }
    }
    public enum Result
    {
        Won, Lose, Draw
    }
}
