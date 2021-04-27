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
            int activeCustomer = 1;
            DataAction addUser= new DataAction();

            addUser.AddUser(txtUsername.Text, txtPassword.Text, activeCustomer, loginUser.username);

            this.Close();
        }

    }
}
