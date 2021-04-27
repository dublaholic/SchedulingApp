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
    public partial class Customers : Form
    {
        User loginUser;
        public Customers(User user)
        {
            InitializeComponent();
            FormatDGV(dgvCustomers);
            GetCustomerList();
            loginUser = user;

        }

        private void FormatDGV(DataGridView d)
        {

            d.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            d.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow;
            d.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            d.RowHeadersVisible = false;
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GetCustomerList()
        {
            DataAction customerData = new DataAction();
            dgvCustomers.DataSource = customerData.GetCustomers();
            dgvCustomers.Refresh();
           
           
        }
        private void Form1_Activated(object sender, System.EventArgs e)
        {
       
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            Customer modCustomer = (Customer)dgvCustomers.CurrentRow.DataBoundItem;
            ManageCustomer manageCustomer = new ManageCustomer(modCustomer, loginUser);
            manageCustomer.FormClosed += Customer_FormClosed;
            manageCustomer.Show();
            

        }
        private void Customer_FormClosed(object sender, FormClosedEventArgs e)
        {
            GetCustomerList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form addCustomer = new AddCustomer(loginUser);
            addCustomer.FormClosed += Customer_FormClosed;
            addCustomer.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult deleteConf = MessageBox.Show("Selecting Ok will permenitly delete this record. Are you sure you would like to continue?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (deleteConf == DialogResult.OK)
            {
                DataAction deleteCustomer = new DataAction();
                Customer customerToDelete = (Customer)dgvCustomers.CurrentRow.DataBoundItem;
                bool deleteResult = deleteCustomer.DeleteCustomer(customerToDelete);
                if (deleteResult == true)
                {
                    MessageBox.Show("Successfully deleted customer!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetCustomerList();
                }
                else if (deleteResult == false)
                {
                    MessageBox.Show("Unable to delete the customer! Please make sure all appointments for the customer are deleted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    GetCustomerList();
                }
            }
            else
            {
                GetCustomerList();
            }
           
        }
    }
}
