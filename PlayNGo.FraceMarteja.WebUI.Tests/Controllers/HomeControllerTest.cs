using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayNGo.FraceMarteja.WebUI;
using PlayNGo.FraceMarteja.WebUI.Controllers;
using PlayNGo.FraceMarteja.DAL.Contracts;
using PlayNGo.FraceMarteja.DAL.Services;
using PlayNGo.FraceMarteja.DAL.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace PlayNGo.FraceMarteja.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private IHandService _handService;
        private IPlayerService _playerService;
        private IEvaluatorService _evaluatorService;
        private IRoundService _roundService;
        private IGroupService _groupService;
        private IPlayerGroupService _playerGroupService;

        [TestInitialize]
        public void Setup()
        {
            _handService = new HandService(new PlayNGoDBEntities());
            _playerService = new PlayerService(new PlayNGoDBEntities());            
            _roundService = new RoundService(new PlayNGoDBEntities());
            _groupService = new GroupService(new PlayNGoDBEntities());
            _playerGroupService = new PlayerGroupService(new PlayNGoDBEntities());

            _evaluatorService = new EvaluatorService(_handService, _roundService, _groupService, _playerGroupService);
        }

        [TestMethod]
        public void Hand_Count_Less_Than_Two()
        {
            int groupCount = 1;
            Assert.IsFalse(_evaluatorService.IsValid(groupCount), "Count should be greater than or equalt 2.");            
        }

        [TestMethod]
        public void Hand_Count_More_Than_Five()
        {
            int groupCount = 6;
            Assert.IsFalse(_evaluatorService.IsValid(groupCount), "Count should be less than or equalt 5.");
        }

        [TestMethod]
        public void Hand_Valid_Count()
        {
            int groupCount = 3;
            Assert.IsTrue(_evaluatorService.IsValid(groupCount), "Count should be less than or equalt 5.");
        }

        [TestMethod]
        public void Is_PlayerName_AlreadyExists()
        {
            string testName = "TestName";

            Player player = null;

            Task.Run(async () =>
            {
                player = await _playerService.GetAll().FirstOrDefaultAsync(t => t.Name == testName);
            }).GetAwaiter().GetResult();

            if (player == null)
            {
                player = new Player();
                player.Name = testName;
                player.Gender = "M";
                player.CreatedBy = "unit-test";
                player.CreatedDate = DateTime.UtcNow;

                Task.Run(async () =>
                {
                    await _playerService.Insert(player);
                }).GetAwaiter().GetResult();
            }
            bool isAlreadyExists = false;
            Task.Run(async () =>
            {
                isAlreadyExists = await _playerService.IsAlreadyExists(testName);
            }).GetAwaiter().GetResult();


            Assert.IsTrue(isAlreadyExists);
        }

        [TestMethod]
        public void Number_Of_Total_Hands_Is_Equal_To_10()
        {
            int numberOfRecords = 0;
            Task.Run(async () =>
            {
                numberOfRecords = await _handService.GetAll().CountAsync();
            }).GetAwaiter().GetResult();

            Assert.AreEqual(10, numberOfRecords);
        }
    }
}
