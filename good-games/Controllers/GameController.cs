using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using GoodGamesApp.Models;

namespace GoodGamesApp.Controllers
{
  public class GameController : Controller
  {
    [HttpGet("/games")]
    public ActionResult AllGames()
    {
      List<Game> allGames = Game.GetAll();
      return View(allGames);
    }

    [HttpGet("/games/new")]
    public ActionResult NewGame()
    {
      List<Retailer> allRetailers = Retailer.GetAll();
      return View(allRetailers);
    }

    [HttpPost("/games")]
    public ActionResult AddGame()
    {
      string gameName = Request.Form["game-name"];
      string gameGenre = Request.Form["genre"];
      string gameSystem = Request.Form["game-system"];
      int releaseYear = Int32.Parse(Request.Form["game-release-year"]);
      int gameRating = Int32.Parse(Request.Form["game-rating"]);
      Game newGame = new Game(gameName, gameGenre, gameSystem, releaseYear, gameRating);
      newGame.Save();
      return RedirectToAction("AllGames");
    }

    [HttpGet("/games/details/{id}")]
    public ActionResult GameDetails(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Game selectedGame = Game.Find(id);
      List<Retailer> gameRetailers = selectedGame.GetRetailers();
      List<Retailer> allRetailers = Retailer.GetAll();
      model.Add("selectedGame", selectedGame);
      model.Add("gameRetailers", gameRetailers);
      model.Add("allRetailers", allRetailers);
      return View(model);
    }

    [HttpPost("/games/{gameId}/retailers/new")]
    public ActionResult AddRetailerToGame(int gameId)
    {
      Game game = Game.Find(gameId);
      Retailer retailer = Retailer.Find(Int32.Parse(Request.Form["retailer-id"]));
      game.AddRetailer(retailer);
      return RedirectToAction("GameDetails", new {id = gameId});
    }

    [HttpPost("/games/delete/{id}")]
    public ActionResult DeleteGame(int id)
    {
      Game thisGame = Game.Find(id);
      thisGame.Delete();
      return RedirectToAction("AllGames");
    }
  }
}
