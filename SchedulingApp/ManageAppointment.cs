using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedulingApp
{
    public partial class ManageAppointment : Form
    {
        public ManageAppointment(Appointment modAppointment)
        {
            InitializeComponent();
           
            setupDropdowns();



            txtAppointmentId.Text = modAppointment.appointmentId.ToString();
            cmbCustomerName.SelectedItem = modAppointment.customerName;
            cmbUserName.SelectedItem = modAppointment.userName;
            txtTitle.Text = modAppointment.title.ToString();
            txtDescription.Text = modAppointment.description.ToString();
            txtLocation.Text = modAppointment.location.ToString();
            txtContact.Text = modAppointment.contact.ToString();
            txtType.Text = modAppointment.type.ToString();
            txtURL.Text = modAppointment.URL.ToString();
            txtStart.Text = modAppointment.start.ToString();
            txtEnd.Text = modAppointment.end.ToString();
        }
        public void setupDropdowns()
        {
            DataAction getDropdownInfo = new DataAction();
            List<string> customerDropdown = new List<string>(getDropdownInfo.getCustomerDropdowns());
            List<string> userDropdown = new List<string>(getDropdownInfo.getUserDropdowns());

            foreach (string customer in customerDropdown)
            {
                cmbCustomerName.Items.Add(customer);
            }

            foreach(string user in userDropdown)
            {
                cmbUserName.Items.Add(user);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
