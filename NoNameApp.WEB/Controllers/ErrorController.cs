using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoNameApp.WEB.Controllers
{
    public class ErrorController : Controller
    {

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return View();
        }

        public ActionResult BadRequest()
        {
            Response.StatusCode = 400;
            return View();
        }
        public ActionResult InternalServerError()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}