using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_app_project_template_v11.Controllers
{
    public class PlaylistBase
    {
        [Key]
        [Display (Name = "Playlist Id")]
        public int PlaylistId { get; set; }

        [Required, StringLength(120)]
        [Display(Name = "Playlist Name")]
        public string Name{ get; set; }

        [Display(Name = "Number of tracks on the playlist")]
        public int TracksCount { get; set; }
    }

    public class PlaylistWithTracksDetails : PlaylistBase
    {
        public PlaylistWithTracksDetails()
        {
            Tracks = new List<TrackBase>();
        }

        public ICollection<TrackBase> Tracks { get; set; }
    }

    public class PlaylistEditTrackDetails
    {
        public PlaylistEditTrackDetails()
        {
            TrackIds = new List<int>();
        }

        [Key]
        [DisplayName("Playlist Id")]
        public int PlaylistId { get; set; }

        public IEnumerable<int> TrackIds { get; set; }
    }

    public class PlaylistEditTracksForm
    {
        [Key]
        [DisplayName("Playlist Id")]
        public int PlaylistId { get; set; }

        [DisplayName("Playlist Name")]
        [Required, StringLength(120)]
        public string Name { get; set; }
        public int TracksCount { get; set; }
        public MultiSelectList TrackList { get; set; }
        public IEnumerable<TrackBase> TracksOnPlaylist { get; set; }
    }
}