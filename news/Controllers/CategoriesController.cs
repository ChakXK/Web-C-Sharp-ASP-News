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
using PagedList;

namespace news.Controllers
{
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories/Details/5
        public ActionResult Details(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            var Articles = db.Articles.Where(a=>a.Id_category==id).OrderByDescending(a=>a.Datetime)
                .ToPagedList(pageNumber, pageSize);
            if (Articles == null)
            {
                return HttpNotFound();
            }
            return View(Articles);
        }
    }
}
