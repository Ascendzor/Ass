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


        public ActionResult Index(int id = 0)
        {
            return View(db.Clients.ToList());
        }

        /**
         * To replase Edit, delete, details views
         */
        public ActionResult Change(int id = 0)
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
        public ActionResult Change(Client client, string submitButton)
        {
            switch (submitButton)
            {
                /* save the changes to the database */
                case "Save Changes":
                    {
                        if (ModelState.IsValid)
                        {
                            Client currentClient = db.Clients.Find(client.ClientId);
                            currentClient.Name = client.Name;
                            currentClient.Notes = client.Notes;
                            currentClient.LastModified = DateTime.Now;
                            db.Entry(currentClient).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("../Home");
                        }
                        return View(client);
                    }
                /* delete the client from the database */
                case "Delete":
                    {
                        Client currentClient = db.Clients.Find(client.ClientId);
                        db.Clients.Remove(currentClient);
                        db.SaveChanges();
                        return RedirectToAction("../Home");
                    }
                default:
                    return View(client);
            }
        }

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
                return RedirectToAction("../Home");
            }

            return View(client);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}