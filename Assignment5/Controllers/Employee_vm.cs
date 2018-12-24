using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_app_project_template_v11.Controllers
{
    public class EmployeeBase : EmployeeAdd
    {
        [Key]
        public int EmployeeId { get; set; }
    }

    public class EmployeeAdd
    {
        public EmployeeAdd()
        {
            LastName = "";
            FirstName = "";
            Title = "";
            BirthDate = DateTime.Now.AddYears(-25);
            HireDate = DateTime.Now.AddYears(-25);
            Address = "";
            City = "";
            State = "";
            Country = "";
            PostalCode = "";
            Phone = "";
            Fax = "";
            Email = "";
        }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }

    public class EmployeeEditContactInfoForm
    {
        [Key]
        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NumOfWeeksVacation { get; set; }
    }

    public class EmployeeEditContactInfo
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NumOfWeeksVacation { get; set; }
    }
}