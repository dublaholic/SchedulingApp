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
    public partial class ManageUser : Form
    {
        public User loginUser;
        public int isActive;
        bool txtUsernameValid = false;
        bool txtPasswordValid = false;


        public ManageUser(User modUser, User logUser)
        {
            InitializeComponent();
            loginUser = logUser;
            txtUserId.Text = modUser.userID.ToString(); 
            txtUsername.Text = modUser.username;
            txtPassword.Text = modUser.password;
            chkActive.Checked =  modUser.active >= 1 ? chkActive.Checked = true : chkActive.Checked = false;
         
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool validate = ValidateForm();

            if(validate == true)
            {
                DataAction modUser = new DataAction();
                modUser.ModifyUser(txtUserId.Text, txtUsername.Text, txtPassword.Text, isActive, loginUser.username);
                this.Close();
            }
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
            {
                isActive = 1;
            }
            else
            {
                isActive = 0;
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
