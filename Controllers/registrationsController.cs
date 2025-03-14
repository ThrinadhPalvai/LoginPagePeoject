﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LoginPagePeoject;

namespace LoginPagePeoject.Controllers
{
    public class registrationsController : Controller
    {
        private proteckhydbEntities1 db = new proteckhydbEntities1();

        // GET: registrations
        public ActionResult Index()
        {
            var registrations = db.registrations.Include(r => r.DesignTb);
            return View(registrations.ToList());
        }

        // GET: registrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registration registration = db.registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: registrations/Create
        public ActionResult Create()
        {
            ViewBag.Designation = new SelectList(db.DesignTbs, "Id", "Designation");
            return View();
        }

        // POST: registrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(registration reg, loginpage logPage)
        {
            {
                db.registrations.Add(reg);
                db.loginpages.Add(logPage);
                logPage.Id = reg.Id;
                logPage.Username = reg.Username;
                logPage.Password = reg.Password;
                logPage.Designation = reg.Designation.ToString();

                db.SaveChanges();
                ViewBag.Designation = new SelectList(db.DesignTbs, "Id", "Designation", reg.Designation);
                return RedirectToAction("Index");
            }


            //ViewBag.Designation = new SelectList(db.DesignTbs, "Id", "Designation", reg.Designation);
            //return View(reg);
        }

        // GET: registrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registration registration = db.registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.Designation = new SelectList(db.DesignTbs, "Id", "Designation", registration.Designation);
            return View(registration);
        }

        // POST: registrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Designation = new SelectList(db.DesignTbs, "Id", "Designation", registration.Designation);
            return View(registration);
        }

        // GET: registrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registration registration = db.registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            registration registration = db.registrations.Find(id);
            db.registrations.Remove(registration);
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
