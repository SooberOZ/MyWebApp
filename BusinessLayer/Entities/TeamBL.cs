using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public class TeamBL
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PlayerBL> players { get; set; }
        public Tier League { get; set; }
    }
    public enum Tier
    {
        First, Second, Third
    }
}
