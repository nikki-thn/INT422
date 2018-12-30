using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment8.Models;
using System.Security.Claims;

namespace Assignment8.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();
                cfg.CreateMap<Models.Genre, Controllers.GenreBase>();
                // Object mapper definitions

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
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

        //========================GENRES=================================
        public IEnumerable<GenreBase> GenreGetAll()
        {
            return Mapper.Map<IEnumerable<Models.Genre>, IEnumerable<Controllers.GenreBase>>(ds.Genres);
        }

        public GenreBase GenreGetById(int id)
        {
            //Attempt to fetch the object
            var o = ds.Genres.Find(id);
            //return the results, or null if not found
            return (o == null) ? null : Mapper.Map<Models.Genre, Controllers.GenreBase>(o);
        }

        //==========================ALBUM===============================




        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Role claims

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here

                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Staff" });
                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Genres
            if (ds.Genres.Count() == 0)
            {
                ds.Genres.Add(new Genre { Name = "Jazz" });
                ds.Genres.Add(new Genre { Name = "Classical" });
                ds.Genres.Add(new Genre { Name = "Electric" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "Classic Rock" });
                ds.Genres.Add(new Genre { Name = "RnB" });
                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Artists 
            if (ds.Artists.Count() == 0)
            {
                ds.Artists.Add(new Models.Artist
                {
                    Name = "Queen",
                    BirthName = "Queen",
                    BirthOrStartDate = DateTime.Parse("1970, 01, 01"),
                    Executive = "exec@example.com",
                    Genre = "Rock",
                    UrlArtist = "https://en.wikipedia.org/wiki/Queen_(band)#/media/File:Queen_%E2%80%93_montagem_%E2%80%93_new.png"
                });

                ds.Artists.Add(new Models.Artist
                {
                    Name = "Ella Fitzgerald",
                    BirthName = "Ella Jane Fitzgerald",
                    BirthOrStartDate = DateTime.Parse("1934, 11, 21"),
                    Executive = "exec@example.com",
                    Genre = "Jazz",
                    UrlArtist = "https://en.wikipedia.org/wiki/Ella_Fitzgerald#/media/File:Ella_Fitzgerald_(Gottlieb_02871).jpg"
                });

                ds.Artists.Add(new Models.Artist
                {
                    Name = "Louis Armstrong",
                    BirthName = "Louis Daniel Armstrong",
                    BirthOrStartDate = DateTime.Parse("1920, 01, 01"),
                    Executive = "exec@example.com",
                    Genre = "Jazz",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/0/0e/Louis_Armstrong_restored.jpg"
                });

                ds.Artists.Add(new Models.Artist
                {
                    Name = "Evgeny Kissin",
                    BirthName = "Evgeny Igorevitch Kissin",
                    BirthOrStartDate = DateTime.Parse("1987, 01, 01"),
                    Executive = "exec@example.com",
                    Genre = "Classical",
                    UrlArtist = "https://en.wikipedia.org/wiki/Evgeny_Kissin#/media/File:Evgeny_Kissin_TA_2011.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Albums  #####
            var a1 = ds.Artists.SingleOrDefault(a => a.Name == "Queen");
            var a2 = ds.Artists.SingleOrDefault(a => a.Name == "Ella Fitzgerald");
            var a3 = ds.Artists.SingleOrDefault(a => a.Name == "Evgeny Kissin");
            var a4 = ds.Artists.SingleOrDefault(a => a.Name == "Louis Armstrong");

            if (ds.Albums.Count() == 0)
            {
                ds.Albums.Add(new Models.Album
                {
                    Name = "A Night at the Opera",
                    Coordinator = "exec@example.com",
                    Genre = "Rock",
                    ReleaseDate = DateTime.Parse("1994-10-21"),
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/4/4d/Queen_A_Night_At_The_Opera.png",
                    Artists = new List<Artist> { a1 },
                    Tracks = new List<Track>()
                });

                ds.Albums.Add(new Models.Album
                {
                    Name = "Ella Fitzgerald Sings the Cole Porter Song Book",
                    Coordinator = "coor@example.com",
                    Genre = "Jazz",
                    ReleaseDate = DateTime.Parse("1956-02-09"),
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/3/30/Ellaportersongbook.jpg",
                    Artists = new List<Artist> { a2 },
                    Tracks = new List<Track>()
                });

                ds.Albums.Add(new Models.Album
                {
                    Name = "Ella and Louis",
                    Coordinator = "coor@example.com",
                    Genre = "Jazz",
                    ReleaseDate = DateTime.Parse("1956-08-16"),
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/0/0d/Ellaandlouis.jpg",
                    Artists = new List<Artist> { a2, a4 },
                    Tracks = new List<Track>()
                });

                ds.Albums.Add(new Models.Album
                {
                    Name = "Beethoven: Piano Concerto No. 5",
                    Coordinator = "coor@example.com",
                    Genre = "Classical",
                    ReleaseDate = DateTime.Parse("2009-01-01"),
                    UrlAlbum = "https://cps-static.rovicorp.com/3/JPG_1080/MI0002/843/MI0002843275.jpg?partner=allrovi.com",
                    Artists = new List<Artist> { a3 },
                    Tracks = new List<Track>()
                });
                ds.SaveChanges();
                done = true;
            }//albums

            // ############################################################
            // Tracks
            var t1 = ds.Albums.SingleOrDefault(a => a.Name == "Beethoven: Piano Concerto No. 5");
            var t2 = ds.Albums.SingleOrDefault(a => a.Name == "Ella and Louis");

            if (ds.Tracks.Count() == 0)
            {
                ds.Tracks.Add(new Track
                {
                    Name = "Dream a Little Dream Of Me",
                    Clerk = "clerk@example.com",
                    Composers = "Fabian Andre / Gus Kahn / Wilbur Schwandt",
                    Genre = "Jazz",
                    Albums = new List<Album> { t2 }
                });

                ds.Tracks.Add(new Track
                {
                    Name = " The Nearness of You",
                    Clerk = "coor@example.com",
                    Composers = "Hoagy Carmichael / Ned Washington",
                    Genre = "Jazz",
                    Albums = new List<Album> { t2 }
                });

                ds.Tracks.Add(new Track
                {
                    Name = "Piano Concerto No.5",
                    Clerk = "exec@example.com",
                    Composers = "Ludwig Beethoven",
                    Genre = "Classical",
                    Albums = new List<Album> { t1 }
                });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // How to use...

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}
