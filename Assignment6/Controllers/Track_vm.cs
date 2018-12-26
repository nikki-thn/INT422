using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_app_project_template_v11.Controllers
{
    public class TrackBase
    {
        [Key]
        public int TrackId { get; set; }

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

        // Composed read-only property
        [Display(Name = "Track Details")]
        public string NameFull
        {
            get
            {
                var ms = Math.Round((((double)Milliseconds / 1000) / 60), 1);
                var composer = string.IsNullOrEmpty(Composer) ? "" : ", composer " + Composer;
                var trackLength = (ms > 0) ? ", " + ms.ToString() + " minutes" : "";
                var unitPrice = (UnitPrice > 0) ? ", $ " + UnitPrice.ToString() : "";

                return string.Format("{0}{1}{2}{3}", Name, composer, trackLength, unitPrice);
            }
        }

        // Composed read-only property

        public string NameShort
        {
            get
            {
                var ms = Math.Round((((double)Milliseconds / 1000) / 60), 1);
                var unitPrice = (UnitPrice > 0) ? " $ " + UnitPrice.ToString() : "";
                return string.Format("{0} - {1}", Name, unitPrice);
            }
        }
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

        // Composed read-only property
        [Display(Name = "Track Details")]
        public string NameFull
        {
            get
            {
                var ms = Math.Round((((double)Milliseconds / 1000) / 60), 1);
                var composer = string.IsNullOrEmpty(Composer) ? "" : ", composer " + Composer;
                var trackLength = (ms > 0) ? ", " + ms.ToString() + " minutes" : "";
                var unitPrice = (UnitPrice > 0) ? ", $ " + UnitPrice.ToString() : "";

                return string.Format("{0}{1}{2}{3}", Name, composer, trackLength, unitPrice);
            }
        }

        // Composed read-only property

        public string NameShort
        {
            get
            {
                var ms = Math.Round((((double)Milliseconds / 1000) / 60), 1);
                var unitPrice = (UnitPrice > 0) ? " $ " + UnitPrice.ToString() : "";
                return string.Format("{0} - {1}", Name, unitPrice);
            }
        }
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