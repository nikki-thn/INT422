using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_app_project_template_v11.Controllers
{
    public class EmployeesController : Controller
    {
        private Manager m = new Manager();

        // GET: Employees
        public ActionResult Index()
        {
            //Fetch the enumerable employees from manager
            var e = m.EmployeeGetAll();

            //return data as a view
            return View(e); //short-hand: return View(m.CustomerGetAll());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            //Attempt to get the matching object
            var o = m.EmployeeGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(EmployeeAdd newEmployee)
        {
            //validate the input
            if (!ModelState.IsValid)
            {
                return View(newEmployee);
            }

            //Process the input
            var addedEmployee = m.EmployeeAdd(newEmployee);

            if (addedEmployee == null)
            {
                return View(newEmployee);
            }
            else
            {
                return RedirectToAction("details", new { id = addedEmployee.EmployeeId });
            }
        }   

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            //Attempt to fetch the object
            var e = m.EmployeeGetById(id.GetValueOrDefault());

            if (e == null)
            {
                return HttpNotFound(); 
            }
            else
            {
                var editForm = m.mapper.Map<EmployeeBase, EmployeeEditContactInfoForm>(e);
                return View(editForm);
            }
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, EmployeeEditContactInfo newItem)
        {
            //Validate the input
            if (!ModelState.IsValid)
            {
                //"Version1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.EmployeeId });
            }

            if (id.GetValueOrDefault() != newItem.EmployeeId)
            {
                //this appears to be data tampering, so redirect the user away
                return RedirectToAction("index");
            }

            //Attempt to do the update
            var editedItem = m.EmployeeEditContactInfo(newItem);

            if (editedItem == null)
            {
                //There was a problem updating the object
                //Our "version1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.EmployeeId });
            }
            else
            {
                //Show the details view, which will show the updated data
                return RedirectToAction("details", new { id = newItem.EmployeeId });
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            var e = m.EmployeeGetById(id.GetValueOrDefault());

            if (e == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                return View(e);
            }
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var e = m.EmployeeDelete(id.GetValueOrDefault());
            return RedirectToAction("index");
        }
    }
}
