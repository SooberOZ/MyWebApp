using AutoMapper;
using BusinessLayer.Entities;
using DataLayer;
using DataLayer.Entities;
using System.Numerics;

namespace BusinessLayer
{
    public class BL : IDisposable
    {
        private  UnitOfWorks _DB { get; }
        private readonly IMapper _mapper;

        public BL()
        {
            _DB = new UnitOfWorks();
        }

        public void AddPlayers(PlayerBL element)
        {
            _DB.Players.Create(_mapper.Map<Player>(element));
            _DB.Save();
        }

        public void AddTeam(TeamBL element)
        {
            List<Player> players = _mapper.Map<List<Player>>(element.players);
            Game league = _mapper.Map<Game>(element.League);
            Team team = new Team()
            {
                Name = element.Name,
                players = players,
                League = (DataLayer.Entities.Tier)element.League
            };
            _DB.Teams.Create(team);
            _DB.Save();
        }
        public void AddGame(GameBL element)
        {
            _DB.Games.Create(_mapper.Map<Game>(element));
            _DB.Save();
        }
        public void DeletePlayer(int id)
        {
            _DB.Players.Delete(id);
            _DB.Save();
        }
        public void DeleteTeam(int id)
        {
            _DB.Teams.Delete(id);
            _DB.Save();
        }
        public void DeleteGame(int id)
        {
            _DB.Games.Delete(id);
            _DB.Save();
        }
        public void UpdatePlayer(PlayerBL element)
        {
            Player toUpdate = _DB.Players.Read(element.Id);
            if (toUpdate != null)
            {
                toUpdate = _mapper.Map<Player>(element);
                _DB.Players.Update(toUpdate);
                _DB.Save();
            }
        }
        public void UpdateTeam(TeamBL element)
        {
            Team toUpdate = _DB.Teams.Read(element.Id);
            List<Player> list = new List<Player>();
            foreach (Player player in list)
            {
                list.Add(_DB.Players.Read(player.Id));
            }
            if (toUpdate != null)
            {
                toUpdate.players = list;
                toUpdate.League = toUpdate.League;
                toUpdate.Name = element.Name;

                _DB.Teams.Update(toUpdate);
                _DB.Save();
            }
        }
        public void UpdateGame(GameBL element)
        {
            Game toUpdate = _DB.Games.Read(element.Id);
            if (toUpdate != null)
            {
                toUpdate = _mapper.Map<Game>(element);
                _DB.Games.Update(toUpdate);
                _DB.Save();
            }
        }
        public IEnumerable<PlayerBL> GetPlayers()
        {
            List<PlayerBL> result = new List<PlayerBL>();

            foreach (var item in _DB.Players.ReadAll())
                result.Add(_mapper.Map<PlayerBL>(item));

            return result;
        }
        public IEnumerable<TeamBL> GetTeams()
        {
            List<TeamBL> result = new List<TeamBL>();

            foreach (var item in _DB.Teams.ReadAll())
            {
                result.Add(new TeamBL
                {
                    players = _mapper.Map<List<PlayerBL>>(item.players),
                    Name = item.Name,
                    League = (BusinessLayer.Entities.Tier)item.League,
                });
            }
            return result;
        }
        public IEnumerable<GameBL> GetGames()
        {
            List<GameBL> result = new List<GameBL>();
            foreach (var item in _DB.Games.ReadAll())
            {
                result.Add(_mapper.Map<GameBL>(item));
            }
            return result;
        }
        public void Dispose()
        {
            _DB.Dispose();
        }

       
    }
}