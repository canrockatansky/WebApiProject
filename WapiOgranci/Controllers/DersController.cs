using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WapiOgranci.Controllers
{
    public class DersController : ApiController
    {
        WebApiCfCrudEntities db = new WebApiCfCrudEntities();

        public HttpResponseMessage Get()
        {
            var drs = db.Derslers.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, drs);
        }
        public HttpResponseMessage Get(int id)
        {
            //return db.Employees.FirstOrDefault(x => x.Id == id);
            Dersler ders = db.Derslers.FirstOrDefault(x => x.Id == id);
            if (ders == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Id'si {id} olan çalışan bulunamadı.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, ders);
        }

        public HttpResponseMessage Post(Dersler ders)
        {
            try
            {
                db.Derslers.Add(ders);
                if (db.SaveChanges() > 0)
                {
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, ders);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + ders.Id);
                    return message;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Veri ekleme başarısız.!!!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put([FromBody] MyBodyType myType, [FromUri] Dersler ders)
        {
            try
            {
                Dersler drs = db.Derslers.FirstOrDefault(x => x.Id == myType.Id);
                if (drs == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Ders Id : " + ders.Id);
                }
                else
                {
                    //myType.deneme = "Bu işlem başarılı oldu.";
                    drs.DersAdi = ders.DersAdi;
                    drs.OgrId = ders.OgrId;
                    
                    if (db.SaveChanges() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, drs + "/" + myType.deneme);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Güncelleme yapılamadı!!!");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Dersler drs = db.Derslers.FirstOrDefault(x => x.Id == id);
                if (drs == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee Id :" + id + " bulunamadı.");
                }
                else
                {
                    db.Derslers.Remove(drs);
                    if (db.SaveChanges() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Ders Id :" + id);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Silme işlemi yapılamadı!!!");
                    }
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public class MyBodyType///Complex Type 
        {
            public int Id { get; set; }
            public string deneme { get; set; }

        }
    }
}
