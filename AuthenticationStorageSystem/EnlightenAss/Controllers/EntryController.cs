using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssClassLibrary;

namespace EnlightenAss.Controllers
{
    public class EntryController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        //
        // GET: /Entry/
        //Returns entries where ProjectId = id
        public ActionResult Index(int id = 0)
        {
            ViewBag.ProjectId = id;
            return View(db.Entries.Where(i => i.ProjectId == id));
        }

        //
        // GET: /Entry/Details/5

        public ActionResult Details(int id = 0)
        {
            Entry entry = db.Entries.Find(id);
            if (entry == null)
            {
                return HttpNotFound();
            }
            return View(entry);
        }

        //
        // GET: /Entry/Create

        public ActionResult Create(int id = 0)
        {
            ViewBag.ProjectIdNum = id;
            ViewBag.ProjectId = new SelectList(db.Projects.Where(i => i.ProjectId == id), "ProjectId", "Name");
            return View();
        }

        //
        // POST: /Entry/Create

        [HttpPost]
        public ActionResult Create(Entry entry)
        {
            if (ModelState.IsValid)
            {
                db.Entries.Add(entry);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = entry.ProjectId });
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", entry.ProjectId);
            return View(entry);
        }

        //
        // GET: /Entry/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Entry entry = db.Entries.Find(id);
            if (entry == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", entry.ProjectId);
            return View(entry);
        }

        //
        // POST: /Entry/Edit/5

        [HttpPost]
        public ActionResult Edit(Entry entry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", entry.ProjectId);
            return View(entry);
        }

        //
        // GET: /Entry/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Entry entry = db.Entries.Find(id);
            if (entry == null)
            {
                return HttpNotFound();
            }
            return View(entry);
        }

        //
        // POST: /Entry/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Entry entry = db.Entries.Find(id);
            db.Entries.Remove(entry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}