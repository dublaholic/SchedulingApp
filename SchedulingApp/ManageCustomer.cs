﻿using System;
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
        int cityID;
        public User loginUser;
        bool txtNameValid = false;
        bool txtAddressValid = false;
        bool cmbCityValid = false;
        bool txtPostalValid = false;
        bool txtPhoneValid = false;
        public ManageCustomer(Customer modCustomer, User user)
        {
            InitializeComponent();
            setupDropdowns();
            loginUser = user;
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
            switch (cmbCity.SelectedItem)
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
                default:
                    cmbCountry.SelectedItem = "";
                    cityID = -1;
                    break;



            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool validate = ValidateForm();
            if (validate == true)
            {
                bool activeCustomer;
                DataAction modCustomer = new DataAction();

                if (chkActive.Checked == true)
                {
                    activeCustomer = true;
                }
                else
                {
                    activeCustomer = false;

                }

                modCustomer.ModifyCustomer(Int32.Parse(txtId.Text), txtName.Text, activeCustomer, txtAddress.Text, txtAddress2.Text, cityID, txtPostal.Text, txtPhone.Text, loginUser.username);
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
    }
}
