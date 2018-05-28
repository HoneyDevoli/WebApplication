using Microsoft.AspNet.Identity;
using NoNameApp.Entities;
using NoNameApp.LogicContracts;
using NoNameApp.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoNameApp.WEB.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        IUserService userService;
        public UserController(IUserService serv)
        {
            userService = serv;
        }


        public ActionResult Index()
        {
            User user = GetUser();
            var userSerials = userService.GetUserSerials(user);
            return View(userSerials);
        }
        
        public ActionResult Calendar()
        {
            User user = GetUser();
            var userEpisode = userService.GetUserEpisode(user);
            return View(userEpisode);
        }

        public new ActionResult Profile()
        {
            try
            {
                User user = GetUser();
                var userSerials = user.Serials.ToList();
                var watchedSer = userSerials
                    .Select(serial => new WatchedViewModel
                    {
                        Ser = serial.Id,
                        CountEp = user.Episodes.Where(s => s.Season.SerialId == serial.Id).Count(),
                        Rat = user.Ratings.Where(r => r.SerialId == serial.Id).FirstOrDefault()
                    })
                    .ToList();
                ViewBag.Watched = watchedSer;
                var spentTime = userService.SpentTime(user);
                ViewBag.SpentHours = spentTime / 60;
                ViewBag.SpentDays = spentTime / 3600;
                ViewBag.AmountEp = user.Episodes.Count();
                return View(userSerials);
            }
            catch
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        #region help
        private User GetUser()
        {
            string email = User.Identity.Name;
            var user = userService.GetUser(email);
            return user;
        }
        #endregion
    }
}