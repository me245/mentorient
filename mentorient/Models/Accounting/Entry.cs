using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace mentorient.Models.Accounting
{
    public class Entry
    {

        public int Id { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime DatePaid { get; set; }
        public string Description { get; set; }
        public decimal AmountDue { get; set; }
        public decimal AmountPaid { get; set; }
        public string Comments { get; set; }
        public int TenantId { get; set; }
    }
}