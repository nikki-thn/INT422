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

                //Add a mapping from CustomerAdd to Customer
                //Handles incoming data from the browser user
                cfg.CreateMap<Controllers.CustomerAdd, Models.Customer>();
                cfg.CreateMap<Controllers.EmployeeAdd, Models.Employee>();
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




    }
}