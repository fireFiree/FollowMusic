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
    public class SongsController : Controller
    {
        //
        // GET: /Songs/
        ApplicationDbContext me = new ApplicationDbContext();
        public ActionResult Index()
        {       
            return View();
        }
        public ActionResult Search(string data = "", int page = 0)
        {
            var res = me.Song.Where(o => (o.SongName.Contains(data))).ToList();
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
            List<Song> g = me.Song.ToList();
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
        public ActionResult Add(Song s)
        {
            me.Song.Add(s);
            me.SaveChanges();

            return RedirectToAction("Show");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Song result = me.Song.Where(a => a.Song_id == id).Single();
         
            // Создаем список команд для передачи в представление
            //SelectList pl = new SelectList(me.Users, "User_id", "UserName", result.Playlist_id);
            //ViewBag.Playlists = pl;

            return View("Edit", result);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(Song u)
        {
            me.Entry(u).State = EntityState.Modified;
            me.SaveChanges();

            return RedirectToAction("Show");
        }
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            Song u = me.Song.Where(us => us.Song_id == id).Single();

            foreach (var t in me.Playlist)
            {
                u.Playlist.Remove(t);
            }

            foreach (var t in me.Album)
            {
                u.Album.Remove(t);
            }
            me.Song.Remove(u);
            me.SaveChanges();

            return RedirectToAction("Show");
        }

        public ActionResult Details(int id)
        {
            Song song = me.Song.Where(us => us.Song_id == id).Include(a => a.Album).Single();
            return View("Details", song);
        }
	}
}