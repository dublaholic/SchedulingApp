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
    public partial class AddUser : Form
    {
        public User loginUser;
        bool txtUsernameValid = false;
        bool txtPasswordValid = false;
        public AddUser(User user)
        {
            InitializeComponent();
            loginUser = user; 
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

                int activeCustomer = 1;
                DataAction addUser = new DataAction();

                addUser.AddUser(txtUsername.Text, txtPassword.Text, activeCustomer, loginUser.username);

                this.Close();
            }
        }
        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsername.BackColor = System.Drawing.Color.Salmon;
                txtUsernameValid = false;
            }
            else
            {
                txtUsername.BackColor = System.Drawing.Color.White;
                txtUsernameValid = true;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.BackColor = System.Drawing.Color.Salmon;
                txtPasswordValid = false;
            }
            else
            {
                txtPassword.BackColor = System.Drawing.Color.White;
                txtPasswordValid = true;
            }
            if (txtUsernameValid == false || txtPasswordValid == false)
            {
                DialogResult fieldError = MessageBox.Show("Please fill in all required fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

    }
}
