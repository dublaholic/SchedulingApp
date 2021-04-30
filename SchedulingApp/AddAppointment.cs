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
    public partial class AddAppointment : Form
    {
        public User loginUser; 
        public AddAppointment(User user)
        {
            InitializeComponent();
            setupDropdowns();
            InitializeDateTimePicker();
            loginUser = user;
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

            foreach (string user in userDropdown)
            {
                cmbUserName.Items.Add(user);
            }
        }
        public void InitializeDateTimePicker()
        {
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "yyyy-MM-dd hh:mm tt";
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "yyyy-MM-dd hh:mm tt";

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataAction addAppointment = new DataAction();
            bool validation = ValidateForm();
            
            if(validation == true)
            { 
                addAppointment.AddAppointment(cmbCustomerName.Text, cmbUserName.Text, txtTitle.Text, txtDescription.Text, txtLocation.Text, txtContact.Text, cmbType.Text, txtURL.Text, dtpStart.Value, dtpEnd.Value, loginUser.username);
                this.Close();
            }

        }
        private void addAppointment_Load(object sender, EventArgs e)
        {
            
        }
        private bool ValidateForm()
        {
            DataAction validate = new DataAction();

            DateTime localStart = dtpStart.Value;
            DateTime localEnd = dtpEnd.Value;
            DateTime businessStart = DateTime.Today.AddHours(8);
            DateTime businessEnd = DateTime.Today.AddHours(20);
            

            if (localStart.TimeOfDay < businessStart.TimeOfDay || localEnd.TimeOfDay > businessEnd.TimeOfDay)
            {
                DialogResult OfficeClosed = MessageBox.Show("Unable to add appointment as the office is closed durring this time!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int bookingResult = validate.CheckDoubleBook(loginUser.userID, dtpStart.Value, dtpEnd.Value);

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

            return true;
        }
    }
}
