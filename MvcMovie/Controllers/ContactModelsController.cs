using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class ContactModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContactModels
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUserid = User.Identity.GetUserId();
                //var contactModels = db.ContactModels.Include(c => c.ApplicationUser);
                var contactModels = db.ContactModels.Where(c => c.ApplicationUserId == currentUserid);
                return View(contactModels.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: ContactModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactModel contactModel = db.ContactModels.Find(id);
            if (contactModel == null)
            {
                return HttpNotFound();
            }
            return View(contactModel);
        }

        // GET: ContactModels/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.ApplicationUserId = new SelectList(User.Identity.GetUserId(), "Id", "Email");
            }
            
            
            return View();
        }

        // POST: ContactModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactModelId,ApplicationUserId,Console,Game,Age,FirstName,LastName,State,Phone,Email,Password,Password2,Check18,AgreedToTerms")] ContactModel contactModel)
        {
            //if (contactModel.Age == null)
            //{
            //    contactModel.Age = 0;
            //}
            if (ModelState.IsValid)
            {
                db.ContactModels.Add(contactModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(User.Identity.GetUserId(), "Id", "Email", contactModel.ApplicationUserId);
            return View(contactModel);
        }

        // GET: ContactModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactModel contactModel = db.ContactModels.Find(id);
            if (contactModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(User.Identity.GetUserId(), "Id", "Email", contactModel.ApplicationUserId);
            return View(contactModel);
        }

        // POST: ContactModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactModelId,ApplicationUserId,Console,Game,Age,FirstName,LastName,State,Phone,Email,Password,Password2,Check18,AgreedToTerms")] ContactModel contactModel)
        {
            //if (contactModel.Age == null)
            //{
            //    contactModel.Age = 0;
            //}
            if (ModelState.IsValid)
            {
                db.Entry(contactModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(User.Identity.GetUserId(), "Id", "Email", contactModel.ApplicationUserId);
            return View(contactModel);
        }

        // GET: ContactModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactModel contactModel = db.ContactModels.Find(id);
            if (contactModel == null)
            {
                return HttpNotFound();
            }
            return View(contactModel);
        }

        // POST: ContactModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactModel contactModel = db.ContactModels.Find(id);
            db.ContactModels.Remove(contactModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
