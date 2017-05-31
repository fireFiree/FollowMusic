using FollowMusic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Word = Microsoft.Office.Interop.Word;

namespace FollowMusic.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext me = new ApplicationDbContext();
        [Authorize]
        public ActionResult Report()
        {
            Word.Application WordApp = new Word.Application();
            WordApp.Visible = false;

            Word.Document doc = WordApp.Documents.Open("C://template_FollowMusic.docx");

            Word.Bookmark bmUsers = doc.Bookmarks["users"];
            Word.Bookmark bmOrders = doc.Bookmarks["orders"];
            Word.Bookmark bmSongs = doc.Bookmarks["songs"];
            Word.Bookmark bmShops = doc.Bookmarks["shops"];
            Word.Bookmark bmPls = doc.Bookmarks["pls"];
            Word.Bookmark bmGenres = doc.Bookmarks["genres"];
            Word.Bookmark bmArtists = doc.Bookmarks["artists"];
            Word.Bookmark bmAlbums = doc.Bookmarks["albums"];
            Word.Bookmark bmDate = doc.Bookmarks["data"];
            Word.Bookmark bmLabels = doc.Bookmarks["labels"];

            WordApp.Selection.SetRange(bmUsers.Range.Start, bmUsers.Range.End);
            WordApp.Selection.Font.Size = 16;
            WordApp.Selection.Text = me.AppUsers.Count().ToString();

            WordApp.Selection.SetRange(bmOrders.Range.Start, bmOrders.Range.End);
            WordApp.Selection.Font.Size = 16;
            WordApp.Selection.Text = me.Orders.Count().ToString();

            WordApp.Selection.SetRange(bmSongs.Range.Start, bmSongs.Range.End);
            WordApp.Selection.Font.Size = 16;
            WordApp.Selection.Text = me.Song.Count().ToString();

            WordApp.Selection.SetRange(bmShops.Range.Start, bmShops.Range.End);
            WordApp.Selection.Font.Size = 16;
            WordApp.Selection.Text = me.Shop.Count().ToString();

            WordApp.Selection.SetRange(bmPls.Range.Start, bmPls.Range.End);
            WordApp.Selection.Font.Size = 16;
            WordApp.Selection.Text = me.Playlist.Count().ToString();

            WordApp.Selection.SetRange(bmGenres.Range.Start, bmGenres.Range.End);
            WordApp.Selection.Font.Size = 16;
            WordApp.Selection.Text = me.Genre.Count().ToString();

            WordApp.Selection.SetRange(bmAlbums.Range.Start, bmAlbums.Range.End);
            WordApp.Selection.Font.Size = 16;
            WordApp.Selection.Text = me.Album.Count().ToString();

            WordApp.Selection.SetRange(bmArtists.Range.Start, bmArtists.Range.End);
            WordApp.Selection.Font.Size = 16;
            WordApp.Selection.Text = me.Artist.Count().ToString();

            WordApp.Selection.SetRange(bmLabels.Range.Start, bmLabels.Range.End);
            WordApp.Selection.Font.Size = 16;
            WordApp.Selection.Text = me.Label.Count().ToString();

            WordApp.Selection.SetRange(bmDate.Range.Start, bmDate.Range.End);
            WordApp.Selection.Font.Size = 16;
            WordApp.Selection.Text = DateTime.Today.ToString("yyyy/MM/dd");

            doc.SaveAs2(Server.MapPath("~/Statistic.docx"));
            doc.Close();

            WordApp.Quit(Word.WdSaveOptions.wdSaveChanges);

            string file_path = Server.MapPath("~/Statistic.docx");
            string file_type = "application/docx";
            string file_name = "FollowMusic_Statistic.docx";

            return File(file_path, file_type, file_name);
        }

        [Authorize]
        public ActionResult Statistic()
        {
            ViewBag.NumArtists = me.Artist.Count();
            ViewBag.NumAlbums = me.Album.Count();
            ViewBag.NumUsers = me.AppUsers.Count();
            ViewBag.NumPL = me.Playlist.Count();
            ViewBag.NumSongs = me.Song.Count();
            ViewBag.NumShops = me.Shop.Count();
            ViewBag.NumOrders = me.Orders.Count();
            ViewBag.NumLabels = me.Label.Count();
            ViewBag.Date = DateTime.Today.ToString("dd/MM/yyyy");
            return View("Statistic");
        }
        public ActionResult Index()
        {
            return View();
        }

    }
}