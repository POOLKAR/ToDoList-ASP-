using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDoList_Jefremov_.Models;

namespace ToDoList_Jefremov_.Controllers
{
    public class ToDoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ToDoes
        public ActionResult Index()
        {
			return View();
        }

		private IEnumerable<ToDo> GetMyToDoes()
		{
			string CurrentUserId = User.Identity.GetUserId();
			ApplicationUser currentUser = db.Users.FirstOrDefault
				(x => x.Id == CurrentUserId);
			return db.ToDo.ToList().Where(x => x.User == currentUser);
		}


		public ActionResult BuildToDoTable()
		{
			return PartialView("_ToDoTable",GetMyToDoes());
		}

        // GET: ToDoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDo.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            return View(toDo);
        }

        // GET: ToDoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Date,Description,isDone")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
				string currentUserId = User.Identity.GetUserId();
				ApplicationUser currentUser = db.Users.FirstOrDefault
					(x => x.Id == currentUserId);
				toDo.User = currentUser;
                db.ToDo.Add(toDo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toDo);
        }



		[HttpPost]
		[ValidateAntiForgeryToken]

		public ActionResult AJAXCreate([Bind(Include = "Id,Name,Date,Description")]ToDo toDo)
		{
			if(ModelState.IsValid)
			{
				string currentUserId = User.Identity.GetUserId();
				ApplicationUser currentUser = db.Users.FirstOrDefault
					(x => x.Id == currentUserId);
				toDo.User = currentUser;
				toDo.isDone = false;
				db.ToDo.Add(toDo);
				db.SaveChanges();
			}

			return PartialView("_ToDoTable", GetMyToDoes());
		}

        // GET: ToDoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDo.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            return View(toDo);
        }

        // POST: ToDoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Date,Description,isDone")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDo);
        }

        // GET: ToDoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDo.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            return View(toDo);
        }

        // POST: ToDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDo toDo = db.ToDo.Find(id);
            db.ToDo.Remove(toDo);
            db.SaveChanges();
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
