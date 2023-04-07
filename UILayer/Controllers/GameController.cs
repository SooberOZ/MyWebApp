using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using UILayer.ViewModels;
using UILayer.Models;

namespace UILayer.Controllers
{
    public class GamesController : Controller
    {
        private static int currentEditionID;
        private readonly IMapper _mapper;

        public GamesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult AddPage()
        {
            using (var db = new BL())
            {
                GameViewModel model = new GameViewModel()
                {
                    Players = _mapper.Map<List<PlayerModel>>(db.GetPlayers()),
                    Teams = _mapper.Map<List<TeamModel>>(db.GetTeams())
                };
                return View(model);
            }
        }
        public IActionResult AddGame(int[] team, string date, string result)
        {
            List<TeamModel> choosenTeam = new List<TeamModel>();
            Models.Result choosenResult = 0;
            using (var db = new BL())
            {
                List<TeamModel> listTeams = _mapper.Map<List<TeamModel>>(db.GetTeams());

                for (int i = 0; i < team.Length; i++)
                {
                    foreach (TeamModel teamModel in listTeams)
                    {
                        if (teamModel.Id == team[i])
                        {
                            choosenTeam.Add(teamModel);
                            break;
                        }
                    }
                }

                switch (result)
                {
                    case "Won":
                        choosenResult = Models.Result.Won;
                        break;

                    case "Lose":
                        choosenResult = Models.Result.Lose;
                        break;

                    case "Draw":
                        choosenResult = Models.Result.Draw;
                        break;

                }

                foreach (TeamModel teams in choosenTeam)
                {
                    db.UpdateTeam(_mapper.Map<TeamBL>(teams));
                };

                GameModel game = new GameModel()
                {
                    Date = date,
                    Result = choosenResult,
                    Teams = choosenTeam
                };

                db.AddGame(_mapper.Map<GameBL>(game));
            }
            return Redirect("~/Home/Index");
        }
        public IActionResult ShowAllPage()
        {
            List<GameModel> list = null;

            using (var db = new BL())
                list = _mapper.Map<List<GameModel>>(db.GetGames());

            return View(list);
        }

    }
}
