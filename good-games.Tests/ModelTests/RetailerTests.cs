using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using GoodGamesApp.Models;
using GoodGamesApp;

namespace GoodGamesApp.Tests
{
  [TestClass]
  public class RetailerTests : IDisposable
  {
    public void Dispose()
    {
      Game.DeleteAll();
      Retailer.DeleteAll();
    }

    public RetailerTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=good_games_test;";
    }

    [TestMethod]
    public void Delete_DeletesRetailerAssociationsFromDatabase_RetailerList()
    {
      Game testGame = new Game("Super Mario 64", "Platformer", "Nintendo 64", 1996, 98);
      testGame.Save();

      Retailer testRetailer = new Retailer("GameStop", "www.GameStop.com");
      testRetailer.Save();

      testRetailer.AddGame(testGame);
      testRetailer.Delete();

      List<Retailer> resultGameRetailers = testGame.GetRetailers();
      List<Retailer> testGameRetailers = new List<Retailer> {};

      CollectionAssert.AreEqual(testGameRetailers, resultGameRetailers);
    }

    [TestMethod]
    public void Test_AddGame_AddsGameToRetailer()
    {
      Retailer testRetailer = new Retailer("GameStop", "www.GameStop.com");
      testRetailer.Save();

      Game testGame = new Game("Super Mario 64", "Platformer", "Nintendo 64", 1996, 98);
      testGame.Save();

      Game testGame2 = new Game("Ocarina of Time", "RPG", "Nintendo 64", 1998, 99);
      testGame2.Save();

      testRetailer.AddGame(testGame);
      testRetailer.AddGame(testGame2);

      List<Game> result = testRetailer.GetGames();
      List<Game> testList = new List<Game>{testGame, testGame2};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Edit_UpdatesRetailerInDatabase_String()
    {
      Retailer testRetailer = new Retailer("GameStop", "www.GameStop.com");
      testRetailer.Save();
      string testName = "Best Buy";
      string testWebsite = "www.bestbuy.com";
      testRetailer.Edit(testName, testWebsite);
      Retailer testRetailer2 = new Retailer("Best Buy", "www.bestbuy.com");
      Assert.AreEqual(testRetailer, testRetailer2);

    }

    [TestMethod]
    public void GetGames_ReturnsAllRetailerGames_GameList()
    {
      Retailer testRetailer = new Retailer("GameStop", "www.GameStop.com");
      testRetailer.Save();

      Game testGame = new Game("Super Mario 64", "Platformer", "Nintendo 64", 1996, 98);
      testGame.Save();

      Game testGame2 = new Game("Ocarina of Time", "RPG", "Nintendo 64", 1998, 99);
      testGame2.Save();

      testRetailer.AddGame(testGame);
      List<Game> savedGames = testRetailer.GetGames();
      List<Game> testList = new List<Game> {testGame};

      CollectionAssert.AreEqual(testList, savedGames);

    }
  }
}
