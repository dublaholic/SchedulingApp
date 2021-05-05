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
        bool txtTitleValid = false;
        bool txtDescriptionValid = false;
        bool txtLocationValid = false;
        bool txtContactValid = false;
        bool txtURLValid = false;
        bool cmbCustomerNameValid = false;
        bool cmbUserNameValid = false;
        bool cmbTypeValid = false;
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
            dtpStart.Value = modAppointment.start;
            dtpEnd.Value = modAppointment.end; 
        }
        public void InitializeDateTimePicker()
        {
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "yyyy-MM-dd hh:mm tt";
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "yyyy-MM-dd hh:mm tt";

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
            bool validation = ValidateForm();
            
            if (validation == true)
            {
                modAppointment.ModifyAppointment(Int32.Parse(txtAppointmentId.Text), cmbCustomerName.Text, cmbUserName.Text, txtTitle.Text, txtDescription.Text, txtLocation.Text, txtContact.Text, cmbType.Text, txtURL.Text, dtpStart.Value, dtpEnd.Value, loginUser.username);
                this.Close();
            }
        }
        private bool ValidateForm()
        {
            DataAction validate = new DataAction();

            DateTime localStart = dtpStart.Value;
            DateTime localEnd = dtpEnd.Value;
            DateTime businessStart = DateTime.Today.AddHours(8);
            DateTime businessEnd = DateTime.Today.AddHours(17);


            if (localStart.TimeOfDay < businessStart.TimeOfDay || localEnd.TimeOfDay > businessEnd.TimeOfDay)
            {
                DialogResult OfficeClosed = MessageBox.Show("Unable to add appointment as the office is closed durring this time!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (localStart.TimeOfDay > localEnd.TimeOfDay)
            {
                DialogResult endBeforeStart = MessageBox.Show("The appointments end time cannot be before the start time!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int bookingResult = validate.CheckDoubleBookMod(int.Parse(txtAppointmentId.Text), loginUser.userID, dtpStart.Value, dtpEnd.Value);

            if (bookingResult >= 1)
            {
                DialogResult doubleBook = MessageBox.Show("Unable to add appointment as it would cause double booking!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (bookingResult < 0)
            {
                DialogResult doubleBook = MessageBox.Show("There was an issue saving the appointment, please review the appointment and try again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbCustomerName.Text))
            {
                cmbCustomerName.BackColor = System.Drawing.Color.Salmon;
                cmbCustomerNameValid = false;
            }
            else
            {
                cmbCustomerName.BackColor = System.Drawing.Color.White;
                cmbCustomerNameValid = true;
            }
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                txtTitle.BackColor = System.Drawing.Color.Salmon;
                txtTitleValid = false;
            }
            else
            {
                txtTitle.BackColor = System.Drawing.Color.White;
                txtTitleValid = true;
            }
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                txtDescription.BackColor = System.Drawing.Color.Salmon;
                txtDescriptionValid = false; 
            }
            else
            {
                txtDescription.BackColor = System.Drawing.Color.White;
                txtDescriptionValid = true;
            }
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                txtLocation.BackColor = System.Drawing.Color.Salmon;
                txtLocationValid = false;
            }
            else
            {
                txtLocation.BackColor = System.Drawing.Color.White;
                txtLocationValid = true;
            }
            if (string.IsNullOrWhiteSpace(txtContact.Text))
            {
                txtContact.BackColor = System.Drawing.Color.Salmon;
                txtContactValid = false;
            }
            else
            {
                txtContact.BackColor = System.Drawing.Color.White;
                txtContactValid = true;
            }
            if (string.IsNullOrWhiteSpace(txtURL.Text))
            {
                txtURL.BackColor = System.Drawing.Color.Salmon;
                txtURLValid = false;    
            }
            else
            {
                txtURL.BackColor = System.Drawing.Color.White;
                txtURLValid = true;
            }
            if (string.IsNullOrWhiteSpace(cmbUserName.Text))
            {
                cmbUserName.BackColor = System.Drawing.Color.Salmon;
                cmbUserNameValid = false;
            }
            else
            {
                cmbUserName.BackColor = System.Drawing.Color.White;
                cmbUserNameValid = true;
            }
            if (string.IsNullOrWhiteSpace(cmbType.Text))
            {
                cmbType.BackColor = System.Drawing.Color.Salmon;
                cmbTypeValid = false;
            }
            else
            {
                cmbType.BackColor = System.Drawing.Color.White;
                cmbTypeValid = true;
            }
            if (txtTitleValid == false || txtDescriptionValid == false || txtLocationValid == false || txtContactValid == false || txtURLValid == false || cmbCustomerNameValid == false || cmbUserNameValid == false || cmbTypeValid == false)
            {
                DialogResult fieldError = MessageBox.Show("Please fill in all required fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContact_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtURL_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbUserName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
