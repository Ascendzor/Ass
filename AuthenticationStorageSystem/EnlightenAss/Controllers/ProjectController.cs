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
    public class ProjectController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        /**
         * Returns projects where ClientId = id
         */
        public ActionResult Index(int id = 0)
        {
            Client client = db.Clients.Find(id);

            //if client with id cannot be found return view of all projects (maybe should change to unable to find page)
            if (client == null) return PartialView(db.Projects.ToList());

            //Set Client name and Client id viewbags, for html links and labels inside view
            ViewBag.ClientName = client.Name;
            ViewBag.ClientId = id;

            //return list of projects with matching client id
            return PartialView(db.Projects.Where(i => i.ClientId == id));
        }

        /**
         * Return all projects, rather than projects under specific client
         */
        public ActionResult IndexAll(int id = 0)
        {
            return PartialView("Index", db.Projects.ToList());
        }


        /**
         * View to see all fields, edit the data or delete
         */
        public ActionResult Change(int id = 0)
        {
            Project project = db.Projects.Find(id);
            if (project == null) return HttpNotFound();

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", project.ClientId);
            return PartialView(project);
        }

        /**
         * Some fields cannot be edited by the user, therefore they are changed statically 
         */
        [HttpPost]
        public ActionResult Change(Project project)
        {
            if (ModelState.IsValid)
            {
                Project currentProject = db.Projects.Find(project.ProjectId);
                currentProject.Name = project.Name;
                currentProject.Notes = project.Notes;
                currentProject.LastModified = DateTime.Now;
                db.Entry(currentProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("../Entry/Index", new { id = project.ProjectId });
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", project.ClientId);
            return PartialView(project);

        }

        /**
         * Do not know what these tags do...
         */
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Project project)
        {
            Project deleteProject = db.Projects.Find(project.ProjectId);
            //Store parent directory for redirect action
            int clientId = deleteProject.ClientId;
            db.Projects.Remove(deleteProject);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = clientId });
        }

        //
        // GET: /Project/Create
        public ActionResult Create(int id = 0)
        {
            //Drop down list for client name
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", id /*sets default value*/);
            //Id to return to
            ViewBag.ClientIdNum = id;

            return PartialView();
        }

        /**
         * Some fields cannot be set by the user, therefore they are set statically 
         */
        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                project.DateAdded = DateTime.Now;
                project.isArchived = false;
                project.LastModified = DateTime.Now;
                project.LastModifiedBy = "X";
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = project.ClientId });
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", project.ClientId);
            return PartialView(project);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}