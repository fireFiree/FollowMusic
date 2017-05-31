using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Data.Entity;
using FollowMusic.Models;

namespace FollowMusic.Controllers
{
    public class LabelController : Controller
    {
        ApplicationDbContext me = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string data = "", int page = 0)
        {
            var res = me.Label.Where(o => (o.Label_name.Contains(data) || o.Description.Contains(data) || o.Countries.Contains(data))).ToList();
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
            List<Label> g = me.Label.ToList();
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
        public ActionResult Add(Label g)
        {
            me.Label.Add(g);
            me.SaveChanges();

            return RedirectToAction("Show");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Label result = me.Label.Where(a => a.Label_id == id).Single();
            return View("Edit", result);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(Label g)
        {
            me.Entry(g).State = EntityState.Modified;
            me.SaveChanges();

            return RedirectToAction("Show");
        }
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            Label u = me.Label.Where(us => us.Label_id == id).Single();
            var al = me.Album.Where(m => m.Label_id == id);
            foreach (var c in me.Album)
            {
                u.Album.Remove(c);
            }
            foreach (var c in al)
            {
                me.Album.Remove(c);
            }

            me.Label.Remove(u);
            me.SaveChanges();

            return RedirectToAction("Show");
        }
    }
}