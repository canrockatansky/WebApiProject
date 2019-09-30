using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WapiOgranci.Controllers
{
    public class DerslerController : Controller
    {
        // GET: Dersler
        public ActionResult Index()
        {

            //ViewBag.Data = GetApiData();
            ViewBag.Ogrenci = new SelectList(GetApiData(), "Id", "Ad");
            return View();
        }
        public List<Ogrenciler> GetApiData()
        {

            var apiUrl = "http://localhost:56198/api/Ogrenci";

            //Connect API
            Uri url = new Uri(apiUrl);
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;

            string json = client.DownloadString(url);
            //END

            //JSON Parse START
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<Ogrenciler> jsonList = ser.Deserialize<List<Ogrenciler>>(json);
            //END

            return jsonList;
        }

    }
}