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

        //
        // GET: /Project/
        //returns projects where ClientId = id
        public ActionResult Index(int id = 0)
        {
            Client client = db.Clients.Find(id);

            //if client with id cannot be found return view of all projects
            if (client == null) return View(db.Projects.ToList());

            //Set Client name and Client id viewbags, for html links and labels inside view
            ViewBag.ClientName = client.Name;
            ViewBag.ClientId = id;

            //return list of projects with matching client id
            //var projects = db.Projects.Include(p => p.Client);
            return View(db.Projects.Where(i => i.ClientId == id));
        }

        public ActionResult IndexAll(int id = 0)
        {
            return View("Index", db.Projects.ToList());
        }


        /**
         * Edit Delete and details all in one view
         */
        public ActionResult Change(int id = 0)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", project.ClientId);
            return View(project);
        }

        /**
         * Some fields cannot be edited by the user, therefore they are changed statically 
         **/
        [HttpPost]
        public ActionResult Change(Project project, string submitButton)
        {
            switch (submitButton)
            {
                /* save the changes to the database */
                case "Save Changes":
                    {
                        if (ModelState.IsValid)
                        {
                            Project currentProject = db.Projects.Find(project.ProjectId);
                            currentProject.Name = project.Name;
                            currentProject.Notes = project.Notes;
                            currentProject.LastModified = DateTime.Now;
                            db.Entry(currentProject).State = EntityState.Modified;
                            db.SaveChanges();
                            //return RedirectToAction("Index", new { id = project.ClientId });
                            return RedirectToAction("../Home");
                        }
                        ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", project.ClientId);
                        return View(project);
                    }
                /* delete the client from the database */
                case "Delete":
                    {
                        Project deleteProject = db.Projects.Find(project.ProjectId);
                        //Store parent directory for redirect action
                        int clientId = project.ClientId; 
                        db.Projects.Remove(deleteProject);
                        db.SaveChanges();
                        //return RedirectToAction("Index", new { id = clientId });
                        return RedirectToAction("../Home");
                    }
                default:
                    ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", project.ClientId);
                    return View(project);
            }
        }

        //
        // GET: /Project/Create
        public ActionResult Create(int id = 0)
        {
            //Drop down list for client name
            ViewBag.ClientList = new SelectList(db.Clients, "ClientId", "Name", id /*sets default value*/);
            //Id to return to
            ViewBag.ClientIdNum = id;

            return View();
        }

        /**
         * Some fields cannot be set by the user, therefore they are set statically 
         **/
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
                //return RedirectToAction("Index", new { id = project.ClientId });
                return RedirectToAction("../Home");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", project.ClientId);
            return View(project);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}