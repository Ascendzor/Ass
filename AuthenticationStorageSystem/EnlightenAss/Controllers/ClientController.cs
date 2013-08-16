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
        static int a = 0;

        public ActionResult Index(int id = 0)
        {
             
            return View(db.Clients.ToList());
        }

        public ActionResult Search()
        {

            a = a + 1;

            ViewBag.Number = Convert.ToString(a);

            List<Client> relevantClients = new List<Client>();
            string searchString = "asd";
            foreach (Client item in db.Clients)
            {
                if (item.Name.Contains(searchString))
                {
                    relevantClients.Add(item);

                    ViewBag.test = item.ToString();
                }
            }

            return View(relevantClients);
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

        //
        // POST: /Client/Create

        [HttpPost]
        public ActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
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

        //
        // POST: /Client/Edit/5

        [HttpPost]
        public ActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
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