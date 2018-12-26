using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_app_project_template_v11.Controllers
{
    public class MediaTypeBase
    {
        [Key]
        public int MediaTypeId { get; set; }
        public string Name { get; set; }
    }
}