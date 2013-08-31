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
    public class ClientController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        //
        // GET: /Client/
        

        public ActionResult Index(int id = 0)
        {
            return View(db.Clients.ToList());
        }

        //
        // GET: /Client/Details/5

        public ActionResult Details(int id = 0)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        //
        // GET: /Client/Create

        public ActionResult Create()
        {
            return View();
        }

        /**
         * Some fields cannot be set by the user, therefore they are set statically 
         **/
        [HttpPost]
        public ActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                client.isArchived = false;
                client.DateAdded = DateTime.Now;
                client.LastModified = DateTime.Now;
                client.LastModifiedBy = "X";
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("../Home/Index");
            }

            return View(client);
        }

        //
        // GET: /Client/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        /**
         * Some fields cannot be edited by the user, therefore they are changed statically 
         **/
        [HttpPost]
        public ActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                Client currentClient = db.Clients.Find(client.ClientId);
                currentClient.Name = client.Name;
                currentClient.Notes = client.Notes;
                currentClient.LastModified = DateTime.Now;
                db.Entry(currentClient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        //
        // GET: /Client/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        //
        // POST: /Client/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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