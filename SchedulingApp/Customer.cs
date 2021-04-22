using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingApp
{
    public class Customer
    {
        public int customerId { get; set; }
        public string customerName { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
        public string phone { get; set; }
        public bool active { get; set; }
        
        public Customer()
        {

        }

        public Customer(int CustomerId, string CustomerName, string Address, string Address2, string City, string Country, string PostalCode, string Phone, bool Active)
        {

        }

        public Customer(string CustomerName, string Address, string Address2, string PostalCode, string Phone, bool Active)
        {
            
            customerName = CustomerName;
            address = Address;
            address2 = Address2;
            postalCode = PostalCode;
            phone = Phone;
            active = Active;
        }
        
        
    }

}
