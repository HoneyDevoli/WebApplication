using NoNameApp.Entities;
using NoNameApp.LogicContracts;
using NoNameApp.WEB.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using NoNameApp.BLL.Infrastructure;
using System.Collections.Generic;

namespace NoNameApp.WEB.Controllers
{
    public class SerialController : Controller
    {
        IUserService userService;
        ISerialService serialService;
        IEpisodeService episodeService;
        public SerialController(ISerialService sServ, IUserService uServ, IEpisodeService eServ)
        {
            serialService = sServ;
            userService = uServ;
            episodeService = eServ;
        }

        
        public ActionResult Index(int? page, string message)
        {
            ViewBag.Role = User.Identity.GetUserRole();
            int pageSize = 11;
            int pageNumber = (page ?? 1);
            var serials = serialService.GetSerials().ToPagedList(pageNumber, pageSize);
            ViewBag.Count = serialService.GetSerials().Count();
            if (message != null)
                ViewBag.StatusMessage = message;
            else
                ViewBag.StatusMessage = "";
            return View(serials);
        }

        [Authorize]
        [HttpGet]
        public ActionResult SerialInfo(int? id,string message)
        {
            try
            {
                var user = GetUser();
                var serial = serialService.GetSerial(id);
                var serialWatchedEp = userService.WatchedEp(user, serial);
                ViewBag.staredItems = userService.GetRateUser(user, serial);
                ViewBag.Watched = userService.CheckSerial(user, serial);
                if (message != null)
                    ViewBag.StatusMessage = message;
                else
                    ViewBag.StatusMessage = "";
                return View(serialWatchedEp);
            }
            catch
            {
                return RedirectToAction("HttpNotFound", "Error");
            }
        }
        
        [HttpPost]
        public ActionResult SerialInfo(int[] selectedEpisode, Serial selectedSerial)
        {
            try
            {
                var user = GetUser();
                var serial = serialService.GetSerial(selectedSerial.Id);
                userService.DelAllEpisode(user, serial);
                if (selectedEpisode != null)
                    foreach (var id in selectedEpisode)
                    {
                        var episode = episodeService.GetEpisode(id);
                        userService.AddEpisode(user, episode);
                    }
                return RedirectToAction("Index", "User");
            }
            catch
            {
                return RedirectToAction("BadRequest", "Error");
            }
        }

        public void AjaxStar(int starCount, int? id)
        {
            var user = GetUser();
            var serial = serialService.GetSerial(id);
            userService.AddRating(user, serial, starCount);
        }
        
        public ActionResult Watch(int id)
        {
            try
            {
                var user = GetUser();
                var serial = serialService.GetSerial(id);
                userService.AddSerial(user, serial);
                return RedirectToAction("SerialInfo", new { id = id });
            }
            catch
            {
                return RedirectToAction("BadRequest", "Error");
            }
        }

        public ActionResult UnWatch(int id)
        {
            try
            {
                var user = GetUser();
                var serial = serialService.GetSerial(id);
                userService.DelSerial(user, serial);
                return RedirectToAction("SerialInfo", new { id = id });
            }
            catch
            {
                return RedirectToAction("BadRequest", "Error");
            }
        }
        

        [HttpGet]
        public ActionResult Search(string search)
        {
            List<Serial> serials = null;
            try
            {
                serials = serialService.GetSearchSerials(search);
            }
            catch (ValidationException ex)
            {
                ViewBag.StatusMessage = ex.Message;
            }
            return View(serials);

        }
        #region modalDel

        [Authorize(Roles = "admin")]
        public ActionResult ConfirmDelEpisode(int idEp, int idSer)
        {
            ViewBag.IdEp = idEp;
            ViewBag.IdSer = idSer;
            return PartialView("_ConfirmDelEpisode");
        }

        [Authorize(Roles = "admin")]
        public ActionResult ConfirmDelSeason(int idSeas, int idSer)
        {
            ViewBag.IdSeas = idSeas;
            ViewBag.IdSer = idSer;
            return PartialView("_ConfirmDelSeason");
        }

        [Authorize(Roles = "admin")]
        public ActionResult ConfirmDelSerial(int id)
        {
            ViewBag.IdSer = id;
            return PartialView("_ConfirmDelSerial");
        }
        #endregion
        #region admin 
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult ToAdding()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ToAdding(Serial serial)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    OperationDetails operationDetails = serialService.Adding(serial.Name, serial.Begin, serial.Picture, serial.Country
                , serial.Description, serial.Duration, serial.Status);
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                    return RedirectToAction("Index", new { message = operationDetails.Message });
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
               
            }
            return View(serial);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            string message = "";
            try
            {
                OperationDetails opDet = serialService.Delete(id);
                message = opDet.Message;
            }
            catch (ValidationException ex)
            {
                message = ex.Message;
            }
            return RedirectToAction("Index", new {message });
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteSeason(int idSeas, int  idSer)
        {
            string message = "";
            try
            {
                OperationDetails opDet = serialService.DeleteSeason(idSeas);
                message = opDet.Message;
            }
            catch (ValidationException ex)
            {
                message = ex.Message;
            }
            return RedirectToAction("SerialInfo", new { id = idSer, message });
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteEpisode(int idEp,int id)
        {
            string message = "";
            try
            {
                OperationDetails opDet=serialService.DeleteEpisode(idEp);
                message = opDet.Message;
            }
            catch (ValidationException ex)
            {
                message = ex.Message;
            }
            return RedirectToAction("SerialInfo", new { id, message });
        }
        

        [HttpPost]
        public ActionResult ChangeEpisode(string idEp, string name, int numberOfEpisode, int id)
        {
            string message="";
            try
            {
                var temp = Convert.ToInt32(idEp);
                OperationDetails opDet =serialService.ChangeEpisode(temp, name, numberOfEpisode);
                message = opDet.Message;
            }
            catch (ValidationException ex)
            {
                message = ex.Message;
            }
            return RedirectToAction("SerialInfo", new { id, message });
        }

        [HttpPost]
        public ActionResult ChangeName(int id, string name)
        {
            string message = "";
            try
            {
                var temp = Convert.ToInt32(id);
                OperationDetails opDet = serialService.ChangeSerialName(id, name);
                message = opDet.Message;
            }
            catch (ValidationException ex)
            {
                message = ex.Message;
            }
            return RedirectToAction("SerialInfo", new { id, message });
        }

        [HttpPost]
        public ActionResult ChangeNumberSeason(string id, int numberOfSeasons, int SerialId)
        {
            string message = "";
            try
            {
                var temp = Convert.ToInt32(id);
                OperationDetails opDet = serialService.ChangeSeason(temp, numberOfSeasons);
                message = opDet.Message;
            }
            catch (ValidationException ex)
            {
                message = ex.Message;
            }
            return RedirectToAction("SerialInfo", new { id = SerialId, message });
        }

        [HttpPost]
        public ActionResult ChangeInfoSer(int id, DateTime? begin, string picture,
            string country, int duration, bool status, string description)
        {
            string message = "";
            try
            {
                OperationDetails opDet = serialService.ChangeInfoSer(id, begin, picture, country, duration, status);
                message = opDet.Message;
            }
            catch (ValidationException ex)
            {
                message = ex.Message;
            }
            return RedirectToAction("SerialInfo", new { id, message });
        }

        [HttpPost]
        public ActionResult ChangeDescription(int id, string description)
        {
            string message = "";
            try
            {
                OperationDetails opDet = serialService.ChangeDescription(id, description);
                message = opDet.Message;
            }
            catch (ValidationException ex)
            {
                message = ex.Message;
            }
            return RedirectToAction("SerialInfo", new { id, message });
        }

        [HttpPost]
        public ActionResult AddSeason(int id, string count)
        {
            string message = "";
            try
            {
                var temp = Convert.ToInt32(count);
                OperationDetails opDet = serialService.AddSeason(id, temp);
                message = opDet.Message;
            }
            catch (ValidationException ex)
            {
                message = ex.Message;
            }
            return RedirectToAction("SerialInfo", new { id, message });
        }


        [HttpPost]
        public ActionResult AddEpisode(string id, int serialId, string count, string name, DateTime? date)
        {
            string message = "";
            try
            {
                var temp = Convert.ToInt32(id);
                var temp2 = Convert.ToInt32(count);
                OperationDetails opDet = serialService.AddEpisode(temp, temp2, name, date);
                message = opDet.Message;
            }
            catch (ValidationException ex)
            {
                message = ex.Message;
            }
            return RedirectToAction("SerialInfo", new { id = serialId, message});
        }
#endregion
        #region FuncForHelping
        private User GetUser()
        {
            string email = User.Identity.Name;
            var user = userService.GetUser(email);
            return user;
        }
        #endregion
        #region NewFeatures

       
        [HttpGet]
        public ActionResult WatchingEpisode(int? id)
        {
            try
            {
                var user = GetUser();
                var episode = episodeService.GetEpisode(id);
                ViewBag.Episode = episode;
                if(user.RoleId == 3)
                    return View("WatchingSerialVip",episode);
                else
                    return View("WatchingSerial", episode);
            }
            catch
            {
                return RedirectToAction("HttpNotFound", "Error");
            }
        }

        [HttpPost]
        public ActionResult WatchingEpisode(int? id,string video)
        {
            Episode ep = episodeService.GetEpisode(id);
            ep.Video = video;
            episodeService.AddLinkEp(ep);
            return RedirectToAction("WatchingEpisode",id);
        }
        #endregion
    }

}