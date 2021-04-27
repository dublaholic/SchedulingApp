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
            DataAction modUser = new DataAction();
            modUser.ModifyUser(txtUserId.Text, txtUsername.Text, txtPassword.Text, isActive, loginUser.username);
            this.Close();

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
    }
}
