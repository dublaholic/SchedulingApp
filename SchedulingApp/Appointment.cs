using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingApp
{
    public class Appointment
    {
        public int  appointmentId {get; set;} 
        public string customerName { get; set; }
        public string userName { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string contact { get; set; }
        public string type { get; set; }
        public string URL { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }

    }
}
