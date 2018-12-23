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
        public InvoiceWithDetails()
        {
            InvoiceLines = new List<InvoiceLineBase>();
        }

        public CustomerBase Customer { get; set; }
        public ICollection<InvoiceLineBase> InvoiceLines { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerEmployeeFirstName { get; set; }
        public string CustomerEmployeeLastName { get; set; }
    }

    public class InvoiceLineBase : InvoiceBase
    {
        public int InvoiceLineId { get; set; }
        //public int InvoiceId { get; set; }
        public int TrackId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public TrackBase Track { get; set; }
    }

    public class InvoiceLineWithDetails : InvoiceLineBase
    {
        public InvoiceLineWithDetails()
        {
            InvoiceLines = new List<InvoiceLineBase>();
        }
        public ICollection<InvoiceLineBase> InvoiceLines { get; set; }
        public CustomerBase Customer { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerEmployeeFirstName { get; set; }
        public string CustomerEmployeeLastName { get; set; }
    }
}