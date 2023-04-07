namespace UILayer.Models
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PlayerModel> players { get; set; }
        public Tier League { get; set; }
    }
    public enum Tier
    {
        First, Second, Third
    }
}