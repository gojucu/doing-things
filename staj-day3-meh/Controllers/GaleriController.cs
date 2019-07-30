using staj_day3_meh.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

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
            var hey = context.GaleriTips.ToList().Where(x=>x.ustid==0);
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
                    galeriler.Şema = "Şema 1";
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
        public ActionResult GaleriDuzenle(int id, int? page)
        {
            Galeriler galeriler= context.Galerilers.FirstOrDefault(x => x.Id == id);
            ViewBag.Title = id;
            if (galeriler.Tipi == "Ürün Slider")
            {
                //var urunler = context.Urunlers.ToList().ToPagedList(page ?? 1, 9);
                var sliderResim = context.GaleriResims.ToList().Where(x => x.UrunlerID != null).ToList().ToPagedList(page ?? 1, 9);
                ViewBag.galeriResim = sliderResim;
                //ViewBag.urunler = urunler;
            }
            else
            {
                var galeriResim = context.GaleriResims.ToList().Where(x => x.GalerilerID == id);
                ViewBag.galeriResim = galeriResim;
            }

            var sayi = context.GaleriResims.ToList().Where(x => x.GalerilerID == id).Count();
            ViewBag.sayi = sayi;


            var galerii = context.GaleriTips.ToList().Where(x=>x.ustid==0);
            ViewBag.galeriListe = galerii;
            
            var bar  = context.Galerilers.FirstOrDefault(x => x.Id == id);
            var heyy = context.GaleriTips.FirstOrDefault(x=>x.Ad==bar.Tipi);
            var galeriii = context.GaleriTips.ToList().Where(x => x.ustid == heyy.Id);
            ViewBag.galeriListee = galeriii;

            //var urunResimler = context.GaleriResims.ToList();
            //var urunler = context.GaleriResims.FirstOrDefault(x => x.Id);
            //ViewBag.urunler = urunler;

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
                guncellenecek.Şema = galeriler.Şema;
                guncellenecek.Genişlik = galeriler.Genişlik;
                guncellenecek.Yükseklik = galeriler.Yükseklik;
                guncellenecek.Aktif = galeriler.Aktif;

                context.SaveChanges();
                return RedirectToAction("GaleriDuzenle", "Galeri", new { id = galeriler.Id });
            }
            catch
            {
                return RedirectToAction("Galeriler", "Galeri");
            }
        }

        string ResimKaydet(HttpPostedFileBase file)
        {
            Image orj = Image.FromStream(file.InputStream);
            Bitmap kck = new Bitmap(orj, 250, 250);
            string dosyaadi = Path.GetFileNameWithoutExtension(file.FileName) + Guid.NewGuid() + Path.GetExtension(file.FileName);
            orj.Save(Server.MapPath("~/Content/images/" + dosyaadi));
            kck.Save(Server.MapPath("~/Content/images/small/" + dosyaadi));
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
                    gr.KucukLink = "/Content/images/small/" + dosyaadi;
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


                                    return RedirectToAction("GaleriDuzenle", "Galeri", new { id = gr.GalerilerID });
                                }
                            }
                        }
                    }
                }

                return Json(false, JsonRequestBehavior.AllowGet);

            }
            catch
            {

                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}