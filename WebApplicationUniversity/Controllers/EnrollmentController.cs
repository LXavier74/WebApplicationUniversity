﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationUniversity.Models;

namespace WebApplicationUniversity.Controllers
{
    public class EnrollmentController : Controller
    {
        private WebTestContextEntities1 db = new WebTestContextEntities1();

        // GET: Enrollment
        public ActionResult Index()
        {
            var enrollment = db.Enrollment.Include(e => e.Course).Include(e => e.Student);
            return View(enrollment.ToList());
        }

        // GET: Enrollment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollment.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollment/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Course, "CourseID", "Title");
            ViewBag.StudentID = new SelectList(db.Student, "StudentID", "FullName");
            return View();
        }

        // POST: Enrollment/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentID,Grade,CourseID,StudentID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollment.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Course, "CourseID", "Title", enrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.Student, "StudentID", "FullName", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollment.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Course, "CourseID", "Title", enrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.Student, "StudentID", "FullName", enrollment.StudentID);
            return View(enrollment);
        }

        // POST: Enrollment/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentID,Grade,CourseID,StudentID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Course, "CourseID", "Title", enrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.Student, "StudentID", "FullName", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollment.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollment.Find(id);
            db.Enrollment.Remove(enrollment);
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
