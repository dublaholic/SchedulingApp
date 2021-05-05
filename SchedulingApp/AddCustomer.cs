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
    public partial class AddCustomer : Form
    {
        public User loginUser;
        bool txtNameValid = false;
        bool txtAddressValid = false;
        bool cmbCityValid = false;
        bool txtPostalValid = false;
        bool txtPhoneValid = false;



        int cityID;
        public AddCustomer(User user)
        {
            InitializeComponent();
            setupDropdowns();
            loginUser = user;
        }
        public void setupDropdowns()
        {
            DataAction getDropdownInfo = new DataAction();
            List<string> cityDropdown = new List<string>(getDropdownInfo.getCityDropdowns());
            List<string> countryDropdown = new List<string>(getDropdownInfo.getCountryDropdowns());

            foreach (string city in cityDropdown)
            {
                cmbCity.Items.Add(city);
            }

            foreach (string country in countryDropdown)
            {
                cmbCountry.Items.Add(country);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool validate = ValidateForm();
            if (validate == true)
            {
                bool activeCustomer;
                DataAction addCustomer = new DataAction();

                if (chkActive.Checked == true)
                {
                    activeCustomer = true;
                }
                else
                {
                    activeCustomer = false;

                }

                addCustomer.AddCustomer(txtName.Text, activeCustomer, txtAddress.Text, txtAddress2.Text, cityID, txtPostal.Text, txtPhone.Text, loginUser.username);

                this.Close();
            }
            
            
        }
        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                txtName.BackColor = System.Drawing.Color.Salmon;
                txtNameValid = false;
            }
            else
            {
                txtName.BackColor = System.Drawing.Color.White;
                txtNameValid = true;
            }
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                txtAddress.BackColor = System.Drawing.Color.Salmon;
                txtAddressValid = false;
            }
            else
            {
                txtAddress.BackColor = System.Drawing.Color.White;
                txtAddressValid = true;
            }
            if (string.IsNullOrWhiteSpace(cmbCity.Text))
            {
                cmbCity.BackColor = System.Drawing.Color.Salmon;
                cmbCityValid = false;
            }
            else
            {
                cmbCity.BackColor = System.Drawing.Color.White;
                cmbCityValid = true;
            }
            if (string.IsNullOrWhiteSpace(txtPostal.Text))
            {
                txtPostal.BackColor = System.Drawing.Color.Salmon;
                txtPostalValid = false;
            }
            else
            {
                txtPostal.BackColor = System.Drawing.Color.White;
                txtPostalValid = true;
            }
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                txtPhone.BackColor = System.Drawing.Color.Salmon;
                txtPhoneValid = false;
            }
            else
            {
                txtPhone.BackColor = System.Drawing.Color.White;
                txtPhoneValid = true;
            }

            if (txtNameValid == false || txtAddressValid == false || cmbCityValid == false || txtPostalValid == false || txtPhoneValid == false)
            {
                DialogResult fieldError = MessageBox.Show("Please fill in all required fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void AddCustomer_Load(object sender, EventArgs e)
        {

        }

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            switch(cmbCity.SelectedItem)
            {
                case "New York":
                    cmbCountry.SelectedItem = "US";
                    cityID = 1;
                    break;
                case "Los Angeles":
                    cmbCountry.SelectedItem = "US";
                    cityID = 2;
                    break;
                case "Toronto":
                    cmbCountry.SelectedItem = "Canada";
                    cityID = 3;
                    break;
                case "Vancouver":
                    cmbCountry.SelectedItem = "Canada";
                    cityID = 4;
                    break;
                case "Oslo":
                    cmbCountry.SelectedItem = "Norway";
                    cityID = 5;
                    break;
                default :
                    cmbCountry.SelectedItem = "" ;
                    cityID = -1;
                    break;
            }
        }
    }
}
