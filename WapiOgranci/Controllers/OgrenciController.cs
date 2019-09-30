using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WapiOgranci.Controllers
{
    public class OgrenciController : ApiController
    {
        WebApiCfCrudEntities db = new WebApiCfCrudEntities();

        public HttpResponseMessage Get()
        {
            var ogr = db.Ogrencilers.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, ogr);
        }
        public HttpResponseMessage Get(int id)
        {
            //return db.Employees.FirstOrDefault(x => x.Id == id);
            Ogrenciler ogrenci = db.Ogrencilers.FirstOrDefault(x => x.Id == id);
            if (ogrenci == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Id'si {id} olan çalışan bulunamadı.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, ogrenci);
        }

        public HttpResponseMessage Post(Ogrenciler ogrenci)
        {
            try
            {
                db.Ogrencilers.Add(ogrenci);
                if (db.SaveChanges() > 0)
                {
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, ogrenci);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + ogrenci.Id);
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

        public HttpResponseMessage Put([FromBody] MyBodyType myType, [FromUri] Ogrenciler ogrenci)
        {
            try
            {
                Ogrenciler ogr = db.Ogrencilers.FirstOrDefault(x => x.Id == myType.Id);
                if (ogr == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Ders Id : " + ogrenci.Id);
                }
                else
                {
                    //myType.deneme = "Bu işlem başarılı oldu.";
                    ogr.Ad = ogrenci.Ad;
                    ogr.Soyad = ogrenci.Soyad;
                    ogr.OgrNo = ogrenci.OgrNo;
                    ogr.Email = ogrenci.Email;
                    ogr.Telefon = ogrenci.Telefon;
                    
                    if (db.SaveChanges() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, ogr + "/" + myType.deneme);
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
