using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using UILayer.Models;
using UILayer.ViewModels;

namespace UILayer.Controllers
{
    public class TeamController : Controller
    {
        public static int currentEditionID;
        private readonly IMapper _mapper;

        public TeamController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult AddPage()
        {
            using (var db = new BL())
            {
                TeamViewModel model = new TeamViewModel()
                {
                    Players = _mapper.Map<List<PlayerModel>>(db.GetPlayers())
                };
                return View(model);
            }
        }

        public IActionResult AddTeam(int[] player, string name, string tier)
        {
            List<PlayerModel> chosenPlayer = new List<PlayerModel>();
            Models.Tier chosenTier = 0;
            using (var db = new BL())
            {
                List<PlayerModel> listPlayers = _mapper.Map<List<PlayerModel>>(db.GetPlayers());

                for (int i = 0; i < player.Length; i++)
                {
                    foreach (PlayerModel playerModel in listPlayers)
                    {
                        if (playerModel.Id == player[i])
                        {
                            chosenPlayer.Add(playerModel);
                            break;
                        }
                    }
                }

                switch (tier)
                {
                    case "First":
                        chosenTier = Models.Tier.First;
                        break;
                    case "Second":
                        chosenTier = Models.Tier.Second;
                        break;
                    case "Third":
                        chosenTier = Models.Tier.Third;
                        break;
                }

                foreach (PlayerModel players in chosenPlayer)
                {
                    db.UpdateTeam(_mapper.Map<TeamBL>(players));
                };

                TeamModel team = new TeamModel()
                {
                    Name = name,
                    League = chosenTier,
                    players = chosenPlayer
                };
                db.AddTeam(_mapper.Map<TeamBL>(team));
            }
            return Redirect("~/Home/Index");
        }

        public IActionResult SearchPage()
        {
            return View();
        }

        public IActionResult SearchResult(string name)
        {
            List<TeamModel> foundTeams = new List<TeamModel>();

            using (var db = new BL())
            {
                foreach (TeamModel teams in _mapper.Map<List<TeamModel>>(db.GetTeams()))
                {
                    if (teams.Name == name)
                        foundTeams.Add(teams);
                }
            }

            return View("ShowAllPage", foundTeams);
        }

        public IActionResult ShowAllPage()
        {
            List<TeamModel> list = null;

            using (var db = new BL())
                list = _mapper.Map<List<TeamModel>>(db.GetTeams());

            return View(list);
        }

        public IActionResult Delete(int id)
        {
            using (var db = new BL())
                db.DeleteTeam(id);

            return Redirect("~/Home/Index");
        }
        public IActionResult DeleteAllStadiums()
        {
            using (var db = new BL())
            {
                foreach (TeamModel stadium in _mapper.Map<List<TeamModel>>(db.GetTeams()))
                    db.DeleteTeam(stadium.Id);
            }

            return Redirect("~/Home/Index");
        }

        public IActionResult EditPage(int id)
        {
            TeamModel team = null;

            using (var db = new BL())
            {
                var teams = _mapper.Map<List<TeamModel>>(db.GetTeams());
                foreach (TeamModel model in teams)
                {
                    if (model.Id == id)
                    {
                        team = model;
                        break;
                    }
                }
            }

            if (team != null)
            {
                currentEditionID = team.Id;
                ViewData["TeamName"] = team.Name;
                ViewData["TeamTier"] = team.League;
                ViewData["TeamPlayers"] = team.players;
            }

            return View();
        }
    }
}
