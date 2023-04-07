using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public List<Team> Teams { get; set; }
        public string Date { get; set; }
        public Result Result { get; set; }
    }
    public enum Result
    {
        Won, Lose, Draw
    }
}
