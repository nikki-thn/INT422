using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Web_app_project_template_v11.Models;

namespace Web_app_project_template_v11.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                //Add a mapping from Customer to CustomerBase
                cfg.CreateMap<Models.Customer, Controllers.CustomerBase>();
                cfg.CreateMap<Models.Employee, Controllers.EmployeeBase>();
                cfg.CreateMap<Models.Track, Controllers.TrackBase>();

                //Add a mapping from CustomerAdd to Customer
                //Handles incoming data from the browser user
                cfg.CreateMap<Controllers.CustomerAdd, Models.Customer>();
                cfg.CreateMap<Controllers.EmployeeAdd, Models.Employee>();

                cfg.CreateMap<Controllers.CustomerBase, Controllers.CustomerEditContactInfoForm>();
                cfg.CreateMap<Controllers.EmployeeBase, Controllers.EmployeeEditContactInfoForm>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        //Use AutoMapper to map objects, source to target
        public IEnumerable<CustomerBase> CustomerGetAll()
        {
            return mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerBase>>(ds.Customers);
        }

        //Create a "get one" method
        public CustomerBase CustomerGetById(int id)
        {
            //Attempt to fetch the object
            var obj = ds.Customers.Find(id);

            //return the results, or null if not found
            return (obj == null) ? null : mapper.Map<Customer, CustomerBase>(obj);
        }

        public CustomerBase CustomerAdd(CustomerAdd newItem)
        {
            //Attempt to add the new item
            //Notice how we map the incoming data to the design model object
            var addedItem = ds.Customers.Add(mapper.Map<CustomerAdd, Customer>(newItem));
            ds.SaveChanges();

            //If successful, return the added item, mapped to a view model object
            return (addedItem == null) ? null : mapper.Map<Customer, CustomerBase>(addedItem);
        }

        public CustomerBase CustomerEditContactInfo(CustomerEditContactInfo newItem)
        {
            //Attempt to fetch the object
            var o = ds.Customers.Find(newItem.CustomerId);

            if (o == null)
            {
                //problem - item not found
                return null;
            }
            else
            {
                //update the object with the incoming values
                ds.Entry(o).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                //prepare and return the object
                return mapper.Map<Customer, CustomerBase>(o);
            }
        }

        public bool CustomerDelete(int id)
        {
            // Attempt to fetch the object to be deleted
            var itemToDelete = ds.Customers.Find(id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
                // Remove the object
                ds.Customers.Remove(itemToDelete);
                ds.SaveChanges();
                return true;
            }
        }

        //Use AutoMapper to map objects, source to target
        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeBase>>(ds.Employees);
        }

        public EmployeeBase EmployeeGetById(int id)
        {
            //Attempt to fetch the object
            var o = ds.Employees.Find(id);

            //Return the result, or null if not found
            return (o == null) ? null : mapper.Map<Employee, EmployeeBase>(o);
        }

        public EmployeeBase EmployeeAdd(EmployeeAdd newItem)
        {
            //Attempt to add new employee
            var addedEmployee = ds.Employees.Add(mapper.Map<EmployeeAdd,Employee>(newItem));
            ds.SaveChanges();

            //If successful, return the added item, mapped to a view model object
            return (addedEmployee == null) ? null : mapper.Map<Employee, EmployeeBase>(addedEmployee);
        }

        public EmployeeBase EmployeeEditContactInfo(EmployeeEditContactInfo newEmployee)
        {
            //fetch data to display for GET
            var e = ds.Employees.Find(newEmployee.EmployeeId);

            //Check if fetching was successful
            if (e == null)
            {
                return null;
            }
            else
            {
                //Replace the current data with new data
                ds.Entry(e).CurrentValues.SetValues(newEmployee);
                ds.SaveChanges();

                //return the employeeBase using mapper
                return mapper.Map<Employee, EmployeeBase>(e);
            }
        }

        public bool EmployeeDelete(int id)
        {
            // Attempt to fetch the object to be deleted
            var itemToDelete = ds.Employees.Find(id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
                // Remove the object
                ds.Employees.Remove(itemToDelete);
                ds.SaveChanges();
                return true;
            }
        }

        //Get all Tracks
        public IEnumerable<TrackBase> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(ds.Tracks);
        }

        //GenreId is 9, sorted ascending by track Name
        public IEnumerable<TrackBase> TrackGetAllPop()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(ds.Tracks.Where(t => t.GenreId == 19));
        }

        //Composer contains “Jon Lord”, sorted ascending by TrackId
        public IEnumerable<TrackBase> TrackGetAllDeepPurple()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(ds.Tracks.Where(t => t.Composer.Contains("Jon Lord")).OrderBy(t => t.TrackId));
        }

        //Sorted descending by Milliseconds; use the Take() method to limit the results to 100 items only
        public IEnumerable<TrackBase> TrackGetAllTop100Longest()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(ds.Tracks.OrderByDescending(t => t.Milliseconds).Take(100));
        }

    }
}