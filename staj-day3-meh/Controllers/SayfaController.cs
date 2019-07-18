using staj_day3_meh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace staj_day3_meh.Controllers
{
    public class SayfaController : Controller
    {
        // GET: Sayfa
        SistemModel context = new SistemModel();
        public ActionResult SayfaEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SayfaEkle(Menu menu)
        {
            try { 
            if (menu != null)
            {
               menu.Aktif = true;
                menu.ustid = 0;
               context.Menus.Add(menu);
               context.SaveChanges();
            }
            return RedirectToAction("SayfaEkle", "Sayfa");
            }
            catch
            {
                TempData["hata"] = "Hata! Lütfen gerekli yerleri doldurunuz!";
                return RedirectToAction("SayfaEkle", "Sayfa");
            }
        }
        
        public ActionResult SayfaSil(int id)
        {
            context.Menus.Remove(context.Menus.FirstOrDefault(x => x.id == id));
            context.SaveChanges();
            return RedirectToAction("Dosyalar", "Modul");
        }
    }
}