using DataLayer.EF;
using DataLayer.Entities;
using DataLayer.Interfaces;
using DataLayer.Repository;
using System.Numerics;

namespace DataLayer
{
    public class UnitOfWorks : IUnitOfWork
    {
        private readonly Context _dataBase;
        private PlayerRepository _playersRepository;
        private TeamRepository _teamsRepository;
        private GameRepository _gamesRepository;
        public UnitOfWorks()
        {
            _dataBase = new Context();
        }

        public IRepository<Player> Players
        {
            get
            {
                if (_playersRepository == null)
                    _playersRepository = new PlayerRepository(_dataBase);
                return _playersRepository;
            }
        }
        public IRepository<Game> Games
        {
            get
            {
                if (_gamesRepository == null)
                    _gamesRepository = new GameRepository(_dataBase);
                return _gamesRepository;
            }
        }
        public IRepository<Team> Teams
        {
            get
            {
                if (_teamsRepository == null)
                    _teamsRepository = new TeamRepository(_dataBase);
                return _teamsRepository;
            }
        }
        public void Save()
        {
            _dataBase.SaveChanges();
        }

        public void Dispose()
        {
            _dataBase.Dispose();
        }
    }
}