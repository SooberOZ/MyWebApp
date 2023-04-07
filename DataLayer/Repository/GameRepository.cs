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
    public class GameRepository : IRepository<Game>
    {
        private readonly Context DB;
        public GameRepository(Context context)
        {
            DB = context;
        }
        public IEnumerable<Game> ReadAll() => DB.Games;
        public Game Read(int id) => DB.Games.Find(id);
        public void Create(Game game) => DB.Games.Add(game);
        public void Update(Game game) => DB.Games.Update(game);
        public void Delete(int id)
        {
            Game game = DB.Games.Find(id);
            if (game != null)
            {
                DB.Games.Remove(game);
            }
        }
    }
}
