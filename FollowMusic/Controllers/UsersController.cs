using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList.Mvc;
using PagedList;
using FollowMusic.Models;
using Microsoft.AspNet.Identity;

namespace FollowMusic.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/
        ApplicationDbContext me = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Search(string data = "", int page = 0)
        {
            var res = me.AppUsers.Where(o => (o.UserName.Contains(data) || o.UserNickName.Contains(data))).ToList();
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
        [Authorize]
        public ActionResult Show(int? page)
        {
            List<Users> g = me.AppUsers.ToList();
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
        public ActionResult Add(Users u)
        {
            me.AppUsers.Add(u);
            me.SaveChanges();

            return RedirectToAction("Show", me.Users);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Users result = me.AppUsers.Where(u => u.User_id == id).Single();
            return View("Edit", result);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(Users u)
        {
            //Users a = me.Users.Where(us => us.User_id == u.User_id).Single();
            //a.UserName = u.UserName;
            //a.UserNickName = u.UserNickName;
            //a.UserConcacts = u.UserConcacts;
            me.Entry(u).State = EntityState.Modified;
            me.SaveChanges();

            return RedirectToAction("Show");
        }
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            Users u = me.AppUsers.Where(us => us.User_id == id).Single();
            var pl = me.Playlist.Where(i => i.User_id == u.User_id);
            var o = me.Orders.Where(i => i.User_id == u.User_id);
            var s = me.Song.Include(x => x.Playlist);

            var defUser = new ApplicationUser(); //context.Users.Where(x => x.Email == "ddsda").FirstOrDefault();
            var user = me.Users.Where(x => x.Email == u.UserName).FirstOrDefault();
            if (user != defUser && user != null)
            {
                me.Users.Remove(user);
                me.SaveChanges();
            }

            foreach (var c in o)
            {
                u.Orders.Remove(c);
            }
            foreach (var c in o)
            {
                me.Orders.Remove(c);
            }

            foreach (var c in pl)
            {
                foreach (var k in s)
                {
                    c.Song.Remove(k);
                }

                me.Playlist.Remove(c);
            }
            
            me.AppUsers.Remove(u);
            me.SaveChanges();

            return RedirectToAction("Show");
        }


    }
}