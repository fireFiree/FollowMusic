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
    public class AlbumsController : Controller
    {
        //
        // GET: /Albums/
        ApplicationDbContext me = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string data="",int page = 0)
        {
            var res = me.Album.Include(a => a.Genre).Include(b => b.Artist).Where(o => (o.AlbumName.Contains(data) || o.Artist.ArtistName.Contains(data) || o.Genre.GenreName.Contains(data))).ToList();
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
            List<Album> g = me.Album.ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(g.ToPagedList(pageNumber, pageSize));
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Add()
        {
            SelectList genre = new SelectList(me.Genre, "Genre_id", "GenreName");
            ViewBag.Genres = genre;
            SelectList art = new SelectList(me.Artist, "Artist_id", "ArtistName");
            ViewBag.Artists = art;
            ViewBag.Songs = me.Song.ToList();
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Add(Album pl, int[] selectedSongs)
        {
                if (selectedSongs != null)
                {
                    foreach (var c in me.Song.Where(co => selectedSongs.Contains(co.Song_id)))
                    {
                        pl.Song.Add(c);
                    }
                }
                me.Album.Add(pl);
                me.SaveChanges();

                return RedirectToAction("Show");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Album result = me.Album.Where(a => a.Album_id == id).Include(a => a.Genre).Include(b => b.Artist).Single();

            SelectList genre = new SelectList(me.Genre, "Genre_id", "GenreName", result.Genre.Genre_id);
            ViewBag.Genres = genre;
            SelectList art = new SelectList(me.Artist, "Artist_id", "ArtistName", result.Artist.Artist_id);
            ViewBag.Artists = art;
            ViewBag.Songs = me.Song.ToList();

            return View("Edit", result);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(Album u, int[] selectedSongs)
        {
            Album newAlb = me.Album.Where(a => a.Album_id == u.Album_id).Include(a=>a.Genre).Include(b=>b.Artist).Single();
            newAlb.AlbumName = u.AlbumName;
            newAlb.Artist_id = u.Artist_id;
            newAlb.Genre_id = u.Genre_id;
            newAlb.ReleaseDate = u.ReleaseDate;

            newAlb.Song.Clear();

            if (selectedSongs != null)
            {
                foreach (var c in me.Song.Where(co => selectedSongs.Contains(co.Song_id)))
                {
                    newAlb.Song.Add(c);
                }
            }
            me.Entry(newAlb).State = EntityState.Modified;
            me.SaveChanges();

            return RedirectToAction("Show");
        }
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            Album u = me.Album.Where(us => us.Album_id == id).Single();
            var pl = me.Orders.Where(i => i.Album_id == u.Album_id);

            foreach(var c in pl)
            {
                me.Orders.Remove(c);
            }
            foreach(var t in me.Song)
            {
                u.Song.Remove(t);
            }
            foreach (var c in me.Orders)
            {
                u.Orders.Remove(c);
            }
            me.Album.Remove(u);
            me.SaveChanges();

            return RedirectToAction("Show");
        }

        public ActionResult Details(int id)
        {
            Album s = me.Album.Where(us => us.Album_id == id).Include(a => a.Genre).Include(a => a.Artist).Single();
            return View("Details", s);
        }
	}
}