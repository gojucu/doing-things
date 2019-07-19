using staj_day3_meh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace staj_day3_meh.Controllers
{
    [RoutePrefix("Menu")]
    public class HomeController : Controller
    {
        
        SistemModel context = new SistemModel();

        [Route("{id:int}")]
        public ActionResult Index(int id)
        {
            ViewBag.Title = id;
            Menu me = context.Menus.FirstOrDefault(x => x.id == id);
            var hey = context.Menus.ToList().Where(x => x.ustid == 0 && x.id != id);
            ViewBag.Liste = hey;

            return View(me);
        }
        [HttpPost]
        [ValidateInput(false)]
        [Route("{id:int}")]
        public ActionResult Index(Menu menu)
        {
            Menu guncellenecek = context.Menus.FirstOrDefault(x => x.id == menu.id);
            guncellenecek.Ad = menu.Ad;
            guncellenecek.ustid = menu.ustid;
            guncellenecek.sira = menu.sira;
            guncellenecek.icerik = menu.icerik;
            guncellenecek.dislink = menu.dislink;
            guncellenecek.Aktif = menu.Aktif; 

            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SidebarGetir()
        {
            return View(context.Menus.ToList());
        }
    }
}