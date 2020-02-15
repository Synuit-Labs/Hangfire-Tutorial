using RouteDelivery.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteDelivery.Models
{
    public enum RequestStatus
    {
        Waiting,
        Processing,
        Complete,
        Failed
    }
    public class OptimizationRequest : IEntity
    {

        public OptimizationRequest()
        {
            RequestDate = System.DateTime.Now;
            Status = RequestStatus.Waiting;
            ScheduleDate = System.DateTime.Now.Date;
            OptimizeAfterMinuntes = 0;
        }

        [Display(Name = "Request Date")]
        public DateTime RequestDate { get; set; }
        [Display(Name = "Schedule Date")]
        public DateTime ScheduleDate { get; set; }
        [Display(Name = "Status")]
        public string StatusText { get { return Status.ToString(); } }
        [Display(Name = "Optimize After (Mins)")]
        public int OptimizeAfterMinuntes { get; set; }
        [Display(Name = "Optimize Date Time")]
        public DateTime OptimizeDateTime { get { return RequestDate.AddMinutes(OptimizeAfterMinuntes); } }

        public RequestStatus Status { get; set; }
        public int ID { get; set; }
    }
}
