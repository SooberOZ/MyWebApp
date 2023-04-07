using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using UILayer.Models;


namespace UILayer.Controllers
{
    public class PlayersController : Controller
    {
        public static int currentEditionID;
        private readonly IMapper _mapper;

        public PlayersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult AddPage()
        {

            return View();
        }

        public IActionResult AddPlayer(string nickName, string rank)
        {
            PlayerModel player = new PlayerModel()
            {
                NickName = nickName,
                Rank = rank
            };

            using (var db = new BL())
            {
                db.AddPlayers(_mapper.Map<PlayerBL>(player));
            }
               

            return Redirect("~/Home/Index");
        }

        public IActionResult SearchPage()
        {
            return View();
        }

        public IActionResult SearchResult(string nickName)
        {
            List<PlayerModel> foundPLayer = new List<PlayerModel>();
            using (var db = new BL())
            {
                foreach (PlayerModel player in _mapper.Map<List<PlayerModel>>(db.GetPlayers()))
                {
                    if (player.NickName == nickName)
                    {
                        foundPLayer.Add(player);
                    }
                }
            }
            return View("ShowAllPage", foundPLayer);
        }
        public IActionResult ShowAllPage()
        {
            List<PlayerModel> list = null;
            using (var db = new BL())
            {
                list = _mapper.Map<List<PlayerModel>>(db.GetPlayers());
            }
            return View(list);
        }
        public IActionResult Delete(int id)
        {
            using (var db = new BL())
            {
                db.DeletePlayer(id);
            }
            return Redirect("~/Home/Index");
        }
        public IActionResult DeleteAllPlayers()
        {
            using (var db = new BL())
            {
                foreach (PlayerModel player in _mapper.Map<List<PlayerModel>>(db.GetPlayers()))
                    db.DeletePlayer(player.Id);
            }
            return Redirect("~/Home/Index");
        }
        public IActionResult EditPage(int id)
        {
            PlayerModel player = null;
            using (var db = new BL())
            {
                var players = _mapper.Map<List<PlayerModel>>(db.GetPlayers());
                foreach (var model in players)
                {
                    if (model.Id == id)
                    {
                        player = model;
                        break;
                    }
                }
            }
            if (player != null)
            {
                currentEditionID = player.Id;
                ViewData["PlayerNickName"] = player.NickName;
                ViewData["PlayerRank"] = player.Rank;
            }
            return View();
        }
        public IActionResult SaveChanges(string nickName, string rank)
        {
            PlayerModel player = new PlayerModel()
            {
                Id = currentEditionID,
                NickName = nickName,
                Rank = rank
            };
            using(var db = new BL())
            {
                db.UpdatePlayer(_mapper.Map<PlayerBL>(player));
            }
            
            return Redirect("~/Home/Index");
        }
    }
}
