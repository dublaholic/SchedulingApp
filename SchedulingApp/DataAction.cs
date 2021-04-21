using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;


namespace SchedulingApp
{
    class DataAction
    {
        private const string server = "wgudb.ucertify.com";
        private const string database = "U06Mu8";
        private const string uid = "U06Mu8";
        private const string password = "53688806600";
        private const string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        private MySqlConnection connection = new MySqlConnection(connectionString);
        

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false; 
        }
        
        public int VerifyUser(string username, string password)
        {
            int verificationResult = -1;
            string queryReturn;
            string userQuery = "SELECT EXISTS(SELECT userId FROM user WHERE userName = @username and password = @password)";
            if(this.OpenConnection() == true)
            {
                MySqlCommand userCMD = new MySqlCommand(userQuery, connection);
                userCMD.Parameters.AddWithValue("@username", username);
                userCMD.Parameters.AddWithValue("@password", password);
                queryReturn = userCMD.ExecuteScalar().ToString();
                
                if(queryReturn == "1")
                {
                    string userIdQuery = "SELECT userId FROM user WHERE userName = @username and password = @password";
                    MySqlCommand userIdCMD = new MySqlCommand(userIdQuery, connection);
                    userIdCMD.Parameters.AddWithValue("@username", username);
                    userIdCMD.Parameters.AddWithValue("@password", password);
                    verificationResult = int.Parse(userIdCMD.ExecuteScalar().ToString());
                }
                else
                {
                    verificationResult = -1; 
                }
                this.CloseConnection();
                return verificationResult; 
            }
            else
            {
                return verificationResult;
            }
        }

        public List<Appointment> GetAppointments(int userId, DateTime startDate, DateTime endDate)
        {
            List<Appointment> appointment = new List<Appointment>();
            string appointmentQuery = "SELECT appointment.appointmentId, customer.customerName, user.userName, appointment.title, appointment.description, appointment.location, appointment.contact, appointment.type, appointment.url, appointment.start, appointment.end" +
                " FROM appointment JOIN user on user.userId = appointment.userId JOIN customer on customer.customerId = appointment.customerId" +
                " WHERE appointment.userId = @userId and appointment.start >= @startDate and appointment.end <= @endDate";
            if (this.OpenConnection() == true)
            {
                MySqlCommand appointmentCMD = new MySqlCommand(appointmentQuery, connection);
                appointmentCMD.Parameters.AddWithValue("@userId", userId);
                appointmentCMD.Parameters.AddWithValue("@startDate", startDate);
                appointmentCMD.Parameters.AddWithValue("@endDate", endDate);

                using (MySqlDataReader rdr = appointmentCMD.ExecuteReader())
                {
                    while(rdr.Read())
                    {
                        appointment.Add(new Appointment()
                        {
                            appointmentId = int.Parse(rdr["appointmentId"].ToString()),
                            customerName = rdr["customerName"].ToString(),
                            userName = rdr["userName"].ToString(),
                            title = rdr["title"].ToString(),
                            description = rdr["description"].ToString(),
                            location = rdr["location"].ToString(),
                            contact = rdr["contact"].ToString(),
                            type = rdr["type"].ToString(),
                            URL = rdr["url"].ToString(),
                            start = DateTime.Parse(rdr["start"].ToString()),
                            end = DateTime.Parse(rdr["start"].ToString())

                        }) ; 
                    }
                }
                this.CloseConnection();
                return appointment; 
            }
            else
            {
                return appointment;
            }
        }
        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            string customerQuery = "SELECT customer.customerId, customer.customerName, address.address, address.address2, city.city, country.country, address.postalCode, address.phone, customer.active" +
                " FROM customer LEFT JOIN address ON address.addressId = customer.addressId LEFT JOIN city ON address.cityId = city.cityId LEFT JOIN country ON city.countryId = country.countryId";
            
            if (this.OpenConnection() == true)
            {
                MySqlCommand customerCMD = new MySqlCommand(customerQuery, connection);

                using (MySqlDataReader rdr = customerCMD.ExecuteReader())
                {
                    while(rdr.Read())
                    {
                        customers.Add(new Customer()
                        {
                            customerId = int.Parse(rdr["customerId"].ToString()),
                            customerName = rdr["customerName"].ToString(),
                            address = rdr["address"].ToString(),
                            address2 = rdr["address2"].ToString(),
                            city = rdr["city"].ToString(),
                            country = rdr["country"].ToString(),
                            postalCode = rdr["postalCode"].ToString(),
                            phone = rdr["phone"].ToString(),
                            active = bool.Parse(rdr["active"].ToString())
                        });
                    }
                }
                this.CloseConnection();
                return customers;
            }
            else
            {
                return customers;
            }
        }
        public List<string> getCustomerDropdowns()
        {
            List<string> options = new List<string>();
            string getAppointmentDropdownQuery = "SELECT customerName FROM customer";
            

            if (this.OpenConnection() == true)
            {
                MySqlCommand optionCMD = new MySqlCommand(getAppointmentDropdownQuery, connection);
                
               
                using (MySqlDataReader rdr = optionCMD.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        options.Add(rdr["customerName"].ToString());
                      
                        
                    }
                }
                this.CloseConnection();
                return options;
            }
            else
            {
                return options;
            }
        }

        public List<string> getUserDropdowns()
        {
            List<string> options = new List<string>();
            string getAppointmentDropdownQuery = "SELECT userName FROM user";
            

            if (this.OpenConnection() == true)
            {
                MySqlCommand optionCMD = new MySqlCommand(getAppointmentDropdownQuery, connection);
                

                using (MySqlDataReader rdr = optionCMD.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        options.Add(rdr["userName"].ToString());


                    }
                }
                this.CloseConnection();
                return options;
            }
            else
            {
                return options;
            }
        }
        public List<string> getCityDropdowns()
        {
            List<string> options = new List<string>();
            string getCityDropdownQuery = "SELECT city FROM city";


            if (this.OpenConnection() == true)
            {
                MySqlCommand optionCMD = new MySqlCommand(getCityDropdownQuery, connection);


                using (MySqlDataReader rdr = optionCMD.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        options.Add(rdr["city"].ToString());


                    }
                }
                this.CloseConnection();
                return options;
            }
            else
            {
                return options;
            }
        }

        public List<string> getCountryDropdowns()
        {
            List<string> options = new List<string>();
            string getCountryDropdownQuery = "SELECT country FROM country";


            if (this.OpenConnection() == true)
            {
                MySqlCommand optionCMD = new MySqlCommand(getCountryDropdownQuery, connection);


                using (MySqlDataReader rdr = optionCMD.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        options.Add(rdr["country"].ToString());


                    }
                }
                this.CloseConnection();
                return options;
            }
            else
            {
                return options;
            }
        }
        public int addAddress(string address, string address2, int cityId, string postalCode, string phone, string createdBy)
        {
            DateTime stamp = TimeStamp();
            string formatedDate = FormatDate(stamp);
            string addAddressQuery = 

        }
     







        public DateTime TimeStamp()
        {
            return DateTime.Now.ToUniversalTime();
        }
        public string FormatDate(DateTime value)
        {
            return value.ToString("yyyy/MM/dd HH:mm:ss:ffff");
        }

        public void LogUserActivity(string userLog)
        {
            string docPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"log.txt");
            if (!File.Exists(docPath))
            {
                using (StreamWriter sw = File.CreateText(docPath))
                {
                    sw.WriteLine(userLog);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(docPath))
                {
                    sw.WriteLine(userLog);
                }
            }

        }

    }
}
