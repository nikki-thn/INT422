﻿using System;
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
                cfg.CreateMap<Models.Invoice, Controllers.InvoiceBase>();
                cfg.CreateMap<Models.Album, Controllers.AlbumBase>();
                cfg.CreateMap<Models.Artist, Controllers.ArtistBase>();
                cfg.CreateMap<Models.MediaType, Controllers.MediaTypeBase>();

                //Add a mapping from CustomerAdd to Customer
                //Handles incoming data from the browser user
                cfg.CreateMap<Controllers.CustomerAdd, Models.Customer>();
                cfg.CreateMap<Controllers.EmployeeAdd, Models.Employee>();

                //Mapping for base class and editting class
                cfg.CreateMap<Controllers.CustomerBase, Controllers.CustomerEditContactInfoForm>();
                cfg.CreateMap<Controllers.EmployeeBase, Controllers.EmployeeEditContactInfoForm>();


                //mapping for dataclass and associate entity
                cfg.CreateMap<Models.Invoice, Controllers.InvoiceWithDetails>();
                cfg.CreateMap<Models.Invoice, Controllers.InvoiceLineWithDetails>();
                cfg.CreateMap<Models.InvoiceLine, Controllers.InvoiceLineBase>();
                cfg.CreateMap<Models.InvoiceLine, Controllers.InvoiceLineWithDetails>();
                cfg.CreateMap<Models.Track, Controllers.TrackWithDetails>();
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

        public TrackWithDetails TrackGetById(int id)
        {
            //Attempt to fetch the object
            var o = ds.Tracks.Include("Album.Artist").Include("MediaType").SingleOrDefault(t => t.TrackId == id);

            //Return the result, or null if not found
            return (o == null) ? null : mapper.Map<Track, TrackWithDetails>(o);
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

        //Add new track
        public TrackBase TrackAdd(TrackAdd newItem)
        {
            //Attempt to add new Track
            var a = ds.Albums.Find(newItem.Album);
            var m = ds.MediaTypes.Find(newItem.MediaTypeId);
            var addedTrack = ds.Tracks.Add(mapper.Map<TrackAdd, Track>(newItem));

            if (a == null || m == null)
            {
                return null;
            }
            else
            { 
                addedTrack.Album = a;
                addedTrack.MediaType = m;

                ds.SaveChanges();

                //If successful, return the added item, mapped to a view model object
                return (addedTrack == null) ? null : mapper.Map<Track, TrackBase>(addedTrack);
            }
        }

        //Get all Invoices
        public IEnumerable<InvoiceBase> InvoiceGetAll()
        {
            return mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceBase>>(ds.Invoices.OrderBy(t => t.InvoiceDate));
        }

        public InvoiceBase InvoiceGetById(int id)
        {
            //Attempt to fetch the object
            var i = ds.Invoices.Find(id);

            //Return the result, or null if not found
            return (i  == null) ? null : mapper.Map<Invoice, InvoiceBase>(i);
        }

        public InvoiceLineWithDetails InvoiceGetByIdWithDetails(int id)
        {
            //Attempt to fetch the object
            var o = ds.Invoices.Include("InvoiceLines.Track.MediaType").Include("InvoiceLines.Track.Album.Artist").Include("Customer.Employee").SingleOrDefault(i => i.InvoiceId == id);

            //Return the result, or null if not found
            return (o == null) ? null : mapper.Map<Invoice, InvoiceLineWithDetails>(o);
        }

        //Get all Albums
        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBase>>(ds.Albums);
        }

        //Get Album by Id
        public AlbumBase AlbumGetById(int id)
        {
            //Attempt to fetch the object
            var i = ds.Albums.Find(id);

            //Return the result, or null if not found
            return (i == null) ? null : mapper.Map < Album, AlbumBase>(i);
        }

        //Get all Artists
        public IEnumerable<ArtistBase> ArtistGetAll()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBase>>(ds.Artists);
        }

        //Get all MediaType
        public IEnumerable<MediaTypeBase> MediaTypeGetAll()
        {
            return mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeBase>>(ds.MediaTypes);
        }

        //Get MediaType by Id
        public MediaTypeBase MediaTypeGetById(int id)
        {
            //Attempt to fetch the object
            var i = ds.MediaTypes.Find(id);

            //Return the result, or null if not found
            return (i == null) ? null : mapper.Map<MediaType, MediaTypeBase>(i);
        }

    }
}