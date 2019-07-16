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
        
        public JsonResult Dosyalarr(Resim resim, HttpPostedFileBase Link)
        {
            try
            {
                string uzanti = Path.GetExtension(Link.FileName).ToLower();
                string[] Uzantilar = new[] { ".jpg", ".png", ".docx", ".xlsx" };

                if (uzanti == ".jpg" || uzanti == ".png")
                {
                    string dosyaadi = ResimKaydet(Link);
                    resim.Link = "/Content/images/" + dosyaadi;
                }
                else if (uzanti == ".docx" || uzanti == ".xlsx")
                {
                    string hey = Path.GetFileNameWithoutExtension(Link.FileName) + Guid.NewGuid() + Path.GetExtension(Link.FileName);
                    string dosyaadi = hey;
                    resim.Link = "/Content/images/" + dosyaadi;
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

                int imgId = 0;
                var file = resim.Link;
                //byte[] imagebyte = null;
                if (file != null)
                {
                    //Link.SaveAs(Server.MapPath("/Content/images" + Link.FileName));
                    BinaryReader reader = new BinaryReader(Link.InputStream);
                    //imagebyte = reader.ReadBytes(Link.ContentLength);
                    Resim img = new Resim();
                    img.Link = resim.Link;
                    foreach (string u in Uzantilar)
                    {
                        if (uzanti == u)
                        {
                            if (ModelState.IsValid)
                            {
                                using (context)
                                {


                                    context.Resims.Add(img);
                                    context.SaveChanges();
                                    imgId = img.Id;
                                }
                            }
                        }
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DisplayingImage(int imgid)
        {
            var img = context.Resims.SingleOrDefault(x => x.Id == imgid);
            if (img != null)
            {
                string uzanti = Path.GetExtension(img.Link).ToLower();
                if (uzanti == ".jpg" || uzanti == ".png")
                {
                    return File(img.Link, "image/jpg");
                }
                else if (uzanti == ".docx")
                {
                    return File("/Content/Word.jpg", "image/jpg");
                }
                else if (uzanti == ".xlsx")
                {
                    return File("/Content/excel.jpg", "image/jpg");
                }
                return File("/Content/x-png-23.png", "image/jpg");
            }
            else
            {
                return File("/Content/x-png-23.png", "image/jpg");
            }
        }


        //string message = "";
        //    string uzanti = Path.GetExtension(Link.FileName).ToLower();
        //    string[] Uzantilar = new[] { ".jpg", ".png", ".docx", ".xlsx" };

        //        if (uzanti==".jpg"|| uzanti==".png") {
        //            string dosyaadi = ResimKaydet(Link);
        //            resim.Link = "/Content/images/" + dosyaadi;
        //        }
        //        else if(uzanti== ".docx" || uzanti== ".xlsx")
        //        {
        //            string hey= Path.GetFileNameWithoutExtension(Link.FileName) + Guid.NewGuid() + Path.GetExtension(Link.FileName);
        //            string dosyaadi = hey;
        //            resim.Link = "/Content/images/"+ dosyaadi;
        //        }
        //        else
        //        {
        //        TempData["shortMessage"] = "Lütfen geçerli uzantılardan birini giriniz! (.jpg, .png, .docx ve .xlsx)";

        //        System.Threading.Thread.Sleep(4000);
        //        return RedirectToAction("Dosyalar", "Modul");

        //    }
        //    //System.Threading.Thread.Sleep(4000);

        //    foreach(string u in Uzantilar)
        //        {
        //           if (uzanti == u)
        //            {
        //                if (ModelState.IsValid)
        //                {
        //                        using (context)
        //                        {

        //                            context.Resims.Add(resim);
        //                            context.SaveChanges();

        //                            message = "Dosya kaydedildi!";
        //                }
        //                }    
        //            }
        //        }
        //    TempData["shortMessage"] = message;
        //    return RedirectToAction("Dosyalar", "Modul");
        //}

        public ActionResult DosyaSil(int id)
        {
            context.Resims.Remove(context.Resims.FirstOrDefault(x => x.Id == id));
            context.SaveChanges();
            TempData["shortMessage"] = "Dosya silindi.";
            return RedirectToAction("Dosyalar", "Modul");
        }
    }
}