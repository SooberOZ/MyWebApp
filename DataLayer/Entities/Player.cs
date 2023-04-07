using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string? Rank { get; set; }
    }
}
