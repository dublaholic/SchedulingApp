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

        private void btnModify_Click(object sender, EventArgs e)
        {
            Customer modCustomer = (Customer)dgvCustomers.CurrentRow.DataBoundItem;
            new ManageCustomer(modCustomer).ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form addCustomer = new AddCustomer(loginUser);
            addCustomer.ShowDialog();
        }
    }
}
