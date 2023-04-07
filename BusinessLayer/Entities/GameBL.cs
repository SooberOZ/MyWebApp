using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public class GameBL
    {
        public int Id { get; set; }
        public List<TeamBL> Teams { get; set; }
        public string Date { get; set; }
        public Result Result { get; set; }
    }
    public enum Result
    {
        Won, Lose, Draw
    }
}
