using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_app_project_template_v11.Controllers
{
    public class CustomersController : Controller
    {
        //Reference to a manager object
        private Manager m = new Manager();

        // GET: Customers
        public ActionResult Index()
        {
            //Fetch the collection
            var c = m.CustomerGetAll();
        
            //Pass the collection to the view
            return View(c); //Alternatively, return View(m.CustomerGetAll());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            //Attempt to get the matching object
            var o = m.CustomerGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
            //return View();
        }

        // GET: Customers/Create
        public ActionResult Create()
        {

            //At your option, create and send an object to the view
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        public ActionResult Create(CustomerAdd newItem)
        {
            //Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            //process the input
            var addedItem = m.CustomerAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.CustomerId });
            }
  
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            //attempt to fetch the matching object
            var o = m.CustomerGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Create and configure an "Edit form"

                // Notice that o is a CustomerBase object
                // We must map it to a CustomerEditContactInfoForm object
                // Notice that we can use AutoMapper anywhere,
                // and not just in the Manager class!
                var editForm = m.mapper.Map<CustomerBase, CustomerEditContactInfoForm>(o);
                return View(editForm);
            }
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, CustomerEditContactInfo newItem)
        {
            //Validate the input
            if (!ModelState.IsValid)
            {
                //"Version1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.CustomerId });
            }

            if (id.GetValueOrDefault() != newItem.CustomerId)
            {
                //this appears to be data tampering, so redirect the user away
                return RedirectToAction("index");
            }

            //Attempt to do the update
            var editedItem = m.CustomerEditContactInfo(newItem);

            if (editedItem == null)
            {
                //There was a problem updating the object
                //Our "version1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.CustomerId });
            }
            else
            {
                //Show the details view, which will show the updated data
                return RedirectToAction("details", new { id = newItem.CustomerId });
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            var itemToDelete = m.CustomerGetById(id.GetValueOrDefault());

            if (itemToDelete == null)
            {
                //Dont leak info about the delete attempt
                //simply redirect
                return RedirectToAction("index");
            }
            else
            {
                return View(itemToDelete);
            }
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var result = m.CustomerDelete(id.GetValueOrDefault());

            // "result" will be true or false
            // We probably won't do much with the result, because
            // we don't want to leak info about the delete attempt

            // In the end, we should just redirect to the list view
            return RedirectToAction("index");
        }
    }
}
