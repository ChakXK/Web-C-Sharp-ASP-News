using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using news.Models;

namespace news.Controllers
{
    public class ArticlesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Articles
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var articles = db.Articles.Include(a => a.Category).Include(a => a.User).OrderByDescending(a=>a.Datetime);
            return View(await articles.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            article.Views = article.Views+1;
            db.Entry(article).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return View(article);
           

        }

        // GET: Articles/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Id_category = new SelectList(db.Categories, "Id", "Name");
            ViewBag.Id_user = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Articles/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Text,Heading,Briefly,Editors_Choice,Id_user,Id_category")] Article article,
            HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                if (ModelState.IsValid )
            {
                    article.Datetime = DateTime.Now;
                    db.Articles.Add(article);
                    await db.SaveChangesAsync();
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    Image newimg = new Image();
                    newimg.Name = fileName;
                    newimg.Number = 1;
                    newimg.Id_Article = article.Id;
                    db.Images.Add(newimg);
                    await db.SaveChangesAsync();
                    upload.SaveAs(Server.MapPath("~/Content/imgfiles/" + newimg.Id + fileName));
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.Id_category = new SelectList(db.Categories, "Id", "Name", article.Id_category);
                ViewBag.Id_user = new SelectList(db.Users, "Id", "Email", article.Id_user);
                ViewBag.Error = "Загрузите изображения!";
                return View(article);
            }

            ViewBag.Id_category = new SelectList(db.Categories, "Id", "Name", article.Id_category);
            ViewBag.Id_user = new SelectList(db.Users, "Id", "Email", article.Id_user);
            return View(article);
        }

        // GET: Articles/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_category = new SelectList(db.Categories, "Id", "Name", article.Id_category);
            ViewBag.Id_user = new SelectList(db.Users, "Id", "Email", article.Id_user);
            return View(article);
        }

        // POST: Articles/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Text,Heading,Briefly,Editors_Choice,Id_user,Id_category")] Article article,
            HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var thisArticle = db.Articles.Find(article.Id);
                thisArticle.Text = article.Text;
                thisArticle.Heading = article.Heading;
                thisArticle.Briefly = article.Briefly;
                thisArticle.Editors_Choice = article.Editors_Choice;
                thisArticle.Id_user = article.Id_user;
                thisArticle.Id_category = article.Id_category;

                if(upload!=null)
                {
                    var oldimg=thisArticle.Images.Where(img => img.Number == 1).First();
                    System.IO.File.Delete(Server.MapPath("~/Content/imgfiles/" + oldimg.Id + oldimg.Name));
                    db.Images.Remove(oldimg);
                    await db.SaveChangesAsync();
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    Image newimg = new Image();
                    newimg.Name = fileName;
                    newimg.Number = 1;
                    newimg.Id_Article = article.Id;
                    db.Images.Add(newimg);
                    await db.SaveChangesAsync();
                    upload.SaveAs(Server.MapPath("~/Content/imgfiles/" + newimg.Id + fileName));
                }

                db.Entry(thisArticle).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id_category = new SelectList(db.Categories, "Id", "Name", article.Id_category);
            ViewBag.Id_user = new SelectList(db.Users, "Id", "Email", article.Id_user);
            return View(article);
        }

        // GET: Articles/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Article article = await db.Articles.FindAsync(id);
            db.Articles.Remove(article);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
