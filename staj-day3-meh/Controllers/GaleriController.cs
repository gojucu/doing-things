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
    [RoutePrefix("Galeri")]
    public class GaleriController : Controller
    {
        // GET: Galeri
        SistemModel context = new SistemModel();
        public ActionResult Index()
        {
            return View(context.Galerilers.ToList());
        }

        [Route("Galeriler")]
        public ActionResult Galeriler()
        {
            var hey = context.GaleriTips.ToList();
            ViewBag.Liste = hey;
            var galeri = context.Galerilers.ToList();
            ViewBag.galeriListe = galeri;
            var dosya = context.Resims.ToList();
            ViewBag.dosyaListe = dosya;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [Route("Galeriler")]
        public ActionResult Galeriler(Galeriler galeriler)
        {
            try
            {
                if (galeriler != null)
                {
                    context.Galerilers.Add(galeriler);
                    context.SaveChanges();
                    return RedirectToAction("Galeriler", "Galeri");
                }
                TempData["hata"] = "Hata! Lütfen gerekli yerleri doldurunuz!";
                return RedirectToAction("Galeriler", "Galeri");
            }
            catch
            {
                TempData["hata"] = "Hata! Lütfen gerekli yerleri doldurunuz!";
                return RedirectToAction("Galeriler", "Galeri");
            }
        }

        public ActionResult GaleriSil(int Id)
        {
            context.Galerilers.Remove(context.Galerilers.FirstOrDefault(x => x.Id == Id));
            context.SaveChanges();
            return RedirectToAction("Galeriler", "Galeri");
        }

        [Route("{id:int}")]
        public ActionResult GaleriDuzenle(int id)
        {
            ViewBag.Title = id;
            var galerii = context.GaleriTips.ToList();
            ViewBag.galeriListe = galerii;

            Galeriler galeri = context.Galerilers.FirstOrDefault(x => x.Id == id);
            return View(galeri);
        }

        [HttpPost]
        [ValidateInput(false)]
        [Route("{id:int}")]
        public ActionResult GaleriDuzenle(Galeriler galeriler)
        {
            try
            {

                Galeriler guncellenecek = context.Galerilers.FirstOrDefault(x => x.Id == galeriler.Id);
                guncellenecek.Ad = galeriler.Ad;
                guncellenecek.Tipi = galeriler.Tipi;
                guncellenecek.Genişlik = galeriler.Genişlik;
                guncellenecek.Yükseklik = galeriler.Yükseklik;
                guncellenecek.Aktif = galeriler.Aktif;

                context.SaveChanges();
                return RedirectToAction("Galeriler", "Galeri");
            }
            catch
            {
                return RedirectToAction("Galeriler", "Galeri");
            }
        }

        string ResimKaydet(HttpPostedFileBase file)
        {
            Image orj = Image.FromStream(file.InputStream);
            string dosyaadi = Path.GetFileNameWithoutExtension(file.FileName) + Guid.NewGuid() + Path.GetExtension(file.FileName);
            orj.Save(Server.MapPath("~/Content/images/" + dosyaadi));
            return dosyaadi;

        }

        [HttpPost]
        public ActionResult GaleriyeResimEkle(GaleriResim gr, HttpPostedFileBase Link)
        {
            try
            {
                string uzanti = Path.GetExtension(Link.FileName).ToLower();
                string[] Uzantilar = new[] { ".jpg", ".png" };
                if (uzanti == ".jpg" || uzanti == ".png")
                {
                    string dosyaadi = ResimKaydet(Link);
                    gr.Link = "/Content/images/" + dosyaadi;
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                var file = gr.Link;
                if (file != null)
                {
                    GaleriResim img = new GaleriResim();
                    img.Link = gr.Link;
                    foreach (string u in Uzantilar)
                    {
                        if (uzanti == u)
                        {
                            if (ModelState.IsValid)
                            {
                                using (context)
                                {

                                    context.GaleriResims.Add(gr);
                                    context.SaveChanges();


                                    return RedirectToAction("Galeriler", "Galeri");
                                }
                            }
                        }
                    }
                }

                return RedirectToAction("Galeriler", "Galeri");

            }
            catch
            {

                return RedirectToAction("Galeriler", "Galeri");
            }
        }
    }
}