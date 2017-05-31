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
    public class ArtistsController : Controller
    {
        //
        // GET: /Artists/
        ApplicationDbContext me = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string data = "", int page = 0)
        {
            var res = me.Artist.Where(o => (o.ArtistName.Contains(data) || o.Country.Contains(data) || o.Biography.Contains(data))).ToList();
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
            List<Artist> g = me.Artist.ToList();
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
        public ActionResult Add(Artist u)
        {
            me.Artist.Add(u);
            me.SaveChanges();

            return RedirectToAction("Show");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Artist result = me.Artist.Where(u => u.Artist_id == id).Single();
            return View("Edit", result);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(Artist u)
        {
            me.Entry(u).State = EntityState.Modified;
            me.SaveChanges();

            return RedirectToAction("Show");
        }
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            Artist u = me.Artist.Where(us => us.Artist_id == id).Single();
            var al = me.Album.Where(m => m.Artist_id == id);

            foreach (var c in me.Album)
            {
                u.Album.Remove(c);
            }
            foreach( var c in al)
            {
                me.Album.Remove(c);
            }
            
            me.Artist.Remove(u);
            me.SaveChanges();

            return RedirectToAction("Show");
        }
	}
}