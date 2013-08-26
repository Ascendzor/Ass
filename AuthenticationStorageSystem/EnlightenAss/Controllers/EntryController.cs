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
            //Set viewbags values for the html to use (for html links and labels inside view)
            Project project = db.Projects.Find(id);
            ViewBag.ProjectIdNum = id;
            ViewBag.ClientIdNum = project.ClientId;
            ViewBag.ProjectName = project.Name;

            //Client this project is under
            Client client = db.Clients.Find(project.ClientId);
            ViewBag.ClientName = client.Name;

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
        //Set ViewBag.ProjectId to force the Entry being created to under the current project 
        public ActionResult Create(int id = 0)
        {
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
                return RedirectToAction("Index", new { id = entry.ProjectId });
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
            int projectId = entry.ProjectId; //Store parent directory for redirect action
            db.Entries.Remove(entry);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = projectId });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}