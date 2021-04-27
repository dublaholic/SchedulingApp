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
        public User loginUser;
        public ManageAppointment(Appointment modAppointment, User user)
        {

            InitializeComponent();
            InitializeDateTimePicker();
            setupDropdowns();
            loginUser = user;

            txtAppointmentId.Text = modAppointment.appointmentId.ToString();
            cmbCustomerName.SelectedItem = modAppointment.customerName;
            cmbUserName.SelectedItem = modAppointment.userName;
            txtTitle.Text = modAppointment.title.ToString();
            txtDescription.Text = modAppointment.description.ToString();
            txtLocation.Text = modAppointment.location.ToString();
            txtContact.Text = modAppointment.contact.ToString();
            cmbType.Text = modAppointment.type.ToString();
            txtURL.Text = modAppointment.URL.ToString();
            dtpStart.Text = modAppointment.start.ToString();
            dtpEnd.Text = modAppointment.end.ToString();
        }
        public void InitializeDateTimePicker()
        {
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "yyyy-MM-dd HH:mm";
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm";

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            DataAction modAppointment = new DataAction();

            modAppointment.ModifyAppointment(Int32.Parse(txtAppointmentId.Text), cmbCustomerName.Text, cmbUserName.Text, txtTitle.Text, txtDescription.Text, txtLocation.Text, txtContact.Text, cmbType.Text, txtURL.Text, dtpStart.Value, dtpEnd.Value, loginUser.username);
            this.Close();
        }
    }
}
