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
        User loginUser;
        Customer newCustomer;
        int cityID;
        public AddCustomer(User user)
        {
            InitializeComponent();
            setupDropdowns();
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
            txtName.Text = newCustomer.customerName;
            txtAddress.Text = newCustomer.address;
            txtAddress2.Text = newCustomer.address2;
            cmbCity.SelectedItem = newCustomer.city;
            cmbCountry.SelectedItem = newCustomer.country;
            txtPostal.Text = newCustomer.postalCode;
            txtPhone.Text = newCustomer.phone;
            if (chkActive.Checked == true)
            {
                newCustomer.active = true;
            }
            else
            {
                newCustomer.active = false;
            }
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
                    cityID = 2;
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
