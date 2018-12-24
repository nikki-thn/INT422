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


        // GET: Tracks/Details/5
        public ActionResult TrackWithDetails(int? id)
        {
            var t = m.TrackGetById(id.GetValueOrDefault());

            if (t == null)
            {
                return HttpNotFound();
            }
            else
            { 
                return View(t);
            }
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

        // GET: Tracks/Create
        public ActionResult Create()
        {
            var form = new TrackAddForm();
            //ViewData.Model = form;
            form.AlbumList = new SelectList(m.AlbumGetAll(), "AlbumId", "Title");
            form.MediaTypeList = new SelectList(m.MediaTypeGetAll(), "MediaTypeId", "Name");

            return View(form);
        }

        // POST: Tracks/Create
        [HttpPost]
        public ActionResult Create(TrackAdd newTrack)
        {
            //validate the input
            if (!ModelState.IsValid)
            {
                return View(newTrack);
            }

            //Process the input
            var addedTrack = m.TrackAdd(newTrack);

            if (addedTrack == null)
            {
                return View(newTrack);
            }
            else
            {
                return RedirectToAction("TrackWithDetails", new { id = addedTrack.TrackId });
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
