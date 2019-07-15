using staj_day3_meh.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace staj_day3_meh.Controllers
{
    public class ModulController : Controller
    {
        // GET: Modul

        SistemModel context = new SistemModel();
        public ActionResult Index()
        {
            return View();
        }
        
        [Route("Dosyalar")]
        public ActionResult Dosyalar()
        {
            return View(context.Resims.ToList());
        }

        string ResimKaydet(HttpPostedFileBase file)
        {
              Image orj = Image.FromStream(file.InputStream);
              string dosyaadi = Path.GetFileNameWithoutExtension(file.FileName) + Guid.NewGuid() + Path.GetExtension(file.FileName);
              orj.Save(Server.MapPath("~/Content/images/" + dosyaadi));
              return dosyaadi;
             
        }
        
        [HttpPost]
        [Route("Dosyalar")]
        public ActionResult Dosyalar(Resim resim, HttpPostedFileBase Link)
        {
            string message = "";
            string uzanti = Path.GetExtension(Link.FileName).ToLower();
            string[] Uzantilar = new[] { ".jpg", ".png", ".docx", ".xlsx" };
           
                if (uzanti==".jpg"|| uzanti==".png") {
                    string dosyaadi = ResimKaydet(Link);
                    resim.Link = "/Content/images/" + dosyaadi;
                }
                else if(uzanti== ".docx" || uzanti== ".xlsx")
                {
                    string hey= Path.GetFileNameWithoutExtension(Link.FileName) + Guid.NewGuid() + Path.GetExtension(Link.FileName);
                    string dosyaadi = hey;
                    resim.Link = "/Content/images/"+ dosyaadi;
                }
                else
                {
                TempData["shortMessage"] = "Lütfen geçerli uzantılardan birini giriniz! (.jpg, .png, .docx ve .xlsx)";
                
                System.Threading.Thread.Sleep(4000);
                return RedirectToAction("Dosyalar", "Modul");
                
            }
            //System.Threading.Thread.Sleep(4000);

            foreach(string u in Uzantilar)
                {
                   if (uzanti == u)
                    {
                        if (ModelState.IsValid)
                        {
                                using (context)
                                {

                                    context.Resims.Add(resim);
                                    context.SaveChanges();

                                    message = "Dosya kaydedildi!";
                        }
                        }    
                    }
                }
            TempData["shortMessage"] = message;
            return RedirectToAction("Dosyalar", "Modul");
        }

        public ActionResult DosyaSil(int id)
        {
            context.Resims.Remove(context.Resims.FirstOrDefault(x => x.Id == id));
            context.SaveChanges();
            TempData["shortMessage"] = "Dosya silindi.";
            return RedirectToAction("Dosyalar", "Modul");
        }
    }
}