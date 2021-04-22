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
    public partial class ManageCustomer : Form
    {
      //  int cityID;
        public ManageCustomer(Customer modCustomer)
        {
            InitializeComponent();
            setupDropdowns();
            txtId.Text = modCustomer.customerId.ToString();
            txtName.Text = modCustomer.customerName;
            txtAddress.Text = modCustomer.address;
            txtAddress2.Text = modCustomer.address2;
            cmbCity.SelectedItem = modCustomer.city;
            cmbCountry.SelectedItem = modCustomer.country;
            txtPostal.Text = modCustomer.postalCode;
            txtPhone.Text = modCustomer.phone;
            chkActive.Checked = modCustomer.active ? chkActive.Checked = true : chkActive.Checked = false;


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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            //switch (cmbCity.SelectedItem)
            //{
            //    case "New York":
            //        cmbCountry.SelectedItem = "US";
            //        cityID = 2;
            //        break;
            //    case "Los Angeles":
            //        cmbCountry.SelectedItem = "US";
            //        cityID = 2;
            //        break;
            //    case "Toronto":
            //        cmbCountry.SelectedItem = "Canada";
            //        cityID = 3;
            //        break;
            //    case "Vancouver":
            //        cmbCountry.SelectedItem = "Canada";
            //        cityID = 4;
            //        break;
            //    case "Oslo":
            //        cmbCountry.SelectedItem = "Norway";
            //        cityID = 5;
            //        break;
            //    default:
            //        cmbCountry.SelectedItem = "";
            //        cityID = -1;
            //        break;


                   
            //}
        }
            
    }
}
