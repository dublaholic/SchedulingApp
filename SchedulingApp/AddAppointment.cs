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
            addAppointment.AddAppointment(cmbCustomerName.Text, cmbUserName.Text, txtTitle.Text, txtDescription.Text, txtLocation.Text, txtContact.Text, cmbType.Text, txtURL.Text, dtpStart.Value, dtpEnd.Value, loginUser.username);
            this.Close();
        }

        private void addAppointment_Load(object sender, EventArgs e)
        {

        }
    }
}
