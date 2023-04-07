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
    public class TeamRepository : IRepository<Team>
    {
        private readonly Context DB;
        public TeamRepository(Context context)
        {
            DB = context;
        }
        public IEnumerable<Team> ReadAll()
        {
            return DB.Teams;
        }
        public Team Read(int id)
        {
            return DB.Teams.Find(id);
        }
        public void Create(Team team)
        {
            DB.Teams.Add(team);
        }
        public void Update(Team team)
        {
            Team previousTeam = DB.Teams.Find(team.Id);
            if (previousTeam != null)
            {
                DB.Remove(previousTeam);
                Team newTeam = new Team()
                {
                    Name = team.Name,
                    League = team.League
                };
                DB.Teams.Add(newTeam);
            }
        }
        public void Delete(int id)
        {
            Team team = DB.Teams.Find(id);
            if (team != null)
            {
                DB.Teams.Remove(team);
            }
        }
    }
}
