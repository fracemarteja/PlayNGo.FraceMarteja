using AutoMapper;
using PlayNGo.FraceMarteja.DAL;
using PlayNGo.FraceMarteja.DAL.Contracts;
using PlayNGo.FraceMarteja.DAL.Models;
using PlayNGo.FraceMarteja.DAL.Services;
using PlayNGo.FraceMarteja.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PlayNGo.FraceMarteja.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHandService _handService;
        private readonly IPlayerService _playerService;
        private readonly IEvaluatorService _evaluatorService;
        private readonly IRoundService _roundService; 

        public HomeController(IHandService handService, IPlayerService playerService, IEvaluatorService evaluatorService, IRoundService roundService)
        {
            _handService = handService;
            _playerService = playerService;
            _evaluatorService = evaluatorService;
            _roundService = roundService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> Hands()
        {
            var hands = Mapper.Map<List<HandViewModel>>(await _handService.GetAllHandsOrderByRank());

            return Json(hands, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Players()
        {
            var hands = Mapper.Map<List<PlayerViewModel>>(await _playerService.GetPlayersOrderedByName());

            return Json(hands, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SavePlayer(PlayerViewModel model)
        {
            var returnValue = new DbResult() { IsSuccess = false, ErrorMessage = "Invalid model" };

            if (!ModelState.IsValid) return Json(returnValue);

            if(await _playerService.IsAlreadyExists(model.Name))
            {
                returnValue.ErrorMessage = string.Format("{0} already exists.", model.Name);

                return Json(returnValue);
            }

            var player = Mapper.Map<Player>(model);
            player.CreatedBy = "system";
            player.CreatedDate = DateTime.UtcNow;

            returnValue = await _playerService.Insert(player);
            returnValue.Data = player.Id;

            return Json(returnValue);
        }

        [HttpPost]
        public async Task<JsonResult> SaveWinners(List<ForEvaluationViewModel> model)
        {
            var returnValue = new DbResult() { IsSuccess = false, ErrorMessage = "Invalid model" };

            if (_evaluatorService.IsValid(model.Count))
            {
                int winnerId = await _evaluatorService.Evaluate(model.Select(m => m.Hand_Id).ToList());
                var forSaving = model.Where(m => m.Hand_Id == winnerId).ToList();
                Dictionary<int, List<int>> groupOfPlayers = new Dictionary<int, List<int>>();
                for(int index = 0; index < forSaving.Count; index++)
                {
                    groupOfPlayers.Add(index, forSaving[index].Player_Ids);
                }

                returnValue = await _evaluatorService.SaveWinners(winnerId, groupOfPlayers);
                returnValue.Data = winnerId;
            }
            else
            {
                returnValue.ErrorMessage = "Items count should be greater than or equal to 2 and less than or equal to 5.";
            }

            return Json(returnValue);
        }

        public async Task<JsonResult> WonHands()
        {
            return Json(await _handService.GetAllHandsOrderByRankWithCount(), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> HandHistory(int handId)
        {
            var result = await _roundService.GetHistoryByHandId(handId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}