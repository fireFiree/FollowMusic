using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList.Mvc;
using PagedList;
using FollowMusic.Models;

namespace FollowMusic.Controllers
{
    public class ShopsController : Controller
    {
        //
        // GET: /Shops/
        ApplicationDbContext me = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string data = "", int page = 0)
        {
            var res = me.Shop.Where(o => (o.ShopName.Contains(data) || o.ShopCity.Contains(data) || o.ShopCountry.Contains(data))).ToList();
            int pageSize = 5;
            int pageNumber;
            if (page == 0)
            {
                pageNumber = 1;
            }
            else
            {
                pageNumber = page;
            }
            return View("Show", res.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Show(int? page)
        {
            List<Shop> g = me.Shop.ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(g.ToPagedList(pageNumber, pageSize));
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Add(Shop s)
        {
            me.Shop.Add(s);
            me.SaveChanges();

            return RedirectToAction("Show");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Shop result = me.Shop.Where(a => a.Shop_id == id).Single();

            return View("Edit", result);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(Shop u)
        {
            me.Entry(u).State = EntityState.Modified;
            me.SaveChanges();

            return RedirectToAction("Show");
        }
        public ActionResult Delete(int id)
        {
            Shop u = me.Shop.Where(us => us.Shop_id == id).Single();
            var o = me.Orders.Where(i => i.Shop_id == u.Shop_id);
            
            foreach(var c in me.Orders)
            {
                u.Orders.Remove(c);
            }

            foreach (var c in o)
            {
                me.Orders.Remove(c);
            }

            me.Shop.Remove(u);
            me.SaveChanges();

            return RedirectToAction("Show");
        }
	}
}