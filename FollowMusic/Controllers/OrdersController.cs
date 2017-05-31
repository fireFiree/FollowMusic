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
    public class OrdersController : Controller
    {
        //
        // GET: /Orders/
        ApplicationDbContext me = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Search(string data = "", int page = 0)
        {
            var res = me.Orders.Include(a => a.Shop).Include(a => a.Album).Include(b => b.Users).Include(a => a.Status).Where(o => (o.Shop.ShopName.Contains(data) || o.Status.Status1.Contains(data) || o.Users.UserName.Contains(data) || o.Album.AlbumName.Contains(data))).ToList();
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
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (User.IsInRole("admin"))
            {
                List<Orders> g = me.Orders.Include(c => c.Users).ToList();

                return View(g.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var user = me.AppUsers.Where(x => x.UserName == User.Identity.Name).First();
                List<Orders> g = me.Orders.Include(c => c.Users).Where(x => x.User_id == user.User_id).ToList();
                return View(g.ToPagedList(pageNumber, pageSize));
            }
        }
        [Authorize(Roles = "user")]
        [HttpGet]
        public ActionResult Add()
        {
            SelectList shops = new SelectList(me.Shop, "Shop_id", "ShopName");
            ViewBag.Shops = shops;
            SelectList albums = new SelectList(me.Album, "Album_id", "AlbumName");
            ViewBag.Albums = albums;
            var user = me.AppUsers.Where(x => x.UserName == User.Identity.Name).First();
            ViewBag.UserId = user.User_id.ToString();
            SelectList status = new SelectList(me.Status, "Status_id", "Status1");
            ViewBag.Status = status;

            return View();
        }
        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult Add(Orders s)
        {
            me.Orders.Add(s);
            me.SaveChanges();

            return RedirectToAction("Show");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Orders res = me.Orders.Where(a => a.Order_id == id).Include(a => a.Shop).Include(b => b.Album).Include(c => c.Users).Include(d => d.Status).Single();
            // Создаем список команд для передачи в представление
            SelectList shops = new SelectList(me.Shop, "Shop_id", "ShopName", res.Shop.Shop_id);
            ViewBag.Shops = shops;
            SelectList albums = new SelectList(me.Album, "Album_id", "AlbumName", res.Album.Album_id);
            ViewBag.Albums = albums;
            SelectList users = new SelectList(me.AppUsers, "User_id", "UserName", res.Users.User_id);
            ViewBag.Users = users;
            SelectList status = new SelectList(me.Status, "Status_id", "Status1", res.Status.Status1);
            ViewBag.Status = status;
            
            return View("Edit", res);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(Orders u)
        {
            me.Entry(u).State = EntityState.Modified;
            me.SaveChanges();

            return RedirectToAction("Show");
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            Orders u = me.Orders.Where(us => us.Order_id == id).Single();


            me.Orders.Remove(u);
            me.SaveChanges();

            return RedirectToAction("Show");
        }
	}
}