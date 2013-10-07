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
            return PartialView(db.Clients.ToList());
        }

        /**
         * View to see all fields, edit the data or delete
         */
        public ActionResult Change(int id = 0)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return PartialView(client);
        }

        /**
         * Some fields cannot be edited by the user, therefore they are changed statically 
         **/
        [HttpPost]
        public ActionResult Change(Client client)
        {
            if (ModelState.IsValid)
            {
                Client currentClient = db.Clients.Find(client.ClientId);
                currentClient.Name = client.Name;
                currentClient.Notes = client.Notes;
                currentClient.LastModified = DateTime.Now;
                currentClient.isArchived = client.isArchived;
                db.Entry(currentClient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("../Project/Index", new { id = client.ClientId });
            }
            return PartialView(client);

        }

        /**
         * Do not know what these tags do...
         */
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Client client)
        {
            Client currentClient = db.Clients.Find(client.ClientId);
            db.Clients.Remove(currentClient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return PartialView();
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
                return RedirectToAction("Index");
            }

            return PartialView(client);
        }

        //toggles the given clients isArchived value
        public void toggleIsArchived(int id)
        {
            Client currentClient = db.Clients.Find(id);
            currentClient.isArchived = !currentClient.isArchived;
            db.Entry(currentClient).State = EntityState.Modified;
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}