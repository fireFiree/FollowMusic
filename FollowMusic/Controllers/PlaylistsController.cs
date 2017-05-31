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
    public class PlaylistsController : Controller
    {
        //
        // GET: /Playlists/
        ApplicationDbContext me = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string data = "", int page = 0)
        {
            int pageSize = 5;
            int pageNumber;
            if (User.IsInRole("admin"))
            {
                var res = me.Playlist.Include(a => a.Users).Where(o => (o.PlaylistName.Contains(data) || o.Users.UserName.Contains(data))).ToList();
                
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
            else
            {
                var user = me.AppUsers.Where(x => x.UserName == User.Identity.Name).First();
                var res = me.Playlist.Include(a => a.Users).Where(o => (o.PlaylistName.Contains(data) || o.Users.UserName.Contains(data)) && o.User_id == user.User_id).ToList();

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
        }
        [Authorize]
        public ActionResult Show(int? page)
        {

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (User.IsInRole("admin"))
            {
                List<Playlist> g = me.Playlist.Include(c => c.Users).ToList();

                return View(g.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var user = me.AppUsers.Where(x => x.UserName == User.Identity.Name).First();
                List<Playlist> g = me.Playlist.Include(c => c.Users).Where(x=> x.User_id == user.User_id).ToList();
                return View(g.ToPagedList(pageNumber, pageSize));
            }
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public ActionResult Add()
        {
            var user = me.AppUsers.Where(x => x.UserName == User.Identity.Name).First();
            ViewBag.UserId = user.User_id.ToString();


            SelectList pl = new SelectList(me.Users, "User_id", "UserName");
            ViewBag.Playlists = pl;

            ViewBag.Songs = me.Song.ToList();

            return View();
        }
        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult Add(Playlist pl, int[] selectedSongs)
        {
            if (selectedSongs != null)
            {
                foreach (var c in me.Song.Where(co => selectedSongs.Contains(co.Song_id)))
                {
                    pl.Song.Add(c);
                }
            }
            me.Playlist.Add(pl);
            me.SaveChanges();

            return RedirectToAction("Show");
        }
        [Authorize(Roles = "user")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Playlist result = me.Playlist.Where(a => a.Playlist_id == id).Include(a => a.Users).Single();

           
            SelectList pl = new SelectList(me.Users, "User_id", "UserName", result.User_id);
            ViewBag.Playlists = pl;
            var user = me.AppUsers.Where(x => x.UserName == User.Identity.Name).First();
            ViewBag.UserId = user.User_id.ToString();


            ViewBag.Songs = me.Song.ToList();

            return View("Edit", result);
        }
        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult Edit(Playlist u, int[] selectedSongs)
        {
            Playlist newPL = me.Playlist.Where(a => a.Playlist_id == u.Playlist_id).Include(a => a.Users).Single();
            newPL.PlaylistName = u.PlaylistName;
            newPL.User_id = u.User_id;

            newPL.Song.Clear();

            if (selectedSongs != null)
            {
                foreach (var c in me.Song.Where(co => selectedSongs.Contains(co.Song_id)))
                {
                    newPL.Song.Add(c);
                }
            }
            me.Entry(newPL).State = EntityState.Modified;
            me.SaveChanges();

            return RedirectToAction("Show");
        }
        public ActionResult Delete(int id)
        {
            Playlist u = me.Playlist.Where(us => us.Playlist_id == id).Single();

            foreach (var t in me.Song)
            {
                u.Song.Remove(t);
            }

            me.Playlist.Remove(u);
            me.SaveChanges();

            return RedirectToAction("Show");
        }

        public ActionResult Details(int id)
        {
            Playlist s = me.Playlist.Where(us => us.Playlist_id == id).Include(a => a.Song).Single();
            return View("Details", s);
        }

    }
}