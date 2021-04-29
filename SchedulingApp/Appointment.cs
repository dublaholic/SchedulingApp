using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingApp
{
    public class Appointment
    {
        public int appointmentId { get; set; }
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


        public Appointment()
        {

        }
        public Appointment(int _appointmentId, string _customerName, string _userName, string _title, string _description, string _location, string _contact, string _type, string _URL, DateTime _start, DateTime _end)
        {
            appointmentId = _appointmentId;
            customerName = _customerName;
            userName = _userName;
            title = _title;
            description = _description;
            location = _location;
            contact = _contact;
            type = _type;
            URL = _URL;
            start = _start;
            end = _end;
        }
        public Appointment(DateTime _start, string _type)
        {
            start = _start;
            type = _type;
        }

    }
}
