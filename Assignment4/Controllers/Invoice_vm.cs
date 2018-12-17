using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Web_app_project_template_v11.Models;

namespace Web_app_project_template_v11.Controllers
{
    public class InvoiceBase
    {   
        [Key]
        public int InvoiceId { get; set; }

        public int CustomerId { get; set; }

        public DateTime InvoiceDate { get; set; }

        [StringLength(70)]
        public string BillingAddress { get; set; }

        [StringLength(40)]
        public string BillingCity { get; set; }

        [StringLength(40)]
        public string BillingState { get; set; }

        [StringLength(40)]
        public string BillingCountry { get; set; }

        [StringLength(10)]
        public string BillingPostalCode { get; set; }

        public decimal Total { get; set; }
    }

    public class InvoiceWithDetails : InvoiceBase
    {
        //public InvoiceWithDetails()
        //{
        //    Customer = new Customer();
        //}

        public CustomerBase Customer { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerEmployeeFirstName { get; set; }
        public string CustomerEmployeeLastName { get; set; }
    }
}