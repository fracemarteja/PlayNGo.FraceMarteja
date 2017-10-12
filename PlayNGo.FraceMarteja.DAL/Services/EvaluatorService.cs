using PlayNGo.FraceMarteja.DAL.Contracts;
using PlayNGo.FraceMarteja.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayNGo.FraceMarteja.DAL.Services
{
    public class EvaluatorService : IEvaluatorService
    {
        private readonly IHandService _handService;
        private readonly IRoundService _roundService;
        private readonly IGroupService _groupService;
        private readonly IPlayerGroupService _playerGroupService;

        public EvaluatorService(IHandService handService, IRoundService roundService, IGroupService groupService, IPlayerGroupService playerGroupService) {
            _handService = handService;
            _roundService = roundService;
            _groupService = groupService;
            _playerGroupService = playerGroupService;
        }

        public async Task<int> Evaluate(List<int> handIds)
        {
            //lowets rankOrder value = highest hand value
            var highestHandId = await _handService.GetAll().Where(t => handIds.Contains(t.Id)).OrderBy(t => t.RankOrder).Select(t => t.Id).FirstOrDefaultAsync();

            return handIds.FirstOrDefault(h => h == highestHandId);
        }

        public async Task<DbResult> SaveWinners(int handId, Dictionary<int, List<int>> groupOfPlayers)
        {
            var returnValue = new DbResult() { IsSuccess = false, ErrorMessage = "Error while saving the wiiners." };
            Round round = new Round();
            round.Description = "Generated Round";
            round.CreatedBy = "system";
            round.CreatedDate = DateTime.UtcNow;

            returnValue = await _roundService.Insert(round);
            if (returnValue.IsSuccess)
            {
                foreach(var groupOfPlayer in groupOfPlayers)
                {
                    Group group = new Group();
                    group.Hand_Id = handId;
                    group.Round_Id = round.Id;
                    group.CreatedBy = "system";
                    group.CreatedDate = DateTime.UtcNow;

                    returnValue = await _groupService.Insert(group);
                    if (returnValue.IsSuccess)
                    {
                        List<PlayerGroup> playerGroups = new List<PlayerGroup>();
                        foreach(int playerId in groupOfPlayer.Value)
                        {
                            PlayerGroup playerGroup = new PlayerGroup();
                            playerGroup.Group_Id = group.Id;
                            playerGroup.Player_Id = playerId;
                            playerGroup.CreatedBy = "system";
                            playerGroup.CreatedDate = DateTime.UtcNow;

                            playerGroups.Add(playerGroup);
                        }

                        returnValue = await _playerGroupService.InsertRange(playerGroups);
                    }
                }
            }

            return returnValue;
        }

        public bool IsValid(int count)
        {
            return (count >= 2 && count <= 5) ? true : false;
        }
    }
}
