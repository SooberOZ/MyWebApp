using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Player> players { get; set; }
        public Tier League { get; set; }
    }
    public enum Tier
    {
        First, Second, Third
    }
}
