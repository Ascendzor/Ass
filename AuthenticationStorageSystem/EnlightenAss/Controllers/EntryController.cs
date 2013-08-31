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
            Project project = db.Projects.Find(id);

            //if client with id cannot be found return view of all projects
            if (project == null) return View(db.Entries.ToList());

            //Set viewbags values for the html to use (for html links and labels inside view)
            ViewBag.ProjectIdNum = id;
            ViewBag.ClientIdNum = project.ClientId;
            ViewBag.ProjectName = project.Name;

            //Client this project is under
            Client client = db.Clients.Find(project.ClientId);
            ViewBag.ClientName = client.Name;

            return View(db.Entries.Where(i => i.ProjectId == id));
        }

        /**
         * Return all entries 
         */
        public ActionResult IndexAll(int id = 0)
        {
            return View("Index", db.Entries.ToList());
        }


        public ActionResult Create(int id = 0)
        {
            //Drop down list for project names
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", id);
            //Id to return to

            ViewBag.ProjectIdNum = id;

            return View();
        }

        /**
         * Some fields cannot be set by the user, therefore they are set statically 
         **/
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
                //return RedirectToAction("Index", new { id = entry.ProjectId });
                return RedirectToAction("../Home");
            }

            //Drop down list for project names
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", entry.ProjectId);
            return View(entry);
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
            return View(entry);
        }

        /**
         * Some fields cannot be edited by the user, therefore they are changed statically 
         **/
        [HttpPost]
        public ActionResult Change(Entry entry, string submitButton)
        {
            switch (submitButton)
            {
                /* save the changes to the database */
                case "Save Changes":
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
                            //return RedirectToAction("Index", new { id = entry.ProjectId });
                            return RedirectToAction("../Home");
                        }
                        ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", entry.ProjectId);
                        return View(entry);
                    }
                /* delete the enrty from the database */
                case "Delete":
                    {
                        Entry entryDelete = db.Entries.Find(entry.EntryId);
                        int projectId = entry.ProjectId; //Store parent directory for redirect action
                        db.Entries.Remove(entryDelete);
                        db.SaveChanges();
                        //return RedirectToAction("Index", new { id = projectId });
                        return RedirectToAction("../Home");
                    }
                default:
                    ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", entry.ProjectId);
                    return View(entry);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}