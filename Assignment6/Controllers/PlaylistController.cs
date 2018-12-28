using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_app_project_template_v11.Controllers
{
    public class PlaylistController : Controller
    {
        private Manager m = new Manager();

        // GET: Playlist
        public ActionResult Index()
        {
            var p = m.PlaylistGetAllWithTracks();
            return View(p);
        }

        // GET: Playlist/Details/5
        public ActionResult Details(int? id)
        {
            var p = m.PlaylistGetByIdWithTracks(id.GetValueOrDefault());

            if (p == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(p);
            }
        }

        // GET: Playlist/Edit/5
        public ActionResult Edit(int? id)
        {
            // Attempt to fetch the matching object
            var o = m.PlaylistGetByIdWithTracks(id.GetValueOrDefault());

            if (o == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Create a form, based on the fetched matching object
                var form = m.mapper.Map<PlaylistEditTracksForm>(o);

                // For the multi select list, configure the "selected" items
                // Notice the use of the Select() method, 
                // which allows us to select/return/use only some properties from the source
                var selectedValues = o.Tracks.Select(t => t.TrackId);

                // For clarity, use the named parameter feature of C#
                form.TrackList = new MultiSelectList
                    (items: m.TrackGetAll(),
                    dataValueField: "TrackId",
                    dataTextField: "Name",
                    selectedValues: selectedValues);

                form.TracksOnPlaylist = o.Tracks.OrderBy(t => t.TrackId);

                return View(form);
            }
        }

        // POST: Playlist/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, PlaylistEditTrackDetails newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.PlaylistId });
            }

            if (id.GetValueOrDefault() != newItem.PlaylistId)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("index");
            }

            // Attempt to do the update
            var editedItem = m.PlaylistEditTracks(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("Edit");
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("Details", new { id = newItem.PlaylistId });
            }
        }

        // GET: Playlist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Playlist/Create
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

        // GET: Playlist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Playlist/Delete/5
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
