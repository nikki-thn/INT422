using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_app_project_template_v11.Controllers
{
    public class TracksController : Controller
    {
        //Reference to a manager object
        private Manager m = new Manager();

        // GET: All Tracks
        public ActionResult Index()
        {
            var t = m.TrackGetAll();

            return View(t);
        }

        // GET: All Tracks
        public ActionResult PopTracksIndex()
        {
            var t = m.TrackGetAllPop();

            return View(t);
        }

        // GET: All Tracks
        public ActionResult DeepPurpleTracksIndex()
        {
            var t = m.TrackGetAllDeepPurple();

            return View(t);
        }

        // GET: All Tracks
        public ActionResult Top100LongestTracksIndex()
        {
            var t = m.TrackGetAllTop100Longest();

            return View(t);
        }

        // GET: Tracks/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tracks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tracks/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tracks/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tracks/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tracks/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tracks/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
