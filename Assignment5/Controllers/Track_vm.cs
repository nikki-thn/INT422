using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_app_project_template_v11.Controllers
{
    public class TrackBase : TrackAdd
    {
        [Key]
        public int TrackId { get; set; }
    }

    public class TrackWithDetails : TrackBase
    {
        public string AlbumArtistName { get; set; }
        public string AlbumTitle { get; set; }
        public MediaTypeBase MediaType { get; set; }
    }

    public class TrackAdd
    {
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string GenreId { get; set; }
        public int? Bytes { get; set; }

        [Range(1, Int32.MaxValue)]
        public int AlbumId { get; set; }
        [Range(1, Int32.MaxValue)]
        public int MediaTypeId { get; set; }
    }

    public class TrackAddForm 
    {
        public string Name { get; set; }
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public decimal UnitPrice { get; set; }

        public SelectList AlbumList { get; set; }
        public SelectList MediaTypeList { get; set; }
    }

}