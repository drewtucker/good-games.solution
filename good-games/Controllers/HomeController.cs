using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using GoodGamesApp.Models;

namespace GoodGamesApp.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet]
      public ActionResult Index()
      {
        return View();
      }

      
    }
}
