using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using GoodGamesApp.Models;

namespace GoodGamesApp.Controllers
{
  public class RetailerController : Controller
  {
    [HttpGet("/retailers")]
    public ActionResult AllRetailers()
    {
      List<Retailer> allRetailers = Retailer.GetAll();
      return View(allRetailers);
    }

    [HttpGet("/retailers/new")]
    public ActionResult NewRetailer()
    {
      return View();
    }

    [HttpPost("/retailers")]
    public ActionResult AddRetailer()
    {
      string retailerName = Request.Form["retailer-name"];
      string retailerWebsite = Request.Form["retailer-website"];
      Retailer newRetailer = new Retailer(retailerName, retailerWebsite);
      newRetailer.Save();
      return RedirectToAction("AllRetailers");
    }

    [HttpGet("/retailers/details/{id}")]
    public ActionResult RetailerDetails(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Retailer selectedRetailer = Retailer.Find(id);
      List<Game> retailerGames = selectedRetailer.GetGames();
      List<Game> allGames = Game.GetAll();
      model.Add("selectedRetailer", selectedRetailer);
      model.Add("retailerGames", retailerGames);
      model.Add("allGames", allGames);

      return View(model);
    }

    [HttpGet("/retailers/edit/{id}")]
    public ActionResult EditRetailerForm(int id)
    {
      Retailer thisRetailer = Retailer.Find(id);
      return View("EditRetailer", thisRetailer);
    }

    [HttpPost("/retailers/edit/{id}")]
    public ActionResult EditRetailer(int id)
    {
      Retailer thisRetailer = Retailer.Find(id);
      thisRetailer.Edit(Request.Form["edit-retailer-name"], Request.Form["edit-retailer-website"]);
      return RedirectToAction("AllRetailers");
    }

    [HttpPost("/retailers/{retailerId}/games/new")]
    public ActionResult AddGameToRetailer(int retailerId)
    {
      Retailer retailer = Retailer.Find(retailerId);
      Game game = Game.Find(Int32.Parse(Request.Form["game-id"]));
      retailer.AddGame(game);
      return RedirectToAction("RetailerDetails", new {id = retailerId});
    }

    [HttpPost("/retailers/delete/{id}")]
    public ActionResult DeleteRetailer(int id)
    {
      Retailer thisRetailer = Retailer.Find(id);
      thisRetailer.Delete();
      return RedirectToAction("AllRetailers");
    }
  }
}
