using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mentorient.Models
{
    public class RepeatingPayment : IDataModel<int>
    {
        public int Id { get; set; }
        public DateTime RepeatedPaymentStartDate { get; set; }
        public DateTime? RepeatedPaymentEndDate { get; set; }
        public int DayToGenerateNewPayment { get; set; }
        public int DayOfMonthDue { get; set; }
        public decimal AmountDue { get; set; }
    }
}
