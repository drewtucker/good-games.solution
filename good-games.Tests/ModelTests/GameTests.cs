using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using GoodGamesApp.Models;
using GoodGamesApp;

namespace GoodGamesApp.Tests
{
  [TestClass]
  public class GameTests : IDisposable
  {
    public void Dispose()
    {
      Game.DeleteAll();
    }

    public GameTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=good_games_test;";
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      int result = Game.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_OverrideTrueIfNamesAreTheSame_Game()
    {
      Game testGame1 = new Game("Super Mario 64", "Platformer", "Nintendo 64", 1996, 98);
      Game testGame2 = new Game("Super Mario 64", "RPG", "Nintendo 64", 1996, 98);

      Assert.AreEqual(testGame1, testGame2);
    }

    [TestMethod]
    public void Save_SavesToDatabase_GameList()
    {
      Game testGame1 = new Game("Super Mario 64", "Platformer", "Nintendo 64", 1996, 98);
      testGame1.Save();
      Game testGame2 = new Game("Ocarina of Time", "RPG", "Nintendo 64", 1998, 99);
      testGame2.Save();

      List<Game> result = Game.GetAll();
      List<Game> testList = new List<Game>{testGame1, testGame2};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToObject_Id()
    {
      Game testGame = new Game("Super Mario 64", "Platformer", "Nintendo 64", 1996, 98);
      testGame.Save();

      Game savedGame = Game.GetAll()[0];

      int result = savedGame.GetId();
      int testId = testGame.GetId();

      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsGameInDatabase_Game()
    {
      Game testGame = new Game("Super Mario 64", "Platformer", "Nintendo 64", 1996, 98);
      testGame.Save();

      Game foundGame = Game.Find(testGame.GetId());

      Assert.AreEqual(testGame, foundGame);
    }

    [TestMethod]
    public void AddRetailer_AddsRetailerToGame_RetailerList()
    {
      Game testGame = new Game("Super Mario 64", "Platformer", "Nintendo 64", 1996, 98);
      testGame.Save();

      Retailer testRetailer = new Retailer("GameStop", "www.GameStop.com");
      testRetailer.Save();

      testGame.AddRetailer(testRetailer);

      List<Retailer> result = testGame.GetRetailers();
    }
  }
}
