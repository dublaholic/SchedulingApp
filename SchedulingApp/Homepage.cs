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
    public partial class Homepage : Form
    {
       
        public User loginUser; 
        public Homepage(User user)
        {
            InitializeComponent();
            FormatDGV(dgvSchedule);
            loginUser = user;
            GetSchedule(loginUser.userID, monthCalendar.SelectionStart.Date);
            //Using statement lambda to make the greeting more efficient and simple to read/maintain 
            Action<string> greet = loguser =>
            {
                string greeting = $"Hello {loguser}!";
                lblGreet.Text = greeting;
            };
            greet(loginUser.username);
            
            
        }
        private void FormatDGV(DataGridView d)
        {

            d.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            d.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow;
            d.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            d.RowHeadersVisible = false;
        }

        private void Homepage_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void GetSchedule(int userId, DateTime startDate)
        {

            DataAction appointmentData = new DataAction();
            DateTime endDate = startDate.Date;
            if (rdoDay.Checked == true)
            {
                endDate = startDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            else if (rdoWeek.Checked == true)
            {
                endDate = startDate.Date.Date.AddDays(7).AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            else if (rdoMonth.Checked == true)
            {
                endDate = startDate.Date.Date.AddMonths(1).AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            
            dgvSchedule.DataSource = appointmentData.GetAppointments(userId, startDate, endDate);
            dgvSchedule.Refresh();
        }

        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            GetSchedule(loginUser.userID, monthCalendar.SelectionStart); 
        }

        private void dgvSchedule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnModifyAppointment_Click(object sender, EventArgs e)
        {
            Appointment modAppointment = (Appointment)dgvSchedule.CurrentRow.DataBoundItem;
            Form ManageAppointment = new ManageAppointment(modAppointment, loginUser);
            ManageAppointment.FormClosed += Custom_FormClosed;
            ManageAppointment.ShowDialog();

        }

        private void mnuCustomers_Click(object sender, EventArgs e)
        {
            Form Customers = new Customers(loginUser);
            Customers.FormClosed += Custom_FormClosed;
            Customers.ShowDialog();
        }

        private void mnuSchedule_Click(object sender, EventArgs e)
        {

        }
        private void Custom_FormClosed(object sender, FormClosedEventArgs e)
        {
            GetSchedule(loginUser.userID, monthCalendar.SelectionStart);
        }

        private void btnDeleteAppointment_Click(object sender, EventArgs e)
        {
            DialogResult deleteConf = MessageBox.Show("Selecting Ok will permenitly delete this record. Are you sure you would like to continue?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (deleteConf == DialogResult.OK)
            {
                DataAction deleteAppointment = new DataAction();
                Appointment appointmentToDelete = (Appointment)dgvSchedule.CurrentRow.DataBoundItem;
                deleteAppointment.DeleteAppointment(appointmentToDelete);
                GetSchedule(loginUser.userID, monthCalendar.SelectionStart);

            }
            else
            {
                GetSchedule(loginUser.userID, monthCalendar.SelectionStart);
            }
        }
        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            Form addAppointment = new AddAppointment(loginUser);
            addAppointment.FormClosed += Custom_FormClosed;
            addAppointment.ShowDialog();
        }

        private void rdoDay_CheckedChanged(object sender, EventArgs e)
        {
            GetSchedule(loginUser.userID, monthCalendar.SelectionStart);
        }

        private void rdoWeek_CheckedChanged(object sender, EventArgs e)
        {
            GetSchedule(loginUser.userID, monthCalendar.SelectionStart);
        }

        private void rdoMonth_CheckedChanged(object sender, EventArgs e)
        {
            GetSchedule(loginUser.userID, monthCalendar.SelectionStart);
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Users = new Users(loginUser);
            Users.FormClosed += Custom_FormClosed;
            Users.ShowDialog();
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Reports = new Reports(loginUser);
            Reports.FormClosed += Custom_FormClosed;
            Reports.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
         
    
            
            
    

