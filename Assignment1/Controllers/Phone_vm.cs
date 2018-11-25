using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INT422_Assignment1.Controllers
{
    public class PhoneBase
    {
        public PhoneBase()
        {

        }

        public int Id { get; set; }

        public String PhoneName { get; set; }

        public string Manufacturer { get; set; }

        public DateTime DateReleased { get; set; }

        public int MSRP { get; set; }

        public double ScreenSize { get; set; }
    }
}