using System.Linq;
using System.Web.Mvc;
using NoNameApp.WEB.Models;
using AutoMapper;
using NoNameApp.Entities;
using System.Collections.Generic;
using NoNameApp.LogicContracts;

namespace NoNameApp.WEB.Controllers
{
    public class HomeController : Controller
    {
        ISerialService serialService;
        public HomeController(ISerialService serv)
        {
            serialService = serv;
        }

        public ActionResult Index()
        {
            var lastSerials =serialService.GetSerials().ToList();
            if (lastSerials.Count() > 3)
                return View(lastSerials.GetRange(lastSerials.Count - 3, 3));
            else
                return View(lastSerials);
        }

    }
}