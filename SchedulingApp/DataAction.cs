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
            catch (MySqlException ex)
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
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }


//VERIFICATION

        public int VerifyUser(string username, string password)
        {
            int verificationResult = -1;
            string queryReturn;
            string userQuery = "SELECT EXISTS(SELECT userId FROM user WHERE userName = @username and password = @password)";
            if (this.OpenConnection() == true)
            {
                MySqlCommand userCMD = new MySqlCommand(userQuery, connection);
                userCMD.Parameters.AddWithValue("@username", username);
                userCMD.Parameters.AddWithValue("@password", password);
                queryReturn = userCMD.ExecuteScalar().ToString();

                if (queryReturn == "1")
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


//GETS

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
                appointmentCMD.Parameters.AddWithValue("@startDate", ToUTC(startDate));
                appointmentCMD.Parameters.AddWithValue("@endDate", ToUTC(endDate));

                using (MySqlDataReader rdr = appointmentCMD.ExecuteReader())
                {
                    while (rdr.Read())
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
                            start = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Parse(rdr["start"].ToString())),
                            end = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Parse(rdr["start"].ToString()))

                        });
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
                    while (rdr.Read())
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
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            string usersQuery = "SELECT userId, userName, password, active FROM user";


            if (this.OpenConnection() == true)
            {
                MySqlCommand usersCMD = new MySqlCommand(usersQuery, connection);

                using (MySqlDataReader rdr = usersCMD.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        users.Add(new User()
                        {
                            userID = int.Parse(rdr["userId"].ToString()),
                            username = rdr["userName"].ToString(),
                            password = rdr["password"].ToString(),
                            active = int.Parse(rdr["active"].ToString())

                        });
                    }
                }
                this.CloseConnection();
                return users;
            }
            else
            {
                return users;
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

//ADDS

        public void AddCustomer(string customerName, bool active, string address, string address2, int cityId, string postalCode, string phone, string createdBy)
        {
            DateTime stamp = TimeStamp();
            string formatedDate = FormatDate(stamp);
            string addAddressQuery = "INSERT INTO address(address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdateBy) " +
                "VALUES (@address, @address2, @cityId, @postalCode, @phone, @createDate, @createdBy, @lastUpdateBy)";
            string getLastAddressId = "SELECT MAX(addressId) FROM address";
            string addCustomer = "INSERT INTO customer(customerName, addressId, active, createDate, createdBy, lastUpdateBy) " +
                "VALUES (@customerName, @addressId, @active, @createDate, @createdBy, @customerLastUpdateBy)";
            if (this.OpenConnection() == true)
            {
                MySqlCommand addAddressCMD = new MySqlCommand(addAddressQuery, connection);
                addAddressCMD.Parameters.AddWithValue("@address", address);
                addAddressCMD.Parameters.AddWithValue("@address2", address2);
                addAddressCMD.Parameters.AddWithValue("@cityId", cityId);
                addAddressCMD.Parameters.AddWithValue("@postalCode", postalCode);
                addAddressCMD.Parameters.AddWithValue("@phone", phone);
                addAddressCMD.Parameters.AddWithValue("@createDate", formatedDate);
                addAddressCMD.Parameters.AddWithValue("@createdBy", createdBy);
                addAddressCMD.Parameters.AddWithValue("@lastUpdateBy", createdBy);

                addAddressCMD.ExecuteNonQuery();


                MySqlCommand getAddressIdCMD = new MySqlCommand(getLastAddressId, connection);
                int returnedAddressId = int.Parse(getAddressIdCMD.ExecuteScalar().ToString());

                MySqlCommand addCustomerCMD = new MySqlCommand(addCustomer, connection);
                addCustomerCMD.Parameters.AddWithValue("@customerName", customerName);
                addCustomerCMD.Parameters.AddWithValue("@addressID", returnedAddressId);
                addCustomerCMD.Parameters.AddWithValue("@active", active);
                addCustomerCMD.Parameters.AddWithValue("@createDate", formatedDate);
                addCustomerCMD.Parameters.AddWithValue("@createdBy", createdBy);
                addCustomerCMD.Parameters.AddWithValue("@customerLastUpdateBy", createdBy);

                addCustomerCMD.ExecuteNonQuery();

                this.CloseConnection();
            }

        }
        public void AddUser(string username, string password, int active, string createdBy)
        {
            DateTime stamp = TimeStamp();
            string formatedDate = FormatDate(stamp);
            string addUserQuery = "INSERT INTO user(username, password, active, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                "VALUES (@username, @password, @active, @createDate, @createdBy, @lastUpdate, @lastUpdateBy)";

            if (this.OpenConnection() == true)
            {
                MySqlCommand addUserCMD = new MySqlCommand(addUserQuery, connection);
                addUserCMD.Parameters.AddWithValue("@username", username);
                addUserCMD.Parameters.AddWithValue("@password", password);
                addUserCMD.Parameters.AddWithValue("@active", active);
                addUserCMD.Parameters.AddWithValue("@createDate", formatedDate);
                addUserCMD.Parameters.AddWithValue("@createdBy", createdBy);
                addUserCMD.Parameters.AddWithValue("@lastUpdate", formatedDate);
                addUserCMD.Parameters.AddWithValue("@lastUpdateBy", createdBy);

                addUserCMD.ExecuteNonQuery();

                this.CloseConnection();
            }

        }
        public void AddAppointment(string customerName, string userName, string title, string description, string location, string contact, string type, string URL, DateTime start, DateTime end, string createdBy)
        {
            DateTime stamp = TimeStamp();
            string formatedDate = FormatDate(stamp);
            string formatedStart = FormatDate(ToUTC(start));
            string formatedEnd = FormatDate(ToUTC(end));
            string getCustomerIdQuery = "SELECT customerId FROM customer WHERE customerName = @customerName";
            string getUserIdQuery = "SELECT userId FROM user WHERE userName = @userName";
            string addAppointmentQuery = "INSERT INTO appointment(customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                "VALUES (@customerId, @userId, @title, @description, @location, @contact, @type, @URL, @start, @end, @createDate, @createdBy, @lastUpdate, @lastUpdateBy)";

            if (this.OpenConnection() == true)
            {
                MySqlCommand getCustomerIdCMD = new MySqlCommand(getCustomerIdQuery, connection);
                getCustomerIdCMD.Parameters.AddWithValue("@customerName", customerName);

                int customerId = int.Parse(getCustomerIdCMD.ExecuteScalar().ToString());

                MySqlCommand getUserIdCMD = new MySqlCommand(getUserIdQuery, connection);
                getUserIdCMD.Parameters.AddWithValue("@userName", userName);

                int userId = int.Parse(getUserIdCMD.ExecuteScalar().ToString());


                MySqlCommand addAppointmentCMD = new MySqlCommand(addAppointmentQuery, connection);
                addAppointmentCMD.Parameters.AddWithValue("@customerId", customerId);
                addAppointmentCMD.Parameters.AddWithValue("@userId", userId);
                addAppointmentCMD.Parameters.AddWithValue("@title", title);
                addAppointmentCMD.Parameters.AddWithValue("@description", description);
                addAppointmentCMD.Parameters.AddWithValue("@location", location);
                addAppointmentCMD.Parameters.AddWithValue("@contact", contact);
                addAppointmentCMD.Parameters.AddWithValue("@type", title);
                addAppointmentCMD.Parameters.AddWithValue("@URL", description);
                addAppointmentCMD.Parameters.AddWithValue("@start", formatedStart);
                addAppointmentCMD.Parameters.AddWithValue("@end", formatedEnd);
                addAppointmentCMD.Parameters.AddWithValue("@createDate", formatedDate);
                addAppointmentCMD.Parameters.AddWithValue("@createdBy", createdBy);
                addAppointmentCMD.Parameters.AddWithValue("@lastUpdate", formatedDate);
                addAppointmentCMD.Parameters.AddWithValue("@lastUpdateBy", createdBy);

                addAppointmentCMD.ExecuteNonQuery();

                this.CloseConnection();
            }
        }


        //MODIFIES 

        public void ModifyCustomer(int customerId, string customerName, bool active, string address, string address2, int cityId, string postalCode, string phone, string createdBy)
        {
            DateTime stamp = TimeStamp();
            string formatedDate = FormatDate(stamp);
            string getAddressIDQuery = "SELECT addressId FROM customer WHERE customerId = @customerId";
            string modAddressQuery = "UPDATE address SET address = @address, address2 = @address2,  postalCode= @postalCode, phone = @phone, lastUpdate = @formatedDate, lastUpdateBy = @createdby " +
                "WHERE addressId = @returnedAddressId";
            string modCustomerQuery = "UPDATE customer SET customerName = @customerName, active = @active, lastUpdate = @customerFormatedDate, lastUpdateBy = @customerCreatedBy " +
                "WHERE customerId = @customerId";
            if (this.OpenConnection() == true)
            {
                MySqlCommand returnAddressIdCMD = new MySqlCommand(getAddressIDQuery, connection);
                returnAddressIdCMD.Parameters.AddWithValue("@customerID", customerId);
                int returnedAddressId = int.Parse(returnAddressIdCMD.ExecuteScalar().ToString());


                MySqlCommand modAddressCMD = new MySqlCommand(modAddressQuery, connection);
                modAddressCMD.Parameters.AddWithValue("@address", address);
                modAddressCMD.Parameters.AddWithValue("@address2", address2);
                modAddressCMD.Parameters.AddWithValue("@cityId", cityId);
                modAddressCMD.Parameters.AddWithValue("@postalCode", postalCode);
                modAddressCMD.Parameters.AddWithValue("@phone", phone);
                modAddressCMD.Parameters.AddWithValue("@formatedDate", formatedDate);
                modAddressCMD.Parameters.AddWithValue("@createdby", createdBy);
                modAddressCMD.Parameters.AddWithValue("@returnedAddressId", returnedAddressId);
                modAddressCMD.ExecuteNonQuery();


                MySqlCommand modCustomerCMD = new MySqlCommand(modCustomerQuery, connection);
                modCustomerCMD.Parameters.AddWithValue("@customerName", customerName);
                modCustomerCMD.Parameters.AddWithValue("@customerReturnedAddressId", returnedAddressId);
                modCustomerCMD.Parameters.AddWithValue("@active", active);
                modCustomerCMD.Parameters.AddWithValue("@customerFormatedDate", formatedDate);
                modCustomerCMD.Parameters.AddWithValue("@customerCreatedBy", createdBy);
                modCustomerCMD.Parameters.AddWithValue("@customerId", customerId);
                modCustomerCMD.ExecuteNonQuery();

                this.CloseConnection();
            }

        }
        public void ModifyUser(string userId, string username, string password, int active, string createdBy)
        {
            DateTime stamp = TimeStamp();
            string formatedDate = FormatDate(stamp);
            string modUserQuery = "UPDATE user SET userName = @userName, password = @password, active = @active, lastUpdate = @FormatedDate, lastUpdateBy = @CreatedBy " +
                "WHERE userId = @userId";
            if (this.OpenConnection() == true)
            {
                MySqlCommand modUserCMD = new MySqlCommand(modUserQuery, connection);
                modUserCMD.Parameters.AddWithValue("@userName", username);
                modUserCMD.Parameters.AddWithValue("@password", password);
                modUserCMD.Parameters.AddWithValue("@active", active);
                modUserCMD.Parameters.AddWithValue("@FormatedDate", formatedDate);
                modUserCMD.Parameters.AddWithValue("@CreatedBy", createdBy);
                modUserCMD.Parameters.AddWithValue("@userId", userId);
                modUserCMD.ExecuteNonQuery();


                this.CloseConnection();
            }

        }
        public void ModifyAppointment(int appointmentId, string customerName, string userName, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end, string createdBy)
        {
            DateTime stamp = TimeStamp();
            string formatedDate = FormatDate(stamp);
            string formatedStart = FormatDate(ToUTC(start));
            string formatedEnd = FormatDate(ToUTC(end));
            string getCustomerIdQuery = "SELECT customerId FROM customer WHERE customerName = @customerName";
            string getUserIdQuery = "SELECT userId FROM user WHERE userName = @userName";
            string modAppointmentQuery = "UPDATE appointment SET customerId = @customerId, userId = @userId, title = @title, description = @description, location = @location, contact = @contact, type = @type, url = @url, start = @start, end = @end, lastUpdate = @formatedDate, lastUpdateBy = @createdby " +
                "WHERE appointmentId = @appointmentId";
            if (this.OpenConnection() == true)
            {
                MySqlCommand getCustomerIdCMD = new MySqlCommand(getCustomerIdQuery, connection);
                getCustomerIdCMD.Parameters.AddWithValue("@customerName", customerName);
                int returnedCustomerId = int.Parse(getCustomerIdCMD.ExecuteScalar().ToString());


                MySqlCommand getUserIdCMD = new MySqlCommand(getUserIdQuery, connection);
                getUserIdCMD.Parameters.AddWithValue("@userName", userName);
                int returnedUserId = int.Parse(getUserIdCMD.ExecuteScalar().ToString());


                MySqlCommand modAppointmentCMD = new MySqlCommand(modAppointmentQuery, connection);
                modAppointmentCMD.Parameters.AddWithValue("@customerId", returnedCustomerId);
                modAppointmentCMD.Parameters.AddWithValue("@userId", returnedUserId);
                modAppointmentCMD.Parameters.AddWithValue("@title", title);
                modAppointmentCMD.Parameters.AddWithValue("@description", description);
                modAppointmentCMD.Parameters.AddWithValue("@location", location);
                modAppointmentCMD.Parameters.AddWithValue("@contact", contact);
                modAppointmentCMD.Parameters.AddWithValue("@type", type);
                modAppointmentCMD.Parameters.AddWithValue("@url", url);
                modAppointmentCMD.Parameters.AddWithValue("@start", formatedStart);
                modAppointmentCMD.Parameters.AddWithValue("@end", formatedEnd);
                modAppointmentCMD.Parameters.AddWithValue("@formatedDate", formatedDate);
                modAppointmentCMD.Parameters.AddWithValue("@createdby", createdBy);
                modAppointmentCMD.Parameters.AddWithValue("@appointmentId", appointmentId);
                modAppointmentCMD.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

//DELETS    

        public bool DeleteCustomer(Customer customer)
        {

            bool deleteResult = false;
            Customer customerToDelete = customer;
            string customerAppointmentsQuery = "SELECT COUNT(appointmentId) FROM appointment WHERE customerId = @appointmentCustomerId";
            string returnAddressIdQuery = "SELECT addressId FROM customer WHERE customerId = @customerId";
            string addressToDeleteQuery = "DELETE FROM address WHERE addressId = @addressId";
            string customerToDeleteQuery = "DELETE FROM customer WHERE customerId = @customerId";

            if (this.OpenConnection() == true)
            {
                MySqlCommand customerAppointmentCMD = new MySqlCommand(customerAppointmentsQuery, connection);
                customerAppointmentCMD.Parameters.AddWithValue("appointmentCustomerId", customerToDelete.customerId);
                int hasAppointment = int.Parse(customerAppointmentCMD.ExecuteScalar().ToString());

                if (hasAppointment < 1)
                {
                    MySqlCommand returnAddressIdCMD = new MySqlCommand(returnAddressIdQuery, connection);
                    returnAddressIdCMD.Parameters.AddWithValue("@customerID", customerToDelete.customerId);
                    int returnedAddressId = int.Parse(returnAddressIdCMD.ExecuteScalar().ToString());

                    MySqlCommand customerToDeleteCMD = new MySqlCommand(customerToDeleteQuery, connection);
                    customerToDeleteCMD.Parameters.AddWithValue("@customerId", customerToDelete.customerId);
                    customerToDeleteCMD.ExecuteNonQuery();


                    MySqlCommand addressToDeleteCMD = new MySqlCommand(addressToDeleteQuery, connection);
                    addressToDeleteCMD.Parameters.AddWithValue("@addressId", returnedAddressId);
                    addressToDeleteCMD.ExecuteNonQuery();

                    this.CloseConnection();
                    deleteResult = true;
                    return deleteResult;

                }
                else
                {
                    this.CloseConnection();
                    deleteResult = false;
                    return deleteResult;
                }
            }
            deleteResult = false;
            return deleteResult;
        }
        public bool DeleteUser(User user)
        {

            bool deleteResult = false;
            User userToDelete = user;
            string userAppointmentsQuery = "SELECT COUNT(appointmentId) FROM appointment WHERE userId = @appointmentUserID";
            string userToDeleteQuery = "DELETE FROM user WHERE userId = @userId";

            if (this.OpenConnection() == true)
            {
                MySqlCommand userAppointmentsCMD = new MySqlCommand(userAppointmentsQuery, connection);
                userAppointmentsCMD.Parameters.AddWithValue("appointmentUserID", userToDelete.userID);
                int hasAppointment = int.Parse(userAppointmentsCMD.ExecuteScalar().ToString());

                if (hasAppointment < 1)
                {
                    MySqlCommand userToDeleteCMD = new MySqlCommand(userToDeleteQuery, connection);
                    userToDeleteCMD.Parameters.AddWithValue("@userId", userToDelete.userID);
                    userToDeleteCMD.ExecuteNonQuery();

                    this.CloseConnection();
                    deleteResult = true;
                    return deleteResult;

                }
                else
                {
                    this.CloseConnection();
                    deleteResult = false;
                    return deleteResult;
                }
            }
            deleteResult = false;
            return deleteResult;
        }
        public void DeleteAppointment(Appointment appointment)
        {

            Appointment appointmentToDelete = appointment;
            string deleteAppointmentsQuery = "DELETE FROM appointment WHERE appointmentId = @appointmentId";


            if (this.OpenConnection() == true)
            {
                MySqlCommand deleteAppointmentCMD = new MySqlCommand(deleteAppointmentsQuery, connection);
                deleteAppointmentCMD.Parameters.AddWithValue("@appointmentId", appointmentToDelete.appointmentId);
                deleteAppointmentCMD.ExecuteNonQuery();

                this.CloseConnection();
            }
            this.CloseConnection();

        }



        //REMINDER

        public List<Appointment> GetReminders(int userId)
        {
            List<Appointment> appointmentReminder = new List<Appointment>();
            DateTime stamp = TimeStamp();
            string formatedDate = FormatDate(stamp);

            string getReminderQuery = "SELECT * FROM appointment JOIN user on user.userId = appointment.userId JOIN customer on customer.customerId = appointment.customerId WHERE user.userId = @userId";
            if (this.OpenConnection() == true)
            {
                MySqlCommand getReminderCMD = new MySqlCommand(getReminderQuery, connection);
                getReminderCMD.Parameters.AddWithValue("@userId", userId);
                getReminderCMD.Parameters.AddWithValue("@currentTime", formatedDate);
                getReminderCMD.ExecuteNonQuery();

                using (MySqlDataReader rdr = getReminderCMD.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        appointmentReminder.Add(new Appointment()
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
                            start = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Parse((rdr["start"].ToString()))),
                            end = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Parse((rdr["end"].ToString())))
                        });
                    }
                }
                this.CloseConnection();
                return appointmentReminder;

            }
            else
            {
                this.CloseConnection();
                return appointmentReminder;
            }
        }





        //REPORTS

        public List<string> GetAppointmentReport(int month)
        {
            string appointmentReportQuery = "SELECT type, count(appointmentId) FROM appointment WHERE MONTH(start) = @month GROUP BY type";
            List<string> result = new List<string>(); 

            if (this.OpenConnection() == true)
            {
                MySqlCommand appointmentReportCMD = new MySqlCommand(appointmentReportQuery, connection);
                appointmentReportCMD.Parameters.AddWithValue("@month", month);

                appointmentReportCMD.ExecuteNonQuery();

                using (MySqlDataReader rdr = appointmentReportCMD.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        result.Add("Appointment Type: " + rdr["type"].ToString() + " -  Number of Appointments: " + rdr["count(appointmentId)"].ToString());
                    }

                }
                this.CloseConnection();
                return result;
            }
            this.CloseConnection();
            return result;
        }

        public List<string> GetScheduleReport(int userId)
        {
            string scheduleReportQuery = "SELECT customer.customerName, appointment.title, appointment.description, appointment.location, appointment.contact, appointment.type, appointment.url, appointment.start, appointment.end " +
                " FROM appointment JOIN customer on customer.customerId = appointment.customerId WHERE userId = @userId";
            List<string> result = new List<string>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand scheduleReportCMD = new MySqlCommand(scheduleReportQuery, connection);
                scheduleReportCMD.Parameters.AddWithValue("@userId", userId);
                
                scheduleReportCMD.ExecuteNonQuery();

                using (MySqlDataReader rdr = scheduleReportCMD.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        result.Add(" Customer Name: " + rdr["customerName"].ToString() + " -  Title: " + rdr["title"].ToString() + " -  Description: " + rdr["description"].ToString() + " -  Location: " + rdr["location"].ToString() + " -  Contact: " + rdr["contact"].ToString() + " -  Type: " + rdr["type"].ToString() + " -  URL: " + rdr["url"].ToString() + " -  Start: " + rdr["start"].ToString() + " -  End: " + rdr["end"].ToString());
                    }

                }
                this.CloseConnection();
                return result;
            }
            this.CloseConnection();
            return result;

        }

        public List<string> GetCustomerReport()
        {
            string customerAuditReportQuery = "SELECT customerName, active, createDate, createdBy, lastUpdate, lastUpdateBy FROM customer";
            List<string> result = new List<string>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand customerAuditReportCMD = new MySqlCommand(customerAuditReportQuery, connection);


                customerAuditReportCMD.ExecuteNonQuery();

                using (MySqlDataReader rdr = customerAuditReportCMD.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        result.Add("Customer Name: " + rdr["customerName"].ToString() + " -  Active: " + rdr["active"].ToString() + " -  Created Date: " + rdr["createDate"].ToString() + " -  Created By: " + rdr["createdBy"].ToString() + " -  Last Update: " + rdr["lastUpdate"].ToString() + " -  Last Updated By: " + rdr["lastUpdateBy"].ToString());
                    }

                }
                this.CloseConnection();
                return result;
            }
            this.CloseConnection();
            return result;
        }





        //MISC

        public DateTime TimeStamp()
        {
            return DateTime.Now.ToUniversalTime();
        }
        public DateTime ToUTC(DateTime value)
        {
            return value.ToUniversalTime();
        }
        public string FormatDate(DateTime value)
        {
            return value.ToString("yyyy-MM-dd HH:mm");
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
