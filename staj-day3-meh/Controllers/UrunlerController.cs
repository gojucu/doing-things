using staj_day3_meh.Models;
using System;
using System.Collections.Generic;
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
    }
}