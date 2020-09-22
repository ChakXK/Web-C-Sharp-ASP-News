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
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var comments = db.Comments.Include(c => c.Article).OrderByDescending(c=>c.Datetime);
            return View(await comments.ToListAsync());
        }

        // POST: Comments/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Text,,User_Name,Email,Id_Article")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Datetime = DateTime.Now;
                db.Comments.Add(comment);
                await db.SaveChangesAsync();
                
            }
            return RedirectToAction("Details", "Articles", new { id=comment.Id_Article});
        }

        [Authorize]
        // GET: Comments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        [Authorize]
        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Comment comment = await db.Comments.FindAsync(id);
            db.Comments.Remove(comment);
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
