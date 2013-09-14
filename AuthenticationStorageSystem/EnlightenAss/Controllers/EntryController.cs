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

        /**
         * Returns entries where ProjectId = id
         */
        public ActionResult Index(int id = 0)
        {
            Project project = db.Projects.Find(id);

            //if project with id cannot be found return view of all entries (maybe should change to unable to find page)
            if (project == null) return PartialView(db.Entries.ToList());

            //Set viewbags values for the html to use (for html links and labels inside view)
            ViewBag.ProjectIdNum = id;
            ViewBag.ClientIdNum = project.ClientId;
            ViewBag.ProjectName = project.Name;

            //Client this project is under
            Client client = db.Clients.Find(project.ClientId);
            ViewBag.ClientName = client.Name;

            return PartialView(db.Entries.Where(i => i.ProjectId == id));
        }

        public ActionResult EntryDisplay(int id = 0)
        {
            Entry entry = db.Entries.Find(id);
            if (entry == null) return HttpNotFound();

            return PartialView(entry);
        }

        /**
         * Return all entries, rather than entiries under specific project
         */
        public ActionResult IndexAll(int id = 0)
        {
            return PartialView("Index", db.Entries.ToList());
        }

        public ActionResult Create(int id = 0)
        {
            //Drop down list for project names
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", id);
            //Id to return to
            ViewBag.ProjectIdNum = id;

            return PartialView();
        }

        /**
         * Some fields cannot be set by the user, therefore they are set statically 
         */
        [HttpPost]
        public ActionResult Create(Entry entry)
        {
            if (ModelState.IsValid)
            {
                entry.DateAdded = DateTime.Now;
                entry.isArchived = false;
                entry.LastModified = DateTime.Now;
                entry.LastModifiedBy = "X";
                db.Entries.Add(entry);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = entry.ProjectId });
            }

            //Drop down list for project names
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", entry.ProjectId);
            return PartialView(entry);
        }


        /**
         * Edit Delete and details all in one view
         */
        public ActionResult Change(int id = 0)
        {
            Entry entry = db.Entries.Find(id);
            if (entry == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", entry.ProjectId);
            return PartialView(entry);
        }

        /**
         * Some fields cannot be edited by the user, therefore they are changed statically 
         */
        [HttpPost]
        public ActionResult Change(Entry entry)
        {
            if (ModelState.IsValid)
            {
                Entry currentEntry = db.Entries.Find(entry.EntryId);
                currentEntry.Username = entry.Username;
                currentEntry.Password = entry.Password;
                currentEntry.Website = entry.Website;
                currentEntry.Notes = entry.Notes;
                currentEntry.LastModified = DateTime.Now;
                currentEntry.LastModifiedBy = "X";
                db.Entry(currentEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EntryDisplay", new { id = entry.EntryId });
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", entry.ProjectId);
            return PartialView(entry);

        }

        /**
         * Do not know what these tags do...
         */
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Entry entry)
        {
            Entry entryDelete = db.Entries.Find(entry.EntryId);
            int projectId = entryDelete.ProjectId; //Store parent directory for redirect action
            db.Entries.Remove(entryDelete);
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