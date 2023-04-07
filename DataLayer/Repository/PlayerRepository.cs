using DataLayer.EF;
using DataLayer.Entities;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class PlayerRepository : IRepository<Player>
    {
        private Context DB;

        public PlayerRepository(Context context)
        {
            DB = context;
        }
        public IEnumerable<Player> ReadAll()
        {
            return DB.Players;
        }
        public Player Read(int id)
        {
            return DB.Players.Find(id);
        }
        public void Create(Player player)
        {
            DB.Players.Add(player);
        }
        public void Update(Player player)
        {
            var previousPlayer = DB.Players.Find(player.Id);
            if (previousPlayer != null)
            {
                DB.Players.Remove(previousPlayer);
                Player newPlayer = new Player()
                {
                    NickName = player.NickName,
                    Rank = player.Rank
                };
                DB.Players.Update(newPlayer);
            }
        }
        public void Delete(int id)
        {
            Player player = DB.Players.Find(id);
            if (player != null)
            {
                DB.Players.Remove(player);
            }
        }
    }
}
