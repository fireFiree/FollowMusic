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
    public class GenresController : Controller
    {
        //
        // GET: /Genres/
        ApplicationDbContext me = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Search(string data = "", int page=0)
        {
            var res = me.Genre.Where(o => (o.GenreName.Contains(data) || o.Descripition.Contains(data) )).ToList();
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
            return View("Show", res.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult Show(int? page)
        {
            List<Genre> g = me.Genre.ToList();
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
        public ActionResult Add(Genre g)
        {
            me.Genre.Add(g);
            me.SaveChanges();

            return RedirectToAction("Show");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Genre result = me.Genre.Where(a => a.Genre_id == id).Single();
            return View("Edit", result);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(Genre g)
        {
            me.Entry(g).State = EntityState.Modified;
            me.SaveChanges();

            return RedirectToAction("Show");
        }
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            Genre u = me.Genre.Where(us => us.Genre_id == id).Single();
            var al = me.Album.Where(m => m.Genre_id == id);
            foreach(var c in me.Album)
            {
                u.Album.Remove(c);
            }
            foreach (var c in al)
            {
                me.Album.Remove(c);
            }

            me.Genre.Remove(u);
            me.SaveChanges();

            return RedirectToAction("Show");
        }
	}
}