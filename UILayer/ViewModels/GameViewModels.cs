using UILayer.Models;

namespace UILayer.ViewModels
{
    public class GameViewModel
    {
        public IEnumerable<PlayerModel> Players { get; set; }
        public IEnumerable<TeamModel> Teams { get; set; }
    }
}
