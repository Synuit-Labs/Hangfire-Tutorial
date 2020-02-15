using RouteDelivery.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteDelivery.Models
{

    public class OptimizationRequest : IEntity
    {
        public enum RequestStatus
        {
            Scheduled,
            Processing,
            Complete,
            Failed,
            Removed,
            Stopped
        }

        public enum RecurringScheduleType
        {
            Daily,
            Hourly,
            Minutely,
            Monthly,
            Weekly,
            Yearly
        }

        public OptimizationRequest()
        {
            RequestDate = System.DateTime.Now;
            Status = RequestStatus.Scheduled;
        }

        [Display(Name = "Request Date")]
        public DateTime RequestDate { get; set; }
        [Display(Name = "Status")]
        public string StatusText { get { return Status.ToString(); } }
        [Display(Name = "Recurring Schedule")]
        public RecurringScheduleType RecurringSchedule { get; set; }

        public RequestStatus Status { get; set; }
        public int ID { get; set; }
    }
}
