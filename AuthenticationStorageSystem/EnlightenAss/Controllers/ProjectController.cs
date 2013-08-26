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
            //Set Client name and Client id viewbags, for html links and labels inside view
            Client client = db.Clients.Find(id);
            ViewBag.ClientName = client.Name;
            ViewBag.ClientId = id;

            //get and return list of projects with matching client id
            var projects = db.Projects.Include(p => p.Client);
            return View(projects.Where(i => i.ClientId == id));
        }

        //
        // GET: /Project/Details/5

        public ActionResult Details(int id = 0)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        //
        // GET: /Project/Create
        //Set ViewBag.ClientId to force the Project being created to be under the current Client 
        public ActionResult Create(int id = 0)
        {
            ViewBag.ClientId = new SelectList(db.Clients.Where(i => i.ClientId == id), "ClientId", "Name");
            return View();
        }

        //
        // POST: /Project/Create

        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = project.ClientId });
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", project.ClientId);
            return View(project);
        }

        //
        // GET: /Project/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", project.ClientId);
            return View(project);
        }

        //
        // POST: /Project/Edit/5

        [HttpPost]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = project.ClientId });
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", project.ClientId);
            return View(project);
        }

        //
        // GET: /Project/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        //
        // POST: /Project/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            int clientId = project.ClientId; //Store parent directory for redirect action
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = clientId });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}