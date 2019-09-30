using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WapiOgranci.Controllers
{
    public class OgrencilerController : Controller
    {
        // GET: Ogrenciler
        public ActionResult Index()
        {
            return View();
        }
    }
}