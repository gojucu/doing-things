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
    [RoutePrefix("Urunler")]
    public class UrunlerController : Controller
    {
        // GET: Urunler
        SistemModel context = new SistemModel();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UrunEkle()
        {
            var urunListesi = context.Urunlers.ToList();
            ViewBag.urunListesi = urunListesi;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UrunEkle(Urunler urunler)
        {
            try
            {
                if (urunler != null)
                {
                    context.Urunlers.Add(urunler);
                    context.SaveChanges();
                    return RedirectToAction("UrunEkle", "Urunler");
                }
                TempData["hata"] = "Hata! Lütfen gerekli yerleri doldurunuz!";
                return RedirectToAction("UrunEkle", "Urunler");

            }
            catch
            {
                TempData["hata"] = "Hata! Lütfen gerekli yerleri doldurunuz!";
                return RedirectToAction("UrunEkle", "Urunler");
            }

        }


        [Route("{id:int}")]
        public ActionResult UrunDuzenle(int id)
        {
            Urunler urunler = context.Urunlers.FirstOrDefault(x => x.Id == id);
            return View(urunler);
        }

        [HttpPost]
        [ValidateInput(false)]
        [Route("{id:int}")]
        public ActionResult UrunDuzenle(Urunler urunler)
        {
            try
            {
                Urunler guncellenecek= context.Urunlers.FirstOrDefault(x => x.Id == urunler.Id);
                guncellenecek.Ad = urunler.Ad;
                guncellenecek.Fiyat = urunler.Fiyat;
                guncellenecek.Aktif = urunler.Aktif;
                guncellenecek.Aciklama = urunler.Aciklama;
                guncellenecek.UzunAcıklama = urunler.UzunAcıklama;
                guncellenecek.Silindi = urunler.Silindi;
                guncellenecek.Renk = urunler.Renk;
                guncellenecek.Beden = urunler.Beden;

                context.SaveChanges();
                return RedirectToAction("UrunDuzenle", "Urunler", new { id = urunler.Id });
            }
            catch
            {

                return RedirectToAction("UrunDuzenle", "Urunler", new { id = urunler.Id });
            }

        }
        public ActionResult UrunSil(int Id)
        {
            context.Urunlers.Remove(context.Urunlers.FirstOrDefault(x => x.Id == Id));
            context.SaveChanges();
            return RedirectToAction("UrunEkle", "Urunler");
        }

        string ResimKaydet(HttpPostedFileBase file)
        {
            Image orj = Image.FromStream(file.InputStream);
            string dosyaadi = Path.GetFileNameWithoutExtension(file.FileName) + Guid.NewGuid() + Path.GetExtension(file.FileName);
            orj.Save(Server.MapPath("~/Content/images/" + dosyaadi));
            return dosyaadi;

        }
        [HttpPost]
        public ActionResult UruneResimEkle(GaleriResim gr, HttpPostedFileBase Link)
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
                    img.UrunlerID = gr.UrunlerID;
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


                                    return RedirectToAction("UrunDuzenle", "Urunler", new { id = gr.UrunlerID });
                                }
                            }
                        }
                    }
                }

                return RedirectToAction("UrunDuzenle", "Urunler", new { id = gr.UrunlerID });

            }
            catch
            {

                return RedirectToAction("UrunDuzenle", "Urunler", new { id = gr.UrunlerID });
            }
        }
    }
}