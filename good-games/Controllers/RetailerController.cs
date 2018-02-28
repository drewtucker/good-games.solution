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
  }
}
