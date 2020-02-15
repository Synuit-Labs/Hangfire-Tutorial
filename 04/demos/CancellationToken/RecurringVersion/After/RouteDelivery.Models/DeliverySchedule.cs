using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteDelivery.Models
{
    public class DeliverySchedule
    {
        public DeliverySchedule()
        {
            ScheduleDate = System.DateTime.Now.Date;
        }

        public int ID { get; set; }
        [Display(Name = "Optimization Job ID")]
        public int OptimizationRecurringJobID { get; set; }
        public DateTime ScheduleDate { get; set; }
        [Display(Name = "Schedule For")]
        public string ScheduleDateFormatted { get { return ScheduleDate.ToShortDateString(); } }
    }
}
